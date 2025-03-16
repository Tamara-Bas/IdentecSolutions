using System.ComponentModel;

namespace IdentecSolutions.Application.Core.Common.Enum
{
    public static class Extensions
    {
        public static string GetDescription<T>(this T enumerationValue) where T: struct
        {
            Type type = enumerationValue.GetType();

            if (!type.IsEnum)
            {
                throw new ArgumentException("Enumeration value must be of Enum type");
            }

            var memberInfo = type.GetMember(enumerationValue.ToString()!);

            object[] attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Any() == true)
            {
                return ((DescriptionAttribute)attributes[0]).Description;
            }
            return enumerationValue.ToString();
        }
    }
}
