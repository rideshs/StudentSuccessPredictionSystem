using System.ComponentModel.DataAnnotations;

namespace StudentSuccessPrediction.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }= string.Empty;

        public string Gender { get; set; }= string.Empty;

        // Foreign keys
        public int SemesterId { get; set; }

        public int CourseId { get; set; }


        // Navigation properties
        public Semester Semester { get; set; }
        public ICollection<Subject>? Subjects { get; set; }
        public Course Course { get; set; }

        // Navigation properties
        public virtual ICollection<AttendanceMark> AttendanceMarks { get; set; }

        public virtual ICollection<AssignmentMark> AssignmentMarks { get; set; }

    }
}
