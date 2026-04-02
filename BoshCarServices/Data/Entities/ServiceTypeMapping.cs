namespace BoshCarServices.Data.Entities
{
    public class ServiceTypeMapping
    {
        public int Id { get; set; }

        public int ServiceId { get; set; }

        public int ServiceTypeId { get; set; }

        public ServiceMaster Service { get; set; }

        public ServiceTypeMaster ServiceType { get; set; }
    }
}
