using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreTest3.Models;
using coreTest3.Models.response;
using Microsoft.AspNetCore.Mvc;

namespace coreTest3.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : Controller
    {
        private MainContext _mainContext;
        private Response response;

        public StudentController(MainContext context)
        {
            _mainContext = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Student>> Get()
        {
            response = new Response(true, null, _mainContext.Students.ToList());
            return Ok(response);
        }

        [HttpGet]
        [Route("id/{id}")]
        public ActionResult<Student> GetById( int? id) 
        {

            if(id <= 0)
            {
                response = new Response(false, "Student id must be higher than zero", null);
                return NotFound(response);
            }
            Student student = _mainContext.Students.FirstOrDefault(s => s.StudentId == id);

            if (student == null)
            {
                response = new Response(false, "Student not found",null);
                return NotFound(response);
            }

            response = new Response(true, null, student);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Student student)
        {
            if (student == null)
            {
                response = new Response(false, "Student data is not supplied", null);
                return NotFound(response);
            }

            if (!ModelState.IsValid)
            {
                response = new Response(false, "Error message", ModelState);
                return BadRequest(response);
            }

            await _mainContext.Students.AddAsync(student);
            await _mainContext.SaveChangesAsync();
            response = new Response(true, "Add the student Successfully", student);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody]Student student)
        {
            if(student == null)
            {
                response = new Response(false, "Student data is not supplied", null);
                return NotFound(response);
            }

            if (!ModelState.IsValid)
            {
                response = new Response(false, "Error Message", ModelState);
                return BadRequest(ModelState);
            }

            Student existingStudent = _mainContext.Students.FirstOrDefault(s => s.StudentId == student.StudentId);

            if (existingStudent == null)
            {
                response = new Response(false, "Student does not exist in the database", null);
                return NotFound(response);
            }

            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.MobileNumber = student.MobileNumber;
            existingStudent.City = student.City;
            existingStudent.Email = student.Email;

            _mainContext.Attach(existingStudent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _mainContext.SaveChangesAsync();
            response = new Response(true, "Update the student Successfully", existingStudent);
            return Ok(response);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if(id == null)
            {
                response = new Response(false, "Id is not supplied", null);
                return NotFound(response);
            }

            Student student = _mainContext.Students.FirstOrDefault(s => s.StudentId == id);

            if(student == null)
            {
                response = new Response(false, "Student is not found with particular id supplied", null);
                return NotFound(response);
            }
            _mainContext.Students.Remove(student);
            await _mainContext.SaveChangesAsync();
            response = new Response(true, "Student is deleted successfully", null);
            return Ok(response);
        }



        ~StudentController()
        {
            _mainContext.Dispose();
        }
    }
}
