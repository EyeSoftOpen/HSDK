namespace EyeSoft.Mapping
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Extensions;
    using EyeSoft.Conventions;
    using Reflection;

    public class MetadataConvention : TypeConvention<object, object>
    {
        protected override Type TryMapTo(Type type)
        {
            var metadataAttribute = type.GetAttribute<MetadataTypeAttribute>();

            if (metadataAttribute.IsNotNull())
            {
                return metadataAttribute.MetadataClassType;
            }

            var metadataType =
                type.Assembly
                    .GetType($"{type.FullName}Metadata");

            return
                metadataType ?? type;
        }
    }
}