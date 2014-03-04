namespace EyeSoft.Windows.Model.Input
{
	using System;

	public static class CommandsDiscoverFactory
	{
		private static readonly Singleton<CommandsDiscover> singletonInstance =
			new Singleton<CommandsDiscover>(() => new CommandsDiscover(new CommandFactory(true)));

		public static void Set(Func<CommandsDiscover> instance)
		{
			singletonInstance.Set(instance);
		}

		public static CommandsDiscover Create()
		{
			return singletonInstance.Instance;
		}
	}
}