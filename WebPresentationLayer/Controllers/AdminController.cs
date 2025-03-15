using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using WebPresentationLayer.Models;
using WebPresentationLayer.Services;

namespace WebPresentationLayer.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
	private readonly UserService _userSrv;
	private readonly TownService _townSrv;
	private readonly FileService _fileSrv;

	public AdminController(UserService userService,
		TownService townService, FileService fileService)
	{
		_userSrv = userService;
		_townSrv = townService;
		_fileSrv = fileService;
	}
	public async Task<IActionResult> Users(
		[FromQuery] string username = null,
		[FromQuery] string name = null,
		[FromQuery] string email = null,
		[FromQuery] string town = null,
		[FromQuery] string role = null,
		[FromQuery] int page = 1,
		[FromQuery] int pageSize = 10
	)
	{
		ViewBag.Items = await _userSrv.ReadAllWithFilterAsync(
			username: username,
			name,
			email,
			town,
			role,
			ascendingUsername: true,
			page,
			pageSize: pageSize,
			useNavigationalProperties: true,
			isReadOnly: true
		);
		ViewBag.Username = username;
		ViewBag.Name = name;
		ViewBag.Email = email;
		ViewBag.Town = town;
		ViewBag.Role = role;
		ViewBag.Page = page;
		ViewBag.PageSize = pageSize;
		ViewBag.ReturnUrl = HttpUtility.UrlEncode(HttpContext.Request.Path + HttpContext.Request.QueryString);
		return View();
	}

	[HttpGet("/admin/users/{userId:guid}")]
	public async Task<IActionResult> UserManage(
		[FromRoute] Guid userId,
		[FromQuery] string returnUrl)
	{
		ViewBag.CancelUrl = !String.IsNullOrWhiteSpace(returnUrl) ? returnUrl : "/admin/users";
		ViewBag.Towns = await _townSrv.ReadAllAsync();
		var dbUser = await _userSrv.ReadAsync(userId, true, true);
		var user = new UserManage
		{
			Email = dbUser.Email,
			UserName = dbUser.UserName,
			PhoneNumber = dbUser.PhoneNumber,
			Name = dbUser.Name,
			PhotoPath = dbUser.PhotoPath,
			Role = dbUser.Role,
			TownId = dbUser.TownId,
		};
		var roles = new List<SelectListItem>();
		foreach (var item in Enum.GetValues<RoleEnum>())
		{
			roles.Add(new SelectListItem { Value = ((int)item).ToString(), Text = item.ToDescriptionString() });
		}
		ViewBag.Roles = roles;
		return View(user);
	}

	[HttpPost("/admin/users/{userId:guid}")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> UserManage([FromRoute] Guid userId,
		[FromQuery] string returnUrl,
		UserManage user)
	{


		if (ModelState.IsValid)
		{
			var dbUser = await _userSrv.ReadAsync(userId, false);
			if (user.Image is not null)
			{
				var fileBytes = new MemoryStream();
				await user.Image.CopyToAsync(fileBytes);
				var extension = Path.GetExtension(user.Image.FileName);
				//Save new photo file
				var imageName = $"{Guid.NewGuid()}{extension}";
				_fileSrv.SaveMemoryStreamToFile(fileBytes, "account", imageName);
				//Delete old photo file
				_fileSrv.DeleteFile(dbUser.PhotoPath);
				dbUser.PhotoPath = $"/account/{imageName}";
			}
			dbUser.UserName = user.UserName;
			dbUser.Email = user.Email;
			dbUser.Name = user.Name;
			dbUser.PhoneNumber = user.PhoneNumber;
			dbUser.TownId = user.TownId;
			dbUser.Role = user.Role;
			await _userSrv.UpdateAsync(dbUser);
			var backUrl = !String.IsNullOrWhiteSpace(returnUrl) ? returnUrl : "/admin/users";
			return LocalRedirect(backUrl);
		}
		else
		{
			ViewBag.Towns = await _townSrv.ReadAllAsync();
			var roles = new List<SelectListItem>();
			foreach (var item in Enum.GetValues<RoleEnum>())
			{
				roles.Add(new SelectListItem { Value = ((int)item).ToString(), Text = item.ToDescriptionString() });
			}
			ViewBag.Roles = roles;
			ViewBag.CancelUrl = !String.IsNullOrWhiteSpace(returnUrl) ? returnUrl : "/admin/users";
			// If there are validation errors, redisplay the form with error messages
			return View(user);
		}
	}


	public IActionResult Pets()
	{
		return View();
	}

	public IActionResult Requests()
	{
		return View();
	}

}
