namespace WebPresentationLayer.Components;
public class FilterGroupComponent : ViewComponent
{
	public async Task<IViewComponentResult> InvokeAsync(FilterGroup Group)
	{
		
		return View("FilterGroupComponent",Group);
	}
}