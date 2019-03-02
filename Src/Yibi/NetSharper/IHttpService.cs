using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Yibi.NetSharper
{
    public interface IHttpService
    {
        Task<NetResponse> GetResponseAsync(HttpRequestMessage request);

        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);
    }
}