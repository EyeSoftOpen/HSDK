namespace EyeSoft.Normalization
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;

	public static class Normalizer
	{
		private static readonly DefaultNormalizer defaultNormalizer = new DefaultNormalizer();

		public static void Set(INormalizer normalizer)
		{
			defaultNormalizer.Set(normalizer);
		}

		public static void Normalize(object obj)
		{
			defaultNormalizer.Normalize(obj);
		}

		public static void Register<T>(INormalizer<T> normalizer) where T : class
		{
			defaultNormalizer.Register(normalizer);
		}

		public static void Register<T, TNormalizer>() where TNormalizer : INormalizer<T>, new() where T : class
		{
			defaultNormalizer.Register<T, TNormalizer>();
		}

		public static void Register(Type type, INormalizer normalizer)
		{
			defaultNormalizer.Register(type, normalizer);
		}

		public static void Remove()
		{
			defaultNormalizer.Remove();
		}

		public static void Remove<T>() where T : class
		{
			defaultNormalizer.Remove<T>();
		}

		public static void Remove(Type type)
		{
			defaultNormalizer.Remove(type);
		}

		public static void NormalizeProperties<T>(object obj, IEnumerable<PropertyInfo> properties, Func<T, T> normalize)
		{
			defaultNormalizer.NormalizeProperties(obj, properties, normalize);
		}

		public static void NormalizeProperties<T>(object obj, Func<T, T> normalize)
		{
			defaultNormalizer.NormalizeProperties(obj, normalize);
		}

		public static void TrimProperties(object obj)
		{
			defaultNormalizer.TrimProperties(obj);
		}

		public static IEnumerable<PropertyInfo> GetStringProperties(object obj)
		{
			return defaultNormalizer.GetStringProperties(obj);
		}

		public static IEnumerable<PropertyInfo> GetPrimitiveProperties(object obj)
		{
			return defaultNormalizer.GetPrimitiveProperties(obj);
		}
	}
}