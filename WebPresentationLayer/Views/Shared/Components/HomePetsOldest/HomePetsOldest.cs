namespace WebPresentationLayer.Components;
public class HomePetsOldest : ViewComponent
{
	public async Task<IViewComponentResult> InvokeAsync()
	{
		List<Pet> pets = new List<Pet>();
		for (int i = 1; i <= 4; i++)
		{
			pets.Add(new Pet
			{
				Id = Guid.NewGuid(),
				PhotoPath = $"/media/pet-{i}.webp",
				Name = "Pet Name",
				Gender = i % 2 == 0 ? GenderEnum.Male : GenderEnum.Female
			});
		}
		ViewBag.Pets = pets;
		return View("HomePetsOldest");
	}
}