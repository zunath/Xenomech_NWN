using System.Reflection;

namespace Xenomech.Core.Extensions
{
    internal static class ReflectionExtensions
    {
        public static string GetFullName(this MemberInfo member)
        {
            return member.DeclaringType != null ? $"{member.DeclaringType.FullName}.{member.Name}" : member.Name;
        }
    }
}
