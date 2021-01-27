namespace EyeSoft.Core.Extensions
{
    using System.Linq;

    public static class ObjectHash
	{
		public static int Combine(params int[] hashes)
		{
			return Combine(false, hashes);
		}

		public static int CombineInvariant(params int[] hashes)
		{
			return Combine(true, hashes);
		}

		public static int Combine(params object[] instances)
		{
			return Combine(false, instances);
		}

		public static int CombineInvariant(params object[] instances)
		{
			return Combine(true, instances);
		}

		public static int Combine(bool orderInvariant = false, params object[] instances)
		{
			return Combine(orderInvariant, instances.Where(x => x != null).Select(x => x.GetHashCode()).ToArray());
		}

		public static int Combine(bool orderInvariant = false, params int[] hashes)
		{
			if (orderInvariant)
			{
				unchecked
				{
					return hashes.Aggregate((current, hash) => current + hash);
				}
			}

			unchecked
			{
				return hashes.Aggregate(17, (current, hash) => (current * 23) + hash);
			}
		}
	}
}