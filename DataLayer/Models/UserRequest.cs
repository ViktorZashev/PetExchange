using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer
{
    public class UserRequest
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();

		[Required]
		[DisplayName("Потвърден")]
		public bool IsAccepted { get; set; } = false;

        [Required]
        public Guid PublicOfferId { get; set; }

        [ForeignKey("PublicOfferId")]
        public PublicOffer PublicOffer { get; set; }

        public UserRequest() { }

		public UserRequest(PublicOffer publicOffer,bool isAccepted)
		{
			Id = Guid.NewGuid();
			PublicOffer = publicOffer;
			PublicOfferId = PublicOffer.Id;
			IsAccepted = isAccepted;
		}
	}
}
