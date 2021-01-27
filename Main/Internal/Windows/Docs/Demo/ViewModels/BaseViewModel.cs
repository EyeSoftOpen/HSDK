namespace EyeSoft.Windows.Model.Demo.ViewModels
{
	using System.Windows.Input;
    using Model.ViewModels;

    public abstract class BaseViewModel : AutoRegisterViewModel
	{
		public ICommand ShowChildCommand { get; private set; }

		protected abstract void ShowChild();
	}
}