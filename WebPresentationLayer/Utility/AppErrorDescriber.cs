using Microsoft.AspNetCore.Identity;

namespace WebPresentationLayer.Utility
{
    public class AppErrorDescriber : IdentityErrorDescriber
    {
        // Клас, който се ангажира с променянето на системните съобщения, които излизат
        // при грешки по графичния интерфейс
        public override IdentityError DuplicateUserName(string userName)
        { // Превежда съобщението, което излиза при избиране на потребителско име, което вече е регистрирано
            var error = base.DuplicateUserName(userName);
            error.Description = "Това потребилеско име е вече заето, изберете друго.";
            return error;
        }
    }
}
