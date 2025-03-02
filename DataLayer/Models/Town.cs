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
    public class Town
	{

		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();


		[Required]
		public string Name { get; set; } = string.Empty;

		public Town() { }

		public Town(string name)
		{
			Id = Guid.NewGuid();
			Name = name;
		}
	}
}
