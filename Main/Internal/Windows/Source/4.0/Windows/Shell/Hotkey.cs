namespace EyeSoft.Windows.Shell
{
	using System;
	using System.Text;

	public class Hotkey
	{
		public Hotkey(bool control, bool shift, char letter)
		{
			var builder = new StringBuilder();

			if (control)
			{
				builder.Append("Ctrl+");
			}

			if (shift)
			{
				builder.Append("Shift+");
			}

			if (builder.Length == 0)
			{
				throw new ArgumentException("The control key or shift or both must be specified.");
			}

			builder.Append(char.ToUpper(letter));

			Text = builder.ToString();
		}

		public string Text { get; private set; }
	}
}