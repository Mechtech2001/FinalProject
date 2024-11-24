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

        // Login method: stores username & premium status within session state
        public IActionResult Login(string username, string password, bool Premium)
        {
            Console.WriteLine("Login attempt received for Username: {0}, Premium: {1}", username, Premium);

            // Find user by username and password
            var user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                Console.WriteLine("User found: {0}", user.Username);

                // Check if the password matches
                if (user.Password == password)
                {
                    Console.WriteLine("Password match successful for {0}", username);

                    // Store Premium in session
                    HttpContext.Session.SetString("Premium", Premium.ToString());
                    Console.WriteLine("Stored Premium status in session: {0}", Premium);

                    // Redirect to Edit page with user ID
                    return RedirectToAction("Edit", new { id = user.UserID });
                }
                else
                {
                    Console.WriteLine("Password mismatch for Username: {0}", username);
                    // TODO: redirect to error page.
                    return View("Error", "Invalid credentials");
                }
            }
            else
            {
                Console.WriteLine("User not found: {0}", username);
                // TODO: redirect to error page.
                return View("Error", "User not found");
            }
        }

        // Add method: Adds exercises depending on user selection
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Exercises = context.Exercises.OrderBy(g => g.Name).ToList();
            return View("Edit", new Exercise());
        }

        // Edit method: Retrieves user and associated exercises, updates premium status from session
        public IActionResult Edit(int id)
        {
            // Finding user based on the ID parameter
            var user = context.Users.Include(u => u.Exercises).FirstOrDefault(u => u.UserID == id);

            if (user == null)
            {
                Console.WriteLine("User with ID {0} not found.", id);
                // TODO: redirect to error page.
                return NotFound();
            }

            // Create ViewModel used to pull data for view
            var viewModel = new ExerciseViewModel
            {
                // Initializing new Exercise for "Add"
                Exercise = new Exercise(),
                User = user
            };

            // If there are existing exercises, use the first one for editing
            if (user.Exercises.Any())
            {
                viewModel.Exercise = user.Exercises.FirstOrDefault();
            }

            // Retrieve Premium from session
            bool Premium = true;
            var PremiumSession = HttpContext.Session.GetString("Premium");
            if (!string.IsNullOrEmpty(PremiumSession))
            {
                Premium = bool.Parse(PremiumSession);
            }

            // Update the user's Premium status based on session value
            user.Premium = Premium;
            context.SaveChanges();

            return View(viewModel);
        }
        
        [HttpPost]
        public IActionResult Edit(ExerciseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(viewModel.Exercise.ExerciseID)) // New Exercise
                {
                    context.Exercises.Add(viewModel.Exercise);
                }
                else // Editing existing Exercise
                {
                    context.Exercises.Update(viewModel.Exercise);
                }
                context.SaveChanges();

                return RedirectToAction("View");
            }
            return View(viewModel.Exercise);
        }
        
    }
}