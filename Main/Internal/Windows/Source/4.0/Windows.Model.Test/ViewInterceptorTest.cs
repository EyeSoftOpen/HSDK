namespace EyeSoft.Wpf.Facilities.Test
{
	using EyeSoft.Wpf.Facilities.ApplicationServices;
	using EyeSoft.Wpf.Facilities.Test.Helpers;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ViewInterceptorTest
	{
		[TestMethod]
		public void CreateWindowVerifyDataContextIsAutomaticallyAssigned()
		{
			ViewInterceptor.Register(Container.Create(), new ViewToViewModelStub());

			var window = new MainWindow();
			window.DataContext.Should().Not.Be.Null();
		}
	}
}