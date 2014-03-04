namespace EyeSoft.Data.Nhibernate.Test.Helpers
{
	using System;

	using EyeSoft.Data.Nhibernate.Test.Mapping;
	using EyeSoft.Reflection;

	using NHibernate.Cfg.MappingSchema;
	using NHibernate.Mapping.ByCode;

	using SharpTestsEx;

	internal class MappingHelper
	{
		public static void Verify(HbmMapping mapping, string mappingResourceName)
		{
			var xml = mapping.AsString();

			var expectedMapping =
				typeof(DataAnnotationsTransactionalCollectionTest)
				.Assembly.ReadResourceText(mappingResourceName);

			Clean(xml).Should().Be.EqualTo(Clean(expectedMapping));
		}

		private static string Clean(string xml)
		{
			return xml.Split(new[] { ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries).Join();
		}
	}
}