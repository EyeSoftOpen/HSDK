namespace EyeSoft.Validation
{
	using System.Collections.Generic;

	public class ValidationError
	{
		public ValidationError(string propertyName, string message, object value)
		{
			PropertyName = propertyName;
			Message = message;
			Value = value;
		}

		public string PropertyName { get; private set; }

		public string Message { get; private set; }

		public object Value { get; private set; }

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			var other = (ValidationError)obj;

			return
				Comparer<string>.Default.Compare(PropertyName, other.PropertyName) == 0 &&
				Comparer<string>.Default.Compare(Message, other.Message) == 0;
		}

		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("Property {0} is not valid. Reason: {1}", PropertyName, Message);
		}
	}
}