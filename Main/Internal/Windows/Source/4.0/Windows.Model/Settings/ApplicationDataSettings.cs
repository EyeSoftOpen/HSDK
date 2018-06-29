namespace EyeSoft.Windows.Model.Settings
{
    using System;
    using IO;
    using Security.Cryptography;
    using Serialization;

    public class ApplicationDataSettings<T> : IApplicationDataSettings<T>
    {
        internal ApplicationDataSettings(DataSettingsConfiguration configuration)
        {
            Configuration = configuration;

            configuration.Register(this);
        }

        public DataSettingsConfiguration Configuration { get; private set; }

        public void Save(T value)
        {
            SaveFromString(Serializer.SerializeToString(value));
        }

        public T Load(Func<T> defaultValue = null)
        {
            var loadToString = LoadToString();

            if (loadToString == null)
            {
                return defaultValue == null ? default(T) : defaultValue();
            }

            var value = Serializer.DeserializeFromString<T>(loadToString);

            return value;
        }

        public override string ToString()
        {
            return Configuration.ToString();
        }

        private void SaveFromString(string data)
        {
            SaveFromByteArray(data.ToByteArray());
        }

        private void SaveFromByteArray(byte[] data)
        {
            var dataPath = Configuration.Path;

            var directory = Storage.File(dataPath).Directory.FullName;
            Storage.Directory(directory).Create();

            Storage.WriteAllBytes(dataPath, Configuration.Protected ? data.Protect() : data);
        }

        private string LoadToString()
        {
            var loadDataToByteArray = LoadToByteArray();

            return loadDataToByteArray == null ? null : loadDataToByteArray.GetString();
        }

        private byte[] LoadToByteArray()
        {
            var dataPath = Configuration.Path;

            if (!Storage.File(dataPath).Exists)
            {
                return null;
            }

            var unprotectedData = Configuration.Protected ? Storage.ReadAllBytes(dataPath).Unprotect() : Storage.ReadAllBytes(dataPath);

            return unprotectedData;
        }
    }
}