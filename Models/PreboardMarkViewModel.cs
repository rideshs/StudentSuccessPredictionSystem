using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentSuccessPrediction.Models
{
    public class PreboardMarkViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int SubjectId { get; set; } // add this property

        public List<SubjectMarkViewModel> Subjects { get; set; }
        public ICollection<SubjectMarkViewModel> SubjectMarkViewModels { get; set; }
        public List<PreboardMark>?PreboardMarks { get; set; }

        //public ICollection<Student> Students { get; set; }
        public List<SelectListItem>? Students { get; set; }

        //public int PreboardMarkobtained { get; set; }

    }
}
