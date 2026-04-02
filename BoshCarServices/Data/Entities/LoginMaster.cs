using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoshCarServices.Data.Entities
{
    public class LoginMaster
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Username { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public int? createdBy { get; set; }

        public DateTime? createdDate { get; set; }

        public int? modifiedBy { get; set; }

        public DateTime? modifiedDate { get; set; }

        public ICollection<CustomerMaster> Customers { get; set; }
    }
}
