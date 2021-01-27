namespace EyeSoft.Core.Messanging
{
    using System;

    public class ExceptionMessage
		: ExceptionMessage<object, Exception>
	{
		public ExceptionMessage(object sender, Exception exception)
			: base(sender, exception)
		{
		}
	}
}