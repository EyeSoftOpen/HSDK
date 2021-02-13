namespace EyeSoft.Windows
{
	using System;
	using System.Threading;

	public class ApplicationMutex : IDisposable
	{
		private readonly Mutex mutex;

		internal ApplicationMutex(string applicationId, Mutex mutex)
		{
			ApplicationId = applicationId;
			this.mutex = mutex;

			try
			{
				IsAlreadyRunning = !mutex.WaitOne(0, true);
			}
			catch (AbandonedMutexException)
			{
				mutex.ReleaseMutex();
				IsAlreadyRunning = false;
			}
		}

		public bool IsAlreadyRunning { get; private set; }
		
		public string ApplicationId { get; private set; }

		public void Dispose()
		{
			ApplicationMutexes.Remove(this);

			mutex.Dispose();
		}

		internal ApplicationMutex AsSingle()
		{
			IsAlreadyRunning = false;

			return this;
		}
	}
}