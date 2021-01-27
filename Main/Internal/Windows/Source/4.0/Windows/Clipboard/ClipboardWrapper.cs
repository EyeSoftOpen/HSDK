namespace EyeSoft.Windows.Clipboard
{
    using System.Windows;

    internal class ClipboardWrapper : IClipboard
	{
		public string GetText()
		{
			return System.Windows.Clipboard.GetText();
		}

		public string GetText(TextDataFormat textDataFormat)
		{
			return System.Windows.Clipboard.GetText(textDataFormat);
		}

		public void SetText(string text)
		{
			try
			{
				System.Windows.Clipboard.SetText(text);
			}
				// ReSharper disable once EmptyGeneralCatchClause
			catch
			{
			}
		}

		public void SetText(string text, TextDataFormat textDataFormat)
		{
			try
			{
				System.Windows.Clipboard.SetText(text, textDataFormat);
			}
				// ReSharper disable once EmptyGeneralCatchClause
			catch
			{
			}
		}

		public void SetDataObject(object data)
		{
			SetDataObject(data, false);
		}

		public void SetDataObject(object data, bool leaveCopy)
		{
			if (!leaveCopy)
			{
				System.Windows.Clipboard.SetDataObject(data);
				return;
			}

			System.Windows.Clipboard.SetDataObject(data, true);
		}
	}
}