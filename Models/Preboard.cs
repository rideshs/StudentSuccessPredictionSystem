using System.ComponentModel.DataAnnotations;

namespace StudentSuccessPrediction.Models
{
    public class Preboard
    {
        [Key]
        public int Id { get; set; }


        // Foreign key property
        public int StudentId { get; set; }

        // Navigation property
        public virtual Student Student { get; set; }
        // Foreign key property
        public int SubjectId { get; set; }

        // Navigation property
        public virtual Subject Subject { get; set; }

        public int MarkObtained { get; set; }
    }
}
