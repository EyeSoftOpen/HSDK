namespace EyeSoft.Data
{
	public interface IApplicationSettings<T>
	{
		void Save(T value);

		T Load();
	}
}