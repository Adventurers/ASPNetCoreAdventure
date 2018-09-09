using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASPNetCore.Routing.Models;

namespace ASPNetCore.Routing.Controllers
{
    //[Route("[controller]/[action]")]
    //[Route("")]
    //[Route("Home")]
    //[Route("[controller]")]
    public class HomeController : Controller
    {
        //[Route("Home")]
        [Route("Default/{sku:SKUConstraint}")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
