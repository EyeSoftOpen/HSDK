namespace EyeSoft.Core.Collections.Generic
{
    using System;
    using System.Collections.Generic;

    public class TupleList<T1, T2, T3> : List<Tuple<T1, T2, T3>>
	{
		public void Add(T1 item1, T2 item2, T3 item3)
		{
			Add(new Tuple<T1, T2, T3>(item1, item2, item3));
		}
	}
}