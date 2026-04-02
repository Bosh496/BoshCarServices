using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoshCarServices.Data.Entities
{
    public class VehicleMaster
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CustomerMaster")]
        public int CId { get; set; }

        [Column(TypeName = "varchar(20)")]
        [Required(ErrorMessage = "Registration number required")]
        [RegularExpression(@"^[A-Z]{2}[0-9]{2}[A-Z]{2}[0-9]{4}$",
        ErrorMessage = "Invalid vehicle number (Example: KA05MQ557)")]
        public string RegNum { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Required(ErrorMessage = "Vehicle make required")]
        public string VehicleMake { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Required(ErrorMessage = "Vehicle model required")]
        public string VehicleModel { get; set; }

        public bool IsActive { get; set; }

        public int? createdBy { get; set; }

        public DateTime? createdDate { get; set; }

        public int? modifiedBy { get; set; }

        public DateTime? modifiedDate { get; set; }

        public CustomerMaster CustomerMaster { get; set; }

    }
}
