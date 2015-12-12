namespace EyeSoft.Nuget.Publisher.Shell
{
	using System;

	public class SingularPluralTimeFormatter : ICustomFormatter, IFormatProvider
	{
		private readonly string plural;

		private readonly string singular;

		public SingularPluralTimeFormatter() { }

		private SingularPluralTimeFormatter(string plural, string singular)
		{
			this.plural = plural;
			this.singular = singular;
		}

		public object GetFormat(Type formatType)
		{
			return formatType == typeof(ICustomFormatter) ? this : null;
		}

		public string Format(string format, object arg, IFormatProvider formatProvider)
		{
			if (arg == null)
			{
				return string.Format(format, (object)null);
			}

			string text;

			switch (format)
			{
				case "S": // second
					text = string.Format(new SingularPluralTimeFormatter("{0} seconds", "{0} second"), "{0}", arg);
					break;
				case "M": // minute
					text = string.Format(new SingularPluralTimeFormatter("{0} minutes", "{0} minute"), "{0}", arg);
					break;
				case "H": // hour
					text = string.Format(new SingularPluralTimeFormatter("{0} hours", "{0} hour"), "{0}", arg);
					break;
				case "D": // day
					text = string.Format(new SingularPluralTimeFormatter("{0} days", "{0} day"), "{0}", arg);
					break;
				default:
					var value = (int)arg;
					var singularPluralFormat = (value == 0 || value > 1) ? plural : singular;

					// plural/ singular
					text = string.Format(singularPluralFormat, arg);  // watch the cast to int here...
					break;
			}

			return text;
		}
	}
}