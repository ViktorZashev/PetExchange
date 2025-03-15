using System.ComponentModel;

namespace DataLayer
{
    public enum PetAgeEnum
    {
        [Description("млад")]
        Young = 0,
        [Description("възрастен")]
        Adult = 1,
    }
}
