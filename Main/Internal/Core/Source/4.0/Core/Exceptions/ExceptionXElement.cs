namespace EyeSoft
{
	using System;
	using System.Collections;
	using System.Linq;
	using System.Xml.Linq;

	/// <summary>Represent an Exception as XML data.</summary>
	public class ExceptionXElement : XElement
	{
		/// <summary>Initializes a new instance.</summary>
		/// <param name="exception">The Exception to serialize.</param>
		public ExceptionXElement(Exception exception) : this(exception, false)
		{
		}

		/// <summary>Initializes a new instance.</summary>
		/// <param name="exception">The Exception to serialize.</param>
		/// <param name="omitStackTrace">
		/// Whether or not to serialize the Exception.StackTrace member
		/// if it's not null.
		/// </param>
		public ExceptionXElement(Exception exception, bool omitStackTrace) : base(FormatFunc(exception, omitStackTrace)())
		{
		}

		private static Func<XElement> FormatFunc(Exception exception, bool omitStackTrace)
		{
			return () => Format(exception, omitStackTrace);
		}

		private static XElement Format(Exception exception, bool omitStackTrace)
		{
			// Validate arguments
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}

			// The root element is the Exception's type
			var root = new XElement(exception.GetType().ToString());

			root.Add(new XElement("Message", exception.Message));

			// StackTrace can be null, e.g.:
			// new ExceptionAsXml(new Exception())
			if (!omitStackTrace && exception.StackTrace != null)
			{
				var elements =
					exception.StackTrace.Split('\n')
						.Select(frame => new { frame, prettierFrame = frame.Replace("   at ", null).Trim() })
						.Select(frame => new XElement("Frame", frame.prettierFrame));

				root.Add(new XElement("StackTrace", elements));
			}

			// Data is never null; it's empty if there is no data
			if (exception.Data.Count > 0)
			{
				var temp =
					exception.Data.Cast<DictionaryEntry>()
						.Select(entry => new { entry, key = entry.Key.ToString() })
						.Select(arg => new { t = arg, value = (arg.entry.Value == null) ? "null" : arg.entry.Value.ToString() })
						.Select(arg => new XElement(arg.@t.key, arg.value));

				root.Add(new XElement("Data", temp));
			}

			// Add the InnerException if it exists
			if (exception.InnerException != null)
			{
				root.Add(new ExceptionXElement(exception.InnerException, omitStackTrace));
			}

			return root;
		}
	}
}