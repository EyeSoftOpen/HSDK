// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace EyeSoft.Data.EntityFramework.Caching
{
	using System;
	using System.Data.Common;
	using System.Data.Entity.Core.Objects;

	using EyeSoft.Data.EntityFramework.Toolkit;

	/// <summary>
	/// Extension methods for EFCachingProvider.
	/// </summary>
	public static class CachingExtensionMethods
	{
		/// <summary>
		/// Gets the cache associated with this connection.
		/// </summary>
		/// <param name="connection">The connection.</param>
		/// <returns><see cref="ICacheWithStatistics"/> implementation.</returns>
		public static IEntityFrameworkCache GetCache(this DbConnection connection)
		{
			return connection.UnwrapConnection<CachingConnection>().Cache;
		}

		/// <summary>
		/// Gets the cache associated with the object context.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns>Object context.</returns>
		public static IEntityFrameworkCache GetCache(this ObjectContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			return GetCache(context.Connection);
		}

		/// <summary>
		/// Sets the <see cref="ICacheWithStatistics"/> implementation for given connection.
		/// </summary>
		/// <param name="connection">The connection.</param>
		/// <param name="cache">The cache.</param>
		public static void SetCache(this DbConnection connection, IEntityFrameworkCache cache)
		{
			connection.UnwrapConnection<CachingConnection>().Cache = cache;
		}

		/// <summary>
		/// Sets the <see cref="ICacheWithStatistics"/> implementation for given object context.
		/// </summary>
		/// <param name="context">The object context.</param>
		/// <param name="cache">The cache.</param>
		public static void SetCache(this ObjectContext context, IEntityFrameworkCache cache)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			SetCache(context.Connection, cache);
		}
	}
}