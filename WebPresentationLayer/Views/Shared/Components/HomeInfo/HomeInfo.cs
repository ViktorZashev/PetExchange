namespace WebPresentationLayer.Components;
public class HomeInfo : ViewComponent
{
	public async Task<IViewComponentResult> InvokeAsync()
	{
		return View("HomeInfo");
	}
}