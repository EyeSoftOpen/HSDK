namespace EyeSoft.Windows.Shell
{
	using System;
	using System.IO;
	using System.Runtime.InteropServices;

	public class ShellExtensions
	{
		public static void CreateShortcut(string title, string linkPath, string filePath, Hotkey hotkey = null)
		{
			var shellType = Type.GetTypeFromProgID("WScript.Shell");
			dynamic shell = Activator.CreateInstance(shellType);
			var shortcut = shell.CreateShortcut(linkPath);

			if (hotkey != null)
			{
				shortcut.Hotkey = hotkey.Text;
			}

			shortcut.Description = title;
			shortcut.TargetPath = filePath;
			shortcut.Save();

			Marshal.ReleaseComObject(shell);
		}

		public static void PinUnpinTaskBar(string filePath, bool pin)
		{
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException(filePath);
			}

			try
			{

				dynamic shellApplication = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"));

				var path = Path.GetDirectoryName(filePath);
				var fileName = Path.GetFileName(filePath);

				var directory = shellApplication.NameSpace(path);
				var link = directory.ParseName(fileName);

				var verbs = link.Verbs();

				var searhingVerb = pin ? "pin to taskbar" : "unpin from taskbar";

				for (var i = 0; i < verbs.Count(); i++)
				{
					var verb = verbs.Item(i);
					string verbName = verb.Name.Replace(@"&", null).ToLower();

					if (!verbName.Equals(searhingVerb))
					{
						continue;
					}

					verb.DoIt();
					break;
				}

				Marshal.ReleaseComObject(shellApplication);

			}
			catch (Exception e)
			{
			    throw;
			}
		}
	}
}
