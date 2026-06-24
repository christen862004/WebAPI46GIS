using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;
using System.Text.Json.Serialization;

namespace WebAPI46GIS.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        
        [JsonIgnore]
        public Department? Department { get; set; }
    }
}
