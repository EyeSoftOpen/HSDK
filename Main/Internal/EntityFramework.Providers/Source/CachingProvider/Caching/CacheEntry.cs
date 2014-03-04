namespace EyeSoft.Data.EntityFramework.Caching
{
	using System;
	using System.Collections.Generic;

	[Serializable]
	public class CacheEntry : IEquatable<CacheEntry>
	{
		public int KeyHashCode { get; set; }

		public string Key { get; set; }

		public object Value { get; set; }

		public IEnumerable<string> DependentEntitySets { get; set; }

		public TimeSpan SlidingExpiration { get; set; }

		public DateTime ExpirationTime { get; set; }

		public DateTime LastAccessTime { get; set; }

		public CacheEntry NextEntry { get; set; }

		public CacheEntry PreviousEntry { get; set; }

		public override bool Equals(object obj)
		{
			var other = obj as CacheEntry;

			if (other == null)
			{
				return false;
			}

			return Equals(other);
		}

		public bool Equals(CacheEntry other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}

			if (KeyHashCode != other.KeyHashCode)
			{
				return false;
			}

			return Key.Equals(other.Key, StringComparison.Ordinal);
		}

		public override int GetHashCode()
		{
			return KeyHashCode;
		}
	}
}