namespace EyeSoft.SequentialIdentity
{
    public interface INewIdParser
    {
        NewId Parse(string text);
    }
}