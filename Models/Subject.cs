using System.ComponentModel.DataAnnotations;

namespace StudentSuccessPrediction.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string SubjectName { get; set; }= string.Empty;
        public string SubjectCode { get; set; }= String.Empty;

        // Navigation properties

        public int? CourseId { get; set; }
        public Course? Course { get; set; }

        public ICollection<Student>? Students { get; set; }


    }
}
