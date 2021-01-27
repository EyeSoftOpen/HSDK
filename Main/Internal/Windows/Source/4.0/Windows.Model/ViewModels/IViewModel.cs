namespace EyeSoft.Windows.Model.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Core.Validation;

    public interface IViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        IEnumerable<ValidationError> Validate();
    }
}