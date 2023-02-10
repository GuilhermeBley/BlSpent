using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BlSpent.Application.Tests.Hosts;

public class DefaultHost
{
    private IServiceProvider _serviceProvider;
    public IServiceProvider ServiceProvider => _serviceProvider;

    public DefaultHost()
        => _serviceProvider = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(ConfigureConfiguration)
            .ConfigureServices(ConfigureServices)
            .ConfigureLogging(ConfigureLogging)
            .Build().Services;

    public virtual void ConfigureServices(HostBuilderContext context, IServiceCollection serviceCollection)
    {

    }

    public virtual void ConfigureLogging(HostBuilderContext context, ILoggingBuilder loggingBuilder)
    {

    }

    public virtual void ConfigureConfiguration(HostBuilderContext context, IConfigurationBuilder configurationBuilder)
    {
        
    }
}