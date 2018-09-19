namespace EyeSoft
{
	using System;
	using System.Runtime.Serialization;

	[Serializable]
	public class ParameterNameSetMoreThanOnceException
		: Exception
	{
		public ParameterNameSetMoreThanOnceException()
		{
		}

		public ParameterNameSetMoreThanOnceException(string message)
			: base(message)
		{
		}

		protected ParameterNameSetMoreThanOnceException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected ParameterNameSetMoreThanOnceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}