namespace EyeSoft.Web
{
	using System;
	using System.IdentityModel.Selectors;
	using System.IdentityModel.Tokens;
	using System.Security;

	public abstract class BaseUserNamePasswordValidator : UserNamePasswordValidator
	{
		public override void Validate(string userName, string password)
		{
			if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
			{
				throw new SecurityTokenException("Username and password required.");
			}

			if (IsValidUsingCache(userName, password))
			{
				return;
			}

			try
			{
				var isValidCredentail = IsValidCredential(userName, password);

				if (isValidCredentail)
				{
					SaveCredentialInCache(userName, password);

					return;
				}

				var exception = new SecurityException(string.Format("Credential {0} not valid.", userName));

				throw exception;
			}
			catch (Exception exception)
			{
				throw ThrowFaultAndLogException(exception);
			}
		}

		protected abstract Exception ThrowFault(Exception exception);

		protected virtual bool IsValidUsingCache(string userName, string password)
		{
			return false;
		}

		protected virtual void SaveCredentialInCache(string userName, string password)
		{
		}

		protected virtual void LogException(Exception exception)
		{
		}

		protected abstract bool IsValidCredential(string userName, string password);

		private Exception ThrowFaultAndLogException(Exception exception)
		{
			LogException(exception);

			return ThrowFault(exception);
		}
	}
}