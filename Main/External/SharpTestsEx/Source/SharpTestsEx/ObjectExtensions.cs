using System;
using System.Reflection;

namespace SharpTestsEx
{
	/// <summary>
	/// Extensions for any System.Object.
	/// </summary>
	public static class ObjectExtensions
	{
		private const BindingFlags DefaultFlags =
			BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;

		/// <summary>
		/// Allow access to a private field of a class instance.
		/// </summary>
		/// <typeparam name="T">The <see cref="Type"/> of the field. </typeparam>
		/// <param name="source">The class instance.</param>
		/// <param name="fieldName">The field name.</param>
		/// <returns>The value of the field.</returns>
		public static T FieldValue<T>(this object source, string fieldName)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source", Properties.Resources.ExceptionMsgAccessToField);
			}
			Type sourceType = source.GetType();
			FieldInfo fieldInfo = sourceType.GetTypeInfo().GetField(fieldName, DefaultFlags);
			if (ReferenceEquals(null, fieldInfo))
			{
				throw new ArgumentOutOfRangeException("fieldName",
				                                      string.Format(Properties.Resources.ExceptionMsgFieldNameTmpl,
				                                                    sourceType.FullName, fieldName));
			}
			if (fieldInfo.FieldType != typeof (T))
			{
				throw new InvalidCastException(string.Format(Properties.Resources.ExceptionMsgInvalidCastTmpl, sourceType.FullName,
				                                             fieldName, fieldInfo.FieldType.FullName, typeof (T).FullName));
			}

			return (T) fieldInfo.GetValue(source);
		}
	}
}