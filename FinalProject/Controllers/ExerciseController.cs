using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FinalProject.Controllers
{
    public class ExerciseController : Controller
    {
        private UserContext context { get; set; }

        public ExerciseController(UserContext ctx) => context = ctx;
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Exercises());
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            ViewBag.Action = "Edit";
            var exercise = context.Exercises.Find(id);
            return View(exercise);
        }

        [HttpPost]
        public IActionResult Edit(Exercises exercise)
        {
            if (ModelState.IsValid)
            {
                if (exercise.ExerciseID.IsNullOrEmpty())
                
                    context.Exercises.Add(exercise);
                
                else
                
                    context.Exercises.Update(exercise);
                    context.SaveChanges();
                    return RedirectToAction(/*redirect to user info page*/); 
                
            } 
            else
            {
                ViewBag.Action = string.IsNullOrEmpty(exercise.ExerciseID) ? "Add" : "Edit";
                return View(exercise);
            }
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var exercise = context.Exercises.Find();
            return View(exercise);
        } 

        [HttpPost]
        public IActionResult Delete(Exercises exercise)
        {
            context.Exercises.Remove(exercise);
            context.SaveChanges();
            return RedirectToAction(/*redirect to user info page*/);
        }
    }
}
