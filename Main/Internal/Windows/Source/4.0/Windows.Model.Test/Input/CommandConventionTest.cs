namespace EyeSoft.Windows.Model.Test.Input
{
	using System;
	using System.Windows.Input;
    using EyeSoft.Reflection;
    using EyeSoft.Windows.Model.Input;

	using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EyeSoft.Windows.Model;
    using FluentAssertions;

	[TestClass]
	public class CommandConventionTest
	{
		[TestMethod]
		public void GetActionMethodFromViewModelByConvention()
		{
			var methods =
				new CommandConvention()
					.Get(typeof(ViewModel1), Reflector.Property<ViewModel1>(x => x.ShowChildCommand));

			methods.ActionMethod.MethodInfo.Name
				.Should().Be("ShowChild", "Cannot find the associated method to the command.");

			methods.CanExecuteMethod.MethodInfo.Name
				.Should().Be("CanShowChild", "Cannot find the associated can method to the command.");

			methods.ActionMethod.IsAsync
				.Should().BeTrue("The discovered command should be identified as async.");
		}

		[TestMethod]
		public void NoSuitableMethodActionForCommandGivesAnError()
		{
			var commandProperty =
				Reflector.Property<ViewModelNoValidActionName>(x => x.ShowChildCommand);

			var methods =
				new CommandConvention()
				.Get(typeof(ViewModelNoValidActionName), commandProperty);

			methods.Errors.Should().HaveCount(2);
		}

		[TestMethod]
		public void IssuesWithMultipleCommandsGiveAUniqueExceptionWithAllDetails()
		{
			Action discoverCommand =
				() =>
					new CommandsDiscover()
						.Discover(new ViewModelNoValidCommands());

			const string ExpectedExceptionMessage =
				"Issues found on type ViewModelNoValidCommands:" + "\r\n" +
				"- command NewCustomerCommand" + "\r\n" +
				" - missing action: looking for NewCustomer or SyncNewCustomer method" + "\r\n" +
				"- command NewOrderCommand" + "\r\n" +
				" - missing action: looking for NewOrder or SyncNewOrder method";

            discoverCommand
                .Should().Throw<InvalidOperationException>()
				.And
                .Message.Should().Be(ExpectedExceptionMessage);
		}

		[TestMethod]
		public void PreAssignedCommandsAreNotModifiedFromTheDiscover()
		{
			var viewModel = new ViewModelNoConsiderAssignedCommands();

			viewModel.NewCustomerCommand.Should().BeOfType<MyAsyncCommand>();
		}

		private class ViewModel1
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

		private class ViewModelNoValidActionName : AutoRegisterViewModel
		{
			public ICommand ShowChildCommand { get; private set; }

			protected void ShowChildWrong()
			{
			}
		}

		private class ViewModelNoValidCommands : AutoRegisterViewModel
		{
			public ICommand NewCustomerCommand { get; private set; }

			public ICommand NewOrderCommand { get; private set; }
		}

		private class ViewModelNoConsiderAssignedCommands : AutoRegisterViewModel
		{
			public ViewModelNoConsiderAssignedCommands()
			{
				NewCustomerCommand = new MyAsyncCommand(this, () => { });
			}

			public ICommand NewCustomerCommand { get; private set; }

			protected void NewCustomer()
			{
			}
		}

		private class MyAsyncCommand : AsyncRefreshCommand
		{
			public MyAsyncCommand(IViewModel viewModel, Action action)
				: base(viewModel, action)
			{
			}
		}
	}
}