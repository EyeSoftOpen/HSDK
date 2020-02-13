namespace EyeSoft.Windows.Model
{
    using EyeSoft.Validation;
    using System.Collections.Generic;
    using System.ComponentModel;

    public interface IViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        IEnumerable<ValidationError> Validate();
    }
}