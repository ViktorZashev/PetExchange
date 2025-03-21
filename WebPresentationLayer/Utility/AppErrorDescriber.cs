using Microsoft.AspNetCore.Identity;

namespace WebPresentationLayer.Utility
{
    public class AppErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            var error = base.DuplicateUserName(userName);
            error.Description = "Това потребилеско име е вече заето, изберете друго.";
            return error;
        }
    }
}
