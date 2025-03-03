using MediatR;
using Utilities.IoOperations.MediatR.Directory.CreateDirectory;

namespace Utilities.IoOperations.MediatR.File.MoveFile;

public class MoveFileCommandHandler(IMediator mediator) : IRequestHandler<MoveFileCommand, bool>
{
	public async Task<bool> Handle(MoveFileCommand request, CancellationToken cancellationToken)
	{
		bool isFileMoved = false;
		string destinationFileFullPath = $@"{request.DestinationFolder}\{Path.GetFileName(request.SourceFile)}";

		await mediator.Send(new CreateDirectoryCommand(request.DestinationFolder), cancellationToken);

		if (!System.IO.File.Exists(destinationFileFullPath))
		{
			System.IO.File.Move(request.SourceFile, destinationFileFullPath);
			isFileMoved = true;
		}

		return isFileMoved;
	}
}
