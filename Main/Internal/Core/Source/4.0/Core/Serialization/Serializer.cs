namespace EyeSoft.Core.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Xml.Serialization;

    public static class Serializer
	{
		private static readonly Singleton<IDictionary<string, ISerializerFactory>> singletonInstance =
			new Singleton<IDictionary<string, ISerializerFactory>>(() => new Dictionary<string, ISerializerFactory>());

		public static string Name => DefaultSerializerFactory().TypeName;

        public static void Set<TSerializerFactory>() where TSerializerFactory : ISerializerFactory, new()
		{
			Set(new TSerializerFactory());
		}

		public static void Set(ISerializerFactory serializerFactory)
		{
			if (singletonInstance.Instance.ContainsKey(serializerFactory.TypeName))
			{
				throw new ArgumentException(string.Format("The serializer with name '{0}' already exists.", serializerFactory.TypeName));
			}

			singletonInstance.Instance.Add(serializerFactory.TypeName, serializerFactory);
		}

		public static ISerializer<T> Type<TSerializerFactory, T>() where TSerializerFactory :
			ISerializerFactory, new()
		{
			return Named<T>(new TSerializerFactory().TypeName);
		}

		public static ISerializer<T> Named<T>(string name)
		{
			return singletonInstance.Instance[name].Create<T>();
		}

		public static T DeserializeFromReader<T>(TextReader reader)
		{
			return Instance<T>().DeserializeFromReader(reader);
		}

		public static T DeserializeFromStream<T>(Stream stream)
		{
			return Instance<T>().DeserializeFromStream(stream);
		}

		public static T DeserializeFromString<T>(string value)
		{
			return Instance<T>().DeserializeFromString(value);
		}

		public static void SerializeToStream<T>(T value, Stream stream)
		{
			Instance<T>().SerializeToStream(value, stream);
		}

		public static string SerializeToString<T>(T value)
		{
			return Instance<T>().SerializeToString(value);
		}

		public static void SerializeToWriter<T>(T value, TextWriter writer)
		{
			Instance<T>().SerializeToWriter(value, writer);
		}

		private static ISerializer<T> Instance<T>()
		{
			return DefaultSerializerFactory().Create<T>();
		}

		private static ISerializerFactory DefaultSerializerFactory()
		{
			if (singletonInstance.Instance.Count == 0)
			{
				return new XmlSerializerFactory();
			}

			var defaultSerializerFactory = singletonInstance.Instance.First().Value;

			return defaultSerializerFactory;
		}
	}
}