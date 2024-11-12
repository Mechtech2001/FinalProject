using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FinalProject.Models
{
    public class Exercises
    {
        [Key] // This explicitly sets ExerciseID as the primary key
        public string ExerciseID { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter a name for the exercise")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage ="Please enter a weight for the exercise")]
        public int? Weight { get; set; } 
        [Required(ErrorMessage = "Please enter the amount of reps for the exercise")]
        public int Reps { get; set; }

        public ICollection<Users> Users { get; set; } 

    }
}
