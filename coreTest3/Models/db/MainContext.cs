using System;
using Microsoft.EntityFrameworkCore;

namespace coreTest3.Models
{
    public class MainContext : DbContext , IDisposable
    {

        public MainContext(DbContextOptions options) : base(options)
        {


        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }



    }
}
