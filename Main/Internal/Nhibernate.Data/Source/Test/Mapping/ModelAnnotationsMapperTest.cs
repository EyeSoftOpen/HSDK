namespace EyeSoft.Data.Nhibernate.Test.Mapping
{
	using System.Linq;

	using EyeSoft.Data.Nhibernate.Mapping;
	using EyeSoft.Reflection;
	using EyeSoft.Testing.Domain.Helpers.Domain2;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using NHibernate.Cfg.MappingSchema;

	using SharpTestsEx;

	[TestClass]
	public class ModelAnnotationsMapperTest
	{
		[TestMethod]
		public void CheckMappingOfDomainWithDataAnnotations()
		{
			var modelAnnotationsMapper =
				new ModelAnnotationsMapper()
					.Map<CustomerAggregate>()
					.Map<HeadOffice>()
					.Map<Order>();

			var mapping =
				modelAnnotationsMapper.CompileMapping();

			mapping
				.RootClasses
				.Length.Should().Be.EqualTo(3);

			CheckCustomerAggregate(mapping.RootClasses.Single(className => className.Name == "CustomerAggregate"));
		}

		private void CheckCustomerAggregate(IPropertiesContainerMapping customerAggregate)
		{
			var nameProperty =
				customerAggregate
					.Properties.SingleOrDefault(p => p.Name == "Name");

			nameProperty.GetField<bool>("notnull").Should().Be.True();
			nameProperty.GetField<int>("length").Should().Be.EqualTo(50);
		}
	}
}