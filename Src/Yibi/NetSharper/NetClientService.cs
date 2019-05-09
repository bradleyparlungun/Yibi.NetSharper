using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Yibi.NetSharper.Enums;
using Yibi.NetSharper.Extensions;

namespace Yibi.NetSharper
{
    public class NetClientService : INetClientService
    {
        private readonly IHttpService _httpService;

        public NetClientService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<NetResponse> ExecuteAsync(NetRequest netRequest)
        {
            using(var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod(Enum.GetName(typeof(HttpMethodOptions),netRequest.Method));

                if (!string.IsNullOrEmpty(netRequest.Resource)) request.RequestUri = new Uri(netRequest.Resource);

                PrepareRequest(netRequest, request);

                return await _httpService.GetResponseAsync(request);
            }
        }

        private void PrepareRequest(NetRequest netRequest, HttpRequestMessage request)
        {
            if (!netRequest.Parameters.Any()) return;

            SetContentType(netRequest);
            AppendHeaders(netRequest, request);
            AppendBody(netRequest, request);
            AppendCookies(netRequest, request);
        }

        private void AppendHeaders(INetRequest netRequest, HttpRequestMessage request)
        {
            var headers = netRequest.Parameters.Where(m => m.ParamsOptions == ParameterOptions.Headers);
            if (headers == null || !headers.Any()) return;

            var contentTypeItem = headers.FirstOrDefault(m => !string.IsNullOrEmpty(m.ContentType));
            if(contentTypeItem != null)
            {
                ContentType = contentTypeItem.ContentType;
            }
            else
            {
                ContentType = string.Empty;
            }

            foreach(var item in headers)
            {
                if (!string.IsNullOrEmpty(item.ContentType)) continue;

                request.Headers.TryAddWithoutValidation(item.Name, item.Value.ToString());
            }

            #region old

            //request.Headers.TryAddWithoutValidation

            //foreach (var item in headers)
            //{
            //    HttpHeaderType
            //    switch (item.Name)
            //    {
            //        case HttpR.AcceptKey:
            //            request.Headers.Accept.TryParseAdd(item.Value.ToString());
            //            break;
            //        case HttpR.AcceptEncodingKey:
            //            request.Headers.AcceptEncoding.TryParseAdd(item.Value);
            //            break;
            //        case HttpR.AcceptLanguageKey:
            //            request.Headers.AcceptLanguage.TryParseAdd(item.Value);
            //            break;
            //        case HttpR.UserAgentKey:
            //            request.Headers.UserAgent.TryParseAdd(item.Value);
            //            break;
            //        case HttpR.RefererKey:
            //            request.Headers.Referrer = new Uri(item.Value);
            //            break;
            //        default:
            //            request.Headers.TryAddWithoutValidation(item.Name, item.Value);
            //            break;
            //    }

            //    if (!string.IsNullOrEmpty(item.ContentType))
            //    {
            //        ContentType = item.ContentType;
            //    }
            //    else
            //    {
            //        request.Headers.
            //    }
            //    switch (item.ParamsOptions)
            //    {
            //        case ParameterOptions.HttpHeader:
            //            switch (item.Name)
            //            {
            //                case HttpR.AcceptKey:
            //                    request.Headers.Accept.TryParseAdd(item.Value);
            //                    break;
            //                case HttpR.AcceptEncodingKey:
            //                    request.Headers.AcceptEncoding.TryParseAdd(item.Value);
            //                    break;
            //                case HttpR.AcceptLanguageKey:
            //                    request.Headers.AcceptLanguage.TryParseAdd(item.Value);
            //                    break;
            //                case HttpR.UserAgentKey:
            //                    request.Headers.UserAgent.TryParseAdd(item.Value);
            //                    break;
            //                case HttpR.RefererKey:
            //                    request.Headers.Referrer = new Uri(item.Value);
            //                    break;
            //                default:
            //                    request.Headers.TryAddWithoutValidation(item.Name, item.Value);
            //                    break;
            //            }
            //            break;
            //        case ParameterOptions.HttpContentHeader:
            //            request.Content.Headers.TryAddWithoutValidation(item.Name, item.Value);
            //            break;
            //        default:
            //            break;
            //    }
            //}

            #endregion
        }

        private void AppendBody(NetRequest netRequest, HttpRequestMessage request)
        {
            if (!HasHttpMethodWithBody(netRequest.Method))
            {
                return;
            }

            if(ContentType == HttpR.ContentType)
            {
                var formItems = netRequest.Parameters.Where(m => m.ParamsOptions == ParameterOptions.Body);
                if (formItems != null && formItems.Any()) request.Content = new FormUrlEncodedContent(formItems.ToNvcs());
            }
            else if(ContentType == HttpR.FormDataContentType)
            {
                //ÔÝ²»ÊµÏÖ
            }
            else
            {
                //body raw
                var bodyItem = netRequest.Parameters.FirstOrDefault(m => m.ParamsOptions == ParameterOptions.Body);
                if (!string.IsNullOrEmpty(bodyItem?.Value?.ToString()))
                {
                    request.Content = new StringContent(bodyItem.Value.ToString());
                }
            }
        }

        private void AppendCookies(NetRequest netRequest, HttpRequestMessage request)
        {
            var items = netRequest.Parameters.Where(m => m.ParamsOptions == ParameterOptions.Cookie);
            if (items == null || !items.Any()) return;

            var sCookie = string.Join(";", items.Select(m => m.ToString()));

            if (request.Headers.TryGetValues(HttpR.CookieKey, out var values))
            {
                var cookieValue = values.First().Trim(';');
                request.Headers.Remove(HttpR.CookieKey);
                request.Headers.Add(HttpR.CookieKey, string.Format("{0}{1}{2}", cookieValue, string.IsNullOrEmpty(cookieValue) ? "" : ";", sCookie));

            }
            else
            {
                request.Headers.Add(HttpR.CookieKey, sCookie);
            }
        }

        private bool HasHttpMethodWithBody(HttpMethodOptions httpMethod)
        {
            if (httpMethod == HttpMethodOptions.Get || httpMethod == HttpMethodOptions.Copy || httpMethod == HttpMethodOptions.Head || httpMethod == HttpMethodOptions.Purge || httpMethod == HttpMethodOptions.Unlock)
                return false;

            return true;
        }

        private void SetContentType(INetRequest netRequest)
        {
            var contentTypeItem = netRequest.Parameters.LastOrDefault(m => m.ParamsOptions == ParameterOptions.Headers && m.Name == HttpR.ContentTypeKey);
            ContentType = contentTypeItem?.ContentType;

            if (string.IsNullOrEmpty(ContentType))
            {
                if(netRequest.Parameters.Any(m=>m.Name == HttpR.ContentDispositionKey))
                {
                    ContentType = HttpR.FormDataContentType;
                }
                else
                {
                    ContentType = HttpR.ContentType;
                }
            }
        }

        private string ContentType { get; set; }
    }
}