namespace EyeSoft.Windows.Model.Test.ViewModels
{
	using System;
	using System.Collections.Generic;
    using EyeSoft.Collections.Generic;
    using EyeSoft.Reflection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EyeSoft.Windows.Model;
    using SharpTestsEx;

	[TestClass]
	public class ViewModelReactPropertyChangedTest
	{
		private const string Once = "Once";

		private static readonly string primary =
			Reflector.PropertyName<ReactViewModel>(x => x.Primary);

		[TestMethod]
		public void VerifyViewModelPropertyChangedActionIsExecuted()
		{
			const string Value1 = "Value1";
			const string Value2 = "Value2";

			var viewModel = new ReactViewModel { Primary = Value1 };

			viewModel.Primary = Value2;

			viewModel.ChangesLog
				.Should().Have.SameSequenceAs(
					new Tuple<string, string>(primary, Once),
					new Tuple<string, string>(primary, Value1),
					new Tuple<string, string>(primary, Value2));
		}

		private class ReactViewModel : ViewModel
		{
			private readonly TupleList<string, string> changes = new TupleList<string, string>();

			public ReactViewModel()
			{
				Property(() => Primary)
					.OnFirstChange(x => changes.Add(primary, Once))
					.OnChanged(x => changes.Add(primary, x));
			}

			public string Primary
			{
				get => GetProperty<string>();
                set => SetProperty(value);
            }

			public IEnumerable<Tuple<string, string>> ChangesLog => changes;
        }
	}
}