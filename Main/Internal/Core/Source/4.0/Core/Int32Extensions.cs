namespace EyeSoft
{
	using System.Globalization;

	public static class Int32Extensions
	{
		public static string ToInvariant(this int value)
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}
	}
}