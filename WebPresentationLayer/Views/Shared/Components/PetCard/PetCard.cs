namespace WebPresentationLayer.Components;
public class PetCard : ViewComponent
{
	public async Task<IViewComponentResult> InvokeAsync(Pet pet)
	{
		ViewBag.Pet = pet;
		var showCreateRequest = true;
		if(HttpContext.Request.Path.ToString().Contains("create-request"))
		showCreateRequest = false;

		ViewBag.ShowCreateRequest = showCreateRequest;
		return View("PetCard");
	}
}