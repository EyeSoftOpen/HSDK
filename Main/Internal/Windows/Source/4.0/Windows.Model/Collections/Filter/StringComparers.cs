using System.Collections.Generic;
using EyeSoft.Collections.Generic;

namespace EyeSoft.Windows.Model.Collections.Filter
{
	public static class StringComparers
	{
		public static readonly IEqualityComparer<string> Contains =
			EqualityComparerFactory<string>.Create((x, y) => x.Contains(y), x => 1);
	}
}