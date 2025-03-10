namespace WebPresentationLayer.Utility;

public class Utils
{
	public string GetActivePage(HttpContext httpContext)
	{
		if (httpContext.Request.RouteValues.ContainsKey("page"))
		{
			if (httpContext.Request.RouteValues["page"]?.ToString() == "/Account/Register")
			{
				return "register";
			}
			else if (httpContext.Request.RouteValues["page"]?.ToString() == "/Account/Login")
			{
				return "login";
			}
		}
		else if (httpContext.Request.RouteValues.ContainsKey("controller"))
		{
			var controller = httpContext.Request.RouteValues["controller"]?.ToString().ToLower();
			var action = "index";
			if (httpContext.Request.RouteValues.ContainsKey("action"))
			{
				action = httpContext.Request.RouteValues["action"]?.ToString().ToLower();
			}

			if (controller == "site")
			{
				return httpContext.Request.RouteValues["action"]?.ToString().ToLower();
			}
			else
			{
				return controller;
			}
		}

		return "home";
	}
}
