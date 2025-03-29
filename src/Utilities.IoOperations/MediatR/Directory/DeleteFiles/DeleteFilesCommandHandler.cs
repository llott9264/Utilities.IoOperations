using MediatR;

namespace Utilities.IoOperations.MediatR.Directory.DeleteFiles;

public class DeleteFilesCommandHandler : IRequestHandler<DeleteFilesCommand>
{
	public Task Handle(DeleteFilesCommand request, CancellationToken cancellationToken)
	{
		request.Directory.EnumerateFiles()
			.ToList()
			.ForEach(f => f.Delete());

		return Task.CompletedTask;
	}
}