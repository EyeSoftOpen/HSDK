namespace EyeSoft.Collections.Generic
{
    using System.Collections.Generic;

    public class FluentDictionary<TKey, TValue> : Dictionary<TKey, TValue>
	{
		public FluentDictionary<TKey, TValue> Entry(TKey key, TValue value)
		{
			Add(key, value);

			return this;
		}
	}
}