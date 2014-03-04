namespace EyeSoft.Windows.Model.Test.Helpers
{
	using System;
	using System.Windows;

	using EyeSoft.Conventions;

	internal class ViewToViewModelStub : TypeConvention<Window, ViewModel>
	{
		protected override Type TryMapTo(Type type)
		{
			if (type == typeof(MainWindow))
			{
				return typeof(MainViewModel);
			}

			if (type == typeof(ChildDialogWindow))
			{
				return typeof(ChildDialogViewModel);
			}

			throw new ArgumentException();
		}
	}
}