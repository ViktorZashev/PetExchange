using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebPresentationLayer.Models;

namespace WebPresentationLayer.Controllers
{
    public class SiteController : Controller
    {
        // Пренасочване към 4-те страници с допълнителна информация
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Terms()
        {
            return View();
        }

    }
}
