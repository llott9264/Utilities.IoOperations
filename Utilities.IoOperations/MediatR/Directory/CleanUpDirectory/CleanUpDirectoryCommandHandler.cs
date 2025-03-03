using MediatR;

namespace Utilities.IoOperations.MediatR.Directory.CleanUpDirectory;

public class CleanUpDirectoryCommandHandler : IRequestHandler<CleanUpDirectoryCommand>
{
	public Task Handle(CleanUpDirectoryCommand request, CancellationToken cancellationToken)
	{
		CleanDirectory(request.Directory, request.RetentionLengthInMonths, request.IsBaseFolder);
		return Task.CompletedTask;
	}

	private void CleanDirectory(DirectoryInfo directory, int retentionLengthInMonths, bool isBaseFolder = true)
	{
		directory.EnumerateFiles()
			.Where(f => f.LastAccessTime < DateTime.Now.AddMonths(retentionLengthInMonths))
			.ToList()
			.ForEach(f => { f.Delete(); });

		directory.EnumerateDirectories()
			.ToList()
			.ForEach(d => { CleanDirectory(d, retentionLengthInMonths, false); });

		if (!isBaseFolder
			&& directory.EnumerateFiles().ToList().Count == 0
			&& directory.EnumerateDirectories().ToList().Count == 0)
		{
			directory.Delete();
		}
	}
}