﻿namespace WebPresentationLayer.Components;
public class Hero : ViewComponent
{
	public string Page { get; set; } = "home";
	public async Task<IViewComponentResult> InvokeAsync()
	{
		Page = new Utility.ViewUtility().GetActivePage(HttpContext);
		return View("Hero",this);
	}
}