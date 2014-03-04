namespace EyeSoft.ServiceModel.Hosting.Web
{
	using System.ServiceModel;

	[ServiceContract]
	public interface IMathService
	{
		[OperationContract]
		int Sum(int a, int b);
	}
}