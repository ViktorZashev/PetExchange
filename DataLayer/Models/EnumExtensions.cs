using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
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
                    // we're only getting the first description we find
                    // others will be ignored
                    description = ((DescriptionAttribute)soAttributes[0]).Description;
                }
            }

            return description;
        }
    }
}
