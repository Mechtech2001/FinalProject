namespace FinalProject.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage="Please enter a username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter a password")]
        public string Password { get; set; }
        public List<Exercises>? ExerciseList { get; set; }
        [Required(ErrorMessage = "Please enter a bodyweight")]
        [Range(1.0, 1000.0)]
        public float BodyWeight { get; set; }
        public bool IsPremiumUser { get; set; }
    }
}
