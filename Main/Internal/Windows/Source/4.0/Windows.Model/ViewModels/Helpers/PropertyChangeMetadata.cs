namespace EyeSoft.Windows.Model.ViewModels.Helpers
{
    using System;

    internal class PropertyChangeMetadata
	{
		private readonly Delegate action;

		private readonly int timesToExecute;

		private int changes;

		public PropertyChangeMetadata(int timesToExecute, Delegate action)
		{
			this.timesToExecute = timesToExecute;
			this.action = action;
		}

		public bool HasToExecute
		{
			get { return (timesToExecute == 0) || (changes < timesToExecute); }
		}

		public bool CanRemove
		{
			get { return (timesToExecute != 0) && (changes > timesToExecute); }
		}

		public void Execute(object value)
		{
			action.DynamicInvoke(value);
		}

		public void Changed()
		{
			changes++;
		}
	}
}