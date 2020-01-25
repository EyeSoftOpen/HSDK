namespace EyeSoft.Mapping
{
    using System;
    using System.Reflection;

    using EyeSoft.Extensions;

    public class MemberInfoMetadata : MemberInfo
    {
        private readonly MemberInfo memberInfo;

        private readonly Type reflectedType;

        private readonly bool nullReflectedTypeFromConstructor;

        internal MemberInfoMetadata(MemberInfo memberInfo) : this(memberInfo, null)
        {
            nullReflectedTypeFromConstructor = true;
        }

        internal MemberInfoMetadata(MemberInfo memberInfo, Type reflectedType)
        {
            if (reflectedType == null && nullReflectedTypeFromConstructor)
            {
                throw new ArgumentException("The reflected type cannot be null", "reflectedType");
            }

            if ((memberInfo.MemberType != MemberTypes.Property) && (memberInfo.MemberType != MemberTypes.Field))
            {
                throw new ArgumentException("Allowed only field or property in the constructor.", "memberInfo");
            }

            this.memberInfo = memberInfo;
            this.reflectedType = reflectedType;

            SetType();
            SetCanRead();
            SetCanWrite();
            Accessor = AccessorHelper.Get(memberInfo);

            if (!(memberInfo is MemberInfoMetadata))
            {
                Incapsulated = memberInfo;
                return;
            }

            var incapsulated = memberInfo;

            while (incapsulated is MemberInfoMetadata)
            {
                incapsulated = (incapsulated as MemberInfoMetadata).Incapsulated;
            }

            Incapsulated = incapsulated;
        }

        public override string Name
        {
            get { return memberInfo.Name; }
        }

        public override Type DeclaringType
        {
            get { return memberInfo.DeclaringType; }
        }

        public override Type ReflectedType
        {
            get { return reflectedType ?? memberInfo.ReflectedType; }
        }

        public bool CanRead { get; private set; }

        public bool CanWrite { get; private set; }

        public Type Type { get; private set; }

        public override MemberTypes MemberType
        {
            get { return memberInfo.MemberType; }
        }

        public override int MetadataToken
        {
            get { return memberInfo.MetadataToken; }
        }

        public Accessors Accessor { get; private set; }

        public MemberInfo Incapsulated { get; private set; }

        public override object[] GetCustomAttributes(bool inherit)
        {
            return memberInfo.GetCustomAttributes(inherit);
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return memberInfo.GetCustomAttributes(attributeType, inherit);
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            return memberInfo.IsDefined(attributeType, inherit);
        }

        public override string ToString()
        {
            return $"{Type.Name} {Name}";
        }

        private void SetType()
        {
            if (memberInfo is MemberInfoMetadata)
            {
                Type = memberInfo.Convert<MemberInfoMetadata>().Type;
                return;
            }

            if (memberInfo.MemberType == MemberTypes.Field)
            {
                Type = memberInfo.Convert<FieldInfo>().FieldType;
                return;
            }

            if (memberInfo.MemberType == MemberTypes.Property)
            {
                Type = memberInfo.Convert<PropertyInfo>().PropertyType;
            }
        }

        private void SetCanRead()
        {
            if (memberInfo is MemberInfoMetadata)
            {
                CanRead = memberInfo.Convert<MemberInfoMetadata>().CanRead;
                return;
            }

            if (memberInfo.MemberType == MemberTypes.Field)
            {
                CanRead = true;
                return;
            }

            CanRead = memberInfo.Convert<PropertyInfo>().CanRead;
        }

        private void SetCanWrite()
        {
            if (memberInfo is MemberInfoMetadata)
            {
                CanWrite = memberInfo.Convert<MemberInfoMetadata>().CanWrite;
                return;
            }

            if (memberInfo.MemberType == MemberTypes.Field)
            {
                CanWrite = true;
                return;
            }

            CanWrite = memberInfo.Convert<PropertyInfo>().CanWrite;
        }
    }
}