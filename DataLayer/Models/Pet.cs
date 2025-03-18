using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer
{
	public class Pet
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();

		[Required]
		[DisplayName("Име")]
		public string Name { get; set; } = string.Empty;

		[Required]
		public string PhotoPath { get; set; } = string.Empty;

		[Required]
		[DisplayName("Добавен")]
		public DateTime AddedOn { get; set; }

		[DisplayName("Осиновен")]
		public DateTime? AdoptedOn { get; set; }

		[Required]
		[DisplayName("Порода")]
		public string Breed { get; set; } = string.Empty;

		[Required]
		[DisplayName("Роден на")]
		public DateTime Birthday { get; set; }

		public int AgeDays
		{
			get => (DateTime.Now - Birthday).Days;
		}

		public PetAgeEnum AgeEnum
		{
			get
			{
				if (AgeDays <= 90) return PetAgeEnum.Young;
				return PetAgeEnum.Adult;
			}
		}

		[Required]
		[DisplayName("Тип домашен любимец")]
		public PetTypeEnum PetType { get; set; } = PetTypeEnum.Other;

		[Required]
		[DisplayName("Пол")]
		public GenderEnum Gender { get; set; } = GenderEnum.Other;

		[Required]
		[DisplayName("Описание")]
		public string Description { get; set; } = string.Empty;

		[Required]
		[DisplayName("Включена клетка")]
		public bool IncludesCage { get; set; } = false;

		[Required]
		public Guid UserId { get; set; }

		[ForeignKey("UserId")]
		public User User { get; set; }

		[DisplayName("Активен")]
		[Required(ErrorMessage = "задължително")]
		public bool IsActive { get; set; } = true;

		[NotMapped]
		public Town Town
		{
			get
			{
				return User.Town;
			}
		}

		[NotMapped]
		public Guid TownId
		{
			get
			{
				return Town.Id;
			}
		}

		public List<UserRequest> UserRequests { get; set; } = new();

		public Pet() { }

		public Pet(User user, string name, string photoPath, int age, PetTypeEnum petType, string description, bool includesCage)
		{
			Id = Guid.NewGuid();
			User = user;
			UserId = User.Id;
			Name = name;
			Birthday = DateTime.Now.AddMonths(-3);
			PetType = petType;
			PhotoPath = photoPath;
			Description = description;
			IncludesCage = includesCage;
		}

	}
}
