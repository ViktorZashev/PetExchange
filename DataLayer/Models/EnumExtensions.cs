using System.ComponentModel;
using System.Globalization;

namespace DataLayer
{
    public static class EnumExtensions
    {
        public static string ToDescriptionString<TEnum>(this TEnum e) where TEnum : IConvertible
        {
            string description = "";

            if (e is Enum)
            {
                Type type = e.GetType();
                var memInfo = type.GetMember(type.GetEnumName(e.ToInt32(CultureInfo.InvariantCulture)));
                var soAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (soAttributes.Length > 0)
                {
                    // Достъпваме само първото поле за описание
                    // Ще игнорираме всички други
                    description = ((DescriptionAttribute)soAttributes[0]).Description;
                }
            }

            return description;
        }
    }
}
