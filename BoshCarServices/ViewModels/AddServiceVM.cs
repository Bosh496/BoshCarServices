using BoshCarServices.Data.Entities;

namespace BoshCarServices.ViewModels
{
    public class AddServiceVM
    {
        public CustomerMaster Customer { get; set; } = new();

        public VehicleMaster Vehicle { get; set; } = new();

        public ServiceMaster Service { get; set; } = new();
    }
}
