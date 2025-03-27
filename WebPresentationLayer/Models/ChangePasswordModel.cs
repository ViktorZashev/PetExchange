using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebPresentationLayer.Models
{
	public class ChangePasswordModel
	// Модел, служещ за дефиниране и валидация на данни при смяната на парола
	{
		[Required(ErrorMessage = "задължително")]
		[MinLength(6, ErrorMessage = "паролата трябва да съдържа минимум 6 символа")]
		[DisplayName("Нова парола")]
		public string NewPassword { get; set; } = string.Empty;

		[Required(ErrorMessage = "задължително")]
		[DisplayName("Потвърди парола")]
		[Compare("NewPassword", ErrorMessage = "паролите не съвпадат")]
		public string ConfirmPassword { get; set; } = string.Empty;

	}
}
