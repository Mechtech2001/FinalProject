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
                    return View("Error", "Invalid credentials");
                }
            }
            else
            {
                Console.WriteLine("User not found: {0}", username);
                return View("Error", "User not found");
            }
        }

        // Edit method: Retrieves user and associated exercises, updates premium status from session
        public IActionResult Edit(int id)
        {
            var user = context.Users.Include(u => u.Exercises).FirstOrDefault(u => u.UserID == id);

            if (user == null)
            {
                Console.WriteLine("User with ID {0} not found.", id);
                return NotFound();
            }

            // Retrieve Premium from session
            bool Premium = true;
            var PremiumSession = HttpContext.Session.GetString("Premium");
            if (!string.IsNullOrEmpty(PremiumSession))
            {
                Premium = bool.Parse(PremiumSession);
            }
            Console.WriteLine("Retrieved Premium from session: {0}", Premium);

            // Update the user's Premium status based on session value
            user.Premium = Premium;
            context.SaveChanges();
            Console.WriteLine("User {0} premium status updated to: {1}", user.Username, Premium);

            // Create ViewModel used to pull data for view
            var viewModel = new ExerciseViewModel
            {
                Exercise = user.Exercises.FirstOrDefault(),
                User = user
            };

            Console.WriteLine("Passing user and exercise data to the view. Exercise count: {0}", user.Exercises.Count);

            return View(viewModel);
        }
        
        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                context.Update(user);
                context.SaveChanges();
           
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("Model state is invalid for user {0}. Errors: {1}", user.Username, ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return View(user);
            }
        }
        
    }
}