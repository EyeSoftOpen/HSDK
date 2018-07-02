namespace EyeSoft.Windows.Model.Settings
{
    public interface IApplicationSettings<T>
    {
        void Save(T value);

        T Load();
    }
}