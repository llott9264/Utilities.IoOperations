namespace Utilities.IoOperations.Tests;

public class FileTests
{
	[Fact]
	public void Move_MovesFile_True()
	{
		//Arrange
		const string sourceFolder = @"DirectoryFolder\";
		const string sourceFile = "MoveFile.txt";
		const string sourceFilePath = $@"{sourceFolder}\{sourceFile}";
		const string destinationFolder = $@"{sourceFolder}\MoveDirectory\";
		const string destinationFilePath = $@"{destinationFolder}\{sourceFile}";

		if (System.IO.File.Exists(destinationFilePath)) System.IO.File.Delete(destinationFilePath);

		//Act
		bool isMoved = File.Move(sourceFilePath, destinationFolder);

		//Assert
		Assert.True(isMoved);
		Assert.True(System.IO.File.Exists(destinationFilePath));
	}
}