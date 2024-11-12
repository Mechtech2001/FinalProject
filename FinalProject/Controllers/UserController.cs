using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class UserController : Controller
    {

        private UserContext context { get; set; }
        public UserController(UserContext ctx) => context = ctx;
        public IActionResult Index()
        {
            return View();
        }

        // Method that allows users to login and stores username & premium status within session state.
        public IActionResult Login(string username, string password, bool isPremium)
        {
            // Debugging step: Check if isPremium is correctly received
            Console.WriteLine($"Received isPremium: {isPremium}");

            // Query to find first user that matches specified username/password
            var user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            // If user is valid
            if (user != null)
            {
                // Update the Premium status for the user
                user.Premium = isPremium;

                // Save changes to the database
                context.SaveChanges();
                //DEBUGGING
                Console.WriteLine($"Premium status updated: {user.Premium}");

                // Store user info and premium status in session
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Premium", isPremium.ToString());

                // DEBUGGING
                var premiumStatus = HttpContext.Session.GetString("Premium");
                Console.WriteLine($"Session Premium status: {premiumStatus}");

                // Redirect to Edit view
                return RedirectToAction("Edit", new { id = user.UserID });
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return View("Home/Index"); // Return to login page if failed
        }

        // Method used to view the user/exercise data.
        public IActionResult Edit(int id)
        {
            // Retreiving the exercise and user data
            var user = context.Users.Include(u => u.Exercises).FirstOrDefault(u => u.UserID == id);

            // Invalid user
            if (user == null)
            {
                return NotFound();
            }

            // Create ViewModel used to pull data for view
            var viewModel = new ExerciseViewModel
            {
                Exercise = user.Exercises.FirstOrDefault(),
                User = user
            };

            return View(viewModel); // Pass the view model to the view
        }

        [HttpPost]
        public IActionResult Edit(Users user)
        {
            if (ModelState.IsValid)
            {
                context.Update(user);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
    }


}
