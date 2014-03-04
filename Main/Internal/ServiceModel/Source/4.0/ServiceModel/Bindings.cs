namespace EyeSoft.ServiceModel
{
	using System.ServiceModel;
	using System.ServiceModel.Channels;

	public static class Bindings
	{
		public static Binding Basic
		{
			get { return new BasicHttpBinding(); }
		}

		public static WSHttpBinding WsMessageWithUsername
		{
			get
			{
				var wsBinding = new WSHttpBinding(SecurityMode.Message);
				wsBinding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;

				return wsBinding;
			}
		}
	}
}