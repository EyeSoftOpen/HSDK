namespace EyeSoft.Windows.Model.Test.ViewModels
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.ViewModels;
    using SharpTestsEx;

	[TestClass]
	public class ViewModelPropertyAssignmentTest
	{
		[TestMethod]
		public void ViewModelMultipleAssignDifferentValuePropertyChangesShouldBeMore()
		{
			var valuePropertyViewModel = new ValuePropertyViewModel();

			var propertyChanged = false;

			valuePropertyViewModel.PropertyChanged += (s, e) => propertyChanged = true;
			valuePropertyViewModel.Changed.Should().Be.False();

			const int Value = 3;

			valuePropertyViewModel.Value = Value;
			valuePropertyViewModel.Value = Value;
			valuePropertyViewModel.Value.Should().Be.EqualTo(Value);

			valuePropertyViewModel.Changes.Should().Be.EqualTo(1);
			valuePropertyViewModel.Changed.Should().Be.True();

			propertyChanged.Should().Be.True();
		}

		[TestMethod]
		public void ViewModelMultipleAssignSameInstanceOfTheSameObjectReferencePropertyChangesShouldBeMore()
		{
			var valuePropertyViewModel = new ReferencePropertyViewModel();

			var propertyChanged = false;

			valuePropertyViewModel.PropertyChanged += (s, e) => propertyChanged = true;

			valuePropertyViewModel.Changed.Should().Be.False();

			var value = new Foo { Id = 3 };

			valuePropertyViewModel.Value = value;
			valuePropertyViewModel.Value.Should().Be.SameInstanceAs(value);

			valuePropertyViewModel.Value = value;
			valuePropertyViewModel.Value.Should().Be.SameInstanceAs(value);

			valuePropertyViewModel.Changes.Should().Be.EqualTo(1);
			valuePropertyViewModel.Changed.Should().Be.True();

			propertyChanged.Should().Be.True();
		}

		[TestMethod]
		public void ViewModelMultipleAssignDifferentInstanceOfTheSameObjectReferencePropertyChangesShouldBeMore()
		{
			var valuePropertyViewModel = new ReferencePropertyViewModel();

			var propertyChanged = false;

			valuePropertyViewModel.PropertyChanged += (s, e) => propertyChanged = true;

			valuePropertyViewModel.Changed.Should().Be.False();

			var value = new Foo { Id = 3 };
			valuePropertyViewModel.Value = value;
			valuePropertyViewModel.Value.Should().Be.SameInstanceAs(value);

			var value2 = new Foo { Id = 3 };
			valuePropertyViewModel.Value = value2;
			valuePropertyViewModel.Value.Should().Be.SameInstanceAs(value);

			valuePropertyViewModel.Changes.Should().Be.EqualTo(1);
			valuePropertyViewModel.Changed.Should().Be.True();

			propertyChanged.Should().Be.True();
		}

		private class ValuePropertyViewModel : ViewModel
		{
			public int Value
			{
				get => GetProperty<int>();
                set => SetProperty(value);
            }
		}

		private class ReferencePropertyViewModel : ViewModel
		{
			public Foo Value
			{
				get => GetProperty<Foo>();
                set => SetProperty(value);
            }
		}

		private class Foo
		{
			public int Id { get; set; }

			public override bool Equals(object obj)
			{
				if (obj == null || GetType() != obj.GetType())
				{
					return false;
				}

				var other = (Foo)obj;
				return Id.Equals(other.Id);
			}

			public override int GetHashCode()
			{
				return Id;
			}
		}
	}
}