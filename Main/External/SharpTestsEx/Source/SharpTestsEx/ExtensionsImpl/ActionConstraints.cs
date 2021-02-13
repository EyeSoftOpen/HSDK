using System;
using SharpTestsEx.Assertions;

namespace SharpTestsEx.ExtensionsImpl
{
	public class ActionConstraints : Constraint<Action>, IActionConstraints
	{
		public ActionConstraints(Action actual) : base(actual) { }
		public ActionConstraints(Action actual, Func<string> messageBuilder) : base(actual, messageBuilder) { }

		#region IActionConstraints Members

		public IActionAndConstraints<IThrowConstraints<TException>, TException> Throw<TException>() where TException : Exception
		{
			var exception = TryCatch(() => AssertionInfo.Actual());
			var c = new Assertion<Type, Type>(Properties.Resources.PredicateThrow, typeof (TException),
			                                  a => a != null && a == typeof (TException), new ActionThrowsMessageBuilder());
			c.Assert(exception == null ? null : exception.GetType(), AssertionInfo.FailureMessage);
			return
				new ActionAndConstraints<IThrowConstraints<TException>, TException>(
					new ThrowConstraints<TException>((TException)exception), (TException)exception);
		}

		public IActionAndConstraints<IThrowConstraints<Exception>, Exception> Throw()
		{
			var e = TryCatch(() => AssertionInfo.Actual());
			var c = new Assertion<Exception, Exception>(Properties.Resources.PredicateThrows, null, a => a != null,
			                                            mi =>
			                                            string.Format("{0} {1}.{2}", Properties.Resources.AssertionVerb,
			                                                          mi.AssertionPredicate, mi.CustomMessage));
			c.Assert(e, AssertionInfo.FailureMessage);

			return new ActionAndConstraints<IThrowConstraints<Exception>, Exception>(new ThrowConstraints<Exception>(e), e);
		}

		public void NotThrow()
		{
			var e = TryCatch(() => AssertionInfo.Actual());
			var c = new Assertion<Exception, object>(Properties.Resources.PredicateDoesNotThrow, null,
																							 a => ReferenceEquals(null, a),
																							 mi =>
																							 string.Format("{0} {1} {2}.{3}\n{4}\n{5}",
																														 Properties.Resources.AssertionVerb, mi.AssertionPredicate,
																														 e.GetType().FullName, mi.CustomMessage,
																														 Properties.Resources.FailureMsgNotThrow, e.Message));
			c.Assert(e, AssertionInfo.FailureMessage);			
		}

		#endregion

		internal static Exception TryCatch(Action action)
		{
			try
			{
				action();
				return null;
			}
			catch (Exception exception)
			{
				return exception;
			}
		}
	}

	public class ActionAndConstraints<TConstraints, TException> : AndConstraint<TConstraints>,
	                                                              IActionAndConstraints<TConstraints, TException>
		where TConstraints : class, IAllowClone where TException : Exception
	{
		private readonly TException currentException;

		public ActionAndConstraints(TConstraints rootConstraint, TException currentException) : base(rootConstraint)
		{
			this.currentException = currentException;
		}

		#region Implementation of IActionAndConstraints<TConstraints>

		public TException Exception => currentException;

        #endregion
	}

	public class ThrowConstraints<TException> : IThrowConstraints<TException> where TException : Exception
	{
		public ThrowConstraints(TException currentException)
		{
			ValueOf = currentException;
		}

		#region IThrowConstraints<TException> Members

		public TException ValueOf { get; }

		public TException Exception => ValueOf;

        #endregion

		public object Clone()
		{
			return new ThrowConstraints<TException>(ValueOf);
		}
	}
}