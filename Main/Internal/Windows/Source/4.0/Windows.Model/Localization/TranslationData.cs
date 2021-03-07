namespace EyeSoft.Windows.Model.Localization
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    public class TranslationData : IWeakEventListener, INotifyPropertyChanged, IDisposable
    {
        private readonly string key;

        public TranslationData(string key)
        {
            this.key = key;

            LanguageChangedEventManager.AddListener(TranslationManager.Instance, this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public object Value => TranslationManager.Instance.Translate(key);

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType != typeof(LanguageChangedEventManager))
            {
                return false;
            }

            OnLanguageChanged();
            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~TranslationData()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                LanguageChangedEventManager.RemoveListener(TranslationManager.Instance, this);
            }
        }

        private void OnLanguageChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }
    }
}