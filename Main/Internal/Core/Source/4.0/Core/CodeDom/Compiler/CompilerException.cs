namespace EyeSoft.CodeDom.Compiler
{
	using System;
	using System.Runtime.Serialization;

	[Serializable]
	public class CompilerException :
		Exception
	{
		public CompilerException(string message)
			: base(message)
		{
		}

		public CompilerException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected CompilerException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}