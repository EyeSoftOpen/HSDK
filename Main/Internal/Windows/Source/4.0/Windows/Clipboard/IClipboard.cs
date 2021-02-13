namespace EyeSoft.Windows
{
	using System.Windows;

	internal interface IClipboard
	{
		string GetText();

		string GetText(TextDataFormat textDataFormat);

		void SetText(string text);

		void SetText(string text, TextDataFormat textDataFormat);

		void SetDataObject(object text);

		void SetDataObject(object data, bool leaveCopy);
	}
}