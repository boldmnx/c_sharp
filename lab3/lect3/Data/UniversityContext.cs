using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace lect3.Data
{
    public class UniversityContext : DbContext
    {
        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Major> Majors { get; set; }
    }

}
