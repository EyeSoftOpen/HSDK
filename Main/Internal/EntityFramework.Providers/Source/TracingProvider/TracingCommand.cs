// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace EyeSoft.Data.EntityFramework.Tracing
{
	using System;
	using System.Data;
	using System.Data.Common;
	using System.Diagnostics;
	using System.Threading;

	using EyeSoft.Data.EntityFramework.Toolkit;

	/// <summary>
	/// Implementation of <see cref="DbCommand"/> which traces all commands executed.
	/// </summary>
	internal class TracingCommand : DbCommandWrapper
	{
		private static int globalCommandID;

		/// <summary>
		/// Initializes a new instance of the TracingCommand class.
		/// </summary>
		/// <param name="wrappedCommand">The wrapped command.</param>
		/// <param name="commandDefinition">The command definition.</param>
		public TracingCommand(DbCommand wrappedCommand, DbCommandDefinitionWrapper commandDefinition)
			: base(wrappedCommand, commandDefinition)
		{
			CommandID = Interlocked.Increment(ref globalCommandID);
		}

		/// <summary>
		/// Gets the unique command ID.
		/// </summary>
		/// <value>The command ID.</value>
		public int CommandID { get; private set; }

		/// <summary>
		/// Gets the <see cref="TracingConnection"/> used by this <see cref="T:System.Data.Common.DbCommand"/>.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The connection to the data source.
		/// </returns>
		private new TracingConnection Connection
		{
			get { return (TracingConnection)base.Connection; }
		}

		/// <summary>
		/// Executes the query and returns the first column of the first row in the result set
		/// returned by the query. All other columns and rows are ignored.
		/// </summary>
		/// <returns>
		/// The first column of the first row in the result set.
		/// </returns>
		public override object ExecuteScalar()
		{
			var e =
				new CommandExecutionEventArgs(this, "ExecuteScalar")
					{
						Status = CommandExecutionStatus.Executing
					};
			Connection.RaiseExecuting(e);

			var sw = new Stopwatch();
			try
			{
				sw.Start();
				var result = base.ExecuteScalar();
				sw.Stop();
				e.Result = result;
				e.Duration = sw.Elapsed;
				e.Status = CommandExecutionStatus.Finished;
				Connection.RaiseFinished(e);
				return result;
			}
			catch (Exception ex)
			{
				e.Result = ex;
				e.Status = CommandExecutionStatus.Failed;
				Connection.RaiseFailed(e);
				throw;
			}
		}

		/// <summary>
		/// Executes a SQL statement against a connection object.
		/// </summary>
		/// <returns>The number of rows affected.</returns>
		public override int ExecuteNonQuery()
		{
			var e =
				new CommandExecutionEventArgs(this, "ExecuteNonQuery")
					{
						Status = CommandExecutionStatus.Executing
					};
			var sw = new Stopwatch();

			Connection.RaiseExecuting(e);
			try
			{
				sw.Start();
				var result = base.ExecuteNonQuery();
				sw.Stop();
				e.Result = result;
				e.Duration = sw.Elapsed;
				e.Status = CommandExecutionStatus.Finished;
				Connection.RaiseFinished(e);
				return result;
			}
			catch (Exception ex)
			{
				e.Result = ex;
				e.Status = CommandExecutionStatus.Failed;
				Connection.RaiseFailed(e);
				throw;
			}
		}

		/// <summary>
		/// Executes the command text against the connection.
		/// </summary>
		/// <param name="behavior">An instance of <see cref="T:System.Data.CommandBehavior"/>.</param>
		/// <returns>
		/// A <see cref="T:System.Data.Common.DbDataReader"/>.
		/// </returns>
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			var e =
				new CommandExecutionEventArgs(this, "ExecuteReader")
					{
						Status = CommandExecutionStatus.Executing
					};

			Connection.RaiseExecuting(e);
			try
			{
				var sw = new Stopwatch();
				sw.Start();
				var result = base.ExecuteDbDataReader(behavior);
				sw.Stop();
				e.Result = result;
				e.Status = CommandExecutionStatus.Finished;
				e.Duration = sw.Elapsed;
				Connection.RaiseFinished(e);
				return result;
			}
			catch (Exception ex)
			{
				e.Result = ex;
				e.Status = CommandExecutionStatus.Failed;
				Connection.RaiseFailed(e);
				throw;
			}
		}
	}
}