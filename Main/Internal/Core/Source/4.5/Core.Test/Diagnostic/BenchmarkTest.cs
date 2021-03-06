﻿namespace EyeSoft.Core.Test.Diagnostic
{
    using EyeSoft.Diagnostic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class BenchmarkTest
	{
		[TestMethod]
		public void VerifyBenchmarkWorks()
		{
			int[] actionExecuted = { 0 };
			const int Times = 3;

			Benchmark
				.For(Times)
					.Execute(() => actionExecuted[0]++)
					.Execute(() => actionExecuted[0]++)
				.Report();

			actionExecuted[0]
				.Should().Be(Times * 2);
		}
	}
}