using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StudentSuccessPrediction.Models
{
    public class AssignmentMarkViewModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }


        [Display(Name = "Subject")]
        public int SubjectId { get; set; }

        [Display(Name = "Mark Obtained")]
        public int AssignmentMarkobtained { get; set; }
        public List<SelectListItem>? Students { get; set; }

        public List<SelectListItem> ?Subjects { get; set; }

        public string SubjectName { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
    }
}
