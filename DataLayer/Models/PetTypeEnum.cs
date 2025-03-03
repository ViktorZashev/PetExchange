using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public enum PetTypeEnum
    {
        [Description("Друго")]
        Other = 0,
        [Description("Котка")]
        Cat = 1,
        [Description("Куче")]
        Dog = 2,
        [Description("Риба")]
        Fish = 3,
        [Description("Малък бозайник")]
        SmallMammal = 4,
        [Description("Птица")]
        Bird = 5,
        [Description("Влечуго")]
        Reptile = 6,
        [Description("Земноводно")]
        Amphibian = 7,
        [Description("Кон")]
        Horse = 8
    }
}
