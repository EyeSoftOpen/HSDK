namespace EyeSoft.ServiceModel.Hosting.Web
{
	using System;

	using EyeSoft.ServiceModel.Description;

	[AttributeUsage(AttributeTargets.Class)]
	public class ExceptionErrorHandlerBehaviorAttribute : ErrorHandlerAttribute
	{
		protected override void ProcessUnhandledException(Exception exception)
		{
		}
	}
}