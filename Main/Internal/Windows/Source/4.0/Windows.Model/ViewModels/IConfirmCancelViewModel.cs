namespace EyeSoft.Windows.Model
{
	using System.Windows.Input;

	public interface IConfirmCancelViewModel
	{
		ICommand ConfirmCommand { get; }

		ICommand CancelCommand { get; }
	}
}