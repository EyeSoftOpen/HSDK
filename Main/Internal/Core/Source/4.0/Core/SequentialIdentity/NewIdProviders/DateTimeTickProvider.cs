namespace EyeSoft.Core.SequentialIdentity.NewIdProviders
{
    using System;

    public class DateTimeTickProvider :
        ITickProvider
    {
        public long Ticks => DateTime.UtcNow.Ticks;
    }
}