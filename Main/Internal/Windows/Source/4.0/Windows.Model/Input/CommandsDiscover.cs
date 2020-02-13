namespace EyeSoft.Windows.Model.Input
{

	public class CommandsDiscover : ICommandsDiscover
	{
		private readonly ICommandSetter commandSetter;

		public CommandsDiscover() : this(new CommandFactory(), new CommandConvention())
		{
		}

		public CommandsDiscover(ICommandFactory commandFactory) : this(commandFactory, new CommandConvention())
		{
		}

		public CommandsDiscover(ICommandConvention commandConvention) : this(new CommandFactory(), commandConvention)
		{
		}

		public CommandsDiscover(ICommandFactory commandFactory, ICommandConvention commandConvention, ICommandSetter commandSetter = null)
		{
			CommandFactory = commandFactory;
			CommandConvention = commandConvention;
			this.commandSetter = commandSetter ?? new CommandSetter(commandFactory, commandConvention);
		}

        public ICommandConvention CommandConvention { get; }

        internal ICommandFactory CommandFactory { get; }

        public void Discover(IViewModel viewModel)
		{
			commandSetter.AssignAllCommands(viewModel);
		}
	}
}