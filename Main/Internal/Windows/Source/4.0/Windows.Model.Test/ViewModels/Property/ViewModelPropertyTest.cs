namespace EyeSoft.Windows.Model.Test.ViewModels.Property
{
	using System;
	using System.Collections.Generic;

	using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;

	[TestClass]
	public class ViewModelPropertyTest
	{
		[TestMethod]
		public void CheckOnChangedAction()
		{
			var changed = new List<string>();
			var changing = new List<string>();

			var notifyViewModel = new NotifyViewModel(changed, changing);
			
			const string PropertyName = "Property";

			var dependent = new Dictionary<string, string[]>();
			Action<string, string[]> addToDependent = dependent.Add;

			var property = new ViewModelProperty<int>(notifyViewModel, addToDependent, PropertyName);
			var viewModelProperty = (IViewModelProperty<int>)property;

			var values = new List<int>();
			viewModelProperty.OnChanged(values.Add);

			const int Value = 10;

			property.SetValue(Value, false).Should().Be.True();
			property.Changed(() => Value);

			changing.Should().Have.SameSequenceAs(PropertyName);
			changed.Should().Have.SameSequenceAs(PropertyName);

			values.Should().Have.SameSequenceAs(Value);
		}

		private class NotifyViewModel : INotifyViewModel
		{
			private readonly ICollection<string> changed;

			private readonly ICollection<string> changing;

			public NotifyViewModel(ICollection<string> changed, ICollection<string> changing)
			{
				this.changed = changed;
				this.changing = changing;
			}

			public void OnPropertyChanging(string propertyName)
			{
				changing.Add(propertyName);
			}

			public void OnPropertyChanged(string propertyName)
			{
				changed.Add(propertyName);
			}
		}
	}
}
