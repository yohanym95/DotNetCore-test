using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace coreTest3.Models
{
    public class StudentContext : DbContext , IDisposable
    {

        public StudentContext(DbContextOptions options) : base(options)
        {


        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }



    }
}
