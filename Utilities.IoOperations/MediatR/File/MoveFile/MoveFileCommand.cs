using MediatR;

namespace Utilities.IoOperations.MediatR.File.MoveFile;

public class MoveFileCommand(string sourceFile, string destinationFolder) : IRequest<bool>
{
	public string SourceFile { get; } = sourceFile;
	public string DestinationFolder { get; } = destinationFolder;
}