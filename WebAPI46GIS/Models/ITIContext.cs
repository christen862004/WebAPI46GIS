using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAPI46GIS.Models
{
    public class ITIContext:IdentityDbContext<ApplicationUser>
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public ITIContext(DbContextOptions<ITIContext> options):base(options)
        {
            
        }
    }
}
