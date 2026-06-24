using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI46GIS.Models;

namespace WebAPI46GIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BindController : ControllerBase
    {
        [HttpGet]
       // [HttpGet("{id}/{name}/{ManagerName}")]
       //api/bind?id=1&name=ddd&manager=78787
        public IActionResult CutomObj([FromQuery]Department dept)//from route ,query
        {
            return Ok();
        }
        [HttpPost]//send token body
        
        public IActionResult Add([FromBody]string token)
        {
            return Ok();

        }



        //--------------------------------------
        //[HttpGet("{age}")]//api/bind  (GET)
        ////RouteValue /api/bind/12
        ////Querystring /api/bind?age=12
        //public IActionResult testPrimitive(int age)
        //{
        //    return Ok("ok");
        //}
        //[HttpPost]//post api/bind
        //public IActionResult TestObj(Department dept,string Name)
        //{
        //    return Ok("ok");

        //}
    }
}
