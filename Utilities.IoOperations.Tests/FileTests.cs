using MediatR;
using Moq;
using Utilities.IoOperations.MediatR.Directory.CreateDirectory;
using Utilities.IoOperations.MediatR.File.MoveFile;

namespace Utilities.IoOperations.Tests;

public class FileTests
{
	[Fact]
	public async Task Move_MovesFile_True()
	{
		//Arrange
		const string sourceFolder = @"DirectoryFolder\";
		const string sourceFile = "MoveFile.txt";
		const string sourceFilePath = $@"{sourceFolder}\{sourceFile}";
		const string destinationFolder = $@"{sourceFolder}\MoveDirectory\";
		const string destinationFilePath = $@"{destinationFolder}\{sourceFile}";

		if (!System.IO.Directory.Exists(destinationFolder)) System.IO.Directory.CreateDirectory(destinationFolder);
		if (System.IO.File.Exists(destinationFilePath)) System.IO.File.Delete(destinationFilePath);

		Mock<IMediator> mock = new();
		mock.Setup(m => m.Send(It.IsAny<CreateDirectoryCommand>(), It.IsAny<CancellationToken>()));

		MoveFileCommand request = new(sourceFilePath, destinationFolder);
		MoveFileCommandHandler handler = new(mock.Object);

		//Act
		bool isMoved = await handler.Handle(request, CancellationToken.None);
		
		//Assert
		Assert.True(isMoved);
		Assert.True(System.IO.File.Exists(destinationFilePath));
		mock.Verify(m => m.Send(It.IsAny<CreateDirectoryCommand>(), It.IsAny<CancellationToken>()), Times.Once);
		mock.VerifyNoOtherCalls();
	}
}