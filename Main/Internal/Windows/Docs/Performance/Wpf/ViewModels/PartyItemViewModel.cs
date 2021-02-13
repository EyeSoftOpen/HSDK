namespace EyeSoft.Docs.Performance.Windows.ViewModels
{
    using System.Windows.Input;
    using EyeSoft.Windows.Model;

    public class PartyItemViewModel : AutoRegisterViewModel
	{
		public PartyItemViewModel(int value1, int value2)
		{
			Value1 = value1;
			Value2 = value2;
		}

		public int Value1
		{
			get => GetProperty<int>();
            private set => SetProperty(value);
        }

		public int Value2
		{
			get => GetProperty<int>();
            private set => SetProperty(value);
        }

        public Area Area
        {
            get => GetProperty<Area>();
            private set => SetProperty(value);
        }

        public ICommand UpdateValueCommand { get; private set; }

		public override string ToString()
		{
			return string.Concat(Value1, " - ", Value2);
		}

		public void UpdateValue(PartyItemViewModel value)
		{
            Area = value.Area;
        }

		public bool CanUpdateValue(PartyItemViewModel value)
		{
			return true;
		}
	}

	public enum Area
	{
		Mastership,
		Agenda
	}
}