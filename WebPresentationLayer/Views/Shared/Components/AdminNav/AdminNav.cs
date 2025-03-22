using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WebPresentationLayer.Components;

public class AdminNav : ViewComponent
{
	public string Page { get; set; } = "users";
	public async Task<IViewComponentResult> InvokeAsync()
	{
		Page = new Utility.ViewUtility().GetActivePage(HttpContext);

		return View("AdminNav", this);
	}
}