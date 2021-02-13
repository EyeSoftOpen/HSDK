namespace EyeSoft.Reflection
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class TypeReflector<T> : TypeReflector
	{
		public string PropertyName(Expression<Func<T, object>> expression)
		{
			return Property(expression).Name;
		}

		public string PropertyName<TProperty>(Expression<Func<T, TProperty>> expression)
		{
			return Property(expression).Name;
		}

		public PropertyInfo Property(Expression<Func<T, object>> expression)
		{
			return Property(expression.Body);
		}

		public PropertyInfo Property<TProperty>(Expression<Func<T, TProperty>> expression)
		{
			return Property(expression.Body);
		}

		public MethodInfo GetMethod(Expression<Action<T>> expression)
		{
			return ((LambdaExpression)expression).GetMethod();
		}

		public MethodInfo GetMethod<TResult>(Expression<Func<T, TResult>> expression)
		{
			return ((LambdaExpression)expression).GetMethod();
		}

		public MemberInfo MemberAccessExpression<TProperty>(Expression<Func<T, TProperty>> expression)
		{
			return
				expression.Member();
		}

		public PropertyInfo PropertyExpression<TProperty>(Expression<Func<T, TProperty>> expression)
		{
			return
				expression.PropertyFromExpression();
		}
	}
}