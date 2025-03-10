namespace WebPresentationLayer.Components;
public class Header : ViewComponent
{
	public string Page { get; set; } = "home";
	public async Task<IViewComponentResult> InvokeAsync()
	{
		Page = new Utils().GetActivePage(HttpContext);
		return View("Header", this);
	}
}