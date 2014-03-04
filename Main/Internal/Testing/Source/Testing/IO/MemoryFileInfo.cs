namespace EyeSoft.Testing.IO
{
	using System.IO;
	using System.Text;

	using EyeSoft.IO;

	public class MemoryFileInfo : FileInfoBase
	{
		private readonly byte[] buffer;

		public MemoryFileInfo(string fullName, int length) : this(fullName, new byte[length])
		{
		}

		public MemoryFileInfo(string fullName, byte[] buffer) : base(fullName)
		{
			this.buffer = buffer;
			Length = buffer.Length;
			DirectoryName = Path.GetDirectoryName(fullName);
		}

		public override Stream OpenRead()
		{
			return new MemoryStream(buffer);
		}

		public override Stream OpenWrite()
		{
			return new MemoryStream(buffer);
		}

		public string ReadAllText()
		{
			return Encoding.ASCII.GetString(buffer);
		}
	}
}