using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [DisplayName("Възраст")]
        public int Age { get; set; } = 0;

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

        public Pet() { }

        public Pet(User user, string name, string photoPath, int age, PetTypeEnum petType, string description, bool includesCage)
        {
            Id = Guid.NewGuid();
            User = user;
            UserId = User.Id;
            Name = name;
            Age = age;
            PetType = petType;
            PhotoPath = photoPath;
            Description = description;
            IncludesCage = includesCage;
        }

	}
}
