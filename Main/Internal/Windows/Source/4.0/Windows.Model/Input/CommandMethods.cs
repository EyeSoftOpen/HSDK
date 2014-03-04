namespace EyeSoft.Windows.Model.Input
{
	using System.Collections.Generic;
	using System.Linq;

	public class CommandMethods
	{
		public CommandMethods(CommandMethod actionMethod, CommandMethod canExecuteMethod, IEnumerable<string> errors)
		{
			ActionMethod = actionMethod;
			CanExecuteMethod = canExecuteMethod;
			Errors = errors;
		}

		public CommandMethod ActionMethod { get; private set; }

		public CommandMethod CanExecuteMethod { get; private set; }

		public IEnumerable<string> Errors { get; private set; }

		public bool HasErrors
		{
			get
			{
				return Errors.Any();
			}
		}
	}
}