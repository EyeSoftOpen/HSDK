namespace EyeSoft
{
	using System;

	[Serializable]
	public class NotEqualException
		: EnsuringException
	{
		public NotEqualException(object source, object other)
			: base(GetMessage(source, other))
		{
		}

		private static string GetMessage(object source, object other)
		{
			return
				"The source object is not equal to the other provided.{0}Source: {1}.{0}Other: {2}."
				.NamedFormat(
					Environment.NewLine,
					source,
					other);
		}
	}
}