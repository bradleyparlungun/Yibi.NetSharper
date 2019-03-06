using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yibi.NetSharper.Test
{
    public class Startup
    {
        public IServiceProvider ServiceProvider { get; }
        public IConfiguration Configuration { get; set; }

        public Startup()
        {
            ServiceProvider = CreateServiceProvider();
        }

        private IServiceProvider CreateServiceProvider()
        {
            var builder = new ConfigurationBuilder();
            Configuration = builder.Build();

            var services = new ServiceCollection();

            services.AddNetClient(Configuration);

            return services.BuildServiceProvider();
        }
    }
}
