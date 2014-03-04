// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace EyeSoft.Data.EntityFramework.Tracing
{
	using System;
	using System.Data.Common;
	using System.Data.Objects;
	using System.IO;

	using EyeSoft.Data.EntityFramework.Toolkit;

	/// <summary>
	/// Extension methods for EFTracingProvider.
	/// </summary>
	public static class TracingExtensionMethods
	{
		/// <summary>
		/// Gets the instance of the wrapped <see cref="TracingConnection" /> from <see cref="ObjectContext"/>.
		/// </summary>
		/// <param name="context">The object context.</param>
		/// <returns>Instance of <see cref="TracingConnection"/>.</returns>
		public static TracingConnection GetTracingConnection(this ObjectContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			return GetTracingConnection(context.Connection);
		}

		/// <summary>
		/// Gets the instance of the wrapped <see cref="TracingConnection" /> from <see cref="DbConnection"/>.
		/// </summary>
		/// <param name="connection">The connection object.</param>
		/// <returns>Instance of <see cref="TracingConnection"/>.</returns>
		public static TracingConnection GetTracingConnection(this DbConnection connection)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}

			return connection.UnwrapConnection<TracingConnection>();
		}

		/// <summary>
		/// Sets the trace output.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="output">The output text writer.</param>
		public static void SetTraceOutput(this ObjectContext context, TextWriter output)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			SetTraceOutput(context.Connection, output);
		}

		/// <summary>
		/// Sets the trace output.
		/// </summary>
		/// <param name="connection">The connection.</param>
		/// <param name="output">The output text writer.</param>
		public static void SetTraceOutput(this DbConnection connection, TextWriter output)
		{
			connection.UnwrapConnection<TracingConnection>().CommandExecuting += (sender, e) => output.WriteLine(e.ToTraceString());
		}
	}
}