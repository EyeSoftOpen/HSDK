namespace EyeSoft.SequentialIdentity
{
    public interface IWorkerIdProvider
    {
        byte[] GetWorkerId(int index);
    }
}