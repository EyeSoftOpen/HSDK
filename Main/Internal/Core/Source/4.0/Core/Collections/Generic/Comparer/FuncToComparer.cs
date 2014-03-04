namespace EyeSoft.Collections.Generic
{
	using System;
	using System.Collections;

	public class FuncToComparer : IComparer
	{
		private readonly Func<object, object, int> func;

		public FuncToComparer(Func<object, object, int> func)
		{
			this.func = func;
		}

		public int Compare(object x, object y)
		{
			return func(x, y);
		}
	}
}