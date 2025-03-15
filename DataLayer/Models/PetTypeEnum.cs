using System.ComponentModel;

namespace DataLayer
{
    public enum PetTypeEnum
    {
        [Description("котки")]
        Cat = 0,
        [Description("кучета")]
        Dog = 1,
        [Description("риби")]
        Fish = 2,
        [Description("малки бозайници")]
        SmallMammal = 3,
        [Description("птици")]
        Bird = 4,
        [Description("влечуги")]
        Reptile = 5,
        [Description("земноводни")]
        Amphibian = 6,
        [Description("коне")]
        Horse = 7,
        [Description("други")]
        Other = 8,
    }
}
