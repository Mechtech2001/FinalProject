namespace FinalProject.Models
{
    // Model created to allow for the viewing of P4PStrength within Edit view with Exercise set as main view model.
    public class ExerciseViewModel
    {
        public Exercise Exercise { get; set; } = new Exercise();
        public User User { get; set; } = new User();

        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}
