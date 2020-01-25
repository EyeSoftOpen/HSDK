namespace EyeSoft.Reflection
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    using EyeSoft.Extensions;

    public static class MemberInfoExtensions
    {
        public static bool IsDelegateField(this MemberInfo memberInfo)
        {
            var fieldInfo = memberInfo as FieldInfo;

            return
                fieldInfo != null &&
                fieldInfo.FieldType.EqualsOrSubclassOf<Delegate>();
        }

        public static bool IsCompilerGenerated(this MemberInfo memberInfo)
        {
            return
                memberInfo.GetAttribute<CompilerGeneratedAttribute>().IsNotNull();
        }

        public static T GetAttribute<T>(this MemberInfo memberInfo, bool inherit = true)
        {
            return
                memberInfo
                    .GetCustomAttributes(typeof(T), inherit)
                    .SingleOrDefault()
                    .Convert<T>();
        }

        public static object GetValue(this MemberInfo memberInfo, object instance)
        {
            var propertyInfo = memberInfo as PropertyInfo;

            if (propertyInfo != null)
            {
                if (instance.GetType() == propertyInfo.DeclaringType)
                {
                    return propertyInfo.GetValue(instance, null);
                }

                propertyInfo =
                    propertyInfo
                        .DeclaringType
                        .GetProperty(propertyInfo.Name, propertyInfo.PropertyType);

                return propertyInfo.GetValue(instance, null);
            }

            var fieldInfo = memberInfo as FieldInfo;

            if (fieldInfo != null)
            {
                if (instance.GetType() == fieldInfo.DeclaringType)
                {
                    return fieldInfo.GetValue(instance);
                }

                fieldInfo =
                    fieldInfo
                        .DeclaringType
                        .GetField(fieldInfo.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                return fieldInfo.GetValue(instance);
            }

            throw new ArgumentException("The member must be a FieldInfo or a PropertyInfo.");
        }

        public static void SetValue<T>(this MemberInfo memberInfo, object instance, T value)
        {
            var propertyInfo = memberInfo as PropertyInfo;

            if (propertyInfo != null)
            {
                if (instance.GetType() == propertyInfo.DeclaringType)
                {
                    propertyInfo.SetValue(instance, value, null);
                    return;
                }

                propertyInfo =
                    propertyInfo
                        .DeclaringType
                        .GetProperty(propertyInfo.Name, propertyInfo.PropertyType);

                propertyInfo.SetValue(instance, value, null);

                return;
            }

            var fieldInfo = memberInfo as FieldInfo;

            if (fieldInfo != null)
            {
                if (instance.GetType() == fieldInfo.DeclaringType)
                {
                    fieldInfo.SetValue(instance, value);
                    return;
                }

                fieldInfo =
                    fieldInfo
                        .DeclaringType
                        .GetField(fieldInfo.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                fieldInfo.SetValue(instance, value);

                return;
            }

            throw new ArgumentException("The member must be a FieldInfo or a PropertyInfo.");
        }
    }
}