namespace EyeSoft.Data.EntityFramework.Test.Caching.Helpers
{
	using System;
	using System.Collections.Generic;

	using EyeSoft.Data.EntityFramework.Caching;

	internal class TestMemoryCache : ICacheWithStatistics
	{
		private readonly IDictionary<string, object> dataDictionary =
			new Dictionary<string, object>();

		/// <summary>
		/// Gets the number of cache hits.
		/// </summary>
		/// <value>The number of cache hits.</value>
		public int Hits { get; private set; }

		/// <summary>
		/// Gets the number of cache misses.
		/// </summary>
		/// <value>The number of cache misses.</value>
		public int Misses { get; private set; }

		/// <summary>
		/// Gets the number of cache adds.
		/// </summary>
		/// <value>The number of cache adds.</value>
		public int ItemAdds { get; private set; }

		/// <summary>
		/// Gets the number of cache item invalidations.
		/// </summary>
		/// <value>The number of cache item invalidations.</value>
		public int ItemInvalidations { get; private set; }

		public bool GetItem(string key, out object value)
		{
			if (dataDictionary.ContainsKey(key))
			{
				value = dataDictionary[key];

				Hits++;

				return true;
			}

			value = null;
			return false;
		}

		public void PutItem(
			string key,
			object value,
			IEnumerable<string> dependentEntitySets,
			TimeSpan slidingExpiration,
			DateTime absoluteExpiration)
		{
			dataDictionary.Add(key, value);
		}

		public void InvalidateSets(IEnumerable<string> entitySets)
		{
		}

		public void InvalidateItem(string key)
		{
		}
	}
}