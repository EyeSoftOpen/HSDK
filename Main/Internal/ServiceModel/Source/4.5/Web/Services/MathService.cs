namespace EyeSoft.ServiceModel.Hosting.Web
{
	internal class MathService : IMathService
	{
		private readonly Calculator calculator;

		public MathService(Calculator calculator)
		{
			this.calculator = calculator;
		}

		public int Sum(int a, int b)
		{
			return calculator.Sum(a, b);
		}
	}
}