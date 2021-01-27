namespace EyeSoft.Windows.Model.Test.Helpers.Conventions
{
    using System;
    using System.Windows;
    using Core.Conventions;
    using Model.ViewModels;
    using ViewModels;
    using Views;

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