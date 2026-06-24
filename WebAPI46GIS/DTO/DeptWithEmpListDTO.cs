namespace WebAPI46GIS.DTO
{
    public class DeptWithEmpListDTO
    {
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public List<string> EmpNames { get; set; }
    }
}
