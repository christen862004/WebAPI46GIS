using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI46GIS.Models;

namespace WebAPI46GIS.Controllers
{
    [Route("api/[controller]")]//uniform name api/Department
    [ApiController]//change behaviour contorller (binding ,valiadtion)
    public class DepartmentController : ControllerBase
    {
        private readonly ITIContext context;
        public DepartmentController(ITIContext context)
        {
            this.context = context;
        }

        //CRUD name action start with verb
        #region Get

       
        [HttpGet]//api/Department req verb : get
        public IActionResult showAll()
        { 
            List<Department> deptList= context.Departments.ToList();
            return Ok(deptList);
        }
        
        [HttpGet("{id:int}")]//api/DEpartment/11 verb get
        public IActionResult GetById(int id)
        {
            Department dept=context.Departments.FirstOrDefault(d => d.Id == id);
            if(dept!=null)
                return Ok(dept);
            return BadRequest("Invalid id");
        }
        
        [HttpGet("{name:alpha}")]//api/DEpartment/SD verb get
        public IActionResult GetByName(string name)
        {
            Department dept = context.Departments.FirstOrDefault(d => d.Name == name);
            return Ok(dept);
        }
        #endregion
        //binding 
        //primitive reoutevalue , querystring,default
        //complex req.body
        [HttpPost]//api/Department req verb : post
        public IActionResult add(Department depFromRequest) {
            if (ModelState.IsValid)
            {
                context.Departments.Add(depFromRequest);
                context.SaveChanges();
                //return Created($"http://localhost:1987/api/Department/{depFromRequest.Id}",depFromRequest);//api/department/4
                return CreatedAtAction(actionName:"GetById",routeValues:new {id=depFromRequest.Id },value:depFromRequest);//api/department/4
            }
            return BadRequest(ModelState);
        }



        [HttpPut("{id:int}")]//api/Department/1 req verb : put
      //  [HttpPut]//api/Department?id=1 req verb : put
        public IActionResult update(int id,Department depfronreq) {
            if (ModelState.IsValid)
            {
                //old fref
                Department depfromDb = context.Departments.FirstOrDefault(d => d.Id == id);
                //map
                depfromDb.Name= depfronreq.Name;
                depfromDb.ManagerName= depfronreq.ManagerName;
                //save
                context.SaveChanges();
                return NoContent();//Ok("success");
            }
            return BadRequest(ModelState);
        }


        [HttpDelete("{id:int}")]//api/Department req verb : delete
        public IActionResult removeDept(int id) {
            //  NoContentResult edit | delete 204
            //  OkResult get 200
            //  CreatedResult  post 201
            try
            {
                Department dept = context.Departments.FirstOrDefault(d => d.Id == id);
                context.Departments.Remove(dept);
                context.SaveChanges();
                return NoContent();// Ok("Success");
            }catch(Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }
        
    }
}
