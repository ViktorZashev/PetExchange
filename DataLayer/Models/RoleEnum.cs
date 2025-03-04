using System.ComponentModel;

namespace DataLayer
{
    public enum RoleEnum
    {
        [Description("Потребител")]
        User = 0,
        [Description("Администратор")]
        Admin = 1 
    }
}
