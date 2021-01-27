namespace EyeSoft.Windows.Model.Input
{
    using ViewModels;

    public interface ICommandSetter
	{
		void AssignAllCommands(IViewModel viewModel);
	}
}