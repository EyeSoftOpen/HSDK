namespace EyeSoft.Windows.Model.Input
{
    using System.Windows.Input;

    public interface ICommandBuilder
    {
        ICommand Create(IViewModel viewModel, CommandMethods methods);
    }
}