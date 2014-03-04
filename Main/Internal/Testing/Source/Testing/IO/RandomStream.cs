namespace EyeSoft.Testing.IO
{
	using System;
	using System.IO;

	public class RandomStream
		: Stream
	{
		private readonly long size;

		private readonly int seed;

		private readonly byte min = byte.MinValue;
		private readonly byte max = byte.MaxValue;

		public RandomStream(long size, bool onlyLetters, int seed = 123)
		{
			this.size = size;
			this.seed = seed;

			if (!onlyLetters)
			{
				return;
			}

			min = (byte)'a';
			max = (byte)'z';
		}

		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		public override long Length
		{
			get
			{
				return size;
			}
		}

		public override long Position { get; set; }

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (Position >= Length)
			{
				return 0;
			}

			var random = new Random(seed);

			for (var i = 0; i < Position; i++)
			{
				random.Next(min, max);
			}

			var toRead = size - Position;

			var length = (buffer.Length > toRead) ? toRead : buffer.Length;

			for (var i = 0; i < length; i++)
			{
				buffer[i] = (byte)random.Next(min, max);
			}

			Position += length;

			return (int)length;
		}

		public override void Flush()
		{
			throw new NotSupportedException();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			if (origin != SeekOrigin.Begin)
			{
				throw new ArgumentException("RandomStream supports only SeekOrigin.Begin.");
			}

			Position = offset;

			return Position;
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}
	}
}