namespace EyeSoft.Core.Collections.Generic
{
    using System;
    using System.Collections.Generic;

    public class FuncToComparer<T> : IComparer<T>
	{
		private readonly Func<T, T, int> func;

		public FuncToComparer(Func<T, T, int> func)
		{
			this.func = func;
		}

		public int Compare(T x, T y)
		{
			return func(x, y);
		}
	}
}