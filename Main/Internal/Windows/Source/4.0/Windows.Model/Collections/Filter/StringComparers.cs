namespace EyeSoft.Windows.Model.Collections
{
	using System.Collections.Generic;

	using EyeSoft.Collections.Generic;

	public static class StringComparers
	{
		public static readonly IEqualityComparer<string> Contains =
			EqualityComparerFactory<string>.Create((x, y) => x.Contains(y), x => 1);
	}
}