using System.Linq.Expressions;
using System;
using SharpTestsEx.Assertions;
using SharpTestsEx.Properties;

namespace SharpTestsEx.ExtensionsImpl
{
	public class ExpressionActionConstraints<T> : Constraint<T>, IExpressionActionConstraints<T>
	{
		private readonly T actual;
		private readonly Expression<Action<T>> expression;

		public ExpressionActionConstraints(T actual, Expression<Action<T>> expression)
			: base(actual)
		{
			this.actual = actual;
			this.expression = expression;
		}

		public ExpressionActionConstraints(T actual,Expression<Action<T>> expression, Func<string> messageBuilder) : base(actual, messageBuilder)
		{
			this.actual = actual;
			this.expression = expression;
		}

		#region Implementation of IExpressionActionConstraints<T>

		public IActionAndConstraints<IThrowConstraints<TException>,TException> Throws<TException>() where TException : Exception
		{
			Exception exception = ActionConstraints.TryCatch(() => expression.Compile().Invoke(actual));
			var c = new Assertion<Type, Type>(Resources.PredicateThrow, typeof(TException), a => a != null && a == typeof(TException),
			                                  new ThrowsMessageBuilder(exception, typeof (TException), expression));
			c.Assert(exception == null ? null : exception.GetType(), AssertionInfo.FailureMessage);

			return new ActionAndConstraints<IThrowConstraints<TException>, TException>(new ThrowConstraints<TException>((TException)exception), (TException)exception);
		}

		public class ThrowsMessageBuilder : IMessageBuilder<Type, Type>
		{
			private readonly Exception actualException;
			private readonly Type expectedExceptionType;
			private readonly Expression causeExpression;

			public ThrowsMessageBuilder(Exception actualException, Type expectedExceptionType, Expression causeExpression)
			{
				this.actualException = actualException;
				this.expectedExceptionType = expectedExceptionType;
				this.causeExpression = causeExpression;
			}

			#region Implementation of IMessageBuilder<Type,Type>

			public string Compose(MessageBuilderInfo<Type, Type> info)
			{
				var a = actualException != null ? actualException.GetType() : null;
				var e = expectedExceptionType;
				if (a == null)
				{
					return string.Format("The action ({0}) {1} {2} {3}.{4}", (new ExpressionStringBuilder(causeExpression)), Resources.AssertionVerb, Resources.PredicateThrow, e, info.CustomMessage);
				}
				else
				{
					return string.Format("The action ({0}) {1} {2} {3}.{4}\nThrew: {5}", (new ExpressionStringBuilder(causeExpression)), Resources.AssertionVerb, Resources.PredicateThrow, e, info.CustomMessage, a);
				}
			}

			#endregion
		}

		public IActionAndConstraints<IThrowConstraints<Exception>, Exception> Throws()
		{
			Exception exception = ActionConstraints.TryCatch(() => expression.Compile().Invoke(actual));
			var c = new Assertion<Type, Type>(Resources.PredicateThrow, typeof(Exception), a => a != null,
																				new ThrowsMessageBuilder(exception, null, expression));
			c.Assert(exception == null ? null : exception.GetType(), AssertionInfo.FailureMessage);

			return new ActionAndConstraints<IThrowConstraints<Exception>, Exception>(new ThrowConstraints<Exception>(exception), exception);
		}

		public void NotThrows()
		{
			Exception exception = ActionConstraints.TryCatch(() => expression.Compile().Invoke(actual));
			var c = new Assertion<Type, object>(Resources.PredicateDoesNotThrow, null, a => ReferenceEquals(null, a),
			                                    mi =>
																					string.Format("The action ({0}) {1} {2} {3}.{4}\n{5}\n{6}", (new ExpressionStringBuilder(expression)), Resources.AssertionVerb,
			                                                  mi.AssertionPredicate, exception.GetType().FullName,
			                                                  mi.CustomMessage, Resources.FailureMsgNotThrow, exception.Message));
			c.Assert(exception == null ? null : exception.GetType(), AssertionInfo.FailureMessage);
		}

		#endregion
	}
}