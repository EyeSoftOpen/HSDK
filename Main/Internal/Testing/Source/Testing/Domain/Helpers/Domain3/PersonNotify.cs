namespace EyeSoft.Testing.Domain.Helpers.Domain3
{
	using EyeSoft.Windows.Model;

	public class PersonNotify : ViewModel
	{
		private string name;

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
				OnPropertyChanged("Name");
			}
		}
	}
}