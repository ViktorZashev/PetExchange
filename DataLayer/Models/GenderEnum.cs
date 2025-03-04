using System.ComponentModel;

namespace DataLayer
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
