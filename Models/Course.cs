using StudentSuccessPrediction.Controllers;
using System.ComponentModel.DataAnnotations;

namespace StudentSuccessPrediction.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string CourseName { get; set; }= string.Empty;
        public string CourseCode { get; set; } = string.Empty;  

        // Navigation properties
        public ICollection<Subject>? Subjects { get; set; }
        public ICollection<Student> Students { get; set; }


        // Navigation properties
        //  public ICollection<Semester>? Semesters { get; set; }



    }
}
