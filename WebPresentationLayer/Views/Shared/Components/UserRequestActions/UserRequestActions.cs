namespace WebPresentationLayer.Components;
public class UserRequestActions : ViewComponent
{
	public async Task<IViewComponentResult> InvokeAsync(UserRequest item, User currentUser)
	{
		ViewBag.Item = item;
		ViewBag.CurrentUser = currentUser;
		var requestActionModel = new UserRequestAction{ 
			Message = null,
			PetId = item.PetId,
			RequestId = item.Id
		};
		return View("UserRequestActions",requestActionModel);
	}
}