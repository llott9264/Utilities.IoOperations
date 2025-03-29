using Utilities.IoOperations.MediatR.Directory.CleanUpDirectory;
using Utilities.IoOperations.MediatR.Directory.CreateDirectory;
using Utilities.IoOperations.MediatR.Directory.DeleteFiles;

namespace Utilities.IoOperations.Tests;

public class DirectoryTests
{
	[Fact]
	public void CreateDirectory_ReturnsDirectory_True()
	{
		//Arrange
		const string directoryPath = @"DirectoryFolder\NewDirectory\";
		if (Directory.Exists(directoryPath))
		{
			Directory.Delete(directoryPath);
		}

		CreateDirectoryCommand request = new(directoryPath);
		CreateDirectoryCommandHandler handler = new();

		//Act
		handler.Handle(request, CancellationToken.None);

		//Assert
		Assert.True(Directory.Exists(directoryPath));
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

		Directory.SetLastAccessTime(deletedFolderPath, deleteLastAccessDateTime);
		File.SetLastAccessTime(deletedFolderFileToDelete, deleteLastAccessDateTime);

		Directory.SetLastAccessTime(notDeletedFolderPath, notDeleteLastAccessDateTime);
		File.SetLastAccessTime(notDeletedFolderFileToDelete, deleteLastAccessDateTime);
		File.SetLastAccessTime(notDeletedFile, notDeleteLastAccessDateTime);

		CleanUpDirectoryCommand request = new(di, fileRetentionPeriodInMonths);
		CleanUpDirectoryCommandHandler handler = new();

		//Act
		handler.Handle(request, CancellationToken.None);

		//Assert
		Assert.True(!Directory.Exists(deletedFolderPath));
		Assert.True(Directory.Exists(notDeletedFolderPath));

		Assert.True(!File.Exists(deletedFolderFileToDelete));
		Assert.True(!File.Exists(notDeletedFolderFileToDelete));
		Assert.True(File.Exists(notDeletedFile));

		Assert.True(Directory.Exists(deleteDirectoryPath));
	}

	[Fact]
	public void DeleteFiles_Returns_True()
	{
		//Arrange
		const string deleteFilesDirectoryPath = @"DirectoryFolder\DeleteFilesDirectory\";
		DirectoryInfo di = new(deleteFilesDirectoryPath);

		DeleteFilesCommand request = new(di);
		DeleteFilesCommandHandler handler = new();

		const string fileToDelete = $@"{deleteFilesDirectoryPath}\FileToDelete.txt";

		//Act
		handler.Handle(request, CancellationToken.None);

		//Assert
		Assert.False(File.Exists(fileToDelete));
		Assert.True(Directory.Exists(deleteFilesDirectoryPath));
	}
}