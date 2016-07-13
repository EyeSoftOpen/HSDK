namespace EyeSoft.Demo.Validation.Windows.ViewModels
{
	using EyeSoft.Windows.Model;

	public class MainViewModel : ViewModel
	{
		public MainViewModel()
		{
			HierarchicalViewModel = new HierarchicalViewModel();
			SimpleViewModel = new SimpleViewModel();
		}

		public HierarchicalViewModel HierarchicalViewModel { get; }

		public SimpleViewModel SimpleViewModel { get; }

		protected override void Close()
		{
			Dispose(true);
			base.Close();
		}

		protected override void Dispose(bool disposing)
		{
			HierarchicalViewModel.Dispose();
			SimpleViewModel.Dispose();
			base.Dispose(disposing);
		}
	}
}
