namespace EyeSoft.Testing.Domain
{
	using System.Linq;

	using EyeSoft.Domain.Transactional;
	using EyeSoft.Testing.Domain.Helpers.Domain1;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	public abstract class TransactionalCollectionTest : ITransactionalCollectionTest
	{
		public virtual void AddEntityAndGet()
		{
			using (var collection = CreateTransactionalCollection())
			{
				using (var unitOfWork = new SchoolUnitOfWork(collection))
				{
					unitOfWork.SchoolRepository.Save(Known.Schools.School);
					unitOfWork.SchoolRepository.Commit();
				}
			}

			using (var collection = CreateTransactionalCollection())
			{
				using (var unitOfWork = new SchoolUnitOfWork(collection))
				{
					unitOfWork.SchoolRepository.Single()
						.Name.Should().Be.EqualTo(Known.Schools.School.Name);
				}
			}
		}

		public virtual void ReadEntityWithOneToOne()
		{
			using (var collection = CreateTransactionalCollection())
			{
				using (var unitOfWork = new SchoolUnitOfWork(collection))
				{
					unitOfWork.SchoolRepository.Save(Known.Schools.SchoolWithOneChild);
					unitOfWork.SchoolRepository.Commit();
				}
			}

			using (var collection = CreateTransactionalCollection())
			{
				using (var unitOfWork = new SchoolUnitOfWork(collection))
				{
					unitOfWork.ChildOnlyRepository.Single()
						.Street.Should().Be.EqualTo(Known.Schools.SchoolWithOneChild.Children.Single().Street);
				}
			}
		}

		public virtual void ReadEntityWithOneToMany()
		{
			using (var collection = CreateTransactionalCollection())
			{
				using (var unitOfWork = new SchoolUnitOfWork(collection))
				{
					unitOfWork.SchoolRepository.Save(Known.Schools.SchoolWithTwoChildren);
					unitOfWork.SchoolRepository.Commit();
				}
			}

			using (var collection = CreateTransactionalCollection())
			{
				using (var unitOfWork = new SchoolUnitOfWork(collection))
				{
					unitOfWork.ChildOnlyRepository.Count()
						.Should().Be.EqualTo(Known.Schools.SchoolWithTwoChildren.Children.Count);
				}
			}
		}

		public virtual void ReadEntityWithOneToManyLaterAdded()
		{
			using (var collection = CreateTransactionalCollection())
			{
				using (var unitOfWork = new SchoolUnitOfWork(collection))
				{
					unitOfWork.SchoolRepository.Save(Known.Schools.SchoolWithOneChild);
					unitOfWork.SchoolRepository.Commit();
				}
			}

			using (var collection = CreateTransactionalCollection())
			{
				using (var unitOfWork = new SchoolUnitOfWork(collection))
				{
					unitOfWork.ChildOnlyRepository.Count()
						.Should().Be.EqualTo(Known.Schools.SchoolWithOneChild.Children.Count);
				}
			}
		}

		[TestInitialize]
		public virtual void TestInitialize()
		{
		}

		protected abstract ITransactionalCollection CreateTransactionalCollection();
	}
}