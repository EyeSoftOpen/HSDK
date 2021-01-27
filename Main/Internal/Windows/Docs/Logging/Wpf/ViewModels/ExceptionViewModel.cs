namespace EyeSoft.Docs.Logging.Windows.ViewModels
{
    using System;

    public class ExceptionViewModel
	{
		public ExceptionViewModel(string message, DateTime dateTime)
		{
			Message = message;
			DateTime = dateTime;
		}

		public string Message { get; set; }

		public DateTime DateTime { get; set; }
	}
}