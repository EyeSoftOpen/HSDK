// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace EyeSoft.Data.EntityFramework.Toolkit
{
	using System;
	using System.Collections.Generic;
	using System.Data.Common;
	using System.Data.EntityClient;
	using System.Data.Mapping;
	using System.Data.Metadata.Edm;
	using System.Diagnostics.CodeAnalysis;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using System.Text.RegularExpressions;
	using System.Xml;
	using System.Xml.Linq;

	/// <summary>
	/// Utilities for creating <see cref="EntityConnection"/> with wrapped providers.
	/// </summary>
	public static class EntityConnectionWrapperUtils
	{
		private const string HttpContextTypeName =
			"System.Web.HttpContext, System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		private static readonly Dictionary<string, MetadataWorkspace> metadataWorkspaceMemoizer =
			new Dictionary<string, MetadataWorkspace>();

		private static readonly byte[] systemPublicKeyToken = { 0xB0, 0x3F, 0x5F, 0x7F, 0x11, 0xD5, 0x0A, 0x3A };

		private static readonly Regex resRegex = new Regex(@"^res://(?<assembly>.*)/(?<resource>.*)$");

		/// <summary>
		/// Creates the entity connection with wrappers.
		/// </summary>
		/// <param name="entityConnectionString">The original entity connection string.</param>
		/// <param name="wrapperProviders">List for wrapper providers.</param>
		/// <returns>EntityConnection object which is based on a chain of providers.</returns>
		public static EntityConnection CreateEntityConnectionWithWrappers(string entityConnectionString, params string[] wrapperProviders)
		{
			var ecsb = new EntityConnectionStringBuilder(entityConnectionString);

			// if connection string is name=EntryName, look up entry in the config file and parse it
			if (!string.IsNullOrEmpty(ecsb.Name))
			{
				var connStr = System.Configuration.ConfigurationManager.ConnectionStrings[ecsb.Name];
				if (connStr == null)
				{
					var message = "Specified named connection string '" + ecsb.Name + "' was not found in the configuration file.";
					throw new ArgumentException(message);
				}

				ecsb.ConnectionString = connStr.ConnectionString;
			}

			MetadataWorkspace workspace;
			if (!metadataWorkspaceMemoizer.TryGetValue(ecsb.ConnectionString, out workspace))
			{
				workspace = CreateWrappedMetadataWorkspace(ecsb.Metadata, wrapperProviders);
				metadataWorkspaceMemoizer.Add(ecsb.ConnectionString, workspace);
			}

			var storeConnection = DbProviderFactories.GetFactory(ecsb.Provider).CreateConnection();
			storeConnection.ConnectionString = ecsb.ProviderConnectionString;

			var newEntityConnection =
				new EntityConnection(workspace, DbConnectionWrapper.WrapConnection(storeConnection, wrapperProviders));

			return newEntityConnection;
		}

		private static MetadataWorkspace CreateWrappedMetadataWorkspace(string metadata, IEnumerable<string> wrapperProviderNames)
		{
			// parse Metadata keyword and load CSDL,SSDL,MSL files into XElement structures...
			var csdl = new List<XElement>();
			var ssdl = new List<XElement>();
			var msl = new List<XElement>();
			ParseMetadata(metadata, csdl, ssdl, msl);

			var providerNames = wrapperProviderNames.ToArray();

			// fix all SSDL files by changing 'Provider' to our provider and modifying
			foreach (var ssdlFile in ssdl)
			{
				foreach (var providerName in providerNames)
				{
					ssdlFile.Attribute("ProviderManifestToken").Value =
						ssdl[0].Attribute("Provider").Value + ";" + ssdlFile.Attribute("ProviderManifestToken").Value;
					ssdlFile.Attribute("Provider").Value = providerName;
				}
			}

			// load item collections from XML readers created from XElements...
			var eic = new EdmItemCollection(csdl.Select(c => c.CreateReader()));
			var sic = new StoreItemCollection(ssdl.Select(c => c.CreateReader()));
			var smic = new StorageMappingItemCollection(eic, sic, msl.Select(c => c.CreateReader()));

			// and create metadata workspace based on them.
			var workspace = new MetadataWorkspace();
			workspace.RegisterItemCollection(eic);
			workspace.RegisterItemCollection(sic);
			workspace.RegisterItemCollection(smic);
			return workspace;
		}

		private static void ParseMetadata(string metadata, List<XElement> csdl, List<XElement> ssdl, List<XElement> msl)
		{
			foreach (string component in metadata.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries).Select(c => c.Trim()))
			{
				string translatedComponent = component;

				if (translatedComponent.StartsWith("~", StringComparison.Ordinal))
				{
					var httpContextType = Type.GetType(HttpContextTypeName, true);
					var context = (dynamic)httpContextType.GetProperty("Current").GetValue(null, null);
					if (context == null)
					{
						throw new NotSupportedException("Paths prefixed with '~' are not supported outside of ASP.NET.");
					}

					translatedComponent = context.Server.MapPath(translatedComponent);
				}

				if (translatedComponent.StartsWith("res://", StringComparison.Ordinal))
				{
					ParseResources(translatedComponent, csdl, ssdl, msl);
				}
				else if (Directory.Exists(translatedComponent))
				{
					ParseDirectory(translatedComponent, csdl, ssdl, msl);
				}
				else if (translatedComponent.EndsWith(".csdl", StringComparison.OrdinalIgnoreCase))
				{
					csdl.Add(XElement.Load(translatedComponent));
				}
				else if (translatedComponent.EndsWith(".ssdl", StringComparison.OrdinalIgnoreCase))
				{
					ssdl.Add(XElement.Load(translatedComponent));
				}
				else if (translatedComponent.EndsWith(".msl", StringComparison.OrdinalIgnoreCase))
				{
					msl.Add(XElement.Load(translatedComponent));
				}
				else
				{
					throw new NotSupportedException("Unknown metadata component: " + component);
				}
			}
		}

		[SuppressMessage(
			"Microsoft.Design",
			"CA1031:DoNotCatchGeneralExceptionTypes",
			Justification = "We want to ignore exceptions during loading of references.")]
		private static void ParseResources(
			string resPath,
			ICollection<XElement> csdl,
			ICollection<XElement> ssdl,
			ICollection<XElement> msl)
		{
			var match = resRegex.Match(resPath);
			if (!match.Success)
			{
				throw new NotSupportedException("Not supported resource path: " + resPath);
			}

			var assemblyName = match.Groups["assembly"].Value;
			var resourceName = match.Groups["resource"].Value;

			var assembliesToConsider = new List<Assembly>();
			if (assemblyName == "*")
			{
				assembliesToConsider.AddRange(AppDomain.CurrentDomain.GetAssemblies());
			}
			else
			{
				assembliesToConsider.Add(Assembly.Load(new AssemblyName(assemblyName)));
			}

			var domainManager = AppDomain.CurrentDomain.DomainManager;
			if (domainManager != null && domainManager.EntryAssembly != null)
			{
				foreach (var asmName in domainManager.EntryAssembly.GetReferencedAssemblies())
				{
					var asm = Assembly.Load(asmName);
					if (!assembliesToConsider.Contains(asm))
					{
						assembliesToConsider.Add(asm);
					}
				}
			}

			foreach (var asm in assembliesToConsider.Where(asm => !IsEcmaAssembly(asm) && !IsSystemAssembly(asm)))
			{
				using (var stream = asm.GetManifestResourceStream(resourceName))
				{
					if (stream == null)
					{
						continue;
					}

					if (resourceName.EndsWith(".csdl", StringComparison.OrdinalIgnoreCase))
					{
						csdl.Add(XElement.Load(XmlReader.Create(stream)));
						return;
					}

					if (resourceName.EndsWith(".ssdl", StringComparison.OrdinalIgnoreCase))
					{
						ssdl.Add(XElement.Load(XmlReader.Create(stream)));
						return;
					}

					if (resourceName.EndsWith(".msl", StringComparison.OrdinalIgnoreCase))
					{
						msl.Add(XElement.Load(XmlReader.Create(stream)));
						return;
					}
				}
			}

			throw new InvalidOperationException("Resource " + resPath + " not found.");
		}

		private static bool IsEcmaAssembly(Assembly asm)
		{
			byte[] publicKey = asm.GetName().GetPublicKey();

			// ECMA key is special, as it is only 4 bytes long
			if (publicKey != null && publicKey.Length == 16 && publicKey[8] == 0x4)
			{
				return true;
			}

			return false;
		}

		private static bool IsSystemAssembly(Assembly asm)
		{
			var publicKeyToken = asm.GetName().GetPublicKeyToken();

			if (publicKeyToken != null && publicKeyToken.Length == systemPublicKeyToken.Length)
			{
				return !systemPublicKeyToken.Where((t, i) => t != publicKeyToken[i]).Any();
			}

			return false;
		}

		private static void ParseDirectory(string directory, List<XElement> csdl, List<XElement> ssdl, List<XElement> msl)
		{
			csdl.AddRange(Directory.GetFiles(directory, "*.csdl").Select(XElement.Load));

			ssdl.AddRange(Directory.GetFiles(directory, "*.ssdl").Select(XElement.Load));

			msl.AddRange(Directory.GetFiles(directory, "*.msl").Select(XElement.Load));
		}
	}
}