namespace EyeSoft.Windows.Model.Input
{
    using System.Windows.Input;
    using ViewModels;

    public interface ICommandBuilder
    {
        ICommand Create(IViewModel viewModel, CommandMethods methods);
    }
}