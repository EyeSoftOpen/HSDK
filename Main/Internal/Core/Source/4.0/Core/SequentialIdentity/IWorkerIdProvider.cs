namespace EyeSoft.Core.SequentialIdentity
{
    public interface IWorkerIdProvider
    {
        byte[] GetWorkerId(int index);
    }
}