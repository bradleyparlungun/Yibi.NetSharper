using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using System.IO.Compression;
using System.Net.Http;
using Yibi.NetSharper.Extensions;

namespace Yibi.NetSharper
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientFactory _clientFactory;

        public HttpService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<NetResponse> GetResponseAsync(HttpRequestMessage request)
        {
            var netResponse = new NetResponse();

            var response = await SendAsync(request, CancellationToken.None);

            await ProcessResponse(netResponse, response);

            return netResponse;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var client = _clientFactory.CreateClient(HttpClientNames.Default);
            return await client.SendAsync(request, cancellationToken);
        }

        private async Task ProcessResponse(NetResponse netResponse, HttpResponseMessage response)
        {
            using (response)
            {
                netResponse.StatusCode = (int)response.StatusCode;
                netResponse.IsSuccessful = response.IsSuccessStatusCode;
                netResponse.ContentEncoding = string.Join(",", response.Content.Headers.ContentEncoding);
                netResponse.ContentType = response.Content.Headers.ContentType?.MediaType;
                netResponse.ContentLength = response.Content.Headers.ContentLength;
                netResponse.ResponseUri = response.RequestMessage.RequestUri;

                var headers = Enumerable.ToDictionary(response.Headers, h => h.Key, h => h.Value);
                if (response.Content != null && response.Content.Headers != null)
                {
                    foreach (var item in response.Content.Headers)
                        headers[item.Key] = item.Value;
                }
                netResponse.Headers = headers;

                var cookies = response.Headers?.Where(m => m.Key.ToLower() == "set-cookie");
                if (cookies != null && cookies.Any())
                {
                    var items = new List<string>();
                    foreach (var item in cookies)
                    {
                        items.AddRange(item.Value.Select(m => m.Split(';')[0]));
                    }
                    netResponse.CookieAppend = string.Join(";", items);
                }

                if (response.Content.Headers.ContentEncoding.Any(m => m == "gzip"))
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    using (var decompressed = new GZipStream(stream, CompressionMode.Decompress))
                    {
                        netResponse.RawBytes = await decompressed.AsBytes();
                    }
                }
                else
                {
                    netResponse.RawBytes = await response.Content.ReadAsByteArrayAsync();
                }
            }
        }
    }
}