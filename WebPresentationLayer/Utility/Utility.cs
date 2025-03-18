namespace WebPresentationLayer.Utility;

public class Utility
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
				return action;
			}
			else if (controller == "admin")
			{
				return "admin-" + action;
			}
			else if (controller == "account")
			{
				return "account-" + action;
			}
			else
			{
				return controller;
			}
		}

		return "home";
	}

	public string GetActiveController(HttpContext httpContext)
	{
		if (httpContext.Request.RouteValues.ContainsKey("page"))
		{
			return "site";
		}
		else if (httpContext.Request.RouteValues.ContainsKey("controller"))
		{
			return httpContext.Request.RouteValues["controller"]?.ToString().ToLower();
		}
		return "home";
	}

	public static string ConvertDaysToString(int totalDays, bool isAbbriviation = false)
	{
		const int DaysPerYear = 365;
		const int DaysPerMonth = 30;

		int years = totalDays / DaysPerYear;
		int remainingDaysAfterYears = totalDays % DaysPerYear;

		int months = remainingDaysAfterYears / DaysPerMonth;
		int days = remainingDaysAfterYears % DaysPerMonth;


		if (isAbbriviation)
		{
			if (years > 0)
				return $"{years} {(years != 1 ? "години" : "година")}";
			if (months > 0)
				return $"{months} {(months != 1 ? "месеца" : "месец")}";

			return $"{days} {(days != 1 ? "дни" : "ден")}";
		}
		else
		{
			if (years > 0)
				return $"{years} {(years != 1 ? "години" : "година")}, {months} {(months != 1 ? "месеца" : "месец")}, {days} {(days != 1 ? "дни" : "ден")}";
			if (months > 0)
				return $"{months} {(months != 1 ? "месеца" : "месец")}, {days} {(days != 1 ? "дни" : "ден")}";

			return $"{days} {(days != 1 ? "дни" : "ден")}";

		}
	}

}
