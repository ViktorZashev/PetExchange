using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebPresentationLayer.Models;

namespace WebPresentationLayer.Controllers
{
    public class PetsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
