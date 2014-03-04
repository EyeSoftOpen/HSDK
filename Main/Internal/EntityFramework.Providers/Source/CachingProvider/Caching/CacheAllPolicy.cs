// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace EyeSoft.Data.EntityFramework.Caching
{
	/// <summary>
	/// Implementation of <see cref="CachingPolicy"/> which caches all queries of all size.
	/// </summary>
	internal class CacheAllPolicy : CachingPolicy
	{
		/// <summary>
		/// Determines whether the specified command definition can be cached.
		/// </summary>
		/// <param name="commandDefinition">The command definition.</param>
		/// <returns>
		/// A value of <c>true</c> if the specified command definition can be cached; otherwise, <c>false</c>.
		/// </returns>
		protected internal override bool CanBeCached(CachingCommandDefinition commandDefinition)
		{
			return true;
		}

		/// <summary>
		/// Gets the minimum and maximum number cacheable rows for a given command definition.
		/// </summary>
		/// <param name="cachingCommandDefinition">The command definition.</param>
		/// <param name="minCacheableRows">The minimum number of cacheable rows.</param>
		/// <param name="maxCacheableRows">The maximum number of cacheable rows.</param>
		protected internal override void GetCacheableRows(
			CachingCommandDefinition cachingCommandDefinition,
			out int minCacheableRows,
			out int maxCacheableRows)
		{
			minCacheableRows = 0;
			maxCacheableRows = int.MaxValue;
		}
	}
}