// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace EyeSoft.Data.EntityFramework.Toolkit
{
	using System.Data.Common;
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// Implementation of <see cref="DbProviderFactory"/> for EFProviderWrapper.
	/// </summary>
	public class ProviderWrapperFactory : DbProviderFactoryBase
	{
		/// <summary>
		/// Gets the singleton instance of the EFProviderWrapper factory.
		/// </summary>
		/// <value>The singleton instance.</value>
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Class is immutable")]
		public static readonly ProviderWrapperFactory Instance = new ProviderWrapperFactory();

		/// <summary>
		/// Prevents a default instance of the ProviderWrapperFactory class from being created.
		/// </summary>
		private ProviderWrapperFactory()
			: base(ProviderWrapperServices.Instance)
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
			return new ProviderWrapperConnection();
		}
	}
}