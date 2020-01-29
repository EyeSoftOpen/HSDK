using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Runtime.CompilerServices;

namespace SharpTestsEx.Assertions
{
	/// <summary>
	/// The intention of <see cref="ExpressionStringBuilder"/> is to create a more readable 
	/// string representation for the failure message.
	/// </summary>
	public class ExpressionStringBuilder
	{
		private readonly Expression expression;
		private StringBuilder builder;

		public ExpressionStringBuilder(Expression expression)
		{
			this.expression = expression;
		}

		public override string ToString()
		{
			builder = new StringBuilder();
			ToString(expression);
			return builder.ToString();
		}

		public void ToString(Expression exp)
		{
			if (exp == null)
			{
				builder.Append(Properties.Resources.NullValue);
				return;
			}
			switch (exp.NodeType)
			{
				case ExpressionType.Negate:
				case ExpressionType.NegateChecked:
				case ExpressionType.Not:
				case ExpressionType.Convert:
				case ExpressionType.ConvertChecked:
				case ExpressionType.ArrayLength:
				case ExpressionType.Quote:
				case ExpressionType.TypeAs:
					ToStringUnary((UnaryExpression) exp);
					return;
				case ExpressionType.Add:
				case ExpressionType.AddChecked:
				case ExpressionType.Subtract:
				case ExpressionType.SubtractChecked:
				case ExpressionType.Multiply:
				case ExpressionType.MultiplyChecked:
				case ExpressionType.Divide:
				case ExpressionType.Modulo:
				case ExpressionType.And:
				case ExpressionType.AndAlso:
				case ExpressionType.Or:
				case ExpressionType.OrElse:
				case ExpressionType.LessThan:
				case ExpressionType.LessThanOrEqual:
				case ExpressionType.GreaterThan:
				case ExpressionType.GreaterThanOrEqual:
				case ExpressionType.Equal:
				case ExpressionType.NotEqual:
				case ExpressionType.Coalesce:
				case ExpressionType.ArrayIndex:
				case ExpressionType.RightShift:
				case ExpressionType.LeftShift:
				case ExpressionType.ExclusiveOr:
					ToStringBinary((BinaryExpression) exp);
					return;
				case ExpressionType.TypeIs:
					ToStringTypeIs((TypeBinaryExpression) exp);
					return;
				case ExpressionType.Conditional:
					ToStringConditional((ConditionalExpression) exp);
					return;
				case ExpressionType.Constant:
					ToStringConstant((ConstantExpression) exp);
					return;
				case ExpressionType.Parameter:
					ToStringParameter((ParameterExpression) exp);
					return;
				case ExpressionType.MemberAccess:
					ToStringMemberAccess((MemberExpression) exp);
					return;
				case ExpressionType.Call:
					ToStringMethodCall((MethodCallExpression) exp);
					return;
				case ExpressionType.Lambda:
					ToStringLambda((LambdaExpression) exp);
					return;
				case ExpressionType.New:
					ToStringNew((NewExpression) exp);
					return;
				case ExpressionType.NewArrayInit:
				case ExpressionType.NewArrayBounds:
					ToStringNewArray((NewArrayExpression) exp);
					return;
				case ExpressionType.Invoke:
					ToStringInvocation((InvocationExpression) exp);
					return;
				case ExpressionType.MemberInit:
					ToStringMemberInit((MemberInitExpression) exp);
					return;
				case ExpressionType.ListInit:
					ToStringListInit((ListInitExpression) exp);
					return;
				default:
					throw new Exception(string.Format("Unhandled expression type: '{0}'", exp.NodeType));
			}
		}

		private void ToStringBinding(MemberBinding binding)
		{
			switch (binding.BindingType)
			{
				case MemberBindingType.Assignment:
					ToStringMemberAssignment((MemberAssignment) binding);
					return;
				case MemberBindingType.MemberBinding:
					ToStringMemberMemberBinding((MemberMemberBinding) binding);
					return;
				case MemberBindingType.ListBinding:
					ToStringMemberListBinding((MemberListBinding) binding);
					return;
				default:
					throw new Exception(string.Format("Unhandled binding type '{0}'", binding.BindingType));
			}
		}

		private void ToStringElementInitializer(ElementInit initializer)
		{
			builder.Append("{");
			ToStringExpressionList(initializer.Arguments);
			builder.Append("}");
			return;
		}

