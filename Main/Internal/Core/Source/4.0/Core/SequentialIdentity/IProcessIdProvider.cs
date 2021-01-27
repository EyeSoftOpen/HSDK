namespace EyeSoft.Core.SequentialIdentity
{
    public interface IProcessIdProvider
    {
        byte[] GetProcessId();
    }
}