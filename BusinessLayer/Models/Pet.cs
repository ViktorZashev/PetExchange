using System.Text.Json.Serialization;

namespace BusinessLayer.Models
{
    public class Pet
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

		[JsonPropertyName("user_id")]
		public Guid UserId { get; set; } = Guid.NewGuid();

		[JsonPropertyName("name")]
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
		public bool  IncludesCage{ get; set; } = false;
	}
}
