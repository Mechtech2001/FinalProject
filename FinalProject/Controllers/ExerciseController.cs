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
            ViewBag.Action = "Add";
            return View("Edit", new Exercise());
        }

        [HttpGet]
        public IActionResult Edit(string exerciseId)
        {
            ViewBag.Action = "Edit";
            var exercise = context.Exercises.Find(exerciseId);
            return View(exercise);
        }

        [HttpPost]
        public IActionResult Edit(Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                if (exercise.ExerciseID == 0)

                    context.Exercises.Add(exercise);

                else

                    context.Exercises.Update(exercise);
                context.SaveChanges();
                return RedirectToAction("UserHome", "Exercise");

            }
            else
            {
                ViewBag.Action = (exercise.ExerciseID == 0) ? "Add" : "Edit";
                return View(exercise);
            }
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {

            var exercise = context.Exercises.Find(id);
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