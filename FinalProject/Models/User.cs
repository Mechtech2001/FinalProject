using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class User
    {
        public string UserID { get; set; } = string.Empty;
        [Required(ErrorMessage ="Please enter in your user name.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage ="Please enter your password.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage ="Please enter in your exercise.")]
        public Exercises? Exercises { get; set; }
        [Required(ErrorMessage ="Please enter in your body weight.")]
        [Range(0,1000, ErrorMessage ="Body weight has to be between 0 and 1000 pounds.")]
        public int? BodyWeight { get; set; }
        [Required(ErrorMessage ="Please enter if you are a premiun user")]
        public bool? IsPremiun { get; set; }




    }
}
