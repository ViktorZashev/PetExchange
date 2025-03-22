using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebPresentationLayer.Models
{
	public class ChangePasswordModel
	{
		[Required(ErrorMessage = "задължително")]
		[DisplayName("Нова парола")]
		public string NewPassword { get; set; } = string.Empty;

		[Required(ErrorMessage = "задължително")]
		[DisplayName("Потвърди парола")]
		[Compare("NewPassword", ErrorMessage = "паролите не съвпадат")]
		public string ConfirmPassword { get; set; } = string.Empty;

	}
}
