using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlSpent.Application.Tests;

public abstract class BaseTest : Hosts.DefaultHost
{
    public override void ConfigureServices(HostBuilderContext context, IServiceCollection serviceCollection)
        => serviceCollection
            .AddDbContext<InMemoryDb.AppDbContext>()
            .AddScoped<Application.UoW.IUnitOfWork, UoW.UnitOfWork>();
}