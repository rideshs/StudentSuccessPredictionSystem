namespace StudentSuccessPrediction.Models
{
    public class SubjectMarkViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int SubjectId { get; set; } // Add this line

        public string? SubjectName { get; set; } // Add this new property

        public int MarkObtained { get; set; }

        public int Index { get; set; }

    }
}
