namespace EyeSoft.SequentialIdentity
{
    public interface INewIdFormatter
    {
        string Format(byte[] bytes);
    }
}