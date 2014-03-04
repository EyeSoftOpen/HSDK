// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace EyeSoft.Data.EntityFramework.Toolkit
{
	using System;
	using System.Data.Common;
	using System.Data.Common.CommandTrees;

	/// <summary>
	/// Implementation of common methods of <see cref="DbCommandDefinition"/> class.
	/// </summary>
	public class DbCommandDefinitionWrapper : DbCommandDefinition
	{
		private readonly DbCommandDefinition wrappedCommandDefinition;
		private readonly DbCommandTree commandTree;
		private readonly Func<DbCommand, DbCommandDefinitionWrapper, DbCommand> commandCreator;

		/// <summary>
		/// Initializes a new instance of the DbCommandDefinitionWrapper class.
		/// </summary>
		/// <param name="wrappedCommandDefinition">The wrapped command definition.</param>
		/// <param name="commandTree">The command tree.</param>
		/// <param name="commandCreator">The command creator delegate.</param>
		public DbCommandDefinitionWrapper(
			DbCommandDefinition wrappedCommandDefinition,
			DbCommandTree commandTree,
			Func<DbCommand, DbCommandDefinitionWrapper, DbCommand> commandCreator)
		{
			this.wrappedCommandDefinition = wrappedCommandDefinition;
			this.commandTree = commandTree;
			this.commandCreator = commandCreator;
		}

		/// <summary>
		/// Gets the command tree.
		/// </summary>
		/// <value>The command tree.</value>
		public DbCommandTree CommandTree
		{
			get { return this.commandTree; }
		}

		/// <summary>
		/// Gets the wrapped command definition.
		/// </summary>
		/// <value>The wrapped command definition.</value>
		protected DbCommandDefinition WrappedCommandDefinition
		{
			get { return wrappedCommandDefinition; }
		}

		/// <summary>
		/// Creates and returns a <see cref="T:System.Data.Common.DbCommandDefinition"/> object associated with the current connection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Data.Common.DbCommandDefinition"/>.
		/// </returns>
		public override DbCommand CreateCommand()
		{
			return commandCreator(wrappedCommandDefinition.CreateCommand(), this);
		}
	}
}