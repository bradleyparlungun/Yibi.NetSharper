using Microsoft.Extensions.Configuration;
using Yibi.NetSharper;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class NetClientServiceCollectionExtensions
    {
        public static IServiceCollection AddNetClient(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddSingleton<INetClientService, NetClientService>();
            services.AddSingleton<IHttpService, HttpService>();

            return services;
        }
    }
}
