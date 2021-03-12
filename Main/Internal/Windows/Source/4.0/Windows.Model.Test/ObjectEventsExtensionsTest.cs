namespace EyeSoft.Windows.Model.Test
{
	using System.ComponentModel;
    using EyeSoft.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EyeSoft.Windows.Model;
    using FluentAssertions;

	[TestClass]
	public class ObjectEventsExtensionsTest
	{
		[TestMethod]
		public void RegisterAnEventHandlerThenUnregisterCheckHandlerIsNotExecuted()
		{
			RegisterUnregisterHandler();
		}

		[TestMethod]
		public void RegisterAnEventHandlerThenUnregisterCheckThereAreNoHandlers()
		{
			var person = RegisterUnregisterHandler();

			person.Extend().HasHandlers()
				.Should().BeFalse("An object without event handlers has still references into the handlers dictionary.");
		}

		private object RegisterUnregisterHandler()
		{
			var person = new PersonNotify();
			var executed = false;
			person.PropertyChanged += person.Extend().Register<PropertyChangedEventHandler>((s, e) => executed = true);
			executed.Should().BeFalse("The executed variable changed before the event was raised.");

			person.Name = "Test";
			executed.Should().BeTrue("The event registered using the Register extension method was not raised.");

			executed = false;
			person.Extend().Unregister("PropertyChanged");
			person.Name = "Test2";
			// ReSharper disable once ConditionIsAlwaysTrueOrFalse
			executed.Should().BeFalse("The event was not unregistered from the Unregister extension method.");

			return person;
		}

		private class PersonNotify : ViewModel
		{
			public string Name
			{
				get => GetProperty<string>();
                set => SetProperty(value);
            }
		}
	}
}