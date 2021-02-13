namespace EyeSoft.Collections.Generic
{
    using System;
    using System.Collections.Generic;

    public class DictionaryKey<TKey, TValue>
	{
		private readonly IDictionary<TKey, TValue> dictionary;

		private readonly TKey key;

		internal DictionaryKey(IDictionary<TKey, TValue> dictionary, TKey key)
		{
			this.dictionary = dictionary;
			this.key = key;
		}

		public TValue CreateIfEmpty(Func<TValue> value)
		{
			if (!dictionary.ContainsKey(key))
			{
				dictionary.Add(key, value());
			}

			return dictionary[key];
		}
	}
}