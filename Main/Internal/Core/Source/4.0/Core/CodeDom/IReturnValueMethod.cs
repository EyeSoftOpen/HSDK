namespace EyeSoft.CodeDom
{
	public interface IReturnValueMethod
		: IMethodBody
	{
		CodeDomFluent Return(string variable);
	}
}