using RFECase.Domain;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace RFECase.Service.Utility
{
    public static class StringHelper
    {
        public static string ConvertToStringFrom(this string base64Input)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64Input);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes).Trim();
        }

        public static string ConvertToStringFrom(this EqualityStatus enumValue)
        {
            Type enumType = enumValue.GetType();
            MemberInfo[] memberInfo = enumType.GetMember(enumValue.ToString());
            if (memberInfo?.Length > 0)
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes?.Count() > 0)
                {
                    return ((DescriptionAttribute)attributes.ElementAt(0)).Description;
                }
            }
            return enumValue.ToString();
        }

    }
}
