using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SharpTestsEx.Assertions
{
	public class ExpressionVisitor<TA>
	{
		private const string InvalidOperandsMessageTemplate = "The expression ({0}) is invalid; none of the operands includes the value under test.";
		private const string ConstantExpressionMessageTemplate = "The expression ({0}) is a constant; you may test something else.";

		private readonly TA actualValue;
		private readonly ParameterExpression actual;

		public ExpressionVisitor(TA actualValue, Expression<Func<TA, bool>> expression)
		{
			this.actualValue = actualValue;
			TestExpression = expression;
			actual = expression.Parameters.Single();
		}

		public Expression<Func<TA, bool>> TestExpression { get; private set; }

		public IAssertion<TA> Visit()
		{
			var result = Visit(TestExpression.Body);
			if (!ContainsActualParameter(TestExpression.Body))
			{
				throw new InvalidOperationException(string.Format(InvalidOperandsMessageTemplate, new ExpressionStringBuilder(TestExpression.Body)));				
			}
			return result;
		}

		private UnaryAssertion<TA> Visit(Expression expression)
		{
			switch (expression.NodeType)
			{
				case ExpressionType.Equal:
				case ExpressionType.NotEqual:
				case ExpressionType.GreaterThan:
				case ExpressionType.LessThan:
				case ExpressionType.GreaterThanOrEqual:
				case ExpressionType.LessThanOrEqual:
				case ExpressionType.OrElse:
				case ExpressionType.AndAlso:
					return Visit(expression as BinaryExpression);
				case ExpressionType.Constant:
					throw new InvalidOperationException(string.Format(ConstantExpressionMessageTemplate, expression));
				case ExpressionType.Not:
					return Visit(expression as UnaryExpression);
				case ExpressionType.MemberAccess:
					return new ExpressionAssertion<TA>(Expression.Lambda<Func<TA, bool>>(expression, actual));
				case ExpressionType.Call:
					return Visit(expression as MethodCallExpression);
				case ExpressionType.Parameter:
					return new ExpressionAssertion<TA>(Expression.Lambda<Func<TA, bool>>(expression, actual));
				case ExpressionType.TypeIs:
					return Visit(expression as TypeBinaryExpression);
				default:
					throw new ArgumentOutOfRangeException("expression");
			}
		}

		private bool ContainsActualParameter(Expression expression)
		{
			if (expression == null)
			{
				return false;
			}
			switch (expression.NodeType)
			{
				case ExpressionType.Equal:
				case ExpressionType.NotEqual:
				case ExpressionType.GreaterThan:
				case ExpressionType.LessThan:
				case ExpressionType.GreaterThanOrEqual:
				case ExpressionType.LessThanOrEqual:
				case ExpressionType.OrElse:
				case ExpressionType.AndAlso:
				case ExpressionType.ArrayIndex:
				case ExpressionType.Multiply:
				case ExpressionType.Divide:
				case ExpressionType.Add:
				case ExpressionType.Subtract:
				case ExpressionType.Modulo:
					return GetExpressions(expression as BinaryExpression).Any(ContainsActualParameter);
				case ExpressionType.Constant:
					return false;
				case ExpressionType.Convert:
				case ExpressionType.Not:
				case ExpressionType.TypeAs:
					return ContainsActualParameter(((UnaryExpression)expression).Operand);
				case ExpressionType.MemberAccess:
					return ContainsActualParameter(((MemberExpression) expression).Expression);
				case ExpressionType.Call:
					return GetExpressions(expression as MethodCallExpression).Any(ContainsActualParameter);
				case ExpressionType.Parameter:
					return expression == actual;
				case ExpressionType.TypeIs:
					return ContainsActualParameter(((TypeBinaryExpression) expression).Expression);
				default:
					return false;
			}
		}

		private UnaryAssertion<TA> Visit(MethodCallExpression expression)
		{
			IFailureMagnifier magnifier;
			if (expression.Method.Name == "SequenceEqual" && expression.Method.DeclaringType == typeof(System.Linq.Enumerable))
			{
				magnifier = GetSequenceEqualMagnifier(expression);
			}
			else
			{
				magnifier = new EmptyMagnifier();
			}
			var lambda = Expression.Lambda<Func<TA, bool>>(expression, actual);
			return new ExpressionAssertion<TA>(lambda, new ExpressionMessageComposer<TA>(lambda, magnifier));
		}

		private IFailureMagnifier GetSequenceEqualMagnifier(MethodCallExpression expression)
		{
			Type genericType = expression.Method.GetGenericArguments().First();
			var concreteType = typeof (SameSequenceAsFailureMagnifier<>).MakeGenericType(genericType);
			return (IFailureMagnifier)Activator.CreateInstance(concreteType, expression.Arguments.Select(arg => GetAsValue(arg)).ToArray());
		}

		private IEnumerable<Expression> GetExpressions(MethodCallExpression methodCallExpression)
		{
			yield return methodCallExpression.Object;
			foreach (var argument in methodCallExpression.Arguments)
			{
				yield return argument;
			}
		}

		private UnaryAssertion<TA> Visit(UnaryExpression expression)
		{
			switch (expression.NodeType)
			{
				case ExpressionType.Not:
					return new ExpressionAssertion<TA>(Expression.Lambda<Func<TA, bool>>(expression, actual));
				default: throw new ArgumentOutOfRangeException("expression");
			}
		}

		private UnaryAssertion<TA> Visit(TypeBinaryExpression expression)
		{
			return new ExpressionAssertion<TA>(Expression.Lambda<Func<TA, bool>>(expression, actual));
		}

		private UnaryAssertion<TA> Visit(BinaryExpression expression)
		{
			switch (expression.NodeType)
			{
				case ExpressionType.Equal:
					return GetEqualOperatorGenericAssertion(expression);
				case ExpressionType.NotEqual:
				case ExpressionType.GreaterThan:
				case ExpressionType.LessThan:
				case ExpressionType.GreaterThanOrEqual:
				case ExpressionType.LessThanOrEqual:
					return GetComparisonOperatorGenericAssertion(expression);
				case ExpressionType.AndAlso:
					return new AndAssertion<TA>(Visit(expression.Left), Visit(expression.Right));
				case ExpressionType.OrElse:
					return new OrAssertion<TA>(Visit(expression.Left), Visit(expression.Right));
				default:
					throw new ArgumentOutOfRangeException("expression");
			}
		}

		private ExpressionAssertion<TA> GetComparisonOperatorGenericAssertion(BinaryExpression expression)
		{
			IFailureMagnifier magnifier = new EmptyMagnifier();
			var leftType = expression.Left.Type.GetTypeInfo();
			var rightType = expression.Right.Type.GetTypeInfo();
			if (leftType.IsValueType || rightType.IsValueType)
			{
				magnifier = new ValueTypeComparisonFailureMagnifier(expression.NodeType, () => GetAsValue(expression.Left), () => GetAsValue(expression.Right));
			}
			var lambda = Expression.Lambda<Func<TA, bool>>(expression, actual);
			return new ExpressionAssertion<TA>(lambda, new ExpressionMessageComposer<TA>(lambda, magnifier));
		}

		private UnaryAssertion<TA> GetEqualOperatorGenericAssertion(BinaryExpression expression)
		{
			IFailureMagnifier magnifier = new EmptyMagnifier();
			var leftType = expression.Left.Type;
			var rightType = expression.Right.Type;
			if (leftType == typeof(string) || rightType == typeof(string))
			{
				magnifier = new StringEqualityFailureMagnifier(() => GetAsValue(expression.Left) as string, () => GetAsValue(expression.Right) as string);
			}
			else if (leftType.GetTypeInfo().IsValueType || rightType.GetTypeInfo().IsValueType)
			{
				magnifier = new ValueTypeComparisonFailureMagnifier(expression.NodeType, () => GetAsValue(expression.Left), () => GetAsValue(expression.Right));
			}
			var lambda = Expression.Lambda<Func<TA, bool>>(expression, actual);
			return new ExpressionAssertion<TA>(lambda, new ExpressionMessageComposer<TA>(lambda, magnifier));
		}

		private object GetAsValue(Expression expression)
		{
			switch (expression.NodeType)
			{
				case ExpressionType.Constant:
					return ((ConstantExpression)expression).Value;
				case ExpressionType.Parameter:
					return actualValue;
				case ExpressionType.Call:
				case ExpressionType.MemberAccess:
				case ExpressionType.ArrayIndex:
				case ExpressionType.ArrayLength:
				case ExpressionType.Multiply:
				case ExpressionType.Divide:
				case ExpressionType.Modulo:
				case ExpressionType.Add:
				case ExpressionType.Subtract:
				case ExpressionType.NewArrayInit:
				case ExpressionType.New:
					var unaryExpression = Expression.Convert(expression, expression.Type);
					return Expression.Lambda(unaryExpression, actual).Compile().DynamicInvoke(actualValue);
				case ExpressionType.Convert:
					return GetAsValue(((UnaryExpression) expression).Operand);
				default:
					throw new ArgumentOutOfRangeException("expression");
			}
		}

		private IEnumerable<Expression> GetExpressions(BinaryExpression expression)
		{
			yield return expression.Left;
			yield return expression.Right;
		}
	}
}