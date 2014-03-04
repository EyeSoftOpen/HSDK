namespace EyeSoft.Windows.Model.Test.Input
{
	using System.Windows.Input;

	using Castle.MicroKernel.Registration;
	using Castle.Windsor;

	using EyeSoft.Windows.Model.Input;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class AutoRegisterViewModelTest
	{
		[TestMethod]
		public void GetActionMethodFromViewModelByConvention()
		{
			IWindsorContainer container = new WindsorContainer();
			container.Register(Component.For<CommandConvention>().ImplementedBy<TestCommandConvention>());
			container.Register(Component.For<CommandsDiscover>());
			container.Register(Component.For<TestViewModel>());

			var viewModel1 = container.Resolve<TestViewModel>();

			viewModel1.ShowChildCommand.Should().Not.Be.Null();
		}

		private class TestViewModel : AutoRegisterViewModel
		{
			public ICommand ShowChildCommand { get; private set; }

			protected void ShowChild()
			{
			}

			protected bool CanShowChild()
			{
				return true;
			}
		}

		private class TestCommandConvention : CommandConvention
		{
		}
	}
}