namespace EyeSoft.Core.Test.SequentialIdentity
{
    using System;
    using System.Data.SqlTypes;
    using EyeSoft.SequentialIdentity;
    using EyeSoft.SequentialIdentity.NewIdProviders;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Generating_ids_over_time
    {
        [TestMethod]
        public void Should_keep_them_ordered_for_sql_server()
        {
            var generator = new NewIdGenerator(new TimeLapseTickProvider(), new NetworkAddressWorkerIdProvider());
            generator.Next();

            int limit = 1024;

            var ids = new NewId[limit];
            for (int i = 0; i < limit; i++)
                ids[i] = generator.Next();

            for (int i = 0; i < limit - 1; i++)
            {
                Assert.AreNotEqual(ids[i], ids[i + 1]);

                SqlGuid left = ids[i].ToGuid();
                SqlGuid right = ids[i + 1].ToGuid();
                //Assert.IsTrue(left.Value < right.Value);
                if (i % 128 == 0)
                    Console.WriteLine("Normal: {0} Sql: {1}", left, ids[i].ToSequentialGuid());
            }
        }


        class TimeLapseTickProvider :
            ITickProvider
        {
            TimeSpan _interval = TimeSpan.FromSeconds(2);
            DateTime _previous = DateTime.UtcNow;

            public long Ticks
            {
                get
                {
                    _previous = _previous + _interval;
                    _interval = TimeSpan.FromSeconds((long)_interval.TotalSeconds + 30);
                    return _previous.Ticks;
                }
            }
        }
    }
}