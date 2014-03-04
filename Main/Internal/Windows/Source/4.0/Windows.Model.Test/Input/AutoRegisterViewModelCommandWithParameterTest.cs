namespace EyeSoft.Windows.Model.Test.Input
{
	using System;
	using System.Windows.Input;

	using EyeSoft.Windows.Model.Input;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class AutoRegisterViewModelCommandWithParameterTest
	{
		[TestMethod]
		public void CheckAssignedCommandIsGenericCommandForMethodWithParameter()
		{
			var viewModel = new ViewModelCommandWithParameter();

			viewModel.ShowChildCommand.Should().Not.Be.Null();

			viewModel.ShowChildCommand.Should().Be.OfType<AsyncCommand<string>>();
		}

		private class ViewModelCommandWithParameter : AutoRegisterViewModel
		{
			public ICommand ShowChildCommand { get; private set; }

			protected void ShowChild(string param)
			{
				Console.WriteLine(param);
			}
		}
	}
}