namespace EyeSoft.Windows.Model.Test.Helpers
{
	internal class ChildDialogViewModel : ViewModel
	{
		public ChildDialogViewModel(string name)
		{
			Name = name;
		}

		public string Name { get; set; }
	}
}