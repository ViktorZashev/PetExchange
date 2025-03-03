using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public enum RoleEnum
    {
        [Description("Потребител")]
        User = 0,
        [Description("Администратор")]
        Admin = 1 
    }
}