		private void ToStringUnary(UnaryExpression u)
		{
			switch (u.NodeType)
			{
				case ExpressionType.ArrayLength:
					ToString(u.Operand);
					builder.Append(".Length");
					return;

				case ExpressionType.Negate:
				case ExpressionType.NegateChecked:
					builder.Append("-");
					ToString(u.Operand);
					return;

				case ExpressionType.Not:
					builder.Append("!(");
					ToString(u.Operand);
					builder.Append(")");
					return;

				case ExpressionType.Quote:
					ToString(u.Operand);
					return;

				case ExpressionType.TypeAs:
					builder.Append("(");
					ToString(u.Operand);
					builder.Append(" as ");
					builder.Append(u.Type.DisplayName());
					builder.Append(")");
					return;
				case ExpressionType.Convert:
				case ExpressionType.ConvertChecked:
					ToString(u.Operand);
					return;
			}
			return;
		}

		private void ToStringBinary(BinaryExpression b)
		{
			if (b.NodeType == ExpressionType.ArrayIndex)
			{
				ToString(b.Left);
				builder.Append("[");
				ToString(b.Right);
				builder.Append("]");
			}
			else
			{
				string @operator = ToStringOperator(b.NodeType);
				if (NeedEncloseInParen(b.Left))
				{
					builder.Append("(");
					ToString(b.Left);
					builder.Append(")");
				}
				else
				{
					ToString(b.Left);
				}
				builder.Append(" ");
				builder.Append(@operator);
				builder.Append(" ");
				if (NeedEncloseInParen(b.Right))
				{
					builder.Append("(");
					ToString(b.Right);
					builder.Append(")");
				}
				else
				{
					ToString(b.Right);
				}
			}
		}

		private bool NeedEncloseInParen(Expression operand)
		{
			return operand.NodeType == ExpressionType.AndAlso || operand.NodeType == ExpressionType.OrElse;
		}

		private void ToStringTypeIs(TypeBinaryExpression b)
		{
			builder.Append("(");
			ToString(b.Expression);
			builder.Append(" is ");
			builder.Append(b.TypeOperand.DisplayName());
			builder.Append(")");
			return;
		}

		private void ToStringConstant(ConstantExpression c)
		{
			var value = c.Value;
			if (value != null)
			{
				if (value is string)
				{
					builder.Append("\"").Append(value).Append("\"");
				}
				else if (value.ToString() == value.GetType().ToString())
				{
					// Perhaps is better without nothing (at least for local variables)
					//builder.Append("<value>");
				}
				else if (c.Type.GetTypeInfo().IsEnum)
				{
					builder.Append(c.Type.DisplayName()).Append(".").Append(value);
				}
				else
				{
					builder.Append(value);
				}
			}
			else
			{
				builder.Append(Properties.Resources.NullValue);
			}
		}

		private void ToStringConditional(ConditionalExpression c)
		{
			ToString(c.Test);
			ToString(c.IfTrue);
			ToString(c.IfFalse);
			return;
		}

		private void ToStringParameter(ParameterExpression p)
		{
			if (p.Name != null)
			{
				builder.Append(p.Name);
			}
			else
			{
				builder.Append("<param>");
			}
		}

		private void ToStringMemberAccess(MemberExpression m)
		{
			if (m.Expression != null)
			{
				ToString(m.Expression);
			}
			else
			{
				builder.Append(m.Member.DeclaringType.DisplayName());
			}
			builder.Append(".");
			builder.Append(m.Member.Name);
			return;
		}

		private void ToStringMethodCall(MethodCallExpression m)
		{
			int analizedParam = 0;
			Expression exp = m.Object;
			if (m.Method.GetCustomAttribute(typeof(ExtensionAttribute)) != null)
			{
				analizedParam = 1;
				exp = m.Arguments[0];
			}
			if (exp != null)
			{
				ToString(exp);
				builder.Append(".");
			}
			else if (m.Method.IsStatic)
			{
				builder.Append(m.Method.DeclaringType.DisplayName());
				builder.Append(".");
			}
			builder.Append(m.Method.Name);
			builder.Append("(");
			AsCommaSeparatedValues(m.Arguments.Skip(analizedParam), ToString);
			builder.Append(")");
			return;
		}

		private void ToStringExpressionList(ReadOnlyCollection<Expression> original)
		{
			AsCommaSeparatedValues(original, ToString);
			return;
		}

		private void ToStringMemberAssignment(MemberAssignment assignment)
		{
			builder.Append(assignment.Member.Name);
			builder.Append("= ");
			ToString(assignment.Expression);
			return;
		}

		private void ToStringMemberMemberBinding(MemberMemberBinding binding)
		{
			ToStringBindingList(binding.Bindings);
			return;
		}

