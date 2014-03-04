namespace EyeSoft.Windows.Model
{
	using EyeSoft.Windows.Model.Input;

	public abstract class AutoRegisterViewModel : ViewModel
	{
		protected AutoRegisterViewModel()
		{
			CommandsDiscoverFactory.Create().Discover(this);
		}
	}
}