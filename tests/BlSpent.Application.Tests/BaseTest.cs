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
            .AddScoped<UoW.MemorySession>()
            .AddScoped<UoW.IMemorySession>(serviceProvider => serviceProvider.GetRequiredService<UoW.MemorySession>())
            .AddScoped<Application.UoW.IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<UoW.MemorySession>())
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
            .AddScoped<Application.Services.Interfaces.IRolePageService, Application.Services.Implementation.RolePageService>()
            .AddScoped<Application.Services.Interfaces.ICostService, Application.Services.Implementation.CostService>()
            .AddScoped<Application.Services.Interfaces.IGoalService, Application.Services.Implementation.GoalService>()
            .AddScoped<Application.Services.Interfaces.IEarningService, Application.Services.Implementation.EarningService>();

    protected InternalContext CreateContext(
        Model.UserModel? user = null, 
        Model.RolePageModel? rolePageModel = null,
        bool isNotRememberPassword = false)
    {
        return new InternalContext(
            new Model.ClaimModel(user?.Id, user?.Email, user?.Name, user?.LastName, rolePageModel?.PageId, rolePageModel?.Role, DateTime.MaxValue, 
                isNotRememberPassword: isNotRememberPassword)
        );
    }

    protected class InternalContext : IDisposable
    {
        private static Context.TestContext _context = new();
        public Model.ClaimModel ClaimModel { get; }

        public InternalContext(Model.ClaimModel claim)
        {
            ClaimModel = claim;
            _context.ClaimContext = claim;
        }

        public void Dispose()
        {
            _context.ClaimContext = null;
        }
    }
}