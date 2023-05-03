using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace StudentSuccessPrediction.Models
{
    public class StudentViewModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string Gender { get; set; }

        [Display(Name = "Course")]
        public int CourseId { get; set; }

        [Display(Name = "Semester")]
        public int SemesterId { get; set; }
        public Course? Course { get; set; }

        public IEnumerable<SelectListItem>? Courses { get; set; }
        public IEnumerable<SelectListItem>? Semesters { get; set; }


    }

}
