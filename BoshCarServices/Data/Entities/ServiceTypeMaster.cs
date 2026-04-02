namespace BoshCarServices.Data.Entities
{
    public class ServiceTypeMaster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public int? createdBy { get; set; }

        public DateTime? createdDate { get; set; }

        public int? modifiedBy { get; set; }

        public DateTime? modifiedDate { get; set; }

        
    }
}
