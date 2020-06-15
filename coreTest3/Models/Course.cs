using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace coreTest3.Models
{
    public class Course
    {
        [ForeignKey("Student")]
        public int Id { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
    }
}
