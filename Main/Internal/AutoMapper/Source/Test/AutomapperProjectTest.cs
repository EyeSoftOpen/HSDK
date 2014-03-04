namespace EyeSoft.AutoMapper.Test
{
	using EyeSoft.AutoMapper;
	using EyeSoft.Mapping;
	using EyeSoft.Test.Helpers;
	using EyeSoft.Test.Mapping;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class AutomapperProjectTest : ProjectTest
	{
		[TestMethod]
		public override void VerifyProjectWorks()
		{
			base.VerifyProjectWorks();
		}

		protected override IProjection Create()
		{
			global::AutoMapper.Mapper.CreateMap<Customer, CustomerProjection>();
			return new AutoMapperProjection();
		}
	}
}