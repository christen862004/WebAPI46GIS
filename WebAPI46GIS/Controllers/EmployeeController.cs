using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI46GIS.Models;

namespace WebAPI46GIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]//binding ,automatc valiation
    public class EmployeeController : ControllerBase
    {
        private readonly ITIContext context;

        public EmployeeController(ITIContext _context)
        {
            context = _context;
        }
        [HttpGet("{id}")]
        [Authorize]//SEACH token "request Header" unauthorize
        public ActionResult<GeneralResponse> getbyid(int id)
        {
            Employee employee=context.Employees.FirstOrDefault(e=>e.Id==id);
            GeneralResponse response=new GeneralResponse();

            if (employee != null)
            {
                response.ISucess=true;
                response.Data = employee;
                return response;
            }
            response.ISucess = false;
            response.Data = "Employee not found";
            return response;
        }
        [HttpPost]
        public IActionResult add(Employee emp)
        {
            if (ModelState.IsValid)
            {
                context.Employees.Add(emp);
                context.SaveChanges();
                return Ok("Create");
            }
            return BadRequest(ModelState);
        }
    }
}
