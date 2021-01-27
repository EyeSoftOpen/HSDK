namespace EyeSoft.Windows.Model.Test.ServiceProxy
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.ViewModels;
    using SharpTestsEx;

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
					e.PropertyName.Should().Be.EqualTo("Name");
					changes++;
				};

			Enumerable.Range(1, PropertyChanges).ToList().ForEach(index => vm.Name = setNewName(index));

			changes.Should().Be.EqualTo(expectedChanges);
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
				get
				{
					return name;
				}
				set
				{
					SetProperty(value);
				}
			}
		}
	}
}