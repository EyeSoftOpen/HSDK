namespace EyeSoft.Windows.Model.Test.Helpers
{
	using System;
	using System.Collections.Generic;

	internal class TestDefaultDialogService : DefaultDialogService
	{
		private readonly Action action;

		public TestDefaultDialogService(Action action)
		{
			this.action = action;
		}

		protected override object ShowWindow<TViewModel>(TViewModel model, bool isModal, IEnumerable<object> enumerable, bool b)
		{
			action();

			return null;
		}
	}
}