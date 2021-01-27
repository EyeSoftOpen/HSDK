namespace EyeSoft.Windows.Model.ViewModels
{
    using Input;

    public abstract class AutoRegisterViewModel : ViewModel
	{
		protected AutoRegisterViewModel()
		{
			CommandsDiscoverFactory.Create().Discover(this);
		}
	}
}