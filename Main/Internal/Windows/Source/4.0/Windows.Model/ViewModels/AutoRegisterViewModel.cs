using EyeSoft.Windows.Model.Input;

namespace EyeSoft.Windows.Model.ViewModels
{
	public abstract class AutoRegisterViewModel : ViewModel
	{
		protected AutoRegisterViewModel()
		{
			CommandsDiscoverFactory.Create().Discover(this);
		}
	}
}