namespace EyeSoft.Windows.Model.DialogService
{
	public interface IDialogViewModel<out TRet>
	{
		TRet Result { get; }
	}
}