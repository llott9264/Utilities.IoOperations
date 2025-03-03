using MediatR;

namespace Utilities.IoOperations.MediatR.Directory.CleanUpDirectory;

public class CleanUpDirectoryCommand(DirectoryInfo directory, int retentionLengthInMonths, bool isBaseFolder = true) : IRequest
{
	public DirectoryInfo Directory { get; } = directory;
	public int RetentionLengthInMonths { get; } = retentionLengthInMonths;
	public bool IsBaseFolder { get; } = isBaseFolder;
}