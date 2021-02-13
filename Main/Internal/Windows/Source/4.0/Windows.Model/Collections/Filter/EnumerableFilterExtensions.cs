namespace EyeSoft.Windows.Model.Collections
{
	using System.Collections.Generic;
	using System.Linq;

	public static class EnumerableFilterExtensions
	{
		public static IEnumerable<string> FilteredKeys(this IEnumerable<string> keys)
		{
			return
				keys
					.Where(value => !string.IsNullOrWhiteSpace(value))
					.Select(x => x.Trim().ToLower())
					.ToArray();
		}
	}
}
