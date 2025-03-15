using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebPresentationLayer.Models;

namespace WebPresentationLayer.Controllers
{
    public class PetsController : Controller
    {
        public IActionResult Index()
        {
            List<Pet> pets = new List<Pet>();
			for (int i = 1; i <= 12; i++)
			{
				pets.Add(new Pet{ 
                    Id = Guid.NewGuid(),
                    PhotoPath = $"/media/pet-{i}.webp",
                    Name = "Pet Name",
                    Gender = i%2 == 0 ? GenderEnum.Male : GenderEnum.Female
                });
			}
            ViewBag.Pets = pets;
            ViewBag.Filters = new FilterUtility().GetFilters(Request.GetDisplayUrl());
			return View();
        }

    }
}
