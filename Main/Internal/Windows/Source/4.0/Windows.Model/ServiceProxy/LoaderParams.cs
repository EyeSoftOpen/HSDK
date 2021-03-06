﻿namespace EyeSoft.Windows.Model
{
	using System;

	internal class LoaderParams<TService> where TService : IDisposable
	{
		public LoaderParams(Func<TService> proxyCreator, bool useTaskFactory)
		{
			UseTaskFactory = useTaskFactory;
			ProxyCreator = proxyCreator;
		}

		public bool UseTaskFactory { get; set; }

		public Func<TService> ProxyCreator { get; private set; }
	}
}