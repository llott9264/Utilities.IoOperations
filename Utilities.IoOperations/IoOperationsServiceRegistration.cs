using Microsoft.Extensions.DependencyInjection;

namespace Utilities.IoOperations;

public static class IoOperationsServiceRegistration
{
	public static IServiceCollection AddIoOperationsServices(this IServiceCollection services)
	{
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
		return services;
	}
}