		private void ToStringMemberListBinding(MemberListBinding binding)
		{
			ToStringElementInitializerList(binding.Initializers);
			return;
		}

		private void ToStringBindingList(IEnumerable<MemberBinding> original)
		{
			bool appendComma = false;
			foreach (var exp in original)
			{
				if (appendComma)
				{
					builder.Append(", ");
				}
				ToStringBinding(exp);
				appendComma = true;
			}
			return;
		}

		private void ToStringElementInitializerList(ReadOnlyCollection<ElementInit> original)
		{
			for (int i = 0, n = original.Count; i < n; i++)
			{
				ToStringElementInitializer(original[i]);
			}
			return;
		}

		private void ToStringLambda(LambdaExpression lambda)
		{
			if (lambda.Parameters.Count == 1)
			{
				ToStringParameter(lambda.Parameters[0]);
			}
			else
			{
				builder.Append("(");
				AsCommaSeparatedValues(lambda.Parameters, ToStringParameter);
				builder.Append(")");
			}
			builder.Append(" => ");
			ToString(lambda.Body);
			return;
		}

		private void ToStringNew(NewExpression nex)
		{
			Type type = (nex.Constructor == null) ? nex.Type : nex.Constructor.DeclaringType;
			builder.Append("new ");
			builder.Append(type.DisplayName());
			builder.Append("(");
			AsCommaSeparatedValues(nex.Arguments, ToString);
			builder.Append(")");
			return;
		}

		private void ToStringMemberInit(MemberInitExpression init)
		{
			ToStringNew(init.NewExpression);
			builder.Append(" {");
			ToStringBindingList(init.Bindings);
			builder.Append("}");
			return;
		}

		private void ToStringListInit(ListInitExpression init)
		{
			ToStringNew(init.NewExpression);
			builder.Append(" {");
			bool appendComma = false;
			foreach (var initializer in init.Initializers)
			{
				if (appendComma)
				{
					builder.Append(", ");
				}
				ToStringElementInitializer(initializer);
				appendComma = true;
			}
			builder.Append("}");
			return;
		}

		private void ToStringNewArray(NewArrayExpression na)
		{
			switch (na.NodeType)
			{
				case ExpressionType.NewArrayInit:
					builder.Append("new[] {");
					AsCommaSeparatedValues(na.Expressions, ToString);
					builder.Append("}");
					return;
				case ExpressionType.NewArrayBounds:
					builder.Append("new ");
					builder.Append(na.Type.GetElementType().DisplayName());
					builder.Append("[");
					AsCommaSeparatedValues(na.Expressions, ToString);
					builder.Append("]");
					return;
			}
		}

		private void AsCommaSeparatedValues<T>(IEnumerable<T> source, Action<T> toStringAction) where T: Expression
		{
			bool appendComma = false;
			foreach (var exp in source)
			{
				if (appendComma)
				{
					builder.Append(", ");
				}
				toStringAction(exp);
				appendComma = true;
			}
		}

		private void ToStringInvocation(InvocationExpression iv)
		{
			ToStringExpressionList(iv.Arguments);
			return;
		}

		internal static string ToStringOperator(ExpressionType nodeType)
		{
			switch (nodeType)
			{
				case ExpressionType.Add:
				case ExpressionType.AddChecked:
					return "+";

				case ExpressionType.And:
					return "&";

				case ExpressionType.AndAlso:
					return "&&";

				case ExpressionType.Coalesce:
					return "??";

				case ExpressionType.Divide:
					return "/";

				case ExpressionType.Equal:
					return "==";

				case ExpressionType.ExclusiveOr:
					return "^";

				case ExpressionType.GreaterThan:
					return ">";

				case ExpressionType.GreaterThanOrEqual:
					return ">=";

				case ExpressionType.LeftShift:
					return "<<";

				case ExpressionType.LessThan:
					return "<";

				case ExpressionType.LessThanOrEqual:
					return "<=";

				case ExpressionType.Modulo:
					return "%";

				case ExpressionType.Multiply:
				case ExpressionType.MultiplyChecked:
					return "*";

				case ExpressionType.NotEqual:
					return "!=";

				case ExpressionType.Or:
					return "|";

				case ExpressionType.OrElse:
					return "||";

				case ExpressionType.Power:
					return "^";

				case ExpressionType.RightShift:
					return ">>";

				case ExpressionType.Subtract:
				case ExpressionType.SubtractChecked:
					return "-";
			}
			return nodeType.ToString();
		}
	}
}