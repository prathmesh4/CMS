using CustMgmSys.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustMgmSys.Data
{
    public class CMSDbContext : IdentityDbContext<IdentityUser>
    {
        public CMSDbContext(DbContextOptions<CMSDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }


        // Add DbSet properties for your database tab 
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
