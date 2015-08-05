namespace EyeSoft.Nuget.Publisher.Shell.Nuget
{
	public class PackageUpdateResult
	{
		public PackageUpdateResult(string id, PackageUpdateDataResult data)
		{
			Id = id;
			Data = data;
		}

		public string Id { get; }

		public PackageUpdateDataResult Data { get; }

		public override string ToString()
		{
			return Id;
		}
	}
}