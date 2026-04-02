using BoshCarServices.Data.Entities;
using BoshCarServices.Services;
using BoshCarServices.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace BoshCarServices.Components.Pages
{
    public partial class AddService
    {
        [Parameter]
        [SupplyParameterFromQuery]
        public string? mobile { get; set; }

        [Parameter]
        [SupplyParameterFromQuery]
        public string? regnum { get; set; }
        public CommonAlerts? commonAlerts;

        AddServiceVM model = new();
        List<ServiceTypeVM> serviceTypes = new();
        MudForm form;
        HashSet<int> selectedServiceTypeIds = new();
        private IBrowserFile selectedFile;
        private string selectedFileName = "No file selected";
        private string fileUrl;

        protected override void OnInitialized()
        {
            serviceTypes = _context.ServiceTypeMasters
                .Where(x => x.IsActive)
                .Select(x => new ServiceTypeVM
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

            if (!string.IsNullOrEmpty(mobile))
            {
                var existingCustomer = _context.CustomerMasters
                    .FirstOrDefault(x => x.Mobile == mobile);
                model.Customer.Mobile = mobile;

                if (existingCustomer != null)
                {
                    model.Customer = existingCustomer;

                    var existingVehicle = _context.VehicleMasters
                        .FirstOrDefault(x => x.CId == model.Customer.Id && x.RegNum == regnum);

                    if (existingVehicle != null)
                        model.Vehicle = existingVehicle;
                }
                model.Vehicle.RegNum = regnum;
            }
        }

        void CalculatePoints()
        {
            var points = (int)(model.Service.TotalBill * 0.01m);

            // Cap at 500
            model.Service.RewardPoints = points > 500 ? 500 : points;
        }
        void OnServiceTypesChanged(IEnumerable<int> values)
        {
            selectedServiceTypeIds = values.ToHashSet();
        }

        async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
            selectedFile = e.File;
            if (selectedFile != null)
            {
                selectedFileName = selectedFile.Name;

                // Optional: Validate file size
                var maxSize = 10 * 1024 * 1024; // 10MB
                if (selectedFile.Size > maxSize)
                {
                    selectedFileName = "File too large (max 10MB)";
                    selectedFile = null;

                    commonAlerts?.ShowSnackbar("File size exceeds 10MB limit", Severity.Error, Defaults.Classes.Position.TopCenter);
                }
            }
            else
            {
                selectedFileName = "No file selected";
            }
        }

        async Task SaveService()
        {
            Loader.Show();
            try {

                await form.Validate();

                if (!form.IsValid)
                {
                    commonAlerts?.ShowSnackbar("Please fill all required fields", Severity.Error, Defaults.Classes.Position.TopCenter);
                    return;
                }

                if (!selectedServiceTypeIds.Any())
                {
                    commonAlerts?.ShowSnackbar("Please select at least one service type", Severity.Warning, Defaults.Classes.Position.TopCenter);
                    return;
                }

                string fileUrl = null;

                if (selectedFile != null)
                {
                   

                    using (var ms = new MemoryStream())
                    {
                        await selectedFile.OpenReadStream(10 * 1024 * 1024)
                                          .CopyToAsync(ms);
                        var extension = Path.GetExtension(selectedFile.Name);

                        // Timestamp file name
                        var fileName = $"{Guid.NewGuid()}{extension}";
                        model.Service.FileData = ms.ToArray();
                        model.Service.FileName = fileName;
                        model.Service.ContentType = selectedFile.ContentType;
                    }

                }

                // Scenario 1: Customer
                if (model.Customer.Id == 0)
                {
                    model.Customer.userId = 1;
                    model.Customer.IsActive = true;
                    _context.CustomerMasters.Add(model.Customer);
                    await _context.SaveChangesAsync();
                }

                // Scenario 2: Vehicle
                if (model.Vehicle.Id == 0)
                {
                    model.Vehicle.CId = model.Customer.Id;
                    model.Vehicle.IsActive = true;
                    _context.VehicleMasters.Add(model.Vehicle);
                    await _context.SaveChangesAsync();
                }

                // Scenario 3: Service
                model.Service.VId = model.Vehicle.Id;
                model.Service.createdDate = DateTime.Now;

                // Save the file URL if you have a field for it in your ServiceMaster entity
                // model.Service.FileUrl = fileUrl;

                _context.ServiceMasters.Add(model.Service);
                await _context.SaveChangesAsync();

                foreach (var typeId in selectedServiceTypeIds)
                {
                    _context.ServiceTypeMappings.Add(new ServiceTypeMapping
                    {
                        ServiceId = model.Service.Id,
                        ServiceTypeId = typeId
                    });
                }

                await _context.SaveChangesAsync();
                commonAlerts?.ShowSnackbar("Rewards added succesfully", Severity.Success, Defaults.Classes.Position.TopCenter);
                nav.NavigateTo($"/dashboard?mobile={model.Customer.Mobile}&regnum={model.Vehicle.RegNum}");

            }
            finally { Loader.Hide(); }
           
        }
    }
}