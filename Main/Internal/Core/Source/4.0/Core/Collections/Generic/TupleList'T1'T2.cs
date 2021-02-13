namespace EyeSoft.Collections.Generic
{
    using System;
    using System.Collections.Generic;

    public class TupleList<T1, T2> : List<Tuple<T1, T2>>
	{
		public void Add(T1 item1, T2 item2)
		{
			Add(new Tuple<T1, T2>(item1, item2));
		}
	}
}