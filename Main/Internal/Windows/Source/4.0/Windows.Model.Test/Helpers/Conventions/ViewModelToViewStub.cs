namespace EyeSoft.Windows.Model.Test.Helpers.Conventions
{
    using System;
    using System.Windows;
    using Core.Conventions;
    using Model.ViewModels;
    using ViewModels;
    using Views;

    internal class ViewModelToViewStub : TypeConvention<ViewModel, Window>
	{
		protected override Type TryMapTo(Type type)
		{
			if (type == typeof(ChildDialogViewModel))
			{
				return typeof(ChildDialogWindow);
			}

			throw new ArgumentException();
		}
	}
}