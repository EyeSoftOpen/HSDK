namespace Domain
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Infrastructure;
	using System.Linq;

	public class EstimateRepository : IEstimateRepository
	{
		private readonly FinancialDbContext db;

		public EstimateRepository(FinancialDbContext db)
		{
			this.db = db;
		}

		public IQueryable<Estimate> GetAll()
		{
			return db.EstimateRepository;
		}
		public Estimate GetById(Guid id)
		{
			return db.EstimateRepository.Find(id);
		}
		public void Save(Estimate aggregate)
		{
			var manager = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager;

			try
			{
				if (manager.GetObjectStateEntry(aggregate).State == EntityState.Detached)
				{
					db.EstimateRepository.Add(aggregate);
				}
			}
			catch
			{
				db.EstimateRepository.Add(aggregate);
			}
		}

		public void Commit()
		{
			db.SaveChanges();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}


		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}

			db?.Dispose();
		}
	}
}