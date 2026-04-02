using BoshCarServices.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoshCarServices.Data.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<LoginMaster> LoginMasters { get; set; }

        public DbSet<CustomerMaster> CustomerMasters { get; set; }

        public DbSet<VehicleMaster> VehicleMasters { get; set; }

        public DbSet<ServiceMaster> ServiceMasters { get; set; }
        public DbSet<ServiceTypeMaster> ServiceTypeMasters { get; set; }
        public DbSet<ServiceTypeMapping> ServiceTypeMappings { get; set; }
    }
}
