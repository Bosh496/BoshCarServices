using BoshCarServices.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions;


namespace BoshCarServices.Components.Pages
{
    public partial class ServiceSearch
    {
        string mobile;
        string regNumber;
        [Parameter]
        [SupplyParameterFromQuery(Name = "mobile")]
        public string? mobileParam { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "regnum")]
        public string? regnumParam { get; set; }
        List<ServiceResultVM> results = new();

        int totalPoints = 0;
        string mobileError;
        string regError;
        MudForm form;
        bool IsMobileValid =>
    !string.IsNullOrWhiteSpace(mobile) &&
    Regex.IsMatch(mobile, @"^[0-9]{10}$");

        bool IsVehicleValid =>
            !string.IsNullOrWhiteSpace(regNumber) &&
            Regex.IsMatch(regNumber, @"^[A-Z]{2}[0-9]{2}[A-Z]{2}[0-9]{3,4}$");

        bool IsFormValid => IsMobileValid && IsVehicleValid;

        protected override async Task OnParametersSetAsync()
        {
            if (!string.IsNullOrWhiteSpace(mobileParam) &&
                !string.IsNullOrWhiteSpace(regnumParam))
            {
                mobile = mobileParam;
                regNumber = regnumParam;

                ConvertToUpper();

                if (IsFormValid)
                {
                    await Search();
                }
            }
        }
        void ConvertToUpper()
        {
            if (!string.IsNullOrWhiteSpace(regNumber))
            {
                // Remove everything except letters and numbers
                regNumber = Regex.Replace(regNumber, "[^a-zA-Z0-9]", "")
                                 .ToUpper();
            }
        }
        async Task DownloadFile(int serviceId)
        {
            var service = _context.ServiceMasters
                .FirstOrDefault(x => x.Id == serviceId);

            if (service?.FileData != null)
            {
                var base64 = Convert.ToBase64String(service.FileData);

                await JS.InvokeVoidAsync(
                    "openPdfFromBytes",
                    base64,
                    service.ContentType
                );
            }
        }
        async Task Search()
        {
            if (form != null)
            {
                await form.Validate();

                if (!form.IsValid)
                    return;
            }

           

            results = (from c in _context.CustomerMasters
                       join v in _context.VehicleMasters on c.Id equals v.CId
                       join s in _context.ServiceMasters on v.Id equals s.VId

                       where c.Mobile == mobile && v.RegNum == regNumber

                       select new ServiceResultVM
                       {
                           ServiceId = s.Id,
                           Name = c.Name,
                           Mobile = c.Mobile,
                           RegNum = v.RegNum,
                           ServiceDate = s.createdDate ?? DateTime.Now,
                           RewardPoints = s.RewardPoints,
                           FileData = s.FileData,
                           ContentType = s.ContentType,
                           FileName = s.FileName,

                           ServiceTypes = string.Join(", ",
                               (from map in _context.ServiceTypeMappings
                                join st in _context.ServiceTypeMasters
                                on map.ServiceTypeId equals st.Id
                                where map.ServiceId == s.Id
                                select st.Name).ToList())
                       }).ToList();

            totalPoints = results.Sum(x => x.RewardPoints);
        }

        void RedeemAll()
        {

            var services = _context.ServiceMasters
            .Where(x => results.Select(r => r.ServiceId).Contains(x.Id))
            .ToList();

            foreach (var s in services)
            {
                s.RewardPoints = 0;
            }

            _context.SaveChanges();

            Search();

        }
        void GoToAddService()
        {
            nav.NavigateTo($"/addservice?mobile={mobile}&regnum={regNumber}");
        }
    }
}
