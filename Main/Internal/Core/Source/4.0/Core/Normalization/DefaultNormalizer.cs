namespace EyeSoft.Normalization
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Reflection;

    public class DefaultNormalizer
	{
		private readonly IDictionary<string, EnumerablePropertyInfo> stringPropertiesCache =
			new Dictionary<string, EnumerablePropertyInfo>();

		private readonly IDictionary<string, EnumerablePropertyInfo> primitivePropertiesCache =
			new Dictionary<string, EnumerablePropertyInfo>();

		private readonly IDictionary<Type, INormalizer> typeNormalizers =
			new Dictionary<Type, INormalizer>();

		private readonly Singleton<INormalizer> defaultNormalizer;

		private readonly INormalizer nullNormalizer = new NullNormalizer();

		private readonly INormalizer traverseNormalizer;

		public DefaultNormalizer()
		{
			defaultNormalizer = new Singleton<INormalizer>(() => new CustomOrTrimNormalizer(CustomNormalizeOrTrim));

			traverseNormalizer = new TraverseNormalizer(defaultNormalizer.Instance.Normalize);
		}

		public void Set(INormalizer normalizer)
		{
			defaultNormalizer.Set(() => normalizer);
		}

		public void Normalize(object obj)
		{
			traverseNormalizer.Normalize(obj);
		}

		public void Register<T>(INormalizer<T> normalizer) where T : class
		{
			Register(typeof(T), normalizer);
		}

		public void Register<T, TNormalizer>()
			where TNormalizer : INormalizer<T>, new()
			where T : class
		{
			Register(typeof(T), new TNormalizer());
		}

		public void Register(Type type, INormalizer normalizer)
		{
			if (typeNormalizers.ContainsKey(type))
			{
				const string Format = "The type '{0}' has already specified the normalizer '{1}'.";
				var message = string.Format(Format, type.Name, normalizer.GetType().Name);
				throw new ArgumentException(message);
			}

			typeNormalizers.Add(type, normalizer);
		}

		public void Remove()
		{
			defaultNormalizer.Set(() => new NullNormalizer());
		}

		public void Remove<T>() where T : class
		{
			Remove(typeof(T));
		}

		public void Remove(Type type)
		{
			if (typeNormalizers.ContainsKey(type))
			{
				var message = $"The normalizer for the type '{type.Name}' has already been removed.";
				throw new ArgumentException(message);
			}

			typeNormalizers.Add(type, nullNormalizer);
		}

		public void TrimProperties(object obj)
		{
			var properties = GetStringProperties(obj);

            NormalizeProperties(obj, properties, (Func<string, string>) Trim);
		}

		public void NormalizeProperties<T>(object obj, Func<T, T> normalize)
		{
			NormalizeProperties(obj, GetPrimitiveProperties(obj), normalize);
		}

		public void NormalizeProperties<T>(object obj, IEnumerable<PropertyInfo> properties, Func<T, T> normalize)
		{
			foreach (var property in properties)
			{
				var value = (T)property.GetValue(obj, null);
				property.SetValue(obj, normalize(value), null);
			}
		}

		public IEnumerable<PropertyInfo> GetStringProperties(object obj)
		{
			return stringPropertiesCache.GetProperties(obj.GetType(), PropertyPredicates.String);
		}

		public IEnumerable<PropertyInfo> GetPrimitiveProperties(object obj)
		{
			return primitivePropertiesCache.GetProperties(obj.GetType(), PropertyPredicates.Primitive);
		}

        protected  virtual string Trim(string value)
        {
            var trimmed = string.IsNullOrWhiteSpace(value) ? null : value.Trim();

            return trimmed;
        }

        private void CustomNormalizeOrTrim(object obj)
		{
			var type = obj.GetType();

			var isCustomNormalizerDefined = typeNormalizers.ContainsKey(type);

			if (isCustomNormalizerDefined)
			{
				var normalizer = typeNormalizers[type];
				normalizer.Normalize(obj);
				return;
			}

			TrimProperties(obj);
		}

		private class TraverseNormalizer : INormalizer
		{
			private readonly Action<object> normalize;

			public TraverseNormalizer(Action<object> normalize)
			{
				this.normalize = normalize;
			}

			void INormalizer.Normalize(object obj)
			{
				ObjectTree.Traverse(obj, normalize);
			}
		}

		private class CustomOrTrimNormalizer : INormalizer
		{
			private readonly Action<object> customNormalizeOrTrim;

			public CustomOrTrimNormalizer(Action<object> customNormalizeOrTrim)
			{
				this.customNormalizeOrTrim = customNormalizeOrTrim;
			}

			void INormalizer.Normalize(object obj)
			{
				customNormalizeOrTrim(obj);
			}
		}

		private class NullNormalizer : INormalizer
		{
			void INormalizer.Normalize(object obj)
			{
			}
		}
	}
}