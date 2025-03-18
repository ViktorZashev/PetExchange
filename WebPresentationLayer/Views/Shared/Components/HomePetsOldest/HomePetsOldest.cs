namespace WebPresentationLayer.Components;
public class HomePetsOldest : ViewComponent
{
	private readonly PetService _petSrv;
	public HomePetsOldest(PetService petService)
	{
		_petSrv = petService;
	}
	public async Task<IViewComponentResult> InvokeAsync()
	{
		ViewBag.Pets = await _petSrv.Read4OldestAsync();
		return View("HomePetsOldest");
	}
}