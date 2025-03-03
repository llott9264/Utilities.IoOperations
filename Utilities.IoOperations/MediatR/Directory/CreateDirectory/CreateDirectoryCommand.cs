using MediatR;

namespace Utilities.IoOperations.MediatR.Directory.CreateDirectory;

public class CreateDirectoryCommand(string folder) : IRequest
{
	public string Folder { get; } = folder;
}