namespace EyeSoft.Windows.Model.Test.ServiceProxy
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EyeSoft.Windows.Model;
    using FluentAssertions;

    [TestClass]
	public class ViewModelExtensionsTest
	{
		private const int PropertyChanges = 10;

		private const string NewValue = "New";

		[TestMethod]
		public void VerifyPropertyChangedOnViewModelIsRaisedWithNewValue()
		{
			ChangePropertyAndVerify(index => NewValue + index, PropertyChanges);
		}

		[TestMethod]
		public void VerifyPropertyChangedOnViewModelIsRaisedWithTheSameValue()
		{
			ChangePropertyAndVerify(index => NewValue, 1);
		}

		private static void ChangePropertyAndVerify(Func<int, string> setNewName, int expectedChanges)
		{
			var vm = new ChangePropertyViewModel(string.Empty);

			var changes = 0;

			vm.PropertyChanged += (s, e) =>
				{
					e.PropertyName.Should().Be("Name");
					changes++;
				};

			Enumerable.Range(1, PropertyChanges).ToList().ForEach(index => vm.Name = setNewName(index));

			changes.Should().Be(expectedChanges);
		}

		private class ChangePropertyViewModel : ViewModel
		{
			private readonly string name;

			public ChangePropertyViewModel(string name)
			{
				this.name = name;
			}

			public string Name
			{
				get => name;
                set => SetProperty(value);
            }
		}
	}
}