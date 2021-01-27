namespace EyeSoft.Core.Extensions
{
    using System.Globalization;

    public static class CharExtensions
	{
		public static string ToInvariant(this char value)
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}
	}
}