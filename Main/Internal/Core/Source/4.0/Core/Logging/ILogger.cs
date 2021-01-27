namespace EyeSoft.Core.Logging
{
    using System;

    public interface ILogger
	{
		void Write(string message);

		void Error(Exception exception);
	}
}