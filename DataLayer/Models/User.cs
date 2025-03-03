using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
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
    public class User : IdentityUser<Guid>
	{
        /* Fields from Identity User
		Guid Id,

		public virtual string? UserName { get; set; }

		public virtual string? Email { get; set; }

		public virtual string? PhoneNumber { get; set; }
		*/
        [Required]
		[DisplayName("Име")]
        public string Name { get; set; } = string.Empty;

        public string PhotoPath { get; set; } = string.Empty;

		[DisplayName("Роля")]
        [Required]
		public RoleEnum Role { get; set; } = RoleEnum.User;

        [Required]
        public Guid TownId { get; set; }

        [ForeignKey("TownId")]
        public Town Town { get; set; }

        public List<Pet> Pets { get; set; }

        public List<UserRequests> Requests { get; set; }

        public List<PublicOffer> PublicOffers { get; set; }

        public User() { }

        public User(string username, string name, string photoPath, RoleEnum role, string phoneNumber,string email, Town town, List<Pet> pets)
		{
			Id = Guid.NewGuid();
            UserName = username;
            Name = name;
			PhotoPath = photoPath;
			Role = role;
            Email = email;
			PhoneNumber = phoneNumber;
            Town = town;
            TownId = Town.Id;
            Pets = pets;
        }
    }
}
