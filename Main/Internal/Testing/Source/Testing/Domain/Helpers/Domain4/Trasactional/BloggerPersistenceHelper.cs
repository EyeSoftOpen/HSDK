namespace EyeSoft.Testing.Domain.Helpers.Domain4.Trasactional
{
	using System;
	using System.Linq;

	using EyeSoft.Data.Common;

	using SharpTestsEx;

	public static class BloggerPersistenceHelper
	{
		public static void Check(Func<BloggerUnitOfWork> createUnitOfWork, IDatabaseProvider databaseProvider = null)
		{
			const string KnownEmail = "me@domain.com";
			const string KnownTitle = "New post";

			if (databaseProvider != null)
			{
				databaseProvider.DropIfExists();
			}

			using (var unitOfWork = createUnitOfWork())
			{
				unitOfWork.BlogRepository.Save(BlogFactory.Create(KnownEmail));
				unitOfWork.Commit();
			}

			using (var unitOfWork = createUnitOfWork())
			{
				var blog = unitOfWork.BlogRepository.GetByOwner(KnownEmail);
				blog.AddPost(KnownTitle, DateTime.Now);
				unitOfWork.BlogRepository.Save(blog);
				unitOfWork.Commit();
			}

			using (var unitOfWork = createUnitOfWork())
			{
				var blog = unitOfWork.BlogRepository.GetByOwner(KnownEmail);
				blog.Posts.Single()
					.Title.Should().Be.EqualTo(KnownTitle);
			}
		}
	}
}