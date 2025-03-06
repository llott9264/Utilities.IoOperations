using MediatR;

namespace Utilities.IoOperations.MediatR.File.CopyFile;

public class CopyFileCommand(string sourceFile, string destinationFolder) : IRequest
{
	public string SourceFile { get; } = sourceFile;
	public string DestinationFolder { get; } = destinationFolder;
}