namespace WebPresentationLayer.Components;
public class MenuItemComponent : ViewComponent
{
	public async Task<IViewComponentResult> InvokeAsync(MenuItem Item,MenuItemType Type)
	{
		ViewBag.Item = Item;
		ViewBag.Type = Type;
		return View("MenuItemComponent");
	}
}