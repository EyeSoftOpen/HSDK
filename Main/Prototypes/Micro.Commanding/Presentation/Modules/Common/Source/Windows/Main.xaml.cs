namespace EyeSoft.Architecture.Prototype.Windows
{
	using System.Diagnostics;
	using System.Windows.Navigation;

	public partial class Main
	{
		public Main()
		{
			InitializeComponent();
		}

		private void OnHyperlinkRequestNavigate(object sender, RequestNavigateEventArgs e)
		{
			Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
			e.Handled = true;
		}
	}
}