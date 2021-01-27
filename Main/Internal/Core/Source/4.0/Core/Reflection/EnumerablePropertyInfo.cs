namespace EyeSoft.Core.Reflection
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class EnumerablePropertyInfo : IEnumerable<PropertyInfo>
	{
		private readonly IEnumerable<PropertyInfo> properties;

		public EnumerablePropertyInfo(IEnumerable<PropertyInfo> properties)
		{
			this.properties = properties;
		}

		public IEnumerator<PropertyInfo> GetEnumerator()
		{
			return properties.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}