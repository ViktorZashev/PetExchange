using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer
{
    public class UserRequestAction
	// Служи за определянето на какви дани ще се визуализират
	// при изпращането на заявка към домашен любимец
	{
		public Guid? RequestId { get; set; } = null;
		public Guid PetId { get; set; }

		[DisplayName("Съобщение")]
        public string? Message { get; set; }
	}
}
