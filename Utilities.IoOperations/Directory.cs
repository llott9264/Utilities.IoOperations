namespace Utilities.IoOperations;

public class Directory
{
	public static void DeleteDirectory(DirectoryInfo directory, int retentionLengthInMonths, bool isBaseFolder)
	{
		directory.EnumerateFiles()
			.Where(f => f.LastAccessTime < DateTime.Now.AddMonths(retentionLengthInMonths))
			.ToList()
			.ForEach(f => { f.Delete(); });

		directory.EnumerateDirectories()
			.ToList()
			.ForEach(d => { DeleteDirectory(d, retentionLengthInMonths, false); });

		if (!isBaseFolder
		    && directory.EnumerateFiles().ToList().Count == 0
		    && directory.EnumerateDirectories().ToList().Count == 0)
		{
			directory.Delete();
		}
	}

	public static void DeleteFilesInFolder(DirectoryInfo directory)
	{
		directory.EnumerateFiles()
			.ToList()
			.ForEach(f => f.Delete());
	}
	
	public static void CreateDirectory(string folder)
	{
		if (!System.IO.Directory.Exists(folder))
		{
			System.IO.Directory.CreateDirectory(folder);
		}
	}
}
