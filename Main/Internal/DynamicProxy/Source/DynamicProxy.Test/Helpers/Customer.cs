namespace EyeSoft.DynamicProxy.Test.Helpers
{
	using System.ComponentModel;

	public class Customer : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public virtual string Name { get; set; }

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			var handler = PropertyChanged;

			if (handler != null)
			{
				handler(this, e);
			}
		}
	}
}