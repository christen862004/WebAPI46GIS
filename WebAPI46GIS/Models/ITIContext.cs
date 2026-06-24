using Microsoft.EntityFrameworkCore;

namespace WebAPI46GIS.Models
{
    public class ITIContext:DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public ITIContext(DbContextOptions<ITIContext> options):base(options)
        {
            
        }
    }
}
