using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class UserRequests
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

        public UserRequests() { }

		public UserRequests(PublicOffer publicOffer,bool isAccepted)
		{
			Id = Guid.NewGuid();
			PublicOffer = publicOffer;
			PublicOfferId = PublicOffer.Id;
			IsAccepted = isAccepted;
		}
	}
}
