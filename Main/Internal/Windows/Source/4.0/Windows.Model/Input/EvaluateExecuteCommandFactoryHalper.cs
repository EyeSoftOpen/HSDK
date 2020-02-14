namespace EyeSoft.Windows.Model.Input
{
    public static class EvaluateExecuteCommandFactoryHalper
	{
		public static ICommandFactory Create(bool isAsync)
		{
			return new CommandFactory(isAsync);
		}
	}
}