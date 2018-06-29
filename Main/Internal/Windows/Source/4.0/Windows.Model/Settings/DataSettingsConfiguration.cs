namespace EyeSoft.Windows.Model.Settings
{
    public class DataSettingsConfiguration
    {
        internal DataSettingsConfiguration(ApplicationData applicationData, bool isProtected, string key)
        {
            Data = applicationData;

            Key = key;

            Protected = isProtected;

            Extension = DataSettingsKey.GetExtension(Protected);

            Path = DataSettingsKey.GetFullPathWithExtension(applicationData, key, isProtected);
        }

        public ApplicationData Data { get; private set; }

        public string Path { get; private set; }

        public string Key { get; private set; }

        public bool Protected { get; private set; }

        public string Extension { get; private set; }

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