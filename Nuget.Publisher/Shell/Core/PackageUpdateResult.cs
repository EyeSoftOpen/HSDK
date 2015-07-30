namespace EyeSoft.Nuget.Publisher.Shell
{
	public class PackageUpdateResult
	{
		public PackageUpdateResult(string id, PackageUpdateDataResult data)
		{
			Id = id;
			Data = data;
		}

		public string Id { get; private set; }

		public PackageUpdateDataResult Data { get; private set; }

		public override string ToString()
		{
			return Id;
		}
	}
}