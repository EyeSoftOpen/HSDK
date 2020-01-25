namespace EyeSoft.Reflection
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public static class AssemblyExtensions
    {
        public static string Product(this Assembly assembly)
        {
            return GetCustomAttributeValue<AssemblyProductAttribute>(assembly, x => x.Product);
        }

        public static string Company(this Assembly assembly)
        {
            return GetCustomAttributeValue<AssemblyCompanyAttribute>(assembly, x => x.Company);
        }

        public static string ReadResourceText(this Assembly assembly, string resourceKey, bool isFullName = false)
        {
            using (var stream = assembly.GetResourceStream(resourceKey, isFullName))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static string ReadGzipResourceText(this Assembly assembly, string resourceKey, bool isFullName = false, Encoding encoding = null)
        {
            using (var compressed = assembly.GetResourceStream(resourceKey, isFullName))
            {
                using (var decompressedStream = new MemoryStream())
                {
                    using (var decompressionStream = new GZipStream(compressed, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedStream);
                    }

                    Func<MemoryStream, StreamReader> createReader =
                        stream => encoding == null ? new StreamReader(stream) : new StreamReader(stream, encoding);

                    using (var reader = createReader(decompressedStream))
                    {
                        decompressedStream.Position = 0;

                        var decompressed = reader.ReadToEnd();

                        return decompressed;
                    }
                }
            }
        }

        public static Stream GetResourceStream(this Assembly assembly, string resourceKey, bool isFullName = false)
        {
            var key = isFullName ? resourceKey : string.Concat(assembly.GetName().Name, ".", resourceKey);

            var resourceStream = assembly.GetManifestResourceStream(key);

            if (resourceStream == null)
            {
                throw new ArgumentException(string.Format("The resource '{0}' does not exist in the assembly {1}.", key, assembly.GetName().Name));
            }

            return resourceStream;
        }

        public static string GetCustomAttributeValue<TAttribute>(
            this Assembly assembly, Func<TAttribute, string> readProperty) where TAttribute : Attribute
        {
            var attribute = assembly.GetCustomAttributes(typeof(TAttribute), false).SingleOrDefault();

            return attribute != null ? readProperty((TAttribute)attribute) : null;
        }
    }
}