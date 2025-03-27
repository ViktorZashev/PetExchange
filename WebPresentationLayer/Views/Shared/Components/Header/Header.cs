using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WebPresentationLayer.Components;

public class Header : ViewComponent
{
	private readonly UserManager<User> _userManager;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public Header(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
	{
		_userManager = userManager;
		_httpContextAccessor = httpContextAccessor;
	}
	public string PageName { get; set; } = "home";
	public string ControllerName { get; set; } = "home";
	public List<MenuItem> SiteMenu { get; set; } = new List<MenuItem>();
	public List<MenuItem> UserMenu { get; set; } = new List<MenuItem>();
	public User? CurrentUser { get; set; } = null;
	public async Task<IViewComponentResult> InvokeAsync()
	{
		PageName = new Utility.ViewUtility().GetActivePage(HttpContext);
		ControllerName = new Utility.ViewUtility().GetActiveController(HttpContext);
		//Меню на header
		SiteMenu.Add(new MenuItem()
		{
			Title = "Начало",
			Controller = "Home",
			Action = "Index",
			IsActive = PageName == "home"
		});
		SiteMenu.Add(new MenuItem()
		{
			Title = "Любимци",
			Controller = "Pets",
			Action = "Index",
			IsActive = PageName == "pets"
		});
		SiteMenu.Add(new MenuItem()
		{
			Title = "За нас",
			Controller = "Site",
			Action = "AboutUs",
			IsActive = PageName == "aboutus"
		});

		//Потребителско меню
		var httpContext = _httpContextAccessor.HttpContext;

		if (httpContext?.User is not null)
		{
			var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId is not null)
			{
				CurrentUser = await _userManager.FindByIdAsync(userId);
			}
		}
		if (CurrentUser is not null)
		{
			UserMenu.Add(new MenuItem()
			{
				Title = "Профил",
				Controller = "Account",
				Action = "Details"
			});
			UserMenu.Add(new MenuItem()
			{
				Title = "Смяна парола",
				Controller = "Account",
				Action = "ChangePassword"
			});
			UserMenu.Add(new MenuItem()
			{
				IsDivider = true,
			});
			UserMenu.Add(new MenuItem()
			{
				Title = "Моите любимци",
				Controller = "Account",
				Action = "Pets"
			});
			UserMenu.Add(new MenuItem()
			{
				Title = "Получени Искания",
				Controller = "Account",
				Action = "RequestInbox"
			});
			UserMenu.Add(new MenuItem()
			{
				IsDivider = true,
			});
			UserMenu.Add(new MenuItem()
			{
				Title = "Изпратени Искания",
				Controller = "Account",
				Action = "RequestOutbox"
			});
			UserMenu.Add(new MenuItem()
			{
				IsDivider = true,
			});
			UserMenu.Add(new MenuItem()
			{
				Title = "Изход",
				Area = "Identity",
				Page = "/Account/Logout",
			});
		}
		else
		{
			UserMenu.Add(new MenuItem()
			{
				Title = "Регистрация",
				Area = "Identity",
				Page = "/Account/Register",
				IsActive = PageName == "register"
			});
			UserMenu.Add(new MenuItem()
			{
				Title = "Вход",
				Area = "Identity",
				Page = "/Account/Login",
				IsActive = PageName == "login"
			});
		}
		return View("Header", this);
	}
}