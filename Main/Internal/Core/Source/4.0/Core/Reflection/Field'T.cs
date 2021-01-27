namespace EyeSoft.Core.Reflection
{
    using System.Reflection;

    public class Field<T>
	{
		private readonly FieldInfo fieldInfo;

		public Field(FieldInfo fieldInfo)
		{
			this.fieldInfo = fieldInfo;
		}

		public T Value(object obj)
		{
			return (T)fieldInfo.GetValue(obj);
		}
	}
}