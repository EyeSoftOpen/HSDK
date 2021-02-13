namespace EyeSoft.Conventions
{
    using System;
    using Extensions;

    public abstract class TypeConvention<TSource, TDestination>
    {
        public Type MapTo(Type type)
        {
            if (!type.EqualsOrSubclassOf(typeof(TSource)))
            {
                throw new ArgumentException("The source type must be of type {type}.");
            }

            var mapTo = TryMapTo(type);

            if (!mapTo.EqualsOrSubclassOf(typeof(TDestination)))
            {
                throw new ArgumentException("The destination type must be of type {type}.");
            }

            return mapTo;
        }

        protected abstract Type TryMapTo(Type type);
    }
}
