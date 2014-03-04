// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace EyeSoft.Data.EntityFramework.Toolkit
{
	using System;
	using System.Data.Common;

	/// <summary>
	/// <see cref="DbProviderServices" /> implementation for EFProviderWrapper
	/// </summary>
	internal class ProviderWrapperServices : DbProviderServicesBase
	{
		/// <summary>
		/// Initializes static members of the ProviderWrapperServices class.
		/// </summary>
		static ProviderWrapperServices()
		{
			Instance = new ProviderWrapperServices();
		}

		/// <summary>
		/// Gets the singleton instance of <see cref="ProviderWrapperServices"/>.
		/// </summary>
		/// <value>The singleton instance.</value>
		internal static ProviderWrapperServices Instance { get; private set; }

		/// <summary>
		/// Gets the name default of the wrapped provider.
		/// </summary>
		/// <returns>Name of the default wrapped provider.</returns>
		protected override string DefaultWrappedProviderName
		{
			get { throw new NotSupportedException("Default wrapped provider is not supported"); }
		}

		/// <summary>
		/// Gets the provider invariant name.
		/// </summary>
		/// <returns>Provider invariant name: 'EFProviderWrapper'</returns>
		protected override string ProviderInvariantName
		{
			get { return "EFProviderWrapper"; }
		}
	}
}