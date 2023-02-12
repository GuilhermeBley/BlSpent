using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlSpent.Application.Tests;

public abstract class BaseTest : Hosts.DefaultHost
{
    public override void ConfigureServices(HostBuilderContext context, IServiceCollection serviceCollection)
        => serviceCollection
            .AddDbContext<InMemoryDb.AppDbContext>()
            .AddScoped<Application.UoW.IUnitOfWork, UoW.UnitOfWork>()
            .AddAutoMapper(builder => builder.AddProfile<Profiles.ModelsRepository>())
            
            .AddScoped<Application.Repository.IUserRepository, Repositories.UserRepository>()
            .AddScoped<Application.Repository.IPageRepository, Repositories.PageRepository>()
            
            .AddScoped<Application.Services.Interfaces.IUserService, Application.Services.Implementation.UserService>()
            .AddScoped<Application.Services.Interfaces.IPageService, Application.Services.Implementation.PageService>();
}