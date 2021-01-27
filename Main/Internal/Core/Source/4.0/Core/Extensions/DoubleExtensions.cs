namespace EyeSoft.Core.Extensions
{
    using System;

    public static class DoubleExtensions
	{
		public static int RoundToInt(this double value)
		{
			return (int)Math.Round(value);
		}
	}
}