namespace EyeSoft.DynamicProxy.Test
{
	using System.ComponentModel;

	using EyeSoft.DynamicProxy.Interceptors.Notifier;
	using EyeSoft.DynamicProxy.Test.Helpers;
	using EyeSoft.Reflection;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ProxyBuilderTest
	{
		private const string KnownName = "Bill";

		[TestMethod]
		public void SetPropertyAndCheckPropertyChangedEventIsRaised()
		{
			var customer =
				new ProxyBuilder()
					.MakeNotifyPropertyChanged<Customer>();

			var propertyChangedRaised = false;

			customer.PropertyChanged += (s, e) => { CheckPropertyChanged(s, e); propertyChangedRaised = true; };
			customer.Name = KnownName;

			propertyChangedRaised
				.Should()
				.Be
				.True();
		}

		[TestMethod]
		public void SetPropertyWithNoPropertyChangedSubscriptionNotThrowException()
		{
			var customer = new ProxyBuilder().MakeNotifyPropertyChanged<Customer>();

			Executing
				.This(() => customer.Name = KnownName)
				.Should().NotThrow();
		}

		private void CheckPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			e
				.PropertyName
				.Should()
				.Be
				.EqualTo("Name");

			sender
				.GetPropertyValue(e.PropertyName)
				.Should()
				.Be
				.EqualTo(KnownName);
		}
	}
}