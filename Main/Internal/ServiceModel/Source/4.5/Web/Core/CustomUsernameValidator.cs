namespace EyeSoft.ServiceModel.Hosting.Web
{
	using System;
	using System.ServiceModel;

	using EyeSoft.Web;

	public class CustomUsernameValidator : BaseUserNamePasswordValidator
	{
		protected override bool IsValidCredential(string userName, string password)
		{
			var isValid =
				userName.Equals("user@domain.com", StringComparison.InvariantCultureIgnoreCase) &&
				password.Equals("1FUEI6TYmn9yUHWxeJpVafcE85o=");

			return isValid;
		}

		protected override Exception ThrowFault(Exception exception)
		{
			return new FaultException("Credential not valid.");
		}
	}
}