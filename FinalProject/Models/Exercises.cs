using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Exercises
    {
        // TEST BRANCH COMMENT
        public string ExerciseID { get; set; } = string.Empty;
        [Required(ErrorMessage ="Please enter a name for the exercise")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Please enter a weight for the exercise")]
        public int? Weight { get; set; }
        [Required(ErrorMessage = "Please enter the amount of reps for the exercise")]
        public int Reps { get; set; } 

       
    }
}
