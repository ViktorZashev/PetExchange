using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebPresentationLayer.Models;

namespace WebPresentationLayer.Controllers;

public class ErrorController : Controller
{
	[Route("Error/404")]
	public IActionResult NotFound()
	{
		return View();
	}

	[Route("Error/500")]
	public IActionResult System()
	{
		return View();
	}

}
