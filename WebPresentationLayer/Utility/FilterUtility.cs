namespace WebPresentationLayer.Utility;

public class FilterUtility
{
	public List<FilterGroup> GetFilters(HttpContext httpContext)
	{
		var result = new List<FilterGroup>();
		result.Add(new FilterGroup{ 
			Title = "Вид",
			QueryName = "type",
			Options = new List<Filter>{ 
				new Filter{
					Title = "кучета",
					Value = "dogs",
					Selected = true
				},
				new Filter{
					Title = "котки",
					Value = "cats",
					Selected = false
				},
				new Filter{
					Title = "птички",
					Value = "birds",
					Selected = false
				},
			}
		});
		result.Add(new FilterGroup{ 
			Title = "Пол",
			QueryName = "gender",
			Options = new List<Filter>{ 
				new Filter{
					Title = "женски",
					Value = "female",
					Selected = false
				},
				new Filter{
					Title = "мъжки",
					Value = "male",
					Selected = false
				},
			}
		});

		result.Add(new FilterGroup{ 
			Title = "Възраст",
			QueryName = "age",
			Options = new List<Filter>{ 
				new Filter{
					Title = "млади",
					Value = "young",
					Selected = false
				},
				new Filter{
					Title = "възрастни",
					Value = "adult",
					Selected = false
				},
			}
		});

		result.Add(new FilterGroup{ 
			Title = "Клетка",
			QueryName = "cage",
			Options = new List<Filter>{ 
				new Filter{
					Title = "влючена клетка",
					Value = "included",
					Selected = false
				},
			}
		});


		return result;
	}
}
