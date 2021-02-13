namespace EyeSoft.Windows.Model
{
	public abstract class ConfirmCancelViewModel<TRet> : ConfirmCancelViewModel, IDialogViewModel<TRet>
	{
		public abstract TRet Result { get; }
	}
}