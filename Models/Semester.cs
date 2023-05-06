using StudentSuccessPrediction.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentSuccessPrediction.Models
{
    public class Semester
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }= string.Empty;

        // Navigation properties

        public ICollection<Student> ?Students { get; set; }





    }
}
