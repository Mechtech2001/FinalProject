using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace FinalProject.Models
{
    public class User
    {
        [Key] // This explicitly sets ExerciseID as the primary key
        public int UserID { get; set; }
        [Required(ErrorMessage ="Please enter you user name.")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage ="Please enter in your password.")]
        public string Password { get; set; } = string.Empty;
        public int? BodyWeight { get; set; }
        public int? P4PStrength { get; set; }
        public bool Premium { get; set; }

        // Collection of exercises
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}
