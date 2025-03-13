namespace WebPresentationLayer.Components;
public class PetCard : ViewComponent
{
	public async Task<IViewComponentResult> InvokeAsync(Pet pet)
	{
		return View("PetCard",pet);
	}
}