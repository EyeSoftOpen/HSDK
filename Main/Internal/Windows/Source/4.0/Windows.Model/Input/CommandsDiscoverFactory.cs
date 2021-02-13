namespace EyeSoft.Windows.Model.Input
{
	using System;

	public static class CommandsDiscoverFactory
	{
		private static readonly Singleton<ICommandsDiscover> singletonInstance =
			new Singleton<ICommandsDiscover>(() => new CommandsDiscover(new CommandFactory(true)));

		public static void Set(Func<ICommandsDiscover> instance)
		{
			singletonInstance.Set(instance);
		}

		public static ICommandsDiscover Create()
		{
			return singletonInstance.Instance;
		}
	}
}