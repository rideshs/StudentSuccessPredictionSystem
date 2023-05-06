using System.ComponentModel.DataAnnotations.Schema;

namespace StudentSuccessPrediction.Models
{
    public class AttendanceMark
    {
        public int Id { get; set; }

        public int AttendanceMarkobtained { get; set; }

        // Navigation properties
        public int StudentId { get; set; }
        public virtual Student? Student { get; set; }

        public int SubjectId { get; set; }
        public virtual Subject? Subject { get; set; }

       
    }
}
