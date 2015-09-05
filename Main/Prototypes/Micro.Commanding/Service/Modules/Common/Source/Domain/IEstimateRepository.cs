namespace Domain
{
	using System;
	using System.Linq;

	public interface IEstimateRepository : ITransactionalRepository
	{
		IQueryable<Estimate> GetAll();
		Estimate GetById(Guid id);
		void Save(Estimate aggregate);
	}
}