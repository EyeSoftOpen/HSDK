namespace EyeSoft.Data.Nhibernate.Mapping
{
	using System.Collections.Generic;
	using System.Reflection;
    using EyeSoft.Mapping;
    using NHibernate.Mapping.ByCode;

	internal static class AccessorHelper
	{
		private static readonly IDictionary<Accessors, Accessor> accessorsDictionary =
			new Dictionary<Accessors, Accessor>
				{
					{ Accessors.Field, Accessor.Field },
					{ Accessors.ReadOnly, Accessor.ReadOnly },
					{ Accessors.Property, Accessor.Property },
					{ Accessors.PropertyNoSetter, Accessor.NoSetter }
				};

		public static Accessor Translate(Accessors accessor)
		{
			return accessorsDictionary[accessor];
		}

		public static void SetAccessor(MemberInfoMetadata member, IAccessorPropertyMapper m)
		{
			if (member.Accessor == Accessors.Field)
			{
				m.Access(AccessorHelper.Translate(member.Accessor));
			}

			if (member.MemberType == MemberTypes.Property)
			{
				m.Access(Accessor.Property);
			}
		}
	}
}