namespace EyeSoft.Docs.Performance.Wpf.ViewModels
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using EyeSoft.Windows.Model;
	using EyeSoft.Windows.Model.Collections.ObjectModel;

	public class MainViewModel : ViewModel
	{
		private readonly IObservableCollection<PartyItemViewModel> partyList = CollectionFactory.Create<PartyItemViewModel>();

		public MainViewModel()
		{
			Task.Run(() => Enumerable.Range(1, 10000).ToList().ForEach(x => partyList.Add(new PartyItemViewModel(x, x * 2))));
		}

		public IEnumerable<PartyItemViewModel> PartyList
		{
			get { return partyList; }
		}
	}
}