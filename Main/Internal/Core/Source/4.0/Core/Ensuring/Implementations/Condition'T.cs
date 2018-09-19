namespace EyeSoft.Implementations
{
	using System;
	using System.Linq.Expressions;

	using EyeSoft.Extensions;
	using EyeSoft.Linq.Expressions;

	internal class ConditionTree<T>
		: INamedCondition<T>, INegableCondition<T>
	{
		public ConditionTree(T value)
		{
			Is = this;
			Value = value;
		}

		public ConditionTree(T value, string argumentName)
		{
			Is = this;
			Value = value;
			IsArgument = true;
			ArgumentName = argumentName;
		}

		public INegableCondition<T> Is { get; private set; }

		public T Value { get; private set; }

		public bool IsNegated
		{
			get; private set;
		}

		public bool IsArgument
		{
			get; private set;
		}

		public string ArgumentName
		{
			get; private set;
		}

		public Exception Exception { get; private set; }

		public string Message { get; private set; }

		IBooleanCondition<T> INegableCondition<T>.Not
		{
			get
			{
				IsNegated = true;
				return this;
			}
		}

		public INegableCondition<T> WithException<TException>() where TException : Exception, new()
		{
			Exception = new TException();
			return this;
		}

		public IWithExceptionCondition<T> Named<TArgument>(Expression<Func<TArgument>> expression)
		{
			SetArgumentName(expression.Parameter().Name);

			return this;
		}

		public IWithExceptionCondition<T> Named(string name)
		{
			SetArgumentName(name);

			return this;
		}

		public INegableCondition<T> WithException<TException>(TException exception)
			where TException : Exception
		{
			return
				this
					.Convert<IWithExceptionCondition<T>>()
					.WithException<TException>(exception)
					.Convert<INegableCondition<T>>();
		}

		public INegableCondition<T> WithException<TException>(Action<TException> action)
			where TException : Exception, new()
		{
			return
				this
					.Convert<IWithExceptionCondition<T>>()
					.WithException<TException>(action)
					.Convert<INegableCondition<T>>();
		}

		public INegableCondition<T> WithException<TException>(
			string message,
			params object[] parameters)
			where TException : Exception, new()
		{
			return
				this
					.Convert<IWithExceptionCondition<T>>()
					.WithException<TException>(message, parameters)
					.Convert<INegableCondition<T>>();
		}

		ICondition<T> IWithExceptionCondition<T>.WithException<TException>()
		{
			return
				this
					.Convert<IWithExceptionCondition<T>>()
					.WithException<TException>();
		}

		ICondition<T> IWithExceptionCondition<T>.WithException<TException>(TException exception)
		{
			Exception = exception;

			return this;
		}

		ICondition<T> IWithExceptionCondition<T>.WithException<TException>(Action<TException> action)
		{
			var exception = new TException();
			action(exception);
			Exception = exception;

			return this;
		}

		ICondition<T> IWithExceptionCondition<T>.WithException<TException>(string message, params object[] parameters)
		{
			if (parameters.IsNull())
			{
				Exception = new TException();
				Message = string.Format(message, parameters);

				return this;
			}

			var exceptionWithMessageConstructor =
				typeof(TException)
					.GetConstructor(new[] { typeof(string) });

			Exception = (Exception)exceptionWithMessageConstructor.Invoke(new object[] { message });
			return this;
		}

		internal void SetArgumentName(string argumentName)
		{
			if (ArgumentName != null)
			{
				throw new ParameterNameSetMoreThanOnceException();
			}

			if (string.IsNullOrEmpty(argumentName))
			{
				throw new ArgumentNullException("argumentName");
			}

			IsArgument = true;
			ArgumentName = argumentName;
		}
	}
}