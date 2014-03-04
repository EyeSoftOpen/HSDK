// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace EyeSoft.Data.EntityFramework.Tracing
{
	using System;
	using System.Data.Common;
	using System.Diagnostics.CodeAnalysis;

	using EyeSoft.Data.EntityFramework.Toolkit;

	/// <summary>
	/// EFTracingProvider factory.
	/// </summary>
	[CLSCompliant(false)]
	public class TracingProviderFactory : DbProviderFactoryBase
	{
		/// <summary>
		/// Gets or sets the singleton instance of the provider.
		/// </summary>
		/// <value>The instance.</value>
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Factory is immutale.")]
		public static readonly TracingProviderFactory Instance = new TracingProviderFactory(TracingProviderServices.Instance);

		/// <summary>
		/// Initializes a new instance of the TracingProviderFactory class.
		/// </summary>
		/// <param name="providerProviderServices">The provider services.</param>
		public TracingProviderFactory(DbProviderServices providerProviderServices)
			: base(providerProviderServices)
		{
		}

		/// <summary>
		/// Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbConnection"/> class.
		/// </summary>
		/// <returns>
		/// A new instance of <see cref="T:System.Data.Common.DbConnection"/>.
		/// </returns>
		public override DbConnection CreateConnection()
		{
			return new TracingConnection();
		}
	}
}