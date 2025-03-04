using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer
{
    public class PublicOffer
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid PetId { get; set; }

        [ForeignKey("PetId")]
        public Pet Pet { get; set; }

        public List<UserRequest>? Requests { get; set; }

        [NotMapped]
        public Town Town
        {
            get
            {
                return Pet.User.Town;
            }
        }

        [NotMapped]
        public Guid TownId 
        { 
           get 
           { 
                return Town.Id; 
           } 
        }

        public PublicOffer() { }

        public PublicOffer(Pet pet)
        {
            Id = Guid.NewGuid();
            Pet = pet;
            PetId = Pet.Id;
        }
    }
}
