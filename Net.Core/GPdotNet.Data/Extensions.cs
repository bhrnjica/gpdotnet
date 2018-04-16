using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GPdotNet.Data
{
    public static class Extensions
    {
        
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
    }
}
