using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using coreTest3.Models;
using Microsoft.AspNetCore.Mvc;

namespace coreTest3.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : Controller
    {
        private StudentContext _studentContext;

        public StudentController(StudentContext context)
        {
            _studentContext = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Student>> Get()
        {
            return Ok(_studentContext.Students.ToList());
        }

        [HttpGet]
        [Route("id/{id}")]
        public ActionResult<Student> GetById( int? id) 
        {
            if(id <= 0)
            {
                return NotFound("Student id must be higher than zero");
            }
            Student student = _studentContext.Students.FirstOrDefault(s => s.StudentId == id);

            if (student == null)
            {
                return NotFound("Student not found");
            }

            return Ok(student);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Student student)
        {
            if (student == null)
            {
                return NotFound("Student data is not supplied");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _studentContext.Students.AddAsync(student);
            await _studentContext.SaveChangesAsync();
            return Ok(student);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody]Student student)
        {
            if(student == null)
            {
                return NotFound("Student data is not supplied");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Student existingStudent = _studentContext.Students.FirstOrDefault(s => s.StudentId == student.StudentId);

            if (existingStudent == null)
            {
                return NotFound("Student does not exist in the database");
            }

            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.State = student.State;
            existingStudent.City = student.City;

            _studentContext.Attach(existingStudent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _studentContext.SaveChangesAsync();
            return Ok(existingStudent);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound("Id is not supplied");
            }

            Student student = _studentContext.Students.FirstOrDefault(s => s.StudentId == id);

            if(student == null)
            {
                return NotFound("Student is not found with particular id supplied");
            }
            _studentContext.Students.Remove(student);
            await _studentContext.SaveChangesAsync();
            return Ok("Student is deleted successfully");
        }



        ~StudentController()
        {
            _studentContext.Dispose();
        }
    }
}
