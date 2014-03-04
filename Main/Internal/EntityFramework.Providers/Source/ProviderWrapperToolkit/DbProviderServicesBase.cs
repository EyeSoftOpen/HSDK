// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace EyeSoft.Data.EntityFramework.Toolkit
{
	using System;
	using System.Collections.Generic;
	using System.Data.Common;
	using System.Data.Common.CommandTrees;
	using System.Data.Metadata.Edm;
	using System.Reflection;

	/// <summary>
	/// Common implementation of <see cref="DbProviderServices"/> methods.
	/// </summary>
	public abstract class DbProviderServicesBase : DbProviderServices
	{
		private readonly IDictionary<string, Func<DbProviderManifest, DbCommandTree, DbCommandDefinition>>
			createDbCommandDefinitionFunctions =
			new Dictionary<string, Func<DbProviderManifest, DbCommandTree, DbCommandDefinition>>();

		/// <summary>
		/// Gets the provider invariant name.
		/// </summary>
		/// <returns>Provider invariant name.</returns>
		protected abstract string ProviderInvariantName { get; }

		/// <summary>
		/// Gets the default name of the wrapped provider.
		/// </summary>
		/// <returns>Default name of the wrapped provider (to be used when
		/// provider is not specified in the connction string)</returns>
		protected abstract string DefaultWrappedProviderName { get; }

		/// <summary>
		/// Creates the command definition wrapper.
		/// </summary>
		/// <param name="wrappedCommandDefinition">The wrapped command definition.</param>
		/// <param name="commandTree">The command tree.</param>
		/// <returns><see cref="DbCommandDefinitionWrapper"/> object.</returns>
		public virtual DbCommandDefinitionWrapper CreateCommandDefinitionWrapper(
			DbCommandDefinition wrappedCommandDefinition,
			DbCommandTree commandTree)
		{
			return new DbCommandDefinitionWrapper(
				wrappedCommandDefinition,
				commandTree,
				(cmd, def) => new DbCommandWrapper(cmd, def));
		}

		/// <summary>
		/// Creates the provider manifest wrapper.
		/// </summary>
		/// <param name="providerInvariantName">Provider invariant name.</param>
		/// <param name="providerManifest">The provider manifest to be wrapped.</param>
		/// <returns><see cref="DbProviderManifest"/> wrapper for given provider invariant name wrapping given provider manifest.</returns>
		public virtual DbProviderManifest CreateProviderManifest(string providerInvariantName, DbProviderManifest providerManifest)
		{
			return new DbProviderManifestWrapper(
				ProviderInvariantName,
				providerInvariantName,
				providerManifest);
		}

		/// <summary>
		/// Gets the provider manifest token.
		/// </summary>
		/// <param name="connection">The database connection.</param>
		/// <returns>Provider Manifest Token suitable for inclusion in SSDL file and connection string</returns>
		/// <remarks>
		/// The provider manifest token is created by concatenating wrapped provider invariant name and its
		/// token separated by semicolon, for example when wrapping SqlClient for SQL Server 2005 the provider
		/// manifest token will be "System.Data.SqlClient;2005"
		/// </remarks>
		protected override string GetDbProviderManifestToken(DbConnection connection)
		{
			var wrapper = (DbConnectionWrapper)connection;
			var wrappedConnection = wrapper.WrappedConnection;
			var services = GetProviderServices(wrappedConnection);

			var token = wrapper.WrappedProviderInvariantName + ";" + services.GetProviderManifestToken(wrappedConnection);
			return token;
		}

		/// <summary>
		/// Gets the provider manifest for given provider manifest token.
		/// </summary>
		/// <param name="manifestToken">The provider manifest token.</param>
		/// <returns><see cref="DbProviderManifest"/> for a given token.</returns>
		protected override DbProviderManifest GetDbProviderManifest(string manifestToken)
		{
			if (string.IsNullOrEmpty(manifestToken))
			{
				throw new ArgumentNullException("manifestToken");
			}

			string providerInvariantName;
			string realToken;

			// check if the name of the wrapped provider is specified in the token
			var p = manifestToken.IndexOf(';');
			if (p < 0)
			{
				// wrapped provider is not in the token - use default one
				realToken = manifestToken;
				providerInvariantName = DefaultWrappedProviderName;
			}
			else
			{
				// extract provider name from the token
				providerInvariantName = manifestToken.Substring(0, p);
				realToken = manifestToken.Substring(p + 1);
			}

			// retrieve wrapped provider manifest
			var services = GetProviderServicesByName(providerInvariantName);
			var wrappedProviderManifest = services.GetProviderManifest(realToken);
			var wrapperManifest = CreateProviderManifest(providerInvariantName, wrappedProviderManifest);

			return wrapperManifest;
		}

		/// <summary>
		/// Creates the command definition wrapper for a given provider manifest and command tree.
		/// </summary>
		/// <param name="providerManifest">The provider manifest.</param>
		/// <param name="commandTree">The command tree.</param>
		/// <returns><see cref="DbCommandDefinition"/> object.</returns>
		protected override DbCommandDefinition CreateDbCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree)
		{
			const string ProviderManifestInvariantName = "System.Data.SqlClient";

			var wrapperManifest = providerManifest;

			var dbProviderManifestWrapper = providerManifest as DbProviderManifestWrapper;

			if (dbProviderManifestWrapper != null)
			{
				wrapperManifest = dbProviderManifestWrapper.WrappedProviderManifest;
			}

			var createDbCommandDefinitionFunction =
				GetCreateDbCommandDefinitionFunction(ProviderManifestInvariantName);

			var definition = createDbCommandDefinitionFunction(wrapperManifest, commandTree);
			return CreateCommandDefinitionWrapper(definition, commandTree);
		}

		/// <summary>
		/// Creates a database indicated by connection and creates schema objects
		/// (tables, primary keys, foreign keys) based on the contents of a <see cref="T:System.Data.Metadata.Edm.StoreItemCollection"/>.
		/// </summary>
		/// <param name="connection">Connection to a non-existent database that needs to be created
		/// and populated with the store objects indicated with the storeItemCollection parameter.</param>
		/// <param name="commandTimeout">Execution timeout for any commands needed to create the database.</param>
		/// <param name="storeItemCollection">The collection of all store items based on which the script should be created.</param>
		protected override void DbCreateDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			var wrappedConnection = ((DbConnectionWrapper)connection).WrappedConnection;
			var services = GetProviderServices(wrappedConnection);

			var storeProviderManifestField =
				storeItemCollection
					.GetType()
					.GetField("_providerManifest", BindingFlags.Instance | BindingFlags.NonPublic);

			var storeProviderManifest =
				(DbProviderManifestWrapper)storeProviderManifestField.GetValue(storeItemCollection);

			storeProviderManifestField.SetValue(storeItemCollection, storeProviderManifest.WrappedProviderManifest);

			services.CreateDatabase(wrappedConnection, commandTimeout, storeItemCollection);
		}

		/// <summary>
		/// Returns a value indicating whether a given database exists on the server
		/// and whether schema objects contained in the storeItemCollection have been created.
		/// </summary>
		/// <param name="connection">Connection to a database whose existence is verified by this method.</param>
		/// <param name="commandTimeout">Execution timeout for any commands needed to determine the existence of the database.</param>
		/// <param name="storeItemCollection">The structure of the database whose existence is determined by this method.</param>
		/// <returns>
		/// true if the database indicated by the connection and the <paramref name="storeItemCollection"/> parameter exists.
		/// </returns>
		protected override bool DbDatabaseExists(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			var wrappedConnection = ((DbConnectionWrapper)connection).WrappedConnection;
			var services = GetProviderServices(wrappedConnection);

			return services.DatabaseExists(wrappedConnection, commandTimeout, storeItemCollection);
		}

		/// <summary>
		/// Deletes all store objects specified in the store item collection from the database and the database itself.
		/// </summary>
		/// <param name="connection">Connection to an existing database that needs to be deleted.</param>
		/// <param name="commandTimeout">Execution timeout for any commands needed to delete the database.</param>
		/// <param name="storeItemCollection">The structure of the database to be deleted.</param>
		protected override void DbDeleteDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection)
		{
			var wrappedConnection = ((DbConnectionWrapper)connection).WrappedConnection;
			var services = GetProviderServices(wrappedConnection);

			services.DeleteDatabase(wrappedConnection, commandTimeout, storeItemCollection);
		}

		/// <summary>
		/// Generates a data definition language (DDL0 script that creates schema objects
		/// (tables, primary keys, foreign keys) based on the contents of the<see cref="T:System.Data.Metadata.Edm.StoreItemCollection"/>
		/// parameter and targeted for the version of the database corresponding to the provider manifest token.
		/// </summary>
		/// <param name="providerManifestToken">The provider manifest token identifying the target version.</param>
		/// <param name="storeItemCollection">The structure of the database.</param>
		/// <returns>
		/// A DDL script that creates schema objects based on the contents of the
		/// <see cref="T:System.Data.Metadata.Edm.StoreItemCollection"/> parameter and targeted for the version of the database
		/// corresponding to the provider manifest token.
		/// </returns>
		protected override string DbCreateDatabaseScript(string providerManifestToken, StoreItemCollection storeItemCollection)
		{
			if (string.IsNullOrEmpty(providerManifestToken))
			{
				throw new ArgumentNullException("providerManifestToken");
			}

			string providerInvariantName;
			string realToken;

			// check if the name of the wrapped provider is specified in the token
			var p = providerManifestToken.IndexOf(';');
			if (p < 0)
			{
				// wrapped provider is not in the token - use default one
				realToken = providerManifestToken;
				providerInvariantName = DefaultWrappedProviderName;
			}
			else
			{
				// extract provider name from the token
				providerInvariantName = providerManifestToken.Substring(0, p);
				realToken = providerManifestToken.Substring(p + 1);
			}

			// retrieve wrapped provider manifest
			DbProviderServices services = GetProviderServicesByName(providerInvariantName);

			return services.CreateDatabaseScript(realToken, storeItemCollection);
		}

		/// <summary>
		/// Gets provider services object given provider invariant name.
		/// </summary>
		/// <param name="providerInvariantName">Provider invariant name.</param>
		/// <returns><see cref="DbProviderServices"/> object for a given invariant name.</returns>
		private static DbProviderServices GetProviderServicesByName(string providerInvariantName)
		{
			var factory = DbProviderFactories.GetFactory(providerInvariantName);
			if (factory == null)
			{
				throw new ArgumentException("Invalid provider factory: " + providerInvariantName);
			}

			var serviceProvider = factory as IServiceProvider;
			if (serviceProvider == null)
			{
				throw new ArgumentException("Provider does not support Entity Framework - IServiceProvider is not supported");
			}

			var providerServices = (DbProviderServices)serviceProvider.GetService(typeof(DbProviderServices));
			if (providerServices == null)
			{
				throw new ArgumentException("Provider does not support Entity Framework - DbProviderServices is not supported");
			}

			return providerServices;
		}

		private Func<DbProviderManifest, DbCommandTree, DbCommandDefinition>
			GetCreateDbCommandDefinitionFunction(string providerInvariantName)
		{
			Func<DbProviderManifest, DbCommandTree, DbCommandDefinition> result;

			lock (createDbCommandDefinitionFunctions)
			{
				if (!createDbCommandDefinitionFunctions.TryGetValue(providerInvariantName, out result))
				{
					var ps = GetProviderServicesByName(providerInvariantName);
					createDbCommandDefinitionFunctions[providerInvariantName] = ps.CreateCommandDefinition;

					result = ps.CreateCommandDefinition;
				}
			}

			return result;
		}
	}
}