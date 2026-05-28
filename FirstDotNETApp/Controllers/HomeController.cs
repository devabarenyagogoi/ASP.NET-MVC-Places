// Import namespace. Allows controller to access classes and methods defined in the Models folder, such as ErrorViewModel
using FirstDotNETApp.Models;
// Imports ASP>NET Core MVC classes. Controller, IActionResult, View(), ResponseCache
using Microsoft.AspNetCore.Mvc;
// Diagnostics and debugging. Activity.Current?.Id
using System.Diagnostics;

// Controller belongs to Controller folder/module of project
namespace FirstDotNETApp.Controllers
{
    // Declares a class named HomeController
    // : Controller means HomeController inherits from ASP.NET Core MVC Controller base class
    // Inheritance. View(), Redirect(), Json()
    public class HomeController : Controller
    {
        // IActionResult -> return type for MVC actions. 
        // View, Redirect, JSON, File, Error Page
        // Index() -> Action method. Maps to /Home/Index
        public IActionResult Index()
        {
            // Returns Razor view page.
            // Searches for Views/Home/Index.cshtml
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Attribute. Controls browser caching behavior
        // Duration = 0 -> Cache expires immediately
        // Location = None -> Don't cache anywhere
        // NoStore = true -> Browser should not store the response
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        
        // Different from the above 2
        // Earlier: Opens a view. No data is sent.
        // Now: Creates an object of the ErrorViewModel model. Fills data inside it. Passes data to view (Model = Class that stores and manages data)
        // Method for showing errors
        // Creates ErrorViewModel object. Sends it to Error view
        // Activity.Current?.Id gets the current activity/request ID
        // ? -> if Activity.Current is null, don't crash
        // ?? -> if left (Id) is null, use right
        // HttpContext.TraceIdentifier -> Backup request ID provided by ASP.NET
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
