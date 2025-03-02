using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace BusinessLayer.Models
{
    public class Pet
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;


        public string PhotoPath { get; set; } = string.Empty;

        public int Age { get; set; } = 0;


        public string AnimalType { get; set; } = string.Empty;


        public string Description { get; set; } = string.Empty;


        public bool IncludesCage { get; set; } = false;

        public Pet() { }

        public Pet(User user, string name, string photoPath, int age, string animalType, string description, bool includesCage)
        {
            Id = Guid.NewGuid();
            User = user;
            UserId = user.Id;
            Name = name;
            PhotoPath = photoPath;
            Age = age;
            AnimalType = animalType;
            Description = description;
            IncludesCage = includesCage;
        }

        public Pet(Guid id, User user, string name, string photoPath, int age, string animalType, string description, bool includesCage)
            : this(user, name, photoPath, age, animalType, description, includesCage)
        {
            Id = id;
        }

        public Pet(Guid guid, User user)
        {
            Id = guid;
            User = user;
        }

        public Pet(string name, User user)
        {
            Name = name;
            User = user;
        }
	}
}
