namespace EyeSoft.Core.Mapping
{
    using Conventions;
    using Core.Conventions;
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

        public TypeConvention<object, object> TypeConvention { get; private set; }

        public IKeyConvention KeyConvention { get; private set; }

        public IMemberStrategy PrimitiveStrategy { get; private set; }

        public IMemberStrategy ReferenceStrategy { get; private set; }

        public IMemberStrategy CollectionStrategy { get; private set; }

        public IVersionConvention VersionConvention { get; private set; }
    }
}