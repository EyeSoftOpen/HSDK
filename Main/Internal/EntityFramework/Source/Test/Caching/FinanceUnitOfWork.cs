namespace EyeSoft.Data.EntityFramework.Test.Caching
{
	using System.Data.Entity;

	internal class FinanceUnitOfWork : DbContext
	{
		public FinanceUnitOfWork(string nameOrConnectionString) : base(nameOrConnectionString)
		{
		}

		public IDbSet<Customer> CustomerRepository
		{
			get { return Set<Customer>(); }
		}
	}
}