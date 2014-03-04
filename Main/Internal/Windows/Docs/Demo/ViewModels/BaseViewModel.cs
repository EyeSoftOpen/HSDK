namespace EyeSoft.Windows.Model.Demo.ViewModels
{
	using System.Windows.Input;

	public abstract class BaseViewModel : AutoRegisterViewModel
	{
		public ICommand ShowChildCommand { get; private set; }

		protected abstract void ShowChild();
	}
}