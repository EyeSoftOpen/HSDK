namespace EyeSoft.Reflection
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class TypeReflector
    {
        public string PropertyName(Expression<Func<object>> expression)
        {
            return Property(expression.Body).Name;
        }

        public PropertyInfo Property(Expression<Func<object>> expression)
        {
            return Property(expression.Body);
        }

        public PropertyInfo Property<TProperty>(Expression<Func<TProperty>> expression)
        {
            return Property(expression.Body);
        }

        public PropertyInfo Property(MemberExpression memberExpression)
        {
            var propertyInfo = (PropertyInfo)memberExpression.Member;

            return propertyInfo;
        }

        protected PropertyInfo Property(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Convert)
            {
                var member = (MemberExpression)((UnaryExpression)expression).Operand;
                var propertyInfo = (PropertyInfo)member.Member;
                return propertyInfo;
            }

            if (expression.NodeType != ExpressionType.MemberAccess)
            {
                if ((expression.NodeType == ExpressionType.Convert) && (expression.Type == typeof(object)))
                {
                    return (PropertyInfo)((MemberExpression)((UnaryExpression)expression).Operand).Member;
                }

                throw new Exception($"Invalid expression type: Expected ExpressionType.MemberAccess, Found {expression.NodeType}.");
            }

            return (PropertyInfo)((MemberExpression)expression).Member;
        }
    }
}