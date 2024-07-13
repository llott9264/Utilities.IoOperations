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
	}
}