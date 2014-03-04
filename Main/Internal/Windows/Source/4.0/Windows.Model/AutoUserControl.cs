namespace EyeSoft.Windows.Model
{
	using System.Reflection;
	using System.Windows.Controls;

	public class AutoUserControl : UserControl
	{
		public AutoUserControl()
		{
			var initializeComponentMethod = GetType().GetMethod("InitializeComponent", BindingFlags.Public | BindingFlags.Instance);

			if (initializeComponentMethod != null)
			{
				initializeComponentMethod.Invoke(this, new object[0]);
			}
		}
	}
}