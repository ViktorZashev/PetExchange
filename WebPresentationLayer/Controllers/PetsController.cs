using Microsoft.AspNetCore.Http.Extensions;

namespace WebPresentationLayer.Controllers;
public class PetsController : Controller
{
	private readonly PetService _petSrv;
	public PetsController(PetService petService)
	{
		_petSrv = petService;
	}
	public async Task<IActionResult> Index()
		// Зареждане на домашните любимци за страница "Любимци"
	{
		var filters = new FilterUtility().GetFilters(Request.GetDisplayUrl());
		ViewBag.Filters = filters;

		var selection = new FilterUtility().GetUrlFilters(Request.GetDisplayUrl());

		ViewBag.Pets = await _petSrv.ReadWithFiltersAsync(
			types:selection.Types,
			genders:selection.Gender,
			ages:selection.Age,
			withCage:selection.HasCage,
			page: selection.Page,
			pageSize:8
		);
		ViewBag.Page = selection.Page;
		ViewBag.PrevPageUrl = ViewUtility.GeneratePageUrl(HttpContext,selection.Page-1);
		ViewBag.NextPageUrl = ViewUtility.GeneratePageUrl(HttpContext,selection.Page+1);
		return View();
	}

}
