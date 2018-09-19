namespace EyeSoft.CodeDom
{
	public interface IMethodBodyMultiLine
	{
		IMethodBodyMultiLine AddLine(string line);

		CodeDomFluent Return(string line);

		CodeDomFluent ReturnVoid();
	}
}