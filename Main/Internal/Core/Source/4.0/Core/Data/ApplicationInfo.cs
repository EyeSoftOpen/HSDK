namespace EyeSoft.Data
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Reflection;

	using EyeSoft.Reflection;

	public class ApplicationInfo
	{
		private static readonly Singleton<ApplicationInfo> singletonInstance = new Singleton<ApplicationInfo>(() => FromEntryAssembly());

		private readonly IDictionary<string, object> settingsDictionary = new Dictionary<string, object>();

		private readonly string fullName;

		public ApplicationInfo(string company, string product, Version version = null)
		{
			Company = company;
			Product = product.TrimStart(string.Concat(company, "."));
			Version = version;

			var versionName = Version != null ? Version.ToString() : string.Empty;

			fullName = Path.Combine(Company, Product, versionName);
		}

		public static ApplicationInfo Instance
		{
			get { return singletonInstance.Instance; }
		}

		public string Company { get; private set; }

		public string Product { get; private set; }

		public Version Version { get; private set; }

		public static ApplicationInfo FromEntryAssembly(bool includeVersion = false)
		{
			var assembly = Assembly.GetEntryAssembly();

			if (assembly == null)
			{
				const string Message =
					"To save or read protected data is necessary to provide "
					+ "the Assembly if the Assembly.GetEntryAssembly() returns null.";

				throw new InvalidOperationException(Message);
			}

			var company = assembly.Company();
			var product = assembly.Product();

			var version = includeVersion ? assembly.GetName().Version : null;

			var applicationInfo = new ApplicationInfo(company, product, version);

			return applicationInfo;
		}

		public static void Set(ApplicationInfo applicationInfo)
		{
			singletonInstance.Set(() => applicationInfo);
		}

		public ApplicationDataSettings<T> Settings<T>(string key = null)
		{
			return Settings<T>(key, DataScope.CurrentUser);
		}

		public ApplicationDataSettings<T> Settings<T>(string key, DataScope scope)
		{
			return Settings<T>(key, null, scope);
		}

		public ApplicationDataSettings<T> Settings<T>(params string[] subFolders)
		{
			return Settings<T>(null, subFolders);
		}

		public ApplicationDataSettings<T> Settings<T>(DataScope scope, params string[] subFolders)
		{
			return Settings<T>(null, subFolders, scope);
		}

		public ApplicationDataSettings<T> Settings<T>(bool isProtected, string key = null)
		{
			return Settings<T>(key, isProtected);
		}

		public ApplicationDataSettings<T> Settings<T>(string key, string[] subFolders, DataScope scope = DataScope.CurrentUser)
		{
			return Settings<T>(key, false, subFolders, scope);
		}

		public ApplicationDataSettings<T> Settings<T>(bool isProtected, string[] subFolders)
		{
			return Settings<T>(null, isProtected, subFolders);
		}

		public ApplicationDataSettings<T> Settings<T>(
			string key, bool isProtected, string[] subFolders = null, DataScope scope = DataScope.CurrentUser)
		{
			var applicationData = new ApplicationData(this, scope, subFolders);

			key = DataSettingsKey.KeyOrTypeName<T>(key);

			var fullKey = DataSettingsKey.GetFullPath(applicationData, key);

			ApplicationDataSettings<T> applicationDataSettings;

			if (settingsDictionary.ContainsKey(fullKey))
			{
				applicationDataSettings = (ApplicationDataSettings<T>)settingsDictionary[fullKey];

				var fullKeyWithExtension = DataSettingsKey.GetFullPathWithExtension(applicationData, key, isProtected);

				if (!applicationDataSettings.Configuration.Path.Equals(fullKeyWithExtension))
				{
					var message = string.Format("Cannot change the ApplicationDataSettings with key {0} if already exists.", key);

					throw new InvalidOperationException(message);
				}

				return applicationDataSettings;
			}

			applicationDataSettings = applicationData.Settings<T>(isProtected, key);

			return applicationDataSettings;
		}

		public ApplicationData Data(DataScope scope = DataScope.CurrentUser, params string[] subFolders)
		{
			var applicationData = new ApplicationData(this, scope, subFolders);

			return applicationData;
		}

		public override string ToString()
		{
			return fullName;
		}

		internal void RegisterSettings<T>(ApplicationDataSettings<T> applicationDataSettings)
		{
			var key = DataSettingsKey.GetFullPath(applicationDataSettings.Configuration.Data, applicationDataSettings.Configuration.Key);

			settingsDictionary.Add(key, applicationDataSettings);
		}
	}
}