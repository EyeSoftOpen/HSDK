namespace EyeSoft.Windows.Model.Test
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public abstract class ServiceFactoryTest
	{
		protected readonly ServiceFactoryHelper factoryHelper = ServiceFactoryHelper.Create();

		[TestCleanup]
		public void CleanUp()
		{
			factoryHelper.Dispose();
		}
	}
}