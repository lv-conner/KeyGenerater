using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Microsoft.Extensions.Logging;

namespace SiloHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new SiloHostBuilder()
                    .UseLocalhostClustering()
                    .Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = "dev";
                        options.ServiceId = "KeyGenerator";
                    })
                    .ConfigureServices(services =>
                    {

                    })
                    .AddMemoryGrainStorageAsDefault()
                    .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                    .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(KeyGenerator.Implement.KeyGenerator).Assembly).WithReferences())
                    .ConfigureLogging(logging => logging.AddConsole());
            var host = builder.Build();
            CreateHostBuilder(args).ConfigureServices(services =>
            {
                services.AddSingleton<ISiloHost>(host);
            }).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
    }
}
