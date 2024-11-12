using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace FinalProject.Models
{
    public class Users
    {
        [Key] // This explicitly sets ExerciseID as the primary key
        public int UserID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int BodyWeight { get; set; }
        public int P4PStrength { get; set; }
        public bool Premium { get; set; }

        // Collection of exercises
        public ICollection<Exercises> Exercises { get; set; } = new List<Exercises>();
    }
}
