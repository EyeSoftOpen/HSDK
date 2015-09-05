namespace Model.ViewModels.Main.Persisters
{
	public interface IPersister<in T> : IPersister
	{
		void Persist(T value);
	}
}