using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
	public class Country
	{
		[JsonPropertyName("id")]
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();

		[JsonPropertyName("name")]
		[Required]
		public string Name { get; set; } = string.Empty;

		private Country() { }

		public Country(string name)
		{
			Id = Guid.NewGuid();
			Name = name;
		}
	}
}
