using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreTest3.Models;
using Microsoft.AspNetCore.Mvc;

namespace coreTest3.Controllers
{
    [Route("api/course")]
    [ApiController]
    public class CourseController : Controller
    {
        private StudentContext _studentContext;

        public CourseController(StudentContext context)
        {
            _studentContext = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Course>> Get()
        {

            return Ok(_studentContext.Courses.ToList());
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Course course)
        {

            if(course == null)
            {
                return NotFound("Course data is not supplied");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _studentContext.Courses.AddAsync(course);
            await _studentContext.SaveChangesAsync();
            return Ok(course);
        }

    }

   
}
