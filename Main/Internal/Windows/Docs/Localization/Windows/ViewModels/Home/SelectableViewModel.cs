namespace EyeSoft.Demo.Localization.Windows.ViewModels.Home
{
    public class SelectableViewModel
    {
        public SelectableViewModel(bool isSelected, string code, string name, string description)
        {
            IsSelected = isSelected;
            Code = code;
            Name = name;
            Description = description;
        }

        public bool IsSelected { get; set; }

        public string Code { get; }

        public string Name { get; }

        public string Description { get; }
    }
}