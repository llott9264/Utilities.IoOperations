using MediatR;
using Utilities.IoOperations.MediatR.Directory.CreateDirectory;

namespace Utilities.IoOperations.MediatR.File.CopyFile;

public class CopyFileCommandHandler(IMediator mediator) : IRequestHandler<CopyFileCommand>
{
	public async Task Handle(CopyFileCommand request, CancellationToken cancellationToken)
	{
		string destinationFileFullPath = $@"{request.DestinationFolder}\{Path.GetFileName(request.SourceFile)}";
		await mediator.Send(new CreateDirectoryCommand(request.DestinationFolder), cancellationToken);
		System.IO.File.Copy(request.SourceFile, destinationFileFullPath, true);
	}
}