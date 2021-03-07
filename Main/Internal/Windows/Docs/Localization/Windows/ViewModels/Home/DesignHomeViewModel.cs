using System.Collections.Generic;

#if DEBUG
namespace EyeSoft.Demo.Localization.Windows.ViewModels.Home
{
    public class DesignHomeViewModel : IHomeViewModel
    {
        public DesignHomeViewModel()
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
#endif