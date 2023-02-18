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
            
            .AddSingleton<Context.TestContext>()
            .AddSingleton<Application.Security.ISecurityContext, Context.TestContext>()
            
            .AddScoped<Application.Repository.IUserRepository, Repositories.UserRepository>()
            .AddScoped<Application.Repository.IPageRepository, Repositories.PageRepository>()
            .AddScoped<Application.Repository.IRolePageRepository, Repositories.RolePageRepository>()
            .AddScoped<Application.Repository.ICostRepository, Repositories.CostRepository>()
            .AddScoped<Application.Repository.IGoalRepository, Repositories.GoalRepository>()
            .AddScoped<Application.Repository.IEarningRepository, Repositories.EarningRepository>()

            
            .AddScoped<Application.Services.Interfaces.IUserService, Application.Services.Implementation.UserService>()
            .AddScoped<Application.Services.Interfaces.IPageService, Application.Services.Implementation.PageService>()
            .AddScoped<Application.Services.Interfaces.IRolePageService, Application.Services.Implementation.RolePageService>();

    protected void SetContext(Model.UserModel? user = null)
    {
        var context =
            ServiceProvider.GetRequiredService<Context.TestContext>();
        context.ClaimContext = new Model.ClaimModel(user?.Id, user?.Email, user?.Name, user?.LastName, null, null, DateTime.MaxValue);
    }
}