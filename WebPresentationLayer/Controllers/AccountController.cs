using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebPresentationLayer.Models;

namespace WebPresentationLayer.Controllers;

[Authorize]
public class AccountController : Controller
{
	private readonly PetService _petService;
	private readonly UserRequestsService _requestService;
	private readonly UserManager<User> _userManager;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public AccountController(PetService petService, UserRequestsService requestService,
		UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
	{
		_petService = petService;
		_userManager = userManager;
		_httpContextAccessor = httpContextAccessor;
		_requestService = requestService;
	}
	public IActionResult Details()
	{
		return View();
	}

	public IActionResult ChangePassword()
	{
		return View();
	}

	public IActionResult Pets()
	{
		return View();
	}

	public async Task<IActionResult> RequestInbox()
	{
		User? currentUser = null;
		var httpContext = _httpContextAccessor.HttpContext;
		if (httpContext?.User is not null)
		{
			var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId is not null)
			{
				currentUser = await _userManager.FindByIdAsync(userId);
			}
		}
		if (currentUser is null) return Unauthorized();

		ViewBag.CurrentUser = currentUser;
		ViewBag.Requests = await _requestService.ReadUserRequestInboxAsync(currentUser.Id);
		ViewBag.ShowAcceptSuccess = TempData["ShowRequestAcceptSuccess"];
		ViewBag.ShowDenySuccess = TempData["ShowRequestDenySuccess"];
		return View();
	}

	public async Task<IActionResult> RequestOutbox()
	{
		User? currentUser = null;
		var httpContext = _httpContextAccessor.HttpContext;
		if (httpContext?.User is not null)
		{
			var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId is not null)
			{
				currentUser = await _userManager.FindByIdAsync(userId);
			}
		}
		if (currentUser is null) return Unauthorized();
		ViewBag.CurrentUser = currentUser;
		ViewBag.Requests = await _requestService.ReadUserRequestOutboxAsync(currentUser.Id);
		ViewBag.ShowCreateSuccess = TempData["ShowRequestCreateSuccess"];
		ViewBag.ShowCancelSuccess = TempData["ShowRequestCancelSuccess"];
		return View();
	}

	[HttpPost("/account/deny-request")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DenyRequest([FromForm] Guid requestId,
	[FromForm] UserRequestAction request)
	{
		if (ModelState.IsValid)
		{
			await _requestService.DenyAsync(requestId, request.Message);
		}
		TempData["ShowRequestDenySuccess"] = true;
		return LocalRedirect("/account/RequestInbox");
	}

	[HttpPost("/account/accept-request")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> AcceptRequest([FromForm] Guid requestId,
	[FromForm] UserRequestAction request)
	{
		if (ModelState.IsValid)
		{
			await _requestService.AcceptAsync(requestId, request.Message);
		}
		TempData["ShowRequestAcceptSuccess"] = true;
		return LocalRedirect("/account/RequestInbox");
	}


	[HttpPost("/account/cancel-request")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> CancelRequest([FromForm] Guid requestId)
	{
		if (ModelState.IsValid)
		{
			await _requestService.CancelAsync(requestId);
		}
		TempData["ShowRequestCancelSuccess"] = true;
		return LocalRedirect("/account/RequestOutbox");
	}

	[HttpGet("/account/create-request")]
	public async Task<IActionResult> CreateRequest([FromQuery] Guid petId)
	{
		var pet = await _petService.ReadAsync(petId, useNavigationalProperties: true);
		if (pet is null) return NotFound();
		ViewBag.Pet = pet;

		User? currentUser = null;
		var httpContext = _httpContextAccessor.HttpContext;
		if (httpContext?.User is not null)
		{
			var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId is not null)
			{
				currentUser = await _userManager.FindByIdAsync(userId);
			}
		}
		if (currentUser is null) return Unauthorized();
		ViewBag.CurrentUser = currentUser;
		var request = new UserRequestAction() { PetId = petId, RequestId = null, Message = null};
		return View(request);
	}

	[HttpPost("/account/create-request")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> CreateRequest([FromQuery] Guid petId,
		[FromForm] UserRequestAction request)
	{
		var pet = await _petService.ReadAsync(petId, useNavigationalProperties: true);
		if (pet is null) return NotFound();
		ViewBag.Pet = pet;

		User? currentUser = null;
		var httpContext = _httpContextAccessor.HttpContext;
		if (httpContext?.User is not null)
		{
			var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId is not null)
			{
				currentUser = await _userManager.FindByIdAsync(userId);
			}
		}
		if (currentUser is null) return Unauthorized();
		ViewBag.CurrentUser = currentUser;
		if (ModelState.IsValid)
		{
			var userReq = new UserRequest
			{
				Id = Guid.NewGuid(),
				CreatedOn = DateTime.Now,
				AnswerMessage = null,
				DeniedOn = null,
				AcceptedOn = null,
				PetId = petId,
				RequestMessage = request.Message,
				SenderId = currentUser.Id,
				RecipientId = pet.UserId
			};
			await _requestService.CreateAsync(userReq);
			TempData["ShowRequestCreateSuccess"] = true;
			return LocalRedirect("/Account/RequestOutbox");
		}
		return View(request);
	}
}
