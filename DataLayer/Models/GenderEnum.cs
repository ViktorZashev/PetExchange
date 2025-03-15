using System.ComponentModel;

namespace DataLayer
{
    public enum GenderEnum
    {
        [Description("мъжки")]
        Male = 0,
        [Description("женски")]
        Female = 1,
        [Description("не се знае")]
        Other = 2,
    }
}
