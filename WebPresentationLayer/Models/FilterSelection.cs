namespace WebPresentationLayer.Models;

public class FilterSelection
{
	public List<PetTypeEnum> Types { get; set; } = new();
	public List<GenderEnum> Gender { get; set; } = new();
	public List<PetAgeEnum> Age { get; set; } = new();
	public bool HasCage { get; set; } = false;
}
