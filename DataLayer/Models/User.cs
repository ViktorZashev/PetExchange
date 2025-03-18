using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer
{
	public class User : IdentityUser<Guid>
	{


		[Required(ErrorMessage = "задължително")]
		[DisplayName("Имейл")]
		public override string? Email { get; set; } = null;

		[Required(ErrorMessage = "задължително")]
		[DisplayName("Потребителско име")]
		public override string? UserName { get; set; } = null;

		[Required(ErrorMessage = "задължително")]
		[DisplayName("Телефон")]
		public override string? PhoneNumber { get; set; } = null;

		[Required(ErrorMessage = "задължително")]
		[DisplayName("Име")]
		public string Name { get; set; } = string.Empty;

		public string PhotoPath { get; set; } = string.Empty;

		[DisplayName("Активен")]
		[Required(ErrorMessage = "задължително")]
		public bool IsActive { get; set; } = true;

		[DisplayName("Роля")]
		[Required(ErrorMessage = "задължително")]
		public RoleEnum Role { get; set; } = RoleEnum.User;

		[DisplayName("Град")]
		[Required(ErrorMessage = "задължително")]
		public Guid TownId { get; set; }

		[ForeignKey("TownId")]

		public Town Town { get; set; }

		public List<Pet> Pets { get; set; } = new();

		public List<UserRequest> RequestOutbox { get; set; } = new();
		public List<UserRequest> RequestInbox { get; set; } = new();

		public User() { }

		public User(string username, string name, string photoPath, RoleEnum role, string phoneNumber, string email, Town town, List<Pet> pets)
		{
			Id = Guid.NewGuid();
			UserName = username;
			Name = name;
			PhotoPath = photoPath;
			Role = role;
			Email = email;
			PhoneNumber = phoneNumber;
			Town = town;
			TownId = Town.Id;
			Pets = pets;
		}
	}
}
