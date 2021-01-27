namespace EyeSoft.Core.Collections.Generic
{
    using System.Collections.Generic;

    public static class DictionaryExtensions
    {
        public static IDictionary<TKey, TValue> AddRange<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            foreach (var item in items)
            {
                dictionary.Add(item);
            }

            return dictionary;
        }

        public static DictionaryKey<TKey, TValue> Key<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return new DictionaryKey<TKey, TValue>(dictionary, key);
        }
    }
}