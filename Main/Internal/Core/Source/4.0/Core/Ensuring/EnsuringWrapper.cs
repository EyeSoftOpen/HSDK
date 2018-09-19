namespace EyeSoft
{
	internal class EnsuringWrapper
		: IEnsuring
	{
		private EnsuringWrapper()
		{
		}

		public static IEnsuring Create()
		{
			return new EnsuringWrapper();
		}

		INamedCondition<T> IEnsuring.That<T>(T value)
		{
			return
				Ensure.That(value);
		}
	}
}