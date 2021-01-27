namespace EyeSoft.Core.Test.Helpers
{
    using System.Collections.Generic;

    internal static class KnownEntity
	{
		public static readonly IEnumerable<TestEntity> List =
			new[]
				{
					new TestEntity
						{
							Name = "Bill",
							Age = 2
						}
				};
	}
}