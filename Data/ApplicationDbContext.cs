using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentSuccessPrediction.Controllers;
using StudentSuccessPrediction.Models;

namespace StudentSuccessPrediction.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<StudentSuccessPrediction.Models.Course> Courses { get; set; }
        public DbSet<StudentSuccessPrediction.Models.Subject> Subjects { get; set; }

        public DbSet<StudentSuccessPrediction.Models.Semester>Semesters { get; set; }

        public DbSet<Student> Students { get; set; }


        public DbSet<AttendanceMark> AttendanceMarks { get; set; }
        public DbSet<AssignmentMark> AssignmentMarks { get; set; }



        public DbSet<Preboard> Preboards { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            


            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });


            modelBuilder.Entity<AttendanceMark>()
           .HasOne(a => a.Student)
           .WithMany(s => s.AttendanceMarks)
           .HasForeignKey(a => a.StudentId);


            modelBuilder.Entity<AssignmentMark>()
        .HasOne(a => a.Student)
        .WithMany(s => s.AssignmentMarks)
        .HasForeignKey(a => a.StudentId);

            //create a unique index on the StudentId and SubjectId columns of your PreboardMark table,
            //which will prevent the same student from entering the same subject marks twice. 
            modelBuilder.Entity<Preboard>()
        .HasIndex(p => new { p.StudentId, p.SubjectId })
        .IsUnique();

        }





    }
}