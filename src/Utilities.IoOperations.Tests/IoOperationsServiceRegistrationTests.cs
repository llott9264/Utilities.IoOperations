using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Utilities.IoOperations.MediatR.Directory.CleanUpDirectory;
using Utilities.IoOperations.MediatR.Directory.CreateDirectory;
using Utilities.IoOperations.MediatR.Directory.DeleteFiles;
using Utilities.IoOperations.MediatR.File.CopyFile;
using Utilities.IoOperations.MediatR.File.MoveFile;

namespace Utilities.IoOperations.Tests;

public class IoOperationsServiceRegistrationTests
{
	[Fact]
	public void AddAuthenticationServices_RegistersAllServices_CorrectlyResolvesTypes()
	{
		// Arrange
		ServiceCollection services = new();

		// Act
		_ = services.AddIoOperationsServices();
		ServiceProvider serviceProvider = services.BuildServiceProvider();

		IMediator? mediator = serviceProvider.GetService<IMediator>();

		// Assert
		Assert.NotNull(mediator);
		_ = Assert.IsType<Mediator>(mediator);
	}

	[Fact]
	public void AddAuthenticationServices_ReturnsServiceCollection()
	{
		// Arrange
		ServiceCollection services = new();

		// Act
		IServiceCollection result = services.AddIoOperationsServices();

		// Assert
		Assert.Same(services, result); // Ensures the method returns the same IServiceCollection
	}

	[Fact]
	public void AddApplicationServices_ScopedLifetime_VerifyInstanceWithinScope()
	{
		// Arrange
		ServiceCollection services = new();

		// Act
		_ = services.AddIoOperationsServices();
		ServiceProvider serviceProvider = services.BuildServiceProvider();

		// Assert
		using IServiceScope scope = serviceProvider.CreateScope();
		IMediator? service1 = scope.ServiceProvider.GetService<IMediator>();
		IMediator? service2 = scope.ServiceProvider.GetService<IMediator>();

		Assert.NotSame(service1, service2);
	}

	[Fact]
	public void AddApplicationServices_ScopedLifetime_VerifyInstancesAcrossScopes()
	{
		// Arrange
		ServiceCollection services = new();

		// Act
		_ = services.AddIoOperationsServices();
		ServiceProvider serviceProvider = services.BuildServiceProvider();

		// Assert
		IMediator? service1, service2;

		using (IServiceScope scope1 = serviceProvider.CreateScope())
		{
			service1 = scope1.ServiceProvider.GetService<IMediator>();
		}

		using (IServiceScope scope2 = serviceProvider.CreateScope())
		{
			service2 = scope2.ServiceProvider.GetService<IMediator>();
		}

		Assert.NotSame(service1, service2);
	}

	[Fact]
	public void AddApplicationServices_CleanUpDirectoryHandler_VerifyMediatorHandlerExists()
	{
		// Arrange
		ServiceCollection services = new();

		// Act
		_ = services.AddIoOperationsServices();
		List<ServiceDescriptor> serviceDescriptors = services.ToList();

		// Assert
		ServiceDescriptor? handlerDescriptor = serviceDescriptors.FirstOrDefault(sd =>
			sd.ServiceType == typeof(IRequestHandler<CleanUpDirectoryCommand>));

		Assert.NotNull(handlerDescriptor);
		Assert.Equal(ServiceLifetime.Transient, handlerDescriptor.Lifetime);
	}

	[Fact]
	public void AddApplicationServices_CreateDirectoryCommandHandler_VerifyMediatorHandlerExists()
	{
		// Arrange
		ServiceCollection services = new();

		// Act
		_ = services.AddIoOperationsServices();
		List<ServiceDescriptor> serviceDescriptors = services.ToList();

		// Assert
		ServiceDescriptor? handlerDescriptor = serviceDescriptors.FirstOrDefault(sd =>
			sd.ServiceType == typeof(IRequestHandler<CreateDirectoryCommand>));

		Assert.NotNull(handlerDescriptor);
		Assert.Equal(ServiceLifetime.Transient, handlerDescriptor.Lifetime);
	}

	[Fact]
	public void AddApplicationServices_DeleteFilesCommandHandler_VerifyMediatorHandlerExists()
	{
		// Arrange
		ServiceCollection services = new();

		// Act
		_ = services.AddIoOperationsServices();
		List<ServiceDescriptor> serviceDescriptors = services.ToList();

		// Assert
		ServiceDescriptor? handlerDescriptor = serviceDescriptors.FirstOrDefault(sd =>
			sd.ServiceType == typeof(IRequestHandler<DeleteFilesCommand>));

		Assert.NotNull(handlerDescriptor);
		Assert.Equal(ServiceLifetime.Transient, handlerDescriptor.Lifetime);
	}

	[Fact]
	public void AddApplicationServices_MoveFileCommandHandler_VerifyMediatorHandlerExists()
	{
		// Arrange
		ServiceCollection services = new();

		// Act
		_ = services.AddIoOperationsServices();
		List<ServiceDescriptor> serviceDescriptors = services.ToList();

		// Assert
		ServiceDescriptor? handlerDescriptor = serviceDescriptors.FirstOrDefault(sd =>
			sd.ServiceType == typeof(IRequestHandler<MoveFileCommand, bool>));

		Assert.NotNull(handlerDescriptor);
		Assert.Equal(ServiceLifetime.Transient, handlerDescriptor.Lifetime);
	}

	[Fact]
	public void AddApplicationServices_CopyFileCommandHandler_VerifyMediatorHandlerExists()
	{
		// Arrange
		ServiceCollection services = new();

		// Act
		_ = services.AddIoOperationsServices();
		List<ServiceDescriptor> serviceDescriptors = services.ToList();

		// Assert
		ServiceDescriptor? handlerDescriptor = serviceDescriptors.FirstOrDefault(sd =>
			sd.ServiceType == typeof(IRequestHandler<CopyFileCommand>));

		Assert.NotNull(handlerDescriptor);
		Assert.Equal(ServiceLifetime.Transient, handlerDescriptor.Lifetime);
	}
}