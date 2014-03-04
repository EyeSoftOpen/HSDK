namespace EyeSoft.Data.EntityFramework.Test.Caching.Helpers.Northwind
{
	using System;
	using System.Linq;
	using System.Linq.Expressions;

	using EyeSoft.Domain.Transactional;
	using EyeSoft.EntityFramework.Caching.Demo.Domain;

	internal class CategoryRepository
	{
		private readonly IRepository<Category> repository;

		public CategoryRepository(IRepository<Category> repository)
		{
			this.repository = repository;
		}

		public void Save(Category category)
		{
			repository.Save(category);
		}

		public Category Single(Expression<Func<Category, bool>> clause)
		{
			return repository.Single(clause);
		}
	}
}