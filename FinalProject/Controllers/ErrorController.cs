using Azure;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HandleStatusCode(int statusCode)
        {
            // Log the status code for debugging
            Console.WriteLine($"HandleStatusCode hit with status code: {statusCode}");

            // Default behavior: Show Not Found for 404 or other errors
            if (statusCode == 404)
            {
                ViewData["Title"] = "Page Not Found";
                return View("NotFound");
            }
            else
            {
                ViewData["Title"] = "Error";
                return View("Error");
            }
        }

        [Route("Error/ServerError")]
        public IActionResult HandleServerError()
        {
            return View("ServerError");
        }
    }

}
