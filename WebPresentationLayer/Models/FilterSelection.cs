namespace WebPresentationLayer.Models;

public class FilterSelection
// Клас, който служи за определяне на подадените филтри за 
// филтрирането и страницирането на главната страница с домашните любимци
{
	public List<PetTypeEnum> Types { get; set; } = new();
	public List<GenderEnum> Gender { get; set; } = new();
	public List<PetAgeEnum> Age { get; set; } = new();
	public bool HasCage { get; set; } = false;
	public int Page { get; set; } = 1;
}
