using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GPdotNet.Core
{
    public static class Extensions
    {
        public static T ToEnum<T>(this string enumString)
        {
            return (T)Enum.Parse(typeof(T), enumString);
        }

        /// <summary>
        /// Convert string to boolean, in a forgiving way.
        /// </summary>
        /// <param name="stringVal">String that should either be "True", "False", "Yes", "No", "T", "F", "Y", "N", "1", "0"</param>
        /// <returns>If the trimmed string is any of the legal values that can be construed as "true", it returns true; False otherwise;</returns>
        public static bool ToBool(this string stringVal)
        {
            string normalizedString = (stringVal?.Trim() ?? "false").ToLowerInvariant();
            bool result = (normalizedString.StartsWith("y")
                || normalizedString.StartsWith("t")
                || normalizedString.StartsWith("1"));
            return result;
        }

        public static string Description(this Enum This)
        {
            Type type = This.GetType();

            string name = Enum.GetName(type, This);

            MemberInfo member = type.GetMembers()
                .Where(w => w.Name == name)
                .FirstOrDefault();

            DescriptionAttribute attribute = member != null
                ? member.GetCustomAttributes(true)
                    .Where(w => w.GetType() == typeof(DescriptionAttribute))
                    .FirstOrDefault() as DescriptionAttribute
                : null;

            return attribute != null ? attribute.Description : name;
        }

        public static (int skip, int size) CalculateMinibatch(int dataLength, int it, int miniBatch)
        {
            if (miniBatch <= 0)
                return (0, dataLength);
            //
            int length = dataLength;
            int skip = it * miniBatch / length;
            var val = ((double)length / (double)miniBatch);
            val = Math.Ceiling(val);
            int mod = (int)(it % val);
            if (it < val)
                mod = it;

            int size = miniBatch;
            if (mod * miniBatch + miniBatch > length)
                size = length - mod * miniBatch;

            return (mod * miniBatch, size);
        }
    }
}
