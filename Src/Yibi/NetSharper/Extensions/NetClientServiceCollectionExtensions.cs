using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Yibi.NetSharper;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class NetClientServiceCollectionExtensions
    {
        public static IServiceCollection AddNetClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<DefaultMessageHandler>();
            //services.AddHttpClient();
            services.AddHttpClient(HttpClientNames.Default, h =>
            {
                h.DefaultRequestHeaders.Accept.TryParseAdd(HttpR.Accept);
                h.DefaultRequestHeaders.UserAgent.TryParseAdd(HttpR.UserAgent);
                h.DefaultRequestHeaders.AcceptLanguage.TryParseAdd(HttpR.AcceptLanguage);
                h.DefaultRequestHeaders.AcceptEncoding.TryParseAdd(HttpR.AcceptEncoding);
                h.DefaultRequestHeaders.AcceptCharset.TryParseAdd(HttpR.AcceptCharset);
            })
            .AddHttpMessageHandler<DefaultMessageHandler>();

            services.AddSingleton<INetClientService, NetClientService>();
            services.AddSingleton<IHttpService, HttpService>();

            return services;

            // services.AddHttpClient("named", c =>
            // {
            //     c.BaseAddress = new Uri("TODO");
            //     c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //     c.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
            //     {
            //         NoCache = true,
            //         NoStore = true,
            //         MaxAge = new TimeSpan(0),
            //         MustRevalidate = true
            //     };
            // }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            // {
            //     AllowAutoRedirect = false,
            //     AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            // });

            // services.AddHttpClient("Default").ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            // {
            //     AllowAutoRedirect = false,
            //     AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            // });

        }
    }
}
