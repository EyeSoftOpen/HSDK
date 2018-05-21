namespace EyeSoft.Collections.Generic
{
    using System;
    using System.Collections.Generic;

    public class FuncToEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> func;

        private readonly Func<T, int> hashCode;

        public FuncToEqualityComparer(Func<T, T, bool> func, Func<T, int> hashCode)
        {
            this.func = func;
            this.hashCode = hashCode;
        }

        public bool Equals(T x, T y)
        {
            return func(x, y);
        }

        public int GetHashCode(T obj)
        {
            return hashCode(obj);
        }
    }
}