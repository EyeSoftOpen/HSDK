namespace EyeSoft.Windows.Model.Test.ViewModels
{
	using System;
	using System.Collections.Generic;

	using EyeSoft.Collections.Generic;
	using EyeSoft.Reflection;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ViewModelReactPropertyChangingTest
	{
		private static readonly string primary = Reflector.PropertyName<ReactViewModel>(x => x.Primary);

		[TestMethod]
		public void VerifyViewModelPropertyChangingActionIsExecuted()
		{
			const string Value1 = "Value1";
			const string Value2 = "Value2";
			const string Value3 = "Value3";

			var viewModel = new ReactViewModel { Primary = Value1 };

			viewModel.Primary = Value2;
			viewModel.Primary = Value3;

			viewModel
				.ChangesLog
				.Should().Have.SameSequenceAs(
					new Tuple<string, string>(primary, null),
					new Tuple<string, string>(primary, Value1),
					new Tuple<string, string>(primary, Value2));
		}

		private class ReactViewModel : ViewModel
		{
			private readonly TupleList<string, string> changesLog = new TupleList<string, string>();

			public ReactViewModel()
			{
				Property(() => Primary).OnChanging(AddChange);
			}

			public string Primary
			{
				get { return GetProperty<string>(); }
				set { SetProperty(value); }
			}

			public IEnumerable<Tuple<string, string>> ChangesLog
			{
				get { return changesLog; }
			}

			private void AddChange(string propertyValue)
			{
				changesLog.Add(primary, propertyValue);
			}
		}
	}
}