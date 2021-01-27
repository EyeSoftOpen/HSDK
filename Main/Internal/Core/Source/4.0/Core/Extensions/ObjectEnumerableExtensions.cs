namespace EyeSoft.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ObjectEnumerableExtensions
	{
		public static bool AnyInstanceOf<T>(this IEnumerable<object> enumerable)
			where T : class
		{
			return
				enumerable
				.OfType<T>()
				.Any();
		}

		public static T SingleInstanceOf<T>(this IEnumerable<object> enumerable)
		{
			return
				enumerable
					.OfType<T>()
					.SingleOrDefault();
		}
	}
}