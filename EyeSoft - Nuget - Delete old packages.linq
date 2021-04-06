<Query Kind="Program" />

void Main()
{
	new DirectoryInfo(@"D:\GitHub\HSDK\Packages")
		.GetFiles("*.*")
		.Where(x => !x.Name.Contains("3.0.7760.34875"))
		.ToList()
		.ForEach(x => x.Delete());
}
