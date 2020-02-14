namespace EyeSoft.Windows.Model.Input
{
	public static class AlwaysCanExecuteCommandFactoryHalper
	{
		public static ICommandFactory Create(bool isAsync, IViewModelChecker viewModelChecker)
		{
			return new AlwaysCanExecuteCommandFactory(isAsync, viewModelChecker);
		}
	}
}