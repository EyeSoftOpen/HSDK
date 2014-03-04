namespace EyeSoft.Domain.Test.Transactional
{
	using System.Linq;

	using EyeSoft.Testing.Domain;
	using EyeSoft.Testing.Domain.Helpers.Domain1;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ReadOnlyUnitOfWorkTest
	{
		[TestMethod]
		public void ReadEntity()
		{
			var readOnlyTransactionalCollection = new ReadOnlyTransactionalCollection(new[] { Known.Schools.School });

			using (var unitOfWork = new SchoolReadOnlyUnitOfWork(readOnlyTransactionalCollection))
			{
				unitOfWork
					.CustomerOnlyRepository
					.Single()
					.Name.Should().Be.EqualTo(Known.Schools.School.Name);
			}
		}
	}
}