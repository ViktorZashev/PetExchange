using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Security.Claims;
using System.Web;
using WebPresentationLayer.Models;
using WebPresentationLayer.Services;

namespace WebPresentationLayer.Controllers;

[Authorize]
public class AccountController : Controller
{
	private readonly PetService _petService;
	private readonly UserService _userService;
	private readonly FileService _fileSrv;
	private readonly UserRequestsService _requestService;
	private readonly TownService _townSrv;
	private readonly UserManager<User> _userManager;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public AccountController(PetService petService, UserRequestsService requestService,
		UserManager<User> userManager, TownService townService,UserService userService,FileService _fileSrv, IHttpContextAccessor httpContextAccessor)
	{
		_petService = petService;
		_userManager = userManager;
		_httpContextAccessor = httpContextAccessor;
		_requestService = requestService;
		_townSrv = townService;
		_userService = userService;
		this._fileSrv = _fileSrv;
	}
    public async Task<IActionResult> Details()
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

        var user = new UserManage
        {
            Email = currentUser.Email,
            UserName = currentUser.UserName,
            PhoneNumber = currentUser.PhoneNumber,
            Name = currentUser.Name,
            PhotoPath = currentUser.PhotoPath,
            Role = currentUser.Role,
            isActive = currentUser.IsActive,
            TownId = currentUser.TownId,
        };
        var roles = new List<SelectListItem>();
        foreach (var item in Enum.GetValues<RoleEnum>())
        {
			roles.Add(new SelectListItem { Value = ((int)item).ToString(), Text = item.ToDescriptionString(), Selected = item == user.Role ? true : false });
        }
        ViewBag.Roles = roles;
        ViewBag.Towns = await _townSrv.ReadAllAsync();
        ViewBag.ShowEditSuccess = TempData["ShowEditSuccessfulMessage"];
        return View(user);
    }
	
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Details(
        UserManage user)
    {
		var httpContext = _httpContextAccessor.HttpContext;
		Guid userId;
        if (httpContext?.User is not null)
        {
           userId = new Guid(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
        else return Unauthorized();

        if (ModelState.IsValid)
        {
            var dbUser = await _userService.ReadAsync(userId, false);
            if (user.Image is not null)
            {
                var fileBytes = new MemoryStream();
                await user.Image.CopyToAsync(fileBytes);
                var extension = Path.GetExtension(user.Image.FileName);
                //Save new photo file
                var imageName = $"{Guid.NewGuid()}{extension}";
                _fileSrv.SaveMemoryStreamToFile(fileBytes, "account", imageName);
                //Delete old photo file
                if (!String.IsNullOrWhiteSpace(dbUser.PhotoPath))
                    _fileSrv.DeleteFile(dbUser.PhotoPath);

                dbUser.PhotoPath = $"/account/{imageName}";
            }
            dbUser.UserName = user.UserName;
            dbUser.Email = user.Email;
            dbUser.Name = user.Name;
            dbUser.PhoneNumber = user.PhoneNumber;
            dbUser.TownId = user.TownId;
            dbUser.IsActive = user.isActive;
            await _userService.UpdateAsync(dbUser);
            var backUrl = "/Account/Details";
            TempData["ShowEditSuccessfulMessage"] = true;
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
            ViewBag.CancelUrl = "/account/details";
            // If there are validation errors, redisplay the form with error messages
            return View(user);
        }
    }

    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(string NewPassword, string ConfirmPassword)
    {
        if (NewPassword != ConfirmPassword)
        {
            TempData["ChangePasswordError"] = "Паролите не са еднакви";
            return RedirectToAction("ChangePassword");
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, resetToken, NewPassword);

        if (result.Succeeded)
        {
            TempData["ChangePasswordSuccess"] = true;
            return RedirectToAction("ChangePassword");
        }

        TempData["ChangePasswordError"] = "Грешка при променянето на паролата. Опитай пак!";
        return RedirectToAction("ChangePassword");
    }

    public async Task<IActionResult> Pets(
      [FromQuery] string name = null,
      [FromQuery] string breed = null,
      [FromQuery] string type = null,
      [FromQuery] string gender = null,
      [FromQuery] int page = 1,
      [FromQuery] int pageSize = 10
  )
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

        ViewBag.petItems = await _petService.ReadAllWithFilterAsyncOfUser(currentUser.Id,
            name,
            breed,
            type,
            gender,
            page,
            pageSize,
            useNavigationalProperties: true,
            isReadOnly: true
        );
        ViewBag.petName = name;
        ViewBag.petBreed = breed;
        ViewBag.petType = type;
        ViewBag.petGender = gender;
        ViewBag.petPage = page;
        ViewBag.petPageSize = pageSize;
        ViewBag.GenderOptions = Enum.GetValues(typeof(GenderEnum))
                    .Cast<GenderEnum>()
                    .Select(rt => new SelectListItem
                    {
                        Value = rt.ToDescriptionString(),
                        Text = rt.ToDescriptionString(),
                        Selected = gender == rt.ToDescriptionString() ? true : false
                    })
                    .ToList();

        ViewBag.PetTypeOptions = Enum.GetValues(typeof(PetTypeEnum))
                    .Cast<PetTypeEnum>()
                    .Select(rt => new SelectListItem
                    {
                        Value = rt.ToDescriptionString(),
                        Text = rt.ToDescriptionString(),
                        Selected = type == rt.ToDescriptionString() ? true : false
                    })
                    .ToList();

        ViewBag.ReturnUrl = HttpUtility.UrlEncode(HttpContext.Request.Path + HttpContext.Request.QueryString);
        return View("Views/Account/Pets.cshtml");
    }

    [HttpGet("/account/pets/{petId:guid}")]
    public async Task<IActionResult> PetManage(
       [FromRoute] Guid petId,
       [FromQuery] string returnUrl)
    {
        ViewBag.CancelUrl = !String.IsNullOrWhiteSpace(returnUrl) ? returnUrl : "/account/pets";
        var dbPet = await _petService.ReadAsync(petId, true, true);
        var pet = new PetManage
        {
            Name = dbPet.Name,
            AddedOn = dbPet.AddedOn,
            AdoptedOn = dbPet.AdoptedOn,
            Birthday = dbPet.Birthday,
            Breed = dbPet.Breed,
            PhotoPath = dbPet.PhotoPath,
            isActive = dbPet.IsActive,
            PetType = dbPet.PetType,
            Gender = dbPet.Gender,
            Description = dbPet.Description,
            IncludesCage = dbPet.IncludesCage,
            UserRequests = dbPet.UserRequests
        };
        ViewBag.GenderOptions = Enum.GetValues(typeof(GenderEnum))
                    .Cast<GenderEnum>()
                    .Select(rt => new SelectListItem
                    {
                        Value = rt.ToString(),
                        Text = rt.ToDescriptionString(),
                        Selected = dbPet.Gender == rt ? true : false
                    })
                    .ToList();

        ViewBag.PetTypeOptions = Enum.GetValues(typeof(PetTypeEnum))
                    .Cast<PetTypeEnum>()
                    .Select(rt => new SelectListItem
                    {
                        Value = rt.ToString(),
                        Text = rt.ToDescriptionString(),
                        Selected = dbPet.PetType == rt ? true : false
                    })
                    .ToList();
        return View(pet);
    }

    [HttpPost("/account/pets/{petId:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PetManage([FromRoute] Guid petId,
        [FromQuery] string returnUrl,
        PetManage pet)
    {
        if (ModelState.IsValid)
        {
            var dbPet = await _petService.ReadAsync(petId, false);
            if (pet.Image is not null)
            {
                var fileBytes = new MemoryStream();
                await pet.Image.CopyToAsync(fileBytes);
                var extension = Path.GetExtension(pet.Image.FileName);
                //Save new photo file
                var imageName = $"{Guid.NewGuid()}{extension}";
                _fileSrv.SaveMemoryStreamToFile(fileBytes, "pet", imageName);
                //Delete old photo file
                if (!String.IsNullOrWhiteSpace(dbPet.PhotoPath))
                    _fileSrv.DeleteFile(dbPet.PhotoPath);

                dbPet.PhotoPath = $"/pet/{imageName}";
            }
            dbPet.Name = pet.Name;
            dbPet.AdoptedOn = pet.AdoptedOn;
            dbPet.Birthday = pet.Birthday;
            dbPet.Breed = pet.Breed;
            dbPet.IsActive = pet.isActive;
            dbPet.PetType = pet.PetType;
            dbPet.Gender = pet.Gender;
            dbPet.Description = pet.Description;
            dbPet.IncludesCage = pet.IncludesCage;
            dbPet.UserRequests = pet.UserRequests;
            await _petService.UpdateAsync(dbPet);
            var backUrl = !String.IsNullOrWhiteSpace(returnUrl) ? returnUrl : "/account/pets";
            return LocalRedirect(backUrl);
        }
        else
        {
            ViewBag.CancelUrl = !String.IsNullOrWhiteSpace(returnUrl) ? returnUrl : "/account/pets";
            return View(pet);
        }
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
