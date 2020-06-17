using System.ComponentModel.DataAnnotations;

namespace coreTest3.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [StringLength(10)]
        public string MobileNumber { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

}
