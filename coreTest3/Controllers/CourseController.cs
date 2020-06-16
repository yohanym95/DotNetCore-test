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
        private MainContext _mainContext;

        public CourseController(MainContext context)
        {
            _mainContext = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Course>> Get()
        {

            return Ok(_mainContext.Courses.ToList());
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

            await _mainContext.Courses.AddAsync(course);
            await _mainContext.SaveChangesAsync();
            return Ok(course);
        }

        //[HttpPut]
        //public async Task<ActionResult> Update([FromBody] Course course)
        //{
        //    if (course == null)
        //    {
        //        return NotFound("Course data is not supplied");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    //Student existingStudent = _mainContext.Courses.FirstOrDefault(s => s == student.StudentId);

        //    //if (existingStudent == null)
        //    //{
        //    //    return NotFound("Student does not exist in the database");
        //    //}

        //    //existingStudent.FirstName = student.FirstName;
        //    //existingStudent.LastName = student.LastName;
        //    //existingStudent.State = student.State;
        //    //existingStudent.City = student.City;

        //    //_mainContext.Attach(existingStudent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //    //await _mainContext.SaveChangesAsync();
        //    //return Ok(existingStudent);

        //}

        ~CourseController()
        {
            _mainContext.Dispose();
        }

    }

   
}
