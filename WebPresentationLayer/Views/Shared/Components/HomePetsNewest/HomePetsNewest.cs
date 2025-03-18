namespace WebPresentationLayer.Components;
public class HomePetsNewest : ViewComponent
{
	private readonly PetService _petSrv;
	public HomePetsNewest(PetService petService)
	{
		_petSrv = petService;
	}
	public async Task<IViewComponentResult> InvokeAsync()
	{

		ViewBag.Pets = await _petSrv.Read4NewestAsync();
		return View("HomePetsNewest");
	}
}