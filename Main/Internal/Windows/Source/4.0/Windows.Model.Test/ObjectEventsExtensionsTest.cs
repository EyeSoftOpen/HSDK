namespace EyeSoft.Windows.Model.Test
{
	using System.ComponentModel;
    using EyeSoft.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EyeSoft.Windows.Model;
    using SharpTestsEx;

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
				.Should("An object without event handlers has still references into the handlers dictionary.").Be.False();
		}

		private object RegisterUnregisterHandler()
		{
			var person = new PersonNotify();
			var executed = false;
			person.PropertyChanged += person.Extend().Register<PropertyChangedEventHandler>((s, e) => executed = true);
			executed.Should("The executed variable changed before the event was raised.").Be.False();

			person.Name = "Test";
			executed.Should("The event registered using the Register extension method was not raised.").Be.True();

			executed = false;
			person.Extend().Unregister("PropertyChanged");
			person.Name = "Test2";
			// ReSharper disable once ConditionIsAlwaysTrueOrFalse
			executed.Should("The event was not unregistered from the Unregister extension method.").Be.False();

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