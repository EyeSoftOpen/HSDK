namespace EyeSoft.Core.Test.Collections.Concurrent
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EyeSoft.Collections.Concurrent;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class SafeConcurrentDictionaryTest
	{
		[TestMethod]
		public void CheckContainsKeyAndAddUsingASafeConcurrentDictionaryShouldNotThrow()
		{
			IDictionary<string, int> dictionary = new SafeConcurrentDictionary<string, int>();

			var exception = false;

			Action<int> action =
				x =>
				{
					try
					{
						if (!dictionary.ContainsKey("1"))
						{
							dictionary.Add("1", 23);
						}
					}
					catch
					{
						exception = true;
					}
				};

			Parallel.For(1, Environment.ProcessorCount, action);

			exception.Should().BeFalse();
		}
	}
}