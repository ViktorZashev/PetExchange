using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using WebPresentationLayer.Models;

namespace WebPresentationLayer.Controllers;
public class PetsController : Controller
{
	private readonly PetService _petSrv;
	public PetsController(PetService petService)
	{
		_petSrv = petService;
	}
	public async Task<IActionResult> Index()
	{
		var filters = new FilterUtility().GetFilters(Request.GetDisplayUrl());
		ViewBag.Filters = filters;

		var selection = new FilterUtility().GetUrlFilters(Request.GetDisplayUrl());

		ViewBag.Pets = await _petSrv.ReadWithFiltersAsync(
			types:selection.Types,
			genders:selection.Gender,
			ages:selection.Age,
			withCage:selection.HasCage
		);
		return View();
	}

}
