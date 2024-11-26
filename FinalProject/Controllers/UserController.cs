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

                    HttpContext.Session.SetInt32("UserID", user.UserID);

                    // Store Premium in session
                    HttpContext.Session.SetString("Premium", Premium.ToString());
                    Console.WriteLine("Stored UserID and Premium status in session: {0}, {1}", user.UserID, Premium);

                    Console.WriteLine("Redirecting to UserHome with UserID: {0}", user.UserID);
                    // Redirect to Edit page with user ID
                    return RedirectToAction("UserHome", "Exercise", new { id = user.UserID });
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
    }
}