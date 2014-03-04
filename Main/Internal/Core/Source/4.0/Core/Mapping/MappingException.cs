namespace EyeSoft.Mapping
{
	using System;
	using System.Data;
	using System.Runtime;
	using System.Runtime.Serialization;

	[Serializable]
	public abstract class MappingException
		: EntityException
	{
		[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
		protected MappingException(string message)
			: base(message)
		{
		}

		[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
		protected MappingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
		protected MappingException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}