namespace WebPresentationLayer.Components;
public class Footer : ViewComponent
{
	public async Task<IViewComponentResult> InvokeAsync()
	{
		return View("Footer");
	}
}