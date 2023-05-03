using System.ComponentModel.DataAnnotations;

namespace StudentSuccessPrediction.Models
{
    public class PreboardMark
    {
        [Key]
        public int Id { get; set; }

        public int TotalMark { get; set; }

        // Foreign key property
        public int StudentId { get; set; }

        // Navigation property
        public Student Student { get; set; }

        // Navigation property
        public ICollection<PreboardSubjectMark> PreboardSubjectMarks { get; set; }
    }
}
