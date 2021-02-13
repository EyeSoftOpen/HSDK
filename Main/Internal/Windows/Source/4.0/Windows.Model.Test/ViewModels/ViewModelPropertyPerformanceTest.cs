namespace EyeSoft.Windows.Model.Test.ViewModels
{
	using System;
	using System.Diagnostics;

	using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EyeSoft.Windows.Model;

    [TestClass]
	public class ViewModelPropertyPerformanceTest
	{
		private const int Times = 1000;
		private readonly StressedViewModel viewModel = new StressedViewModel();

		private readonly Stopwatch stopwatch = Stopwatch.StartNew();

		[TestMethod]
		public void PerformanceOnPropertyWriting()
		{
			for (var i = 0; i < Times; i++)
			{
				viewModel.Value = i;
			}

			stopwatch.Stop();

			Console.WriteLine("Wrote {0} times the property of the ViewModel in {1}(ms).", Times, stopwatch.ElapsedMilliseconds);
		}

		[TestMethod]
		public void PerformanceOnPropertyReading()
		{
			for (var i = 0; i < Times; i++)
			{
				var value = viewModel.Value;
			}

			stopwatch.Stop();

			Console.WriteLine("Read {0} times the property of the ViewModel in {1}(ms).", Times, stopwatch.ElapsedMilliseconds);
		}

		private class StressedViewModel : ViewModel
		{
			public int Value
			{
				get => GetProperty<int>();
                set => SetProperty(value);
            }
		}
	}
}