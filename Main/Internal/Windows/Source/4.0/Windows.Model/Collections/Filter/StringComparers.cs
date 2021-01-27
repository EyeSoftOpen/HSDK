namespace EyeSoft.Windows.Model.Collections.Filter
{
    using System.Collections.Generic;
    using Core.Collections.Generic;

    public static class StringComparers
	{
		public static readonly IEqualityComparer<string> Contains =
			EqualityComparerFactory<string>.Create((x, y) => x.Contains(y), x => 1);
	}
}