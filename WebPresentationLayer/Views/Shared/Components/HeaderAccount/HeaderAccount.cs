namespace WebPresentationLayer.Components;
public class HeaderAccount : ViewComponent
{
	public async Task<IViewComponentResult> InvokeAsync()
	{
		return View("HeaderAccount");
	}
}