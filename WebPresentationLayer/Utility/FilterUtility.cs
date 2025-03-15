using System.Web;

namespace WebPresentationLayer.Utility;

public class FilterUtility
{
	public static string typeQueryName = "type";
	public static string genderQueryName = "gender";
	public static string ageQueryName = "age";
	public static string cageQueryName = "cage";
	public List<FilterGroup> GetFilters(string url)
	{
		var result = new List<FilterGroup>();
		var urlFilters = GetUrlFilters(url);
		//type
		var typeFilter = new FilterGroup
		{
			Title = "Вид",
			QueryName = typeQueryName
		};
		foreach (var item in Enum.GetValues<PetTypeEnum>())
		{
			typeFilter.Options.Add(new Filter
			{
				Title = item.ToDescriptionString(),
				Value = ((int)item).ToString(),
				Selected = urlFilters.Types.Contains(item)
			});
		}
		result.Add(typeFilter);

		//gender
		var genderFilter = new FilterGroup
		{
			Title = "Пол",
			QueryName = genderQueryName,
		};
		foreach (var item in Enum.GetValues<GenderEnum>())
		{
			genderFilter.Options.Add(new Filter
			{
				Title = item.ToDescriptionString(),
				Value = ((int)item).ToString(),
				Selected = urlFilters.Gender.Contains(item)
			});
		}
		result.Add(genderFilter);

		var ageFilter = new FilterGroup
		{
			Title = "Възраст",
			QueryName = ageQueryName
		};
		foreach (var item in Enum.GetValues<PetAgeEnum>())
		{
			ageFilter.Options.Add(new Filter
			{
				Title = item.ToDescriptionString(),
				Value = ((int)item).ToString(),
				Selected = urlFilters.Age.Contains(item)
			});
		}
		result.Add(ageFilter);

		result.Add(new FilterGroup
		{
			Title = "Клетка",
			QueryName = cageQueryName,
			Options = new List<Filter>{
				new Filter{
					Title = "с клетка",
					Value = "true",
					Selected = urlFilters.HasCage
				},
			}
		});


		return result;
	}


	public FilterSelection GetUrlFilters(string url)
	{
		var result = new FilterSelection();
		var uri = new Uri(url);
		if (String.IsNullOrWhiteSpace(uri.Query)) return result;
		var queryDict = HttpUtility.ParseQueryString(uri.Query);
		if (queryDict[typeQueryName] is not null)
		{
			var csvList = HttpUtility.UrlDecode(queryDict[typeQueryName]);
			if (!String.IsNullOrWhiteSpace(csvList))
			{
				var valueStringList = csvList.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
				foreach (var value in valueStringList)
				{
					if (Enum.TryParse<PetTypeEnum>(value, true, out PetTypeEnum outEnum))
					{
						if (!result.Types.Contains(outEnum))
							result.Types.Add(outEnum);
					}
				}
			}
		}
		if (queryDict[genderQueryName] is not null)
		{
			var csvList = HttpUtility.UrlDecode(queryDict[genderQueryName]);
			if (!String.IsNullOrWhiteSpace(csvList))
			{
				var valueStringList = csvList.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
				foreach (var value in valueStringList)
				{
					if (Enum.TryParse<GenderEnum>(value, true, out GenderEnum outEnum))
					{
						if (!result.Gender.Contains(outEnum))
							result.Gender.Add(outEnum);
					}
				}
			}
		}
		if (queryDict[ageQueryName] is not null)
		{
			var csvList = HttpUtility.UrlDecode(queryDict[ageQueryName]);
			if (!String.IsNullOrWhiteSpace(csvList))
			{
				var valueStringList = csvList.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
				foreach (var value in valueStringList)
				{
					if (Enum.TryParse<PetAgeEnum>(value, true, out PetAgeEnum outEnum))
					{
						if (!result.Age.Contains(outEnum))
							result.Age.Add(outEnum);
					}
				}
			}
		}
		if (queryDict[cageQueryName] is not null)
		{
			var value = HttpUtility.UrlDecode(queryDict[cageQueryName]);
			if (!String.IsNullOrWhiteSpace(value)
				&& bool.TryParse(value, out bool outBool))
			{
				result.HasCage = outBool;
			}
		}
		return result;
	}

}
