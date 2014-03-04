namespace EyeSoft.Testing.IO
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;

	using EyeSoft.IO;

	public class MemoryStorage : DefaultStorage
	{
		private readonly IList<IFileInfo> fileCollection;

		public MemoryStorage()
		{
			fileCollection = new List<IFileInfo>();
		}

		public MemoryStorage(IEnumerable<IFileInfo> fileCollection)
		{
			this.fileCollection = new List<IFileInfo>(fileCollection);
		}

		public override IFileInfo File(string path)
		{
			Enforce.Argument(() => path);

			var fileInfo =
				fileCollection
					.FirstOrDefault(file => file.FullName == path);

			Ensure
				.That(fileInfo)
				.WithException(new FileNotFoundException(string.Format("The file \"{0}\" does not exist.", path), path))
				.Is.Not.Null();

			return fileInfo;
		}

		public override IDirectoryInfo Directory(string path)
		{
			throw new NotImplementedException();
		}

		public override Stream OpenRead(string path)
		{
			throw new NotImplementedException();
		}

		public override IEnumerable<IFileInfo> GetFiles(string path, string pattern)
		{
			return fileCollection.Where(d => d.FullName == pattern);
		}

		public override void WriteAllBytes(string path, byte[] bytes)
		{
			fileCollection.Add(new MemoryFileInfo(path, bytes));
		}

		public override void WriteAllText(string path, string contents, Encoding encoding = null, bool overwrite = false)
		{
			WriteAllBytes(path, Encoding.Default.GetBytes(contents));
		}

		public override Stream OpenWrite(string name)
		{
			throw new NotSupportedException();
		}

		public override string ReadAllText(string path, Encoding encoding)
		{
			throw new NotSupportedException();
		}

		public override byte[] ReadAllBytes(string path)
		{
			throw new NotSupportedException();
		}

		public override void SaveStreamToFile(Stream stream, string path)
		{
			throw new NotImplementedException();
		}

		public override bool Exists(string path)
		{
			throw new NotSupportedException();
		}

		public override void Delete(string path)
		{
			throw new NotSupportedException();
		}
	}
}