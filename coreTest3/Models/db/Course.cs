using System.ComponentModel.DataAnnotations.Schema;

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
