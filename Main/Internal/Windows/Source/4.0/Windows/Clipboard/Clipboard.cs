namespace EyeSoft.Windows.Clipboard
{
    using System.Windows;
    using Core;

    public class Clipboard : IClipboard
	{
		private static readonly Singleton<IClipboard> singletonInstance =
			new Singleton<IClipboard>(() => new ClipboardWrapper());

		public string GetText()
		{
			return singletonInstance.Instance.GetText();
		}

		public string GetText(TextDataFormat textDataFormat)
		{
			return singletonInstance.Instance.GetText(textDataFormat);
		}

		public void SetText(string text)
		{
			singletonInstance.Instance.SetText(text);
		}

		public void SetText(string text, TextDataFormat textDataFormat)
		{
			singletonInstance.Instance.SetText(text, textDataFormat);
		}

		public void SetDataObject(object text)
		{
			singletonInstance.Instance.SetDataObject(text);
		}

		public void SetDataObject(object data, bool leaveCopy)
		{
			singletonInstance.Instance.SetDataObject(data, leaveCopy);
		}
	}
}