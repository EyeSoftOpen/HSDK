// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace EyeSoft.Data.EntityFramework.Toolkit
{
	using System.Data.Common;

	/// <summary>
	/// Implementation of common methods for <see cref="DbTransaction"/> wrappers.
	/// </summary>
	public abstract class DbTransactionWrapper : DbTransaction
	{
		private readonly DbTransaction wrappedTransaction;
		private readonly DbConnectionWrapper connectionWrapper;

		/// <summary>
		/// Initializes a new instance of the <see cref="DbTransactionWrapper"/> class.
		/// </summary>
		/// <param name="wrappedTransaction">The wrapped transaction.</param>
		/// <param name="connection">The connection.</param>
		protected DbTransactionWrapper(DbTransaction wrappedTransaction, DbConnectionWrapper connection)
		{
			this.wrappedTransaction = wrappedTransaction;
			connectionWrapper = connection;
		}

		/// <summary>
		/// Specifies the <see cref="T:System.Data.IsolationLevel"/> for this transaction.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The <see cref="T:System.Data.IsolationLevel"/> for this transaction.
		/// </returns>
		public override System.Data.IsolationLevel IsolationLevel
		{
			get { return wrappedTransaction.IsolationLevel; }
		}

		/// <summary>
		/// Gets the wrapped transaction.
		/// </summary>
		/// <value>The wrapped transaction.</value>
		public DbTransaction WrappedTransaction
		{
			get { return wrappedTransaction; }
		}

		/// <summary>
		/// Specifies the <see cref="T:System.Data.Common.DbConnection"/> object associated with the transaction.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The <see cref="T:System.Data.Common.DbConnection"/> object associated with the transaction.
		/// </returns>
		protected override DbConnection DbConnection
		{
			get { return connectionWrapper; }
		}

		/// <summary>
		/// Gets the connection wrapper.
		/// </summary>
		/// <value>The connection wrapper.</value>
		protected DbConnectionWrapper ConnectionWrapper
		{
			get { return connectionWrapper; }
		}

		/// <summary>
		/// Commits the database transaction.
		/// </summary>
		public override void Commit()
		{
			wrappedTransaction.Commit();
		}

		/// <summary>
		/// Rolls back a transaction from a pending state.
		/// </summary>
		public override void Rollback()
		{
			wrappedTransaction.Rollback();
		}
	}
}