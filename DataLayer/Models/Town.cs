using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer
{
    public class Town
	{
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
		public Town(Guid id, string name)
		{
			Id = id;
			Name = name;
		}
	}
}
