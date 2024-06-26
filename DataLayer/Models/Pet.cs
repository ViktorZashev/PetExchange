﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace BusinessLayer.Models
{
    public class Pet
    {
        [JsonPropertyName("id")]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        [JsonPropertyName("user")]
        public User User { get; set; }

        [JsonPropertyName("name")]
        [Required]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("photo_path")] // Only for Web View
        public string PhotoPath { get; set; } = string.Empty;

        [JsonPropertyName("age")]
        public int Age { get; set; } = 0;

        [JsonPropertyName("animal_type")]
        public string AnimalType { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("includes_cage")]
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
