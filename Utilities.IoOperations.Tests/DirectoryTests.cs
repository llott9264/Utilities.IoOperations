namespace Utilities.IoOperations.Tests
{
	public class DirectoryTests
	{
		[Fact]
		public void CreateDirectory_ReturnsDirectory_True()
		{
			//Arrange
			const string directoryPath = @"DirectoryFolder\NewDirectory\";
			if (System.IO.Directory.Exists(directoryPath)) System.IO.Directory.Delete(directoryPath);

			//Act
			Utilities.IoOperations.Directory.CreateDirectory(directoryPath);

			//Assert
			Assert.True(System.IO.Directory.Exists(directoryPath));
		}

		[Fact]
		public void DeleteDirectory_Returns_True()
		{
			//Arrange
			const string deleteDirectoryPath = @"DirectoryFolder\DeleteDirectory\";
			DirectoryInfo di = new(deleteDirectoryPath);
			int fileRetentionPeriodInMonths = -1;
			const string deletedFolderPath = $@"{deleteDirectoryPath}\DirectoryToDelete\";
			const string deletedFolderFileToDelete = $@"{deleteDirectoryPath}\DirectoryToDelete\FileToDelete.txt";

			const string notDeletedFolderPath = $@"{deleteDirectoryPath}\DirectoryNotToDelete\";
			const string notDeletedFolderFileToDelete = $@"{notDeletedFolderPath}\FileToDelete.txt";
			const string notDeletedFile = $@"{notDeletedFolderPath}\FileNotToDelete.txt";

			DateTime deleteLastAccessDateTime = DateTime.Now.AddMonths(-3);
			DateTime notDeleteLastAccessDateTime = DateTime.Now.AddDays(-3);

			System.IO.Directory.SetLastAccessTime(deletedFolderPath, deleteLastAccessDateTime);
			System.IO.File.SetLastAccessTime(deletedFolderFileToDelete, deleteLastAccessDateTime);

			System.IO.Directory.SetLastAccessTime(notDeletedFolderPath, notDeleteLastAccessDateTime);
			System.IO.File.SetLastAccessTime(notDeletedFolderFileToDelete, deleteLastAccessDateTime);
			System.IO.File.SetLastAccessTime(notDeletedFile, notDeleteLastAccessDateTime);
			
			//Act
			Directory.DeleteDirectory(di, fileRetentionPeriodInMonths, true);

			//Assert
			Assert.True(!System.IO.Directory.Exists(deletedFolderPath));
			Assert.True(System.IO.Directory.Exists(notDeletedFolderPath));

			Assert.True(!System.IO.File.Exists(deletedFolderFileToDelete));
			Assert.True(!System.IO.File.Exists(notDeletedFolderFileToDelete));
			Assert.True(System.IO.File.Exists(notDeletedFile));

			Assert.True(System.IO.Directory.Exists(deleteDirectoryPath));
		}
	}
}