using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Services;
using Todo.Application.UseCases.User;
using Todo.Domain.Repositories;
using Todo.Infrastructure.Repositories;

namespace Todo.IoC;

public static class DependencyInjectionExtension
{
    public static void AddIoC(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services, configuration);
        AddServices(services, configuration);
        AddUseCases(services, configuration);
        AddTokenGenerator(services, configuration);
    }

    public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordService, PasswordService>();
    }

    public static void AddUseCases(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<CreateUserUseCase>();
    }

    public static void AddTokenGenerator(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
    }
}