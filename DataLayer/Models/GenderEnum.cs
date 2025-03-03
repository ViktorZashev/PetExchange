using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public enum GenderEnum
    {
        [Description("Друг")]
        Other = 0,
        [Description("Мъжки")]
        Male = 1,
        [Description("Женски")]
        Female = 2
    }
}
