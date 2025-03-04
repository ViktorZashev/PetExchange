using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Town
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();

		[Required]
		[DisplayName("Име")]
		public string Name { get; set; } = string.Empty;
		
		public Town() { }
		public Town(string name)
		{
			Id = Guid.NewGuid();
			Name = name;
		}
	}
}
