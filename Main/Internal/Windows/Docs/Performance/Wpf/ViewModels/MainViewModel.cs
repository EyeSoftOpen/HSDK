namespace EyeSoft.Docs.Performance.Windows.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EyeSoft.Windows.Model.Collections.ObjectModel;
    using EyeSoft.Windows.Model.ViewModels;

    public class MainViewModel : AutoRegisterViewModel
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