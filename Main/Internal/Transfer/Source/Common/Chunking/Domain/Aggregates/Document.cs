namespace EyeSoft.Transfer.Chunking.Domain.Aggregates
{
	using EyeSoft.Domain;

	public class Document : Aggregate
	{
		protected Document()
		{
		}

		public string Path { get; protected set; }

		public virtual long Size { get; protected set; }

		public virtual string Sha1 { get; protected set; }

		public static Document Create(string path, long size, string sha1)
		{
			return
				new Document
				{
					Path = path,
					Size = size,
					Sha1 = sha1
				};
		}
	}
}