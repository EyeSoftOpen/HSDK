namespace EyeSoft.Core.Test.Threading
{
    using System.Globalization;
    using System.Threading;

    public static class ThreadExtensions
	{
		public static void AssignCulture(this Thread thread, string culture)
		{
			var cultureInfo = CultureInfo.CreateSpecificCulture(culture);
			thread.AssignCulture(cultureInfo);
		}

		public static void AssignCulture(this Thread thread, CultureInfo cultureInfo)
		{
			thread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = cultureInfo;
		}
	}
}
