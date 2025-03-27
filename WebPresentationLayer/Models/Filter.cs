namespace WebPresentationLayer.Models;

public class FilterGroup
{
	public string Title { get; set; } = String.Empty;
	public string QueryName { get; set; } = String.Empty;
	public List<Filter> Options { get; set; } = new();
	/*public void MarkSelected(HttpContent content){ 
			
	}*/
}

public class Filter
{
	public string Title { get; set; } = String.Empty;
	public string Value { get; set; } = String.Empty;
	public bool Selected { get; set; } = false;
}
