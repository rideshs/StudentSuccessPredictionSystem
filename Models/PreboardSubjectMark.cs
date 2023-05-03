using System.ComponentModel.DataAnnotations;

namespace StudentSuccessPrediction.Models
{
    public class PreboardSubjectMark
    {
        [Key]
        public int Id { get; set; }

        public int MarkObtained { get; set; }

        // Foreign key
        public int SubjectId { get; set; }

        // Navigation property
        public Subject Subject { get; set; }

        // Foreign key
        public int PreboardMarkId { get; set; }

        // Navigation property
        public PreboardMark? PreboardMark { get; set; }
    }
}
