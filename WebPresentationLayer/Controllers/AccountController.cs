using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebPresentationLayer.Models;

namespace WebPresentationLayer.Controllers;

[Authorize]
public class AccountController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ChangePassword()
    {
        return View();
    }

    public IActionResult MyPets()
    {
        return View();
    }

    public IActionResult Requests()
    {
        return View();
    }
}
