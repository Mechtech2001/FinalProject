using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FinalProject.Controllers
{
    public class ExerciseController : Controller
    {
        private UserContext context { get; set; }

        public ExerciseController(UserContext ctx) => context = ctx;
        public IActionResult UserHome()
        {

            // Retrieve user ID from session
            var userID = HttpContext.Session.GetInt32("UserID");

            if (userID == null)
            {
                // Redirect to login if user ID is not in session
                return RedirectToAction("Login", "User");
            }

            // Fetch user and exercises
            var user = context.Users
                .Include(u => u.Exercises)
                .FirstOrDefault(u => u.UserID == userID);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Create and populate ViewModel
            var viewModel = new ExerciseViewModel
            {
                User = user,
                Exercises = user.Exercises
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            // Set ExerciseID to 0 for new exercises
            var exerciseViewModel = new ExerciseViewModel()
            {
                Exercise = new Exercise()
            };
            Console.WriteLine(exerciseViewModel.Exercise.ExerciseID);
            ViewBag.Action = "Add"; // Set action to add for view
            return View("Edit", exerciseViewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Fetch the exercise from the database
            var exercise = context.Exercises.FirstOrDefault(e => e.ExerciseID == id);
            if (exercise == null)
            {
                return NotFound();
            }

            var viewModel = new ExerciseViewModel { Exercise = exercise };
            ViewBag.Action = "Edit"; // Set action to edit for view
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ExerciseViewModel viewModel)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                return View(viewModel); // Return to view if validation fails
            }

            // If adding a new exercise (ExerciseID is null)
            if (viewModel.Exercise.ExerciseID == null)
            {
                // Add the new exercise to the database
                context.Exercises.Add(viewModel.Exercise);

                // Associate exercise with the logged-in user
                var userId = HttpContext.Session.GetInt32("UserID");
                var user = context.Users
                    .Include(u => u.Exercises)
                    .FirstOrDefault(u => u.UserID == userId);

                if (user != null)
                {
                    user.Exercises.Add(viewModel.Exercise);  // Associate the new exercise
                }
            }

            else
            {
                var exercise = context.Exercises.FirstOrDefault(e => e.ExerciseID == viewModel.Exercise.ExerciseID);

                if (exercise == null)
                {
                    return NotFound();
                }

                exercise.Name = viewModel.Exercise.Name;
                exercise.Weight = viewModel.Exercise.Weight;
                exercise.Reps = viewModel.Exercise.Reps;
            }

            // Save changes to the database
            context.SaveChanges();

            // Redirect to UserHome after saving
            return RedirectToAction("UserHome", new { id = viewModel.User.UserID });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {

            var exercise = context.Exercises.Find(id);

            // Error if exercise does not exist
            if (exercise == null)
            {
                return NotFound();
            }
            return View(exercise);
        }

        [HttpPost]
        public IActionResult Delete(Exercise exercise)
        {
            context.Exercises.Remove(exercise);
            context.SaveChanges();
            return RedirectToAction("UserHome", "Exercise");
        }
    }
}