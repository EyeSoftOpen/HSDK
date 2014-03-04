namespace EyeSoft.AutoMapper.Test
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	using EyeSoft.Mapping;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class AutoMapperTest
	{
		private static readonly AutoMapperMapper autoMapperMapper = new AutoMapperMapper();

		static AutoMapperTest()
		{
			Mapper.Set(() => autoMapperMapper);
		}

		[TestMethod]
		public void AutoMapperIgnoreNonExistingPropertiesInSourceType()
		{
			autoMapperMapper.CreateMap<Source, Destination>();

			const string Expected = "Bill";

			Mapper
				.Map<Destination>(new Source { Name = Expected })
				.Name.Should().Be.EqualTo(Expected);
		}

		[TestMethod]
		public void AutoMapperConvertListToReadOnlyCollection()
		{
			autoMapperMapper.CreateMap<SourceWithCollection, DestinationWithCollection>();

			const string Expected = "Item1";

			Mapper
				.Map<DestinationWithCollection>(new SourceWithCollection { List = new[] { Expected } })
				.List.Should().Have.SameSequenceAs(Expected);
		}

		private class Destination
		{
			public string Name { get; set; }

			public string Code { get; set; }
		}

		private class Source
		{
			public string Name { get; set; }
		}

		private class DestinationWithCollection
		{
			public ReadOnlyCollection<string> List { get; set; }
		}

		private class SourceWithCollection
		{
			public IList<string> List { get; set; }
		}
	}
}