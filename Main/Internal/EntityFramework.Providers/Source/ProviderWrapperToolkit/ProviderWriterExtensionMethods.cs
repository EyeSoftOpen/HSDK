// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace EyeSoft.Data.EntityFramework.Toolkit
{
	using System;
	using System.Data.Common;
	using System.Data.EntityClient;
	using System.Data.Objects;
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// Extension methods for handing wrapped providers.
	/// </summary>
	public static class ProviderWriterExtensionMethods
	{
		/// <summary>
		/// Gets the underlying wrapper connection from the <see cref="ObjectContext"/>.
		/// </summary>
		/// <typeparam name="TConnection">Connection type.</typeparam>
		/// <param name="context">The object context.</param>
		/// <returns>Wrapper connection of a given type.</returns>
		[SuppressMessage(
			"Microsoft.Design",
			"CA1004:GenericMethodsShouldProvideTypeParameter",
			Justification = "Type parameter must be specified explicitly.")]
		public static TConnection UnwrapConnection<TConnection>(this ObjectContext context)
			where TConnection : DbConnection
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			return context.Connection.UnwrapConnection<TConnection>();
		}

		/// <summary>
		/// Tries to get the underlying wrapper connection from the <see cref="ObjectContext"/>.
		/// </summary>
		/// <typeparam name="TConnection">Connection type.</typeparam>
		/// <param name="context">The object context.</param>
		/// <param name="result">The result connection.</param>
		/// <returns>A value of true if the given connection type was found in the provider chain, false otherwise.</returns>
		public static bool TryUnwrapConnection<TConnection>(
			this ObjectContext context,
			out TConnection result) where TConnection : DbConnection
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			return context.Connection.TryUnwrapConnection(out result);
		}

		/// <summary>
		/// Gets the underlying wrapper connection from the <see cref="DbConnection"/>.
		/// </summary>
		/// <typeparam name="TConnection">Connection type.</typeparam>
		/// <param name="connection">The connection.</param>
		/// <returns>Wrapper connection of a given type.</returns>
		[SuppressMessage(
			"Microsoft.Design",
			"CA1004:GenericMethodsShouldProvideTypeParameter",
			Justification = "Type parameter must be specified explicitly.")]
		public static TConnection UnwrapConnection<TConnection>(this DbConnection connection) where TConnection : DbConnection
		{
			TConnection result;

			if (connection.TryUnwrapConnection(out result))
			{
				return result;
			}

			var message = "Wrapper provider of type " + typeof(TConnection).FullName + " was not found in the chain.";
			throw new InvalidOperationException(message);
		}

		/// <summary>
		/// Tries to get the underlying wrapper connection from the <see cref="DbConnection"/>.
		/// </summary>
		/// <typeparam name="TConnection">Connection type.</typeparam>
		/// <param name="connection">The connection.</param>
		/// <param name="result">The result connection.</param>
		/// <returns>A value of true if the given connection type was found in the provider chain, false otherwise.</returns>
		public static bool TryUnwrapConnection<TConnection>(
			this DbConnection connection,
			out TConnection result) where TConnection : DbConnection
		{
			var ec = connection as EntityConnection;
			if (ec != null)
			{
				connection = ec.StoreConnection;
			}

			while (connection is DbConnectionWrapper)
			{
				if (connection is TConnection)
				{
					result = (TConnection)connection;
					return true;
				}

				connection = ((DbConnectionWrapper)connection).WrappedConnection;
			}

			result = null;
			return false;
		}
	}
}