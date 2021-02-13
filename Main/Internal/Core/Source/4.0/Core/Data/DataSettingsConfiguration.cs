namespace EyeSoft.Data
{
	public class DataSettingsConfiguration
	{
		internal DataSettingsConfiguration(ApplicationData applicationData, bool isProtected, string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new System.ArgumentException("message", nameof(key));
			}

			Data = applicationData ?? throw new System.ArgumentNullException(nameof(applicationData));

			Key = key;

			Protected = isProtected;

			Extension = DataSettingsKey.GetExtension(Protected);

			Path = DataSettingsKey.GetFullPathWithExtension(applicationData, key, isProtected);
		}

		public ApplicationData Data { get; }

		public string Path { get; }

		public string Key { get; }

		public bool Protected { get; }

		public string Extension { get; }

		public override string ToString()
		{
			return Path;
		}

		internal void Register<T>(ApplicationDataSettings<T> applicationDataSettings)
		{
			Data.Register(applicationDataSettings);
		}
	}
}