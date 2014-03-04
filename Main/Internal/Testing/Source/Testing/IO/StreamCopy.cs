namespace EyeSoft.Testing.IO
{
	using System.IO;

	public class StreamCopy
	{
		public void Transfer(int bufferSize, Stream sourceStream, Stream destinationStream)
		{
			var readBuffer = new byte[bufferSize];

			int bytesRead;

			while ((bytesRead = sourceStream.Read(readBuffer, 0, bufferSize)) > 0)
			{
				destinationStream.Write(readBuffer, 0, bytesRead);
			}
		}
	}
}