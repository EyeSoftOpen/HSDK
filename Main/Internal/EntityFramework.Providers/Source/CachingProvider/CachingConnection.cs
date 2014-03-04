// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace EyeSoft.Data.EntityFramework.Caching
{
	using System.Data;
	using System.Data.Common;

	using EyeSoft.Data.EntityFramework.Toolkit;

	/// <summary>
	/// Implementation of <see cref="DbConnection"/> with support for caching of Entity Framework queries.
	/// </summary>
	public class CachingConnection : DbConnectionWrapper
	{
		/// <summary>
		/// Initializes a new instance of the CachingConnection class.
		/// </summary>
		public CachingConnection()
			: this(null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the CachingConnection class.
		/// </summary>
		/// <param name="wrappedConnection">The wrapped connection.</param>
		public CachingConnection(DbConnection wrappedConnection)
		{
			WrappedConnection = wrappedConnection;
			CachingPolicy = CachingProviderConfiguration.DefaultCachingPolicy;
			Cache = CachingProviderConfiguration.DefaultCache;
		}

		/// <summary>
		/// Gets or sets the cache.
		/// </summary>
		/// <value>The cache.</value>
		public IEntityFrameworkCache Cache { get; set; }

		/// <summary>
		/// Gets or sets the caching policy.
		/// </summary>
		/// <value>The caching policy.</value>
		public CachingPolicy CachingPolicy { get; set; }

		/// <summary>
		/// Gets the name of the default wrapped provider.
		/// </summary>
		/// <returns>Name of the default wrapped provider.</returns>
		protected override string DefaultWrappedProviderName
		{
			get { return CachingProviderConfiguration.DefaultWrappedProvider; }
		}

		/// <summary>
		/// Gets the <see cref="T:System.Data.Common.DbProviderFactory"/> for this <see cref="T:System.Data.Common.DbConnection"/>.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Data.Common.DbProviderFactory"/>.
		/// </returns>
		protected override DbProviderFactory DbProviderFactory
		{
			get { return CachingProviderFactory.Instance; }
		}

		/// <summary>
		/// Starts a database transaction.
		/// </summary>
		/// <param name="isolationLevel">Specifies the isolation level for the transaction.</param>
		/// <returns>
		/// An object representing the new transaction.
		/// </returns>
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			return new CachingTransaction(WrappedConnection.BeginTransaction(isolationLevel), this);
		}
	}
}