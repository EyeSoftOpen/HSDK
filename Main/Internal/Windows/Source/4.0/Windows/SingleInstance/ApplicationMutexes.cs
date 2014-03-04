namespace EyeSoft.Windows
{
	using System.Collections.Generic;
	using System.Threading;

	internal static class ApplicationMutexes
	{
		private static readonly IDictionary<string, Mutex> mutexDictionary = new Dictionary<string, Mutex>();

		public static Mutex CreateOrExisting(string applicationInstanceId)
		{
			lock (mutexDictionary)
			{
				var mutexExists = mutexDictionary.ContainsKey(applicationInstanceId);

				var mutex = mutexExists ? mutexDictionary[applicationInstanceId] : new Mutex(true, applicationInstanceId);

				return mutex;
			}
		}

		public static void Remove(ApplicationMutex applicationMutex)
		{
			lock (mutexDictionary)
			{
				if (mutexDictionary.ContainsKey(applicationMutex.ApplicationId))
				{
					mutexDictionary.Remove(applicationMutex.ApplicationId);
				}
			}
		}
	}
}