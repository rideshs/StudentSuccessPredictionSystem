namespace StudentSuccessPrediction.Models
{
    public class AttendanceMark
    {
        public int Id { get; set; }

        public int AttendanceMarkobtained { get; set; }

        // Navigation properties
        public int StudentId { get; set; }
        public string Subject { get; set; }
        public virtual Student? Student { get; set; }
    }
}
