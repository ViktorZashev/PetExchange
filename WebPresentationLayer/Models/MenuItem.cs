namespace WebPresentationLayer.Models;

/// <summary>
/// MenuItem е клас, дефиниращ характеристиките на навигационно меню
/// </summary>
public class MenuItem
{
	public string Title { get; set; } = String.Empty;
	public string? Area { get; set; } = null;
	public string? Page { get; set; } = null;
	public string? Controller { get; set; } = null;
	public string? Action { get; set; } = null;
	public bool IsActive { get; set; } = false;
	public bool IsDivider { get; set; } = false;

}

