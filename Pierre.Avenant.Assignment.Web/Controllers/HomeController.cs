using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pierre.Avenant.Assignment.Core.Entities;
using Pierre.Avenant.Assignment.Web.Models;

namespace Pierre.Avenant.Assignment.Web.Controllers
{
    public class HomeController : Controller
    {
        private Configuration _config;
        public HomeController(IOptions<Configuration> configuration)
        {
            _config = configuration.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Pierre Avenant Assignment - Upload Account Transaction Data";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Phone Agent";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
