using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebPresentationLayer.Models;

public class UserManage
{

	[Required(ErrorMessage = "задължително")]
	[DisplayName("Имейл")]
	public string? Email { get; set; } = null;

	[Required(ErrorMessage = "задължително")]
	[DisplayName("Потребителско име")]
	public string? UserName { get; set; } = null;

	[Required(ErrorMessage = "задължително")]
	[DisplayName("Телефон")]
	public string? PhoneNumber { get; set; } = null;

	[Required(ErrorMessage = "задължително")]
	[DisplayName("Име")]
	public string? Name { get; set; } = null;

	[DisplayName("Смени Снимка")]
	public IFormFile? Image { get; set; } = null;

	public string? PhotoPath { get; set; } = null;

    [DisplayName("Активност на профила")]
    public bool isActive { get; set; } = true;

    [DisplayName("Роля")]
	[Required(ErrorMessage = "задължително")]
	public RoleEnum Role { get; set; } = RoleEnum.User;

	[DisplayName("Град")]
	[Required(ErrorMessage = "задължително")]
	public Guid TownId { get; set; }


}

