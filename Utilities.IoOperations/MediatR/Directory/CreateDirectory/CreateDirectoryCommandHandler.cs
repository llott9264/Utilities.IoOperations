using MediatR;

namespace Utilities.IoOperations.MediatR.Directory.CreateDirectory;

public class CreateDirectoryCommandHandler : IRequestHandler<CreateDirectoryCommand>
{
	public Task Handle(CreateDirectoryCommand request, CancellationToken cancellationToken)
	{
		if (!System.IO.Directory.Exists(request.Folder))
		{
			System.IO.Directory.CreateDirectory(request.Folder);
		}

		return Task.CompletedTask;
	}
}