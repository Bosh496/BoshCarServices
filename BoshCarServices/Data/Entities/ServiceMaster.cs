using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoshCarServices.Data.Entities
{
    public class ServiceMaster
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("VehicleMaster")]
        public int VId { get; set; }

        
        [Range(1, 1000000, ErrorMessage = "Mileage must be greater than 0")]
        public int Mileage { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Range(1, 1000000, ErrorMessage = "Total bill must be greater than 0")]
        public decimal TotalBill { get; set; }

        public int RewardPoints { get; set; }

        public bool IsActive { get; set; }
        public byte[]? FileData { get; set; }   // actual file
        public string? FileName { get; set; }   // file name
        public string? ContentType { get; set; }
        public int? createdBy { get; set; }

        public DateTime? createdDate { get; set; }

        public int? modifiedBy { get; set; }

        public DateTime? modifiedDate { get; set; }

        public VehicleMaster VehicleMaster { get; set; }
    }
}
