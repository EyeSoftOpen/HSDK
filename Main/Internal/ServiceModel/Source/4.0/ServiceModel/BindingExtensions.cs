namespace EyeSoft.ServiceModel
{
	using System;
	using System.ServiceModel;
	using System.ServiceModel.Channels;
	using System.ServiceModel.Security.Tokens;

	public static class BindingExtensions
	{
		public static Binding SetClockSkew(this WSHttpBinding binding, TimeSpan clockSkew)
		{
			var customBinding = new CustomBinding(binding);

			var security = customBinding.Elements.Find<SymmetricSecurityBindingElement>();

			security.LocalClientSettings.MaxClockSkew = clockSkew;
			security.LocalServiceSettings.MaxClockSkew = clockSkew;

			var secureTokenParams = (SecureConversationSecurityTokenParameters)security.ProtectionTokenParameters;
			var bootstrap = secureTokenParams.BootstrapSecurityBindingElement;
			bootstrap.LocalClientSettings.MaxClockSkew = clockSkew;
			bootstrap.LocalServiceSettings.MaxClockSkew = clockSkew;

			return customBinding;
		}

		public static Binding ChangeMessageSecurityOptions(this Binding binding)
		{
			var customBinding = new CustomBinding(binding);

			var minutes15 = TimeSpan.FromMinutes(15);

			customBinding.ReceiveTimeout = minutes15;
			customBinding.SendTimeout = minutes15;

			var tokenParameters =
				new X509SecurityTokenParameters
					{
						X509ReferenceStyle = X509KeyIdentifierClauseType.Any,
						RequireDerivedKeys = false,
						InclusionMode = SecurityTokenInclusionMode.AlwaysToRecipient
					};

			var securityBindingElement = customBinding.Elements.Find<AsymmetricSecurityBindingElement>();

			securityBindingElement.MessageSecurityVersion = MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10;
			securityBindingElement.InitiatorTokenParameters = tokenParameters;
			securityBindingElement.LocalClientSettings.DetectReplays = false;

			securityBindingElement.IncludeTimestamp = false;
			securityBindingElement.LocalClientSettings.TimestampValidityDuration = new TimeSpan(12, 0, 0);

			return customBinding;
		}

		public static Binding EscludeTimeStamp(this WSHttpBinding binding)
		{
			var elements = binding.CreateBindingElements();
			elements.Find<SecurityBindingElement>().IncludeTimestamp = false;

			var customBinding = new CustomBinding(elements);

			return customBinding;
		}
	}
}