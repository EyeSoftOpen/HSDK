namespace EyeSoft.Testing.Domain.Helpers.Domain4.Trasactional
{
	using System.Linq;

	using EyeSoft.Domain.Transactional;

	public class BlogRepository
	{
		private readonly IRepository<Blog> repository;

		public BlogRepository(IRepository<Blog> repository)
		{
			this.repository = repository;
		}

		public Blog GetByOwner(string email)
		{
			return repository.Single(blog => blog.OwnerEmail == email);
		}

		public void Save(Blog blog)
		{
			repository.Save(blog);
		}
	}
}