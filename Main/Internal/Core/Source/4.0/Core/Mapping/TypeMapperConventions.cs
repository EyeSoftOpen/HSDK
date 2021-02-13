namespace EyeSoft.Mapping
{
    using Conventions;
    using EyeSoft.Conventions;
    using Strategies;

    public class TypeMapperConventions
    {
        public TypeMapperConventions(
            IMemberStrategy primitiveStrategy,
            IMemberStrategy referenceStrategy,
            IMemberStrategy collectionStrategy,
            TypeConvention<object, object> typeConvention,
            IKeyConvention keyConvention,
            IVersionConvention versionConvention)
        {
            PrimitiveStrategy = primitiveStrategy;
            ReferenceStrategy = referenceStrategy;
            CollectionStrategy = collectionStrategy;
            TypeConvention = typeConvention;
            KeyConvention = keyConvention;
            VersionConvention = versionConvention;
        }

        public TypeMapperConventions(IKeyConvention keyConvention, IVersionConvention versionConvention)
        {
            KeyConvention = keyConvention;
            VersionConvention = versionConvention;
        }

        public TypeMapperConventions(
            TypeConvention<object, object> typeConvention,
            IKeyConvention keyConvention,
            IVersionConvention versionConvention)
        {
            TypeConvention = typeConvention;
            KeyConvention = keyConvention;
            VersionConvention = versionConvention;
        }

        public TypeConvention<object, object> TypeConvention { get; }

        public IKeyConvention KeyConvention { get; }

        public IMemberStrategy PrimitiveStrategy { get; }

        public IMemberStrategy ReferenceStrategy { get; }

        public IMemberStrategy CollectionStrategy { get; }

        public IVersionConvention VersionConvention { get; }
    }
}