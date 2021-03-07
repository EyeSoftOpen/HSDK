using System.Collections.Generic;
using EyeSoft.Windows.Model;

namespace EyeSoft.Demo.Localization.Windows.ViewModels.Home
{
    public class HomeViewModel : NavigableViewModel, IHomeViewModel
    {
        public HomeViewModel(INavigableViewModel navigableViewModel)
            : base(navigableViewModel)
        {
            Items = new[]
            {
                new SelectableViewModel(true, "3", "Bill Gates", "A chef"),
                new SelectableViewModel(false, "12", "Steve White", "An expert on logistic")
            };
        }

        public IEnumerable<SelectableViewModel> Items { get; }
    }
}