namespace EyeSoft.Core.SequentialIdentity
{
    public interface INewIdFormatter
    {
        string Format(byte[] bytes);
    }
}