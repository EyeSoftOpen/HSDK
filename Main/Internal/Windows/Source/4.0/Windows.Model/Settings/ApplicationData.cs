namespace EyeSoft.Windows.Model.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class ApplicationData
    {
        private readonly IList<ApplicationData> childrenList = new List<ApplicationData>();

        internal ApplicationData(ApplicationData parent, IEnumerable<string> subFolders)
            : this(parent.ApplicationInfo, parent.Scope, subFolders)
        {
            Parent = parent;
        }

        internal ApplicationData(ApplicationInfo applicationInfo, DataScope scope, IEnumerable<string> subFolders)
        {
            ApplicationInfo = applicationInfo;
            Scope = scope;

            var subFoldersArray = subFolders == null ? null : subFolders.ToArray();

            if (subFoldersArray != null)
            {
                if (subFoldersArray.Any(string.IsNullOrWhiteSpace))
                {
                    throw new ArgumentException("The sub folders must not be empty.");
                }

                SubFolders = new ReadOnlyCollection<string>(subFoldersArray.ToList());
            }

            Path = DataSettingsKey.GetPartialPath(this);

            Children = new ReadOnlyCollection<ApplicationData>(childrenList);
        }

        public ApplicationInfo ApplicationInfo { get; private set; }

        public ApplicationData Parent { get; private set; }

        public ReadOnlyCollection<ApplicationData> Children { get; private set; }

        public string Path { get; private set; }

        public ReadOnlyCollection<string> SubFolders { get; private set; }

        public DataScope Scope { get; private set; }

        public ApplicationData Append(params string[] subFolders)
        {
            var list = new List<string>();

            if (SubFolders != null)
            {
                list.AddRange(SubFolders);
            }

            list.AddRange(subFolders);

            var child = new ApplicationData(this, list);

            childrenList.Add(child);

            return child;
        }

        public ApplicationDataSettings<T> Settings<T>(string key = null)
        {
            return Settings<T>(false, key);
        }

        public ApplicationDataSettings<T> Settings<T>(bool isProtected, string key = null)
        {
            key = DataSettingsKey.KeyOrTypeName<T>(key);

            var configuration = new DataSettingsConfiguration(this, isProtected, key);

            var applicationDataSettings = new ApplicationDataSettings<T>(configuration);

            return applicationDataSettings;
        }

        public override string ToString()
        {
            return Path;
        }

        internal void Register<T>(ApplicationDataSettings<T> applicationDataSettings)
        {
            ApplicationInfo.RegisterSettings(applicationDataSettings);
        }
    }
}