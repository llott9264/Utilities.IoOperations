namespace Utilities.IoOperations
{
	public class File
	{
		public static bool Move(string sourceFile, string destinationFolder)
		{
			bool isFileMoved = false;
			string destinationFile = $@"{destinationFolder}\{Path.GetFileName(sourceFile)}";

			Directory.CreateDirectory(destinationFolder);

			if (!System.IO.File.Exists(destinationFolder))
			{
				System.IO.File.Move(sourceFile, destinationFile);
				isFileMoved = true;
			}

			return isFileMoved;
		}
	}
}