using MediatR;

namespace Utilities.IoOperations.MediatR.Directory.DeleteFiles;

public class DeleteFilesCommand(DirectoryInfo directory) : IRequest
{
	public DirectoryInfo Directory { get; } = directory;
}