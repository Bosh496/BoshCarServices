using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoshCarServices.Data.Entities
{
    public class CustomerMaster
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("LoginMaster")]
        public int userId { get; set; }

        [Column(TypeName = "varchar(150)")]
        [Required(ErrorMessage = "Customer name is required")]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters allowed")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(15)")]
        [Required(ErrorMessage = "Mobile number is required")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Enter valid 10 digit mobile number")]
        public string Mobile { get; set; }

        [Column(TypeName = "varchar(150)")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        public int? createdBy { get; set; }

        public DateTime? createdDate { get; set; }

        public int? modifiedBy { get; set; }

        public DateTime? modifiedDate { get; set; }

        public LoginMaster LoginMaster { get; set; }
    }
}
