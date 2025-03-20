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
	private readonly UserRequestsService _requestSrv;
	private readonly PetService _petSrv;
	private readonly TownService _townSrv;
	private readonly FileService _fileSrv;

	public AdminController(UserService userService, PetService petService,
		TownService townService,UserRequestsService requestService, FileService fileService)
	{
		_userSrv = userService;
		_townSrv = townService;
		_fileSrv = fileService;
		_petSrv = petService;
        _requestSrv = requestService;

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
			page:page,
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
			isActive = dbUser.IsActive,
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
				if(!String.IsNullOrWhiteSpace(dbUser.PhotoPath))
					_fileSrv.DeleteFile(dbUser.PhotoPath);

				dbUser.PhotoPath = $"/account/{imageName}";
			}
			dbUser.UserName = user.UserName;
			dbUser.Email = user.Email;
			dbUser.Name = user.Name;
			dbUser.PhoneNumber = user.PhoneNumber;
			dbUser.TownId = user.TownId;
			dbUser.Role = user.Role;
			dbUser.IsActive = user.isActive;
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

	// Домашни любимци
    public async Task<IActionResult> Pets(
        [FromQuery] string name = null,
		[FromQuery] string breed = null,
        [FromQuery] string type = null,
        [FromQuery] string gender = null,
        [FromQuery] string ownerName = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
    )
    {
        ViewBag.petItems = await _petSrv.ReadAllWithFilterAsync(
            name,
			breed,
            type,
            gender,
            ownerName,
            page,
            pageSize,
            useNavigationalProperties: true,
            isReadOnly: true
        );
        ViewBag.petName = name;
        ViewBag.petBreed = breed;
        ViewBag.petType = type;
        ViewBag.petGender = gender;
        ViewBag.petOwnerName = ownerName;
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
        return View();
    }

    [HttpGet("/admin/pets/{petId:guid}")]
    public async Task<IActionResult> PetManage(
        [FromRoute] Guid petId,
        [FromQuery] string returnUrl)
    {
        ViewBag.CancelUrl = !String.IsNullOrWhiteSpace(returnUrl) ? returnUrl : "/admin/pets";
        var dbPet = await _petSrv.ReadAsync(petId, true, true);
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

    [HttpPost("/admin/pets/{petId:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PetManage([FromRoute] Guid petId,
        [FromQuery] string returnUrl,
        PetManage pet)
    {
        if (ModelState.IsValid)
        {
            var dbPet = await _petSrv.ReadAsync(petId, false);
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
            dbPet.AddedOn = pet.AddedOn;
            dbPet.AdoptedOn = pet.AdoptedOn;
            dbPet.Birthday = pet.Birthday;
            dbPet.Breed = pet.Breed;
            dbPet.IsActive = pet.isActive;
            dbPet.PetType = pet.PetType;
            dbPet.Gender = pet.Gender;
            dbPet.Description = pet.Description;
            dbPet.IncludesCage = pet.IncludesCage;
            dbPet.UserRequests = pet.UserRequests;
            await _petSrv.UpdateAsync(dbPet);
            var backUrl = !String.IsNullOrWhiteSpace(returnUrl) ? returnUrl : "/admin/pets";
            return LocalRedirect(backUrl);
        }
        else
        {
            ViewBag.CancelUrl = !String.IsNullOrWhiteSpace(returnUrl) ? returnUrl : "/admin/pets";
            return View(pet);
        }
    }
    // искания

    public async Task<IActionResult> Requests(
        [FromQuery] string petName = null,
        [FromQuery] string petBreed = null,
        [FromQuery] string senderName = null,
        [FromQuery] string receiverName = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
    )
    {
        ViewBag.requestItems = await _requestSrv.ReadAllWithFilterAsync(
            petName,
            petBreed,
            senderName,
            receiverName,
            page,
            pageSize,
            useNavigationalProperties: true,
            isReadOnly: true
        );
        ViewBag.petName = petName;
        ViewBag.petBreed = petBreed;
        ViewBag.senderName = senderName;
        ViewBag.receiverName = receiverName;
        ViewBag.page = page;
        ViewBag.pageSize = pageSize;
        ViewBag.ReturnUrl = HttpUtility.UrlEncode(HttpContext.Request.Path + HttpContext.Request.QueryString);
        return View();
    }
}
