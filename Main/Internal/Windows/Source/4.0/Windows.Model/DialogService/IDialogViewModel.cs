namespace EyeSoft.Windows.Model
{
	public interface IDialogViewModel<out TRet>
	{
		TRet Result { get; }
	}
}