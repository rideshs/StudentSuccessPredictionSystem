using System.ComponentModel.DataAnnotations.Schema;

namespace StudentSuccessPrediction.Models
{
    public class AssignmentMark
    {
        public int Id { get; set; }

        public int AssignmentMarkobtained { get; set; }

        // Navigation properties
        public int StudentId { get; set; }
        public virtual Student? Student { get; set; }

        public int SubjectId { get; set; }

        [ForeignKey("SubjectId")]
        public virtual Subject? Subject { get; set; }
    }
}
