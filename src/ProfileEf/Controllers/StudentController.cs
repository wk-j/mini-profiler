using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace MyWeb.Controllers {
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StudentController : ControllerBase {
        readonly MyContext _context;
        public StudentController(MyContext context) {
            _context = context;
        }

        [HttpGet]
        public IActionResult Insert() {
            var student = new Student {
                Name = "wk"
            };

            _context.Students.Add(student);
            _context.SaveChanges();
            return Ok(student);
        }

        [HttpGet]
        public IActionResult Query() {
            var student = _context.Students.Where(x => x.Name == "wk").ToList();
            return Ok(student);
        }
    }
}