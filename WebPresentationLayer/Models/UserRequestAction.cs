using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer
{
    public class UserRequestAction
	{
		public Guid? RequestId { get; set; } = null;
		public Guid PetId { get; set; }

		[DisplayName("Съобщение")]
		[Required(ErrorMessage = "задължително")]
        public string? Message { get; set; }
	}
}
