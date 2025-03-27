namespace WebPresentationLayer.Models;

// 2 класа, които дефинират функциите на филтрите и пазят
// информация как те ще се визуализират

public class FilterGroup
{
	public string Title { get; set; } = String.Empty;
	public string QueryName { get; set; } = String.Empty;
	public List<Filter> Options { get; set; } = new();
}

public class Filter
{
	public string Title { get; set; } = String.Empty;
	public string Value { get; set; } = String.Empty;
	public bool Selected { get; set; } = false;
}
