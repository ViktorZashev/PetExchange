using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WebPresentationLayer.Components;

public class PetFilters : ViewComponent
{
	public List<FilterGroup> Filters { get; set; }
	public async Task<IViewComponentResult> InvokeAsync(List<FilterGroup> filters)
	{
		Filters = filters;

        return View("PetFilters", this);
	}
}