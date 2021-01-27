namespace EyeSoft.Core.SequentialIdentity
{
    public interface INewIdParser
    {
        NewId Parse(string text);
    }
}