namespace EyeSoft.Core.Mapping
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public abstract class MappingException : Exception
    {
        protected MappingException(string message) : base(message)
        {
        }

        protected MappingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        protected MappingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}