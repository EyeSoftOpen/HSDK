<Query Kind="Program" />

void Main()
{
	new DirectoryInfo(@"D:\GitHub\HSDK\Main\.nuget")
		.GetFiles("*.nupkg")
		.ToList()
		.ForEach(x =>
		{		
			Util.Cmd($@"D:\GitHub\HSDK\Main\.nuget\nuget.exe push ""{x.FullName}"" -Source https://www.nuget.org/api/v2/package");
		});		
}
