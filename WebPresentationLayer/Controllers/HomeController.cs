using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebPresentationLayer.Models;

namespace WebPresentationLayer.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()  
    {
        return View();
    }

    // Пренасочване към персонализираните страници за грешки

    [Route("Error/404")]
    public IActionResult NotFound()
        
    {
        return View("Views/Error/NotFound.cshtml");
    }

    [Route("Error/500")]
    public IActionResult System()
    {
        return View("Views/Error/System.cshtml");
    }
}

