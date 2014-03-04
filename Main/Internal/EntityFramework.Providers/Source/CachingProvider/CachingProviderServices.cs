// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace EyeSoft.Data.EntityFramework.Caching
{
	using System.Data.Common;
	using System.Data.Common.CommandTrees;

	using EyeSoft.Data.EntityFramework.Toolkit;

	/// <summary>
	/// Implementation of <see cref="DbProviderServices"/> for EFCachingProvider.
	/// </summary>
	internal class CachingProviderServices : DbProviderServicesBase
	{
		/// <summary>
		/// Initializes static members of the CachingProviderServices class.
		/// </summary>
		static CachingProviderServices()
		{
			Instance = new CachingProviderServices();
		}

		/// <summary>
		/// Prevents a default instance of the CachingProviderServices class from being created.
		/// </summary>
		private CachingProviderServices()
		{
		}

		/// <summary>
		/// Gets the singleton instance of <see cref="CachingProviderServices"/>.
		/// </summary>
		/// <value>The singleton instance.</value>
		public static CachingProviderServices Instance { get; private set; }

		/// <summary>
		/// Gets the default name of the wrapped provider.
		/// </summary>
		/// <returns>
		/// Default name of the wrapped provider (to be used when
		/// provider is not specified in the connction string)
		/// </returns>
		protected override string DefaultWrappedProviderName
		{
			get { return CachingProviderConfiguration.DefaultWrappedProvider; }
		}

		/// <summary>
		/// Gets the provider invariant name.
		/// </summary>
		/// <returns>Provider invariant name.</returns>
		protected override string ProviderInvariantName
		{
			get { return "EFCachingProvider"; }
		}

		/// <summary>
		/// Creates the command definition wrapper.
		/// </summary>
		/// <param name="wrappedCommandDefinition">The wrapped command definition.</param>
		/// <param name="commandTree">The command tree.</param>
		/// <returns>
		/// The <see cref="DbCommandDefinitionWrapper"/> object.
		/// </returns>
		public override DbCommandDefinitionWrapper CreateCommandDefinitionWrapper(
			DbCommandDefinition wrappedCommandDefinition,
			DbCommandTree commandTree)
		{
			return new CachingCommandDefinition(wrappedCommandDefinition, commandTree);
		}
	}
}