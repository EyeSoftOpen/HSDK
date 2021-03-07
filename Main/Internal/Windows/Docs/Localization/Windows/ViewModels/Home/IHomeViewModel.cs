using System.Collections.Generic;

namespace EyeSoft.Demo.Localization.Windows.ViewModels.Home
{
    public interface IHomeViewModel
    {
        IEnumerable<SelectableViewModel> Items { get; }
    }
}