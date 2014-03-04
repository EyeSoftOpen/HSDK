namespace EyeSoft.Testing.Domain.Helpers.Domain4
{
	public static class BlogFactory
	{
		public static Blog Create(string email)
		{
			return new Blog(email);
		}
	}
}