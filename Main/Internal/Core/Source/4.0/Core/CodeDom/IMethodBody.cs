namespace EyeSoft.CodeDom
{
	public interface IMethodBody
	{
		IMethodBody AddParameter<T>(string parameterName);

		CodeDomFluent Body(string body);

		IMethodBodyMultiLine Body();
	}
}