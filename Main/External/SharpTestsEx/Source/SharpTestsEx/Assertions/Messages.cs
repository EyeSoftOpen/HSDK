using System;
using System.Collections;
using System.Text;

namespace SharpTestsEx.Assertions
{
	public class Messages
	{
		public static string FormatValue(object value)
		{
			if (ReferenceEquals(null, value))
			{
				return Properties.Resources.NullValue;
			}
			if (value.GetType() == typeof (string))
			{
				return string.Format("\"{0}\"", value);
			}
			
			var v = value as IEnumerable;
			if (v != null)
			{
				return FormatEnumerable(v);
			}
			if (value is DateTime)
			{
				return string.Format("{0:d} {1}", (DateTime)value, ((DateTime)value).TimeOfDay);
			}

			return value.ToString();
		}

		public static string FormatEnumerable(IEnumerable enumerable)
		{
			if (ReferenceEquals(null, enumerable))
			{
				return Properties.Resources.NullValue;
			}
			var result = new StringBuilder(200);
			result.Append('[');
			bool appendComma = false;
			foreach (var element in enumerable)
			{
				if (appendComma)
				{
					result.Append(", ");
				}
				result.Append(FormatValue(element));
				appendComma = true;
			}
			if (!appendComma)
			{
				// is empty
				result.Append(Properties.Resources.EmptyEnumerable);
			}
			result.Append(']');
			return result.ToString();
		}
	}
}