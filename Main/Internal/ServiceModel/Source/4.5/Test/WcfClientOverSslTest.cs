////namespace EyeSoft.ServiceModel.Test
////{
////	using System;

////	using System.Security.Cryptography.X509Certificates;
////	using System.ServiceModel;
////	using System.ServiceModel.Security;

////	using Castle.Facilities.WcfIntegration;
////	using Castle.Facilities.WcfIntegration.Behaviors;
////	using Castle.MicroKernel.Registration;
////	using Castle.Windsor;

////	using EyeSoft.ServiceModel.Hosting.Web;

////	using Microsoft.VisualStudio.TestTools.UnitTesting;

////	using SharpTestsEx;

////	[TestClass]
////	public class WcfClientOverSslTest
////	{
////		[TestMethod]
////		public void VerifyWcfClienCallOverSsl()
////		{
////			new ProxyContainer("localhost", 44301, "EyeMobile", "user@domain.com", "1FUEI6TYmn9yUHWxeJpVafcE85o=")
////				.Register<IMathService>()
////				.Proxy<IMathService>()
////				.Sum(3, 5).Should().Be.EqualTo(8);
////		}

////		public class ProxyContainer
////		{
////			private readonly IWindsorContainer container = new WindsorContainer();

////			private readonly string host;

////			private readonly int port;

////			private readonly string certificateName;

////			private readonly string username;

////			private readonly string password;

////			public ProxyContainer(string host, int port, string certificateName, string username, string password)
////			{
////				container.Kernel.AddFacility<WcfFacility>();

////				this.host = host;
////				this.port = port;
////				this.certificateName = certificateName;
////				this.username = username;
////				this.password = password;
////			}

////			public ProxyContainer Register<TContract>() where TContract : class
////			{
////				var binding = new WSHttpBinding(SecurityMode.TransportWithMessageCredential);
////				binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
////				binding.SendTimeout = TimeSpan.FromMinutes(15);
////				binding.MaxReceivedMessageSize = int.MaxValue;

////				var uri = new UriBuilder("https", host, port, typeof(TContract).Name.Substring(1)).Uri;
////				var identity = EndpointIdentity.CreateDnsIdentity(certificateName);
////				var address = new EndpointAddress(uri, identity);

////				var endpoint = WcfEndpoint.BoundTo(binding).At(address);

////				var credential =
////					new UserNameCredentials(username, password)
////						{
////							CertificateValidationMode = X509CertificateValidationMode.None,
////							RevocationMode = X509RevocationMode.NoCheck
////						};

////				var model = new DefaultClientModel { Endpoint = endpoint }.Credentials(credential);
////				container.Register(Component.For<TContract>().AsWcfClient(model).LifestyleTransient());

////				return this;
////			}

////			public TContract Proxy<TContract>() where TContract : class
////			{
////				return container.Resolve<TContract>();
////			}
////		}
////	}
////}