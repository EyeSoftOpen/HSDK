namespace EyeSoft.Data
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	using EyeSoft.Serialization;

	internal static class DataSettingsKey
	{
		private static readonly IDictionary<DataScope, Environment.SpecialFolder> scopeToFolder =
			new Dictionary<DataScope, Environment.SpecialFolder>
				{
					{ DataScope.LocalMachine, Environment.SpecialFolder.CommonApplicationData },
					{ DataScope.CurrentUser, Environment.SpecialFolder.ApplicationData },
					{ DataScope.CurrentUserLocal, Environment.SpecialFolder.LocalApplicationData }
				};

		public static string GetPartialPath(ApplicationData applicationData)
		{
			var dataPath = Environment.GetFolderPath(scopeToFolder[applicationData.Scope]);

			dataPath = PathFromApplicationInfo(applicationData.ApplicationInfo, dataPath);

			var folders = applicationData.SubFolders;

			if (folders != null && folders.Any())
			{
				var folderTokens = new List<string> { dataPath };
				folderTokens.AddRange(folders);
				dataPath = Path.Combine(folderTokens.ToArray());
			}

			return dataPath;
		}

		public static string GetFullPathWithExtension(ApplicationData applicationData, string key, bool isProtected)
		{
			var fullPath = GetFullPath(applicationData, key);
			var fullPathWithExtension = Path.ChangeExtension(fullPath, GetExtension(isProtected));

			return fullPathWithExtension;
		}

		public static string GetFullPath(ApplicationData applicationData, string key)
		{
			var partialPath = GetPartialPath(applicationData);

			ValidateKey(key);

			var fullPath = Path.Combine(partialPath, key);

			return fullPath;
		}

		public static string GetExtension(bool isProtected)
		{
			var extension = isProtected ? string.Concat(Serializer.Name, ".secure") : Serializer.Name;

			return extension;
		}

		internal static string KeyOrTypeName<TType>(string key = null)
		{
			return key ?? typeof(TType).FriendlyShortName().Replace('<', '\'').Replace('>', '\'');
		}

		private static void ValidateKey(string key)
		{
			var keyIsValid = key.All(c => char.IsLetterOrDigit(c) || c == '\'');

			if (keyIsValid)
			{
				return;
			}

			throw new ArgumentException(string.Format("Key can contains only letter or digit or \', current value {0}", key));
		}

		private static string PathFromApplicationInfo(ApplicationInfo applicationInfo, string dataPath)
		{
			dataPath = Path.Combine(dataPath, applicationInfo.ToString());
			return dataPath;
		}
	}
}