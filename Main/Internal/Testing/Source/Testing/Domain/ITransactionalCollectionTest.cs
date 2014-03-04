namespace EyeSoft.Testing.Domain
{
	public interface ITransactionalCollectionTest
	{
		void AddEntityAndGet();

		void ReadEntityWithOneToOne();

		void ReadEntityWithOneToMany();

		void ReadEntityWithOneToManyLaterAdded();
	}
}