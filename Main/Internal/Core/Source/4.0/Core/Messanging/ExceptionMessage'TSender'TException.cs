namespace EyeSoft.Core.Messanging
{
    using System;

    public class ExceptionMessage<TSender, TException>
		: Message<TSender>
		where TException : Exception
	{
		public ExceptionMessage(TSender sender, TException exception)
			: base(sender)
		{
			Exception = exception;
		}

		public TException Exception { get; private set; }
	}
}