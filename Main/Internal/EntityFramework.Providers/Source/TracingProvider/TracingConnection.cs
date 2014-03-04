// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace EyeSoft.Data.EntityFramework.Tracing
{
	using System;
	using System.Data.Common;
	using System.Diagnostics.CodeAnalysis;
	using System.IO;

	using EyeSoft.Data.EntityFramework.Toolkit;

	/// <summary>
	/// Wrapper <see cref="DbConnection"/> which traces all executed commands.
	/// </summary>
	public class TracingConnection : DbConnectionWrapper
	{
		private static readonly object consoleLockObject = new object();

		/// <summary>
		/// Initializes a new instance of the TracingConnection class.
		/// </summary>
		public TracingConnection()
		{
			AddDefaultListenersFromConfiguration();
		}

		/// <summary>
		/// Initializes a new instance of the TracingConnection class.
		/// </summary>
		/// <param name="wrappedConnection">The wrapped connection.</param>
		public TracingConnection(DbConnection wrappedConnection)
		{
			WrappedConnection = wrappedConnection;
			AddDefaultListenersFromConfiguration();
		}

		/// <summary>
		/// Occurs when database command is executing.
		/// </summary>
		public event EventHandler<CommandExecutionEventArgs> CommandExecuting;

		/// <summary>
		/// Occurs when database command has finished execution.
		/// </summary>
		public event EventHandler<CommandExecutionEventArgs> CommandFinished;

		/// <summary>
		/// Occurs when database command execution has failed.
		/// </summary>
		public event EventHandler<CommandExecutionEventArgs> CommandFailed;

		/// <summary>
		/// Gets the <see cref="T:System.Data.Common.DbProviderFactory"/> for this <see cref="T:System.Data.Common.DbConnection"/>.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Data.Common.DbProviderFactory"/>.
		/// </returns>
		protected override DbProviderFactory DbProviderFactory
		{
			get { return TracingProviderFactory.Instance; }
		}

		/// <summary>
		/// Gets the name of the default wrapped provider.
		/// </summary>
		/// <returns>Name of the default wrapped provider.</returns>
		protected override string DefaultWrappedProviderName
		{
			get { return TracingProviderConfiguration.DefaultWrappedProvider; }
		}

		internal void RaiseExecuting(CommandExecutionEventArgs e)
		{
			if (CommandExecuting != null)
			{
				CommandExecuting(this, e);
			}
		}

		internal void RaiseFinished(CommandExecutionEventArgs e)
		{
			if (CommandFinished != null)
			{
				CommandFinished(this, e);
			}
		}

		internal void RaiseFailed(CommandExecutionEventArgs e)
		{
			if (CommandFailed != null)
			{
				CommandFailed(this, e);
			}
		}

		[SuppressMessage(
			"Microsoft.Globalization",
			"CA1303:Do not pass literals as localized parameters",
			Justification = "Component is non-localizable")]
		private void AddDefaultListenersFromConfiguration()
		{
			if (TracingProviderConfiguration.LogToConsole)
			{
				CommandExecuting += delegate(object sender, CommandExecutionEventArgs e)
				{
					lock (consoleLockObject)
					{
						var oldColor = Console.ForegroundColor;

						try
						{
							Console.ForegroundColor = ConsoleColor.White;
							Console.WriteLine("#{0} Running {1}:", e.CommandId, e.Method);
							Console.ForegroundColor = ConsoleColor.DarkGray;
							Console.WriteLine(e.ToTraceString());
						}
						finally
						{
							Console.ForegroundColor = oldColor;
						}
					}
				};

				CommandFinished += delegate(object sender, CommandExecutionEventArgs e)
				{
					lock (consoleLockObject)
					{
						var oldColor = Console.ForegroundColor;

						try
						{
							Console.ForegroundColor = ConsoleColor.Green;
							Console.WriteLine("#{0} Command completed in {1}", e.CommandId, e.Duration);
						}
						finally
						{
							Console.ForegroundColor = oldColor;
						}
					}
				};

				CommandFailed += delegate(object sender, CommandExecutionEventArgs e)
				{
					lock (consoleLockObject)
					{
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("#{0} Command failed {1}", e.CommandId, e.Result);
					}
				};
			}

			var logFile = TracingProviderConfiguration.LogToFile;

			if (logFile != null)
			{
				CommandExecuting += (sender, e) => File.AppendAllText(logFile, e.ToTraceString() + "\r\n\r\n");
			}

			var logAction = TracingProviderConfiguration.LogAction;

			if (logAction == null)
			{
				return;
			}

			CommandExecuting += (sender, e) => logAction(e);

			CommandFinished += (sender, e) => logAction(e);

			CommandFailed += (sender, e) => logAction(e);
		}
	}
}