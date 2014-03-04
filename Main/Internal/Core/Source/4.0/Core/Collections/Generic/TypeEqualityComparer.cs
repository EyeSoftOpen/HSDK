namespace EyeSoft.Collections.Generic
{
	using System;
	using System.Collections.Generic;

	internal class TypeEqualityComparer : IEqualityComparer<Type>
	{
		public bool Equals(Type x, Type y)
		{
			var assignable = x.IsAssignableFrom(y);

			return assignable;
		}

		public int GetHashCode(Type obj)
		{
			return 0;
		}
	}
}