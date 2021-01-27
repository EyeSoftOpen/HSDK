namespace EyeSoft.Core.Reflection
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Extensions;

    public static class Reflector
    {
        public const BindingFlags InstanceBindingFlags =
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        public static string PropertyName<T>(this Expression<Func<T, object>> expression)
        {
            return new TypeReflector<T>().Property(expression).Name;
        }

        public static string PropertyName<T, TProperty>(this Expression<Func<T, TProperty>> expression)
        {
            return new TypeReflector<T>().Property(expression).Name;
        }

        public static string PropertyName<TProperty>(this Expression<Func<TProperty>> expression)
        {
            return Property(expression).Name;
        }

        public static PropertyInfo Property<TProperty>(this Expression<Func<TProperty>> expression)
        {
            return new TypeReflector().Property(expression);
        }

        public static string PropertyName(this Expression<Func<object>> expression)
        {
            return Property(expression).Name;
        }

        public static PropertyInfo Property<T>(this Expression<Func<T, object>> expression)
        {
            return new TypeReflector<T>().Property(expression);
        }

        public static PropertyInfo Property(MemberExpression memberExpression)
        {
            return new TypeReflector().Property(memberExpression);
        }

        public static PropertyInfo Property<T, TProperty>(this Expression<Func<T, TProperty>> expression)
        {
            return new TypeReflector<T>().Property(expression);
        }

        public static MemberInfo DecodeMemberAccessExpressionOf<T, TProperty>(this Expression<Func<T, TProperty>> expression)
        {
            return new TypeReflector<T>().MemberAccessExpression(expression);
        }

        public static MethodInfo GetMethod<T>(this Expression<Action<T>> expression)
        {
            return GetMethod((LambdaExpression)expression);
        }

        public static MethodInfo GetMethod<T, TResult>(this Expression<Func<T, TResult>> expression)
        {
            return GetMethod((LambdaExpression)expression);
        }

        public static MethodInfo GetMethod(this Expression<Action> expression)
        {
            return GetMethod((LambdaExpression)expression);
        }

        public static MethodInfo GetMethod(this LambdaExpression expression)
        {
            var methodCallExpression = expression.Body as MethodCallExpression;

            if (methodCallExpression == null)
            {
                throw new ArgumentException("Invalid Expression. Expression should consist of a Method call only.");
            }

            return methodCallExpression.Method;
        }

        public static PropertyInfo PropertyFromExpression<T, TProperty>(this Expression<Func<T, TProperty>> expression)
        {
            return
                expression
                    .Member()
                    .Convert<PropertyInfo>();
        }

        public static MemberInfo Member<T, TProperty>(
            this Expression<Func<T, TProperty>> expression)
        {
            MemberInfo memberOfDeclaringType = null;

            if (expression.Body.NodeType != ExpressionType.MemberAccess)
            {
                if ((expression.Body.NodeType == ExpressionType.Convert) && (expression.Body.Type == typeof(object)))
                {
                    memberOfDeclaringType = ((MemberExpression)((UnaryExpression)expression.Body).Operand).Member;
                }
                else
                {
                    var invalidExpressionMessage = $"Invalid expression type: Expected ExpressionType.MemberAccess, Found {expression.Body.NodeType}";

                    throw new Exception(invalidExpressionMessage);
                }
            }
            else
            {
                memberOfDeclaringType = ((MemberExpression)expression.Body).Member;
            }

            if (typeof(T).IsInterface)
            {
                // Type.GetProperty(string name,Type returnType) does not work properly with interfaces
                return memberOfDeclaringType;
            }

            MemberInfo memberOfReflectType = null;

            if (memberOfDeclaringType is PropertyInfo)
            {
                memberOfReflectType =
                    typeof(T)
                    .GetProperties(InstanceBindingFlags)
                    .Single(property =>
                        property.Name.Equals(memberOfDeclaringType.Name) &&
                        property.PropertyType == GetPropertyOrFieldType(memberOfDeclaringType));
            }
            else if (memberOfDeclaringType is FieldInfo)
            {
                memberOfReflectType =
                    typeof(T)
                    .GetField(memberOfDeclaringType.Name, InstanceBindingFlags);
            }
            else
            {
                throw new ArgumentException("The expression must be a FieldInfo or a PropertyInfo.");
            }

            var propertyNotFoundMessage =
                $"Cannot find a instance property with name {memberOfDeclaringType.Name} on type {typeof(T).FullName}.";

            var exception = new ArgumentException(propertyNotFoundMessage);

            return memberOfReflectType;
        }

        public static Type GetPropertyOrFieldType(this MemberInfo propertyOrField)
        {
            if (propertyOrField.MemberType == MemberTypes.Property)
            {
                return ((PropertyInfo)propertyOrField).PropertyType;
            }

            if (propertyOrField.MemberType == MemberTypes.Field)
            {
                return ((FieldInfo)propertyOrField).FieldType;
            }

            throw
                new ArgumentOutOfRangeException(
                    "propertyOrField",
                    $"Expected PropertyInfo or FieldInfo; found {propertyOrField.MemberType}");
        }
    }
}