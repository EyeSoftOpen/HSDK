namespace EyeSoft.Transfer.Service.Test.Helpers
{
	using System.IO;

	internal class RandomStream : Stream
	{
		public RandomStream(int documentSize, bool b, int getHashCode)
		{
			throw new System.NotImplementedException();
		}

		public override void Flush()
		{
			throw new System.NotImplementedException();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new System.NotImplementedException();
		}

		public override void SetLength(long value)
		{
			throw new System.NotImplementedException();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new System.NotImplementedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new System.NotImplementedException();
		}

		public override bool CanRead { get; }

		public override bool CanSeek { get; }

		public override bool CanWrite { get; }

		public override long Length { get; }

		public override long Position { get; set; }
	}
}