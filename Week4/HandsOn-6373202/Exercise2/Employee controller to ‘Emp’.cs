using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/Emp")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new List<string> { "Teja", "Ravi", "Rahul" });
        }

        [HttpGet("ById")]
        public IActionResult GetById([FromQuery] int id)
        {
            return Ok("GetById called. ID: " + id);
        }
    }
}
