namespace EyeSoft.Docs.Performance.Wpf.ViewModels
{
	using EyeSoft.Windows.Model;

	public class PartyItemViewModel : ViewModel
	{
		public PartyItemViewModel(int value1, int value2)
		{
			Value1 = value1;
			Value2 = value2;
		}

		public int Value1
		{
			get { return GetProperty<int>(); }
			private set { SetProperty(value); }
		}

		public int Value2
		{
			get { return GetProperty<int>(); }
			private set { SetProperty(value); }
		}

		public override string ToString()
		{
			return string.Concat(Value1, " - ", Value2);
		}
	}
}