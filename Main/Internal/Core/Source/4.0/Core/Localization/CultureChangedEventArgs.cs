namespace EyeSoft.Core.Localization
{
    using System;
    using System.Globalization;

    public class CultureChangedEventArgs : EventArgs
    {
        public CultureChangedEventArgs(CultureInfo cultureInfo)
        {
            CultureInfo = cultureInfo;
        }

        public CultureInfo CultureInfo { get; }
    }
}