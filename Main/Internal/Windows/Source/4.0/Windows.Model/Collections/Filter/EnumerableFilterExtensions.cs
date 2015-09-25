using System.Collections.Generic;
using System.Linq;

namespace EyeSoft.Windows.Model.Collections.Filter
{
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
