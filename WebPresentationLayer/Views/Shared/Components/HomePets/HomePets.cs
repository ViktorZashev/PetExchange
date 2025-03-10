namespace WebPresentationLayer.Components;
public class HomePets : ViewComponent
{
	public async Task<IViewComponentResult> InvokeAsync()
	{
		return View("HomePets");
	}
}