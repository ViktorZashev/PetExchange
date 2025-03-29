using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Web;
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
		UserManager<User> userManager, TownService townService, UserService userService, FileService _fileSrv, IHttpContextAccessor httpContextAccessor)
	{
		_petService = petService;
		_userManager = userManager;
		_httpContextAccessor = httpContextAccessor;
		_requestService = requestService;
		_townSrv = townService;
		_userService = userService;
		this._fileSrv = _fileSrv;
	}
	#region Details & Change Password
	public async Task<IActionResult> Details()
	// Зареждат се данните на профила
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
	// Актуализират се данните на профила
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
				//Запазва новият файл
				var imageName = $"{Guid.NewGuid()}{extension}";
				_fileSrv.SaveMemoryStreamToFile(fileBytes, "account", imageName);
				//Изтрива старият файл
				if (!String.IsNullOrWhiteSpace(dbUser.PhotoPath) && !dbUser.PhotoPath.Contains("Seeded"))
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
			if (TempData is not null) TempData["ShowEditSuccessfulMessage"] = true;
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
			// Ако има грешки при валидация, презареди формата с валидационни съобщения
			return View(user);
		}
	}

	public IActionResult ChangePassword()
	// Зареждат се полетата за сменянето на паролата
	{
		var changePasswordForm = new ChangePasswordModel();
		return View(changePasswordForm);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordModel form)
	// Актуализация на парола
	{
		if (ModelState.IsValid)
		{
			if (form.NewPassword != form.ConfirmPassword)
			{
				if (TempData is not null) TempData["ChangePasswordError"] = "Паролите не са еднакви";
				return RedirectToAction("ChangePassword");
			}

			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return Unauthorized();
			}

			var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
			var result = await _userManager.ResetPasswordAsync(user, resetToken, form.NewPassword);

			if (result.Succeeded)
			{
				if (TempData is not null) TempData["ChangePasswordSuccess"] = true;
				return RedirectToAction("ChangePassword");
			}

			if (TempData is not null) TempData["ChangePasswordError"] = "Грешка при променянето на паролата. Опитай пак!";
			return RedirectToAction("ChangePassword");
		}
		else
		{
			return View(form);
		}
	}

	#endregion

	#region Pets
	public async Task<IActionResult> Pets(
	  [FromQuery] string name = null,
	  [FromQuery] string breed = null,
	  [FromQuery] string type = null,
	  [FromQuery] string gender = null,
	  [FromQuery] int page = 1,
	  [FromQuery] int pageSize = 10
  // Зареждане на домашните любимци на текущият потребител
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
		ViewBag.ShowEditSuccess = TempData is not null ? TempData["ShowEditSuccessfulMessage"] : false;
		ViewBag.ShowCreateSuccess = TempData is not null ? TempData["ShowCreateSuccessMessage"] : false;
		ViewBag.ShowDeleteSuccess = TempData is not null ? TempData["ShowPetDeleteSuccess"] : false;
		ViewBag.ReturnUrl = HttpUtility.UrlEncode(HttpContext.Request.Path + HttpContext.Request.QueryString);
		ViewBag.PrevPageUrl = ViewUtility.GeneratePageUrl(HttpContext, page - 1);
		ViewBag.NextPageUrl = ViewUtility.GeneratePageUrl(HttpContext, page + 1);
		return View("Views/Account/Pets.cshtml");
	}

	[HttpGet("/account/pets/{petId:guid}")]
	public async Task<IActionResult> PetManage(
	   [FromRoute] Guid petId,
	   [FromQuery] string returnUrl)
	// Зареждане на формата за редакция на домашен любимец
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
	//Актуализация на домашен любимец чрез форма
	{
		var dbPet = await _petService.ReadAsync(petId, false);
		if (ModelState.IsValid)
		{

			if (pet.Image is not null)
			{
				var fileBytes = new MemoryStream();
				await pet.Image.CopyToAsync(fileBytes);
				var extension = Path.GetExtension(pet.Image.FileName);
				//Запазва новият файл
				var imageName = $"{Guid.NewGuid()}{extension}";
				_fileSrv.SaveMemoryStreamToFile(fileBytes, "pet", imageName);
				//Изтрива старият файл
				if (!String.IsNullOrWhiteSpace(dbPet.PhotoPath) && !dbPet.PhotoPath.Contains("Seeded"))
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
			dbPet.IncludesCage = pet.IncludesCage;
			await _petService.UpdateAsync(dbPet);
			var backUrl = !String.IsNullOrWhiteSpace(returnUrl) ? returnUrl : "/account/pets";
			if (TempData is not null) TempData["ShowEditSuccessfulMessage"] = true;
			return LocalRedirect(backUrl);
		}
		else
		{
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
			ViewBag.CancelUrl = !String.IsNullOrWhiteSpace(returnUrl) ? returnUrl : "/account/pets";
			return View(pet);
		}
	}

	[HttpGet("Account/Pets/Create")]
	// Зареждане на форма за създаване на домашен любимец
	public async Task<IActionResult> PetCreate()
	{
		ViewBag.GenderOptions = Enum.GetValues(typeof(GenderEnum))
					.Cast<GenderEnum>()
					.Select(rt => new SelectListItem
					{
						Value = rt.ToString(),
						Text = rt.ToDescriptionString()
					})
					.ToList();

		ViewBag.PetTypeOptions = Enum.GetValues(typeof(PetTypeEnum))
					.Cast<PetTypeEnum>()
					.Select(rt => new SelectListItem
					{
						Value = rt.ToString(),
						Text = rt.ToDescriptionString()
					})
					.ToList();
		ViewBag.CancelUrl = "/account/pets";
		var petManage = new PetManage()
		{
			Birthday = DateTime.Now,
		};
		return View("PetCreate", petManage);
	}

	[HttpPost("/Account/Pets/Create")]
	[ValidateAntiForgeryToken]
	// Създаване на домашен любимец от форма
	public async Task<IActionResult> PetCreate(
		PetManage pet)
	{
		if (ModelState.IsValid)
		{
			var newPet = new Pet();
			if (pet.Image is not null)
			{
				var fileBytes = new MemoryStream();
				await pet.Image.CopyToAsync(fileBytes);
				var extension = Path.GetExtension(pet.Image.FileName);
				//Запазва новият файл
				var imageName = $"{Guid.NewGuid()}{extension}";
				_fileSrv.SaveMemoryStreamToFile(fileBytes, "pet", imageName);

				newPet.PhotoPath = $"/pet/{imageName}";
			}
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
			newPet.Name = pet.Name;
			newPet.AdoptedOn = null;
			newPet.AddedOn = DateTime.Now;
			newPet.Birthday = pet.Birthday;
			newPet.Breed = pet.Breed;
			newPet.IsActive = true;
			newPet.PetType = pet.PetType;
			newPet.Gender = pet.Gender;
			newPet.Description = pet.Description;
			newPet.IncludesCage = pet.IncludesCage;
			newPet.User = currentUser;
			await _petService.CreateAsync(newPet);
			var backUrl = "/account/pets";
			if (TempData is not null) TempData["ShowCreateSuccessMessage"] = true;
			return LocalRedirect(backUrl);
		}
		else
		{
			ViewBag.CancelUrl = "/account/pets";
			return View(pet);
		}
	}
	[HttpPost("/account/pets/delete/{petId:guid}")]
	[ValidateAntiForgeryToken]
	// Изтриване на домашен любимец по първичен ключ
	public async Task<IActionResult> DeletePet([FromRoute] Guid petId)
	{
		await _petService.DeleteAsync(petId);
		if (TempData is not null) TempData["ShowPetDeleteSuccess"] = true;
		var backUrl = "/account/pets";
		return LocalRedirect(backUrl);
	}
	#endregion

	#region RequestsInbox & Outbox
	public async Task<IActionResult> RequestInbox()
	// Зареждане на входните искания на потребителя
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
		ViewBag.ShowAcceptSuccess = TempData is not null ? TempData["ShowRequestAcceptSuccess"] : false;
		ViewBag.ShowDenySuccess = TempData is not null ? TempData["ShowRequestDenySuccess"] : false;
		return View();
	}

	public async Task<IActionResult> RequestOutbox()
	// Зареждане на изходните искания на потребителя
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
		ViewBag.ShowCreateSuccess = TempData is not null ? TempData["ShowRequestCreateSuccess"] : false;
		ViewBag.ShowCancelSuccess = TempData is not null ? TempData["ShowRequestCancelSuccess"] : false;
		return View();
	}

	#endregion

	#region Request Actions
	[HttpPost("/account/deny-request")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DenyRequest([FromForm] Guid requestId,
	[FromForm] UserRequestAction request)
	// Отказ на искане
	{
		if (ModelState.IsValid)
		{
			await _requestService.DenyAsync(requestId, request.Message);
			if (TempData is not null) TempData["ShowRequestDenySuccess"] = true;

		}
		return LocalRedirect("/account/RequestInbox");
	}

	[HttpPost("/account/accept-request")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> AcceptRequest([FromForm] Guid requestId,
	[FromForm] UserRequestAction request)
	// Приемане на искане
	{
		if (ModelState.IsValid)
		{
			await _requestService.AcceptAsync(requestId, request.Message);
			if (TempData is not null) TempData["ShowRequestAcceptSuccess"] = true;
		}
		return LocalRedirect("/account/RequestInbox");
	}


	[HttpPost("/account/cancel-request")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> CancelRequest([FromForm] Guid requestId)
	// Отхвърляне на искане
	{
		if (ModelState.IsValid)
		{
			await _requestService.CancelAsync(requestId);
			if (TempData is not null) TempData["ShowRequestCancelSuccess"] = true;
		}
		return LocalRedirect("/account/RequestOutbox");
	}

	[HttpGet("/account/create-request")]
	public async Task<IActionResult> CreateRequest([FromQuery] Guid petId)
	// Зареждане на формата за създаване на искане
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
		var request = new UserRequestAction() { PetId = petId, RequestId = null, Message = null };
		return View(request);
	}

	[HttpPost("/account/create-request")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> CreateRequest([FromQuery] Guid petId,
		[FromForm] UserRequestAction request)
	// Създаване на искане от форма
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
			if (TempData is not null) TempData["ShowRequestCreateSuccess"] = true;
			return LocalRedirect("/Account/RequestOutbox");
		}
		return View(request);
	}
	#endregion
}
