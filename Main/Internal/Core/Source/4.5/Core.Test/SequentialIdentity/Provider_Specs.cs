namespace MassTransit.NewIdTests
{
    using System;
    using System.Threading;
    using EyeSoft.SequentialIdentity.NewIdProviders;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StopwatchTickProvider_Specs
    {
        //[TestMethod]
        //public void Should_keep_accurate_time()
        //{
        //    TimeSpan timeDelta = TimeSpan.FromSeconds(3);

        //    var timestamp = DateTime.UtcNow;
        //    var provider = new StopwatchTickProvider();
        //    long start = provider.Ticks;
        //    Thread.Sleep(timeDelta);
        //    long stop = provider.Ticks;

        //    var startTime = new DateTime(start);
        //    Console.WriteLine("Start time: {0}, Original: {1}", startTime, timestamp);


        //    long deltaTicks = Math.Abs(stop - start);
        //    // 0.01% acceptable delta
        //    var acceptableDelta = timeDelta.Ticks;

        //    Assert.IsTrue(deltaTicks < acceptableDelta);
        //}

        [TestMethod]
        public void Should_not_lag_time()
        {
            TimeSpan timeDelta = TimeSpan.FromMinutes(1);

            var startProvider = new StopwatchTickProvider();
            Thread.Sleep(timeDelta);
            var endProvider = new StopwatchTickProvider();


            long deltaTicks = Math.Abs(endProvider.Ticks - startProvider.Ticks);
            // 0.01% acceptable delta
            var acceptableDelta = (long)(timeDelta.Ticks * 0.0001);

            Assert.IsTrue(deltaTicks < acceptableDelta);
        }
    }
}