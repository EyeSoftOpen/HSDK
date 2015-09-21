namespace EyeSoft.Architecture.Prototype.Windows.Model.Base
{
	public interface IPersister<in T> : IPersister
	{
		void Persist(T value);
	}
}