namespace EyeSoft.Reflection
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Extensions;

    public static class PropertyInfoExtensions
    {
        public static bool IsAutomaticProperty(this PropertyInfo propertyInfo)
        {
            return
                propertyInfo.GetBackingField().IsNotNull();
        }

        public static FieldInfo GetBackingField(this PropertyInfo propertyInfo)
        {
            var field =
                GetCSharpBackingField(propertyInfo);

            if (field.IsNull())
            {
                field =
                    GetVbBackingField(propertyInfo);
            }

            if (field.IsNull())
            {
                return null;
            }

            return
                field.GetAttribute<CompilerGeneratedAttribute>().IsNull() ?
                    null :
                    field;
        }

        public static bool IsPrivateGet(this PropertyInfo propertyInfo)
        {
            return
                propertyInfo.CanRead &&
                propertyInfo.GetGetMethod().IsNull();
        }

        public static bool IsPrivateSet(this PropertyInfo propertyInfo)
        {
            return
                propertyInfo.CanWrite &&
                propertyInfo.GetSetMethod().IsNull();
        }

        public static bool IsPrivate(this PropertyInfo propertyInfo)
        {
            return CheckScope(propertyInfo, m => m.IsPrivate);
        }

        public static bool IsPublic(this PropertyInfo propertyInfo)
        {
            return CheckScope(propertyInfo, m => m.IsPublic);
        }

        public static Expression<Func<TModel, T>> MemberExpression<TModel, T>(string propertyName)
        {
            var propertyInfo = typeof(TModel).GetProperty(propertyName);

            return propertyInfo.MemberExpression<TModel, T>();
        }

        public static Expression<Func<TModel, T>> MemberExpression<TModel, T>(this PropertyInfo propertyInfo)
        {
            var entityParam = Expression.Parameter(typeof(TModel), "x");
            Expression columnExpr = Expression.Property(entityParam, propertyInfo);

            if (propertyInfo.PropertyType != typeof(T))
            {
                columnExpr = Expression.Convert(columnExpr, typeof(T));
            }

            return Expression.Lambda<Func<TModel, T>>(columnExpr, entityParam);
        }

        private static bool CheckScope(PropertyInfo propertyInfo, Func<MethodInfo, bool> scopeCheck)
        {
            var getMethod = propertyInfo.GetGetMethod(true);
            var setMethod = propertyInfo.GetSetMethod(true);

            if ((getMethod != null) && scopeCheck(getMethod))
            {
                return true;
            }

            if ((setMethod != null) && scopeCheck(setMethod))
            {
                return true;
            }

            return false;
        }

        private static FieldInfo GetCSharpBackingField(PropertyInfo propertyInfo)
        {
            return
                GetBackingField(propertyInfo, "<{PropertyName}>k__BackingField");
        }

        private static FieldInfo GetVbBackingField(PropertyInfo propertyInfo)
        {
            return
                GetBackingField(propertyInfo, "_{PropertyName}");
        }

        private static FieldInfo GetBackingField(PropertyInfo propertyInfo, string fieldNameFormat)
        {
            var fieldName = string.Format(fieldNameFormat, propertyInfo.Name);

            var field =
                propertyInfo
                    .DeclaringType
                    .GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);

            return field;
        }
    }
}