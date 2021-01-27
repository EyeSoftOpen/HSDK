namespace EyeSoft.Windows.Model.Input
{
    using DialogService;
    using ViewModels;

    public class DefaultViewModelChecker : IViewModelChecker
	{
		private readonly string title;
		private readonly string message;

		public DefaultViewModelChecker(string title = "There are validation errors", string message = "Check this errors")
		{
			this.title = title;
			this.message = message;
		}
		public void Check(IViewModel viewModel)
		{
			var error = viewModel.Error;

			DialogService.ShowMessage(title, $"{message}\r\n{error}");
		}
	}
}