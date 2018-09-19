namespace EyeSoft
{
	using System;
	using System.Runtime.Serialization;

	[Serializable]
	public abstract class EnsuringException
		: Exception
	{
		protected EnsuringException()
		{
		}

		protected EnsuringException(string message)
			: base(message)
		{
		}

		protected EnsuringException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected EnsuringException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}