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
        public DateTime CreatedOn { get; set; }

        public DateTime? DeniedOn { get; set; }
        public DateTime? CanceledOn { get; set; }
        public DateTime? AcceptedOn { get; set; }
        [Required]
        public Guid SenderId { get; set; }

        [ForeignKey("SenderId")]
        public User Sender { get; set; }

        [Required]
        public Guid RecipientId { get; set; }

        [ForeignKey("RecipientId")]
        public User Recipient { get; set; }

        [Required]
        public Guid PetId { get; set; }

        [ForeignKey("PetId")]
        public Pet Pet { get; set; }
        public string? RequestMessage { get; set; } = null;
        public string? AnswerMessage { get; set; } = null;

        [NotMapped]
        public bool IsActive { get => AcceptedOn is null && DeniedOn is null && CanceledOn is null; }

        public UserRequest() { }

		public UserRequest(Pet pet,DateTime? acceptedOn)
		{
			Id = Guid.NewGuid();
			Pet = pet;
			Pet.Id = pet.Id;
			AcceptedOn = acceptedOn;
		}
	}
}
