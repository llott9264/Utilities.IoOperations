namespace Utilities.IoOperations
{
	public class File
	{
		public static bool Move(string sourceFile, string destinationFolder)
		{
			bool isFileMoved = false;
			string destinationFileFullPath = $@"{destinationFolder}\{Path.GetFileName(sourceFile)}";
			
			Directory.CreateDirectory(destinationFolder);

			if (!System.IO.File.Exists(destinationFileFullPath))
			{
				System.IO.File.Move(sourceFile, destinationFileFullPath);
				isFileMoved = true;
			}

			return isFileMoved;
		}
	}
}