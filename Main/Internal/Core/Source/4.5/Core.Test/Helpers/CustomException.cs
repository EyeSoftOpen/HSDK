namespace EyeSoft.Core.Test.Helpers
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
	internal class CustomException
		: Exception
	{
		public CustomException()
		{
		}

		public CustomException(string message)
			: base(message)
		{
		}

		protected CustomException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected CustomException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public string Title { get; set; }
	}
}