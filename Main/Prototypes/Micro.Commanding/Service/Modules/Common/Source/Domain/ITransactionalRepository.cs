namespace Domain
{
	using System;

	public interface ITransactionalRepository : IDisposable
	{
		void Commit();
	}
}