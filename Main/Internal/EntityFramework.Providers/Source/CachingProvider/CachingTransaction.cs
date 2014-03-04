// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace EyeSoft.Data.EntityFramework.Caching
{
	using System.Collections.Generic;
	using System.Data.Common;
	using System.Data.Metadata.Edm;
	using System.Linq;

	using EyeSoft.Data.EntityFramework.Toolkit;

	/// <summary>
	/// Implementation of <see cref="DbTransaction"/> for EFCachingProvider.
	/// </summary>
	/// <remarks>
	/// Keeps track of all tables affected by the transaction and invalidates queries dependent on those tables
	/// from the cache.
	/// </remarks>
	public sealed class CachingTransaction : DbTransactionWrapper
	{
		private readonly HashSet<EntitySetBase> affectedEntitySets = new HashSet<EntitySetBase>();

		/// <summary>
		/// Initializes a new instance of the CachingTransaction class.
		/// </summary>
		/// <param name="transaction">The transaction.</param>
		/// <param name="connection">The connection.</param>
		public CachingTransaction(DbTransaction transaction, CachingConnection connection)
			: base(transaction, connection)
		{
		}

		/// <summary>
		/// Gets or sets a value indicating whether this transaction includes modification commands (INSERT, UPDATE, DELETE).
		/// </summary>
		/// <value>
		/// A value of <c>true</c> if this instance includes modification commands; otherwise, <c>false</c>.
		/// </value>
		public bool HasModifications { get; set; }

		/// <summary>
		/// Gets the <see cref="T:System.Data.Common.DbConnection"/> object associated with the transaction.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The <see cref="T:System.Data.Common.DbConnection"/> object associated with the transaction.
		/// </returns>
		private new CachingConnection Connection
		{
			get { return (CachingConnection)base.Connection; }
		}

		/// <summary>
		/// Commits the database transaction.
		/// </summary>
		public override void Commit()
		{
			base.Commit();
			var cache = Connection.Cache;
			if (cache != null && HasModifications)
			{
				cache.InvalidateSets(affectedEntitySets.Select(c => c.Name));
			}
		}

		/// <summary>
		/// Marks the specified entity set as affected by the transaction.
		/// </summary>
		/// <param name="entitySet">The entity set.</param>
		public void AddAffectedEntitySet(EntitySetBase entitySet)
		{
			affectedEntitySets.Add(entitySet);
		}
	}
}