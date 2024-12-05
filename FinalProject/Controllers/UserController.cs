﻿using Microsoft.AspNetCore.Mvc;
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

            // Find user by username
            var user = context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                Console.WriteLine("User not found: {0}", username);
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View("~/Views/Home/Index.cshtml"); // Redisplay login view with an error
            }

            // Check if the password matches
            if (user.Password != password)
            {
                Console.WriteLine("Password mismatch for Username: {0}", username);
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View("~/Views/Home/Index.cshtmlx"); // Redisplay login view with an error
            }

            // Successful login
            Console.WriteLine("Password match successful for {0}", username);

            HttpContext.Session.SetInt32("UserID", user.UserID);

            // Store Premium in session
            HttpContext.Session.SetString("Premium", Premium.ToString());
            Console.WriteLine("Stored UserID and Premium status in session: {0}, {1}", user.UserID, Premium);

            Console.WriteLine("Redirecting to UserHome with UserID: {0}", user.UserID);

            // Redirect to UserHome
            return RedirectToAction("UserHome", "Exercise", new { id = user.UserID });
        }

    }
}