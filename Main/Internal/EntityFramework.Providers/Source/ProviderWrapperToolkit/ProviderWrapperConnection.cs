// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace EyeSoft.Data.EntityFramework.Toolkit
{
	using System;
	using System.Data.Common;

	/// <summary>
	/// <see cref="DbConnection"/> implementation for EFProviderWrapper
	/// </summary>
	internal class ProviderWrapperConnection : DbConnectionWrapper
	{
		/// <summary>
		/// Gets the <see cref="T:System.Data.Common.DbProviderFactory"/> for this <see cref="T:System.Data.Common.DbConnection"/>.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Data.Common.DbProviderFactory"/>.
		/// </returns>
		protected override DbProviderFactory DbProviderFactory
		{
			get
			{
				return ProviderWrapperFactory.Instance;
			}
		}

		/// <summary>
		/// Gets the default wrapped provider.
		/// </summary>
		/// <returns>Name of the default wrapped provider.</returns>
		protected override string DefaultWrappedProviderName
		{
			get { throw new NotImplementedException(); }
		}
	}
}