namespace EyeSoft.Windows.Model.Test.Helpers
{
	using System;
	using System.Windows;

	using EyeSoft.Conventions;

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