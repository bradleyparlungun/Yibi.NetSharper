using System;
using System.Collections.Generic;
using Yibi.NetSharper.Enums;

namespace Yibi.NetSharper
{
    public class NetRequest : INetRequest
    {
        public NetRequest()
        {
            Method = HttpMethodOptions.Get;
            Parameters = new List<Parameter>();
        }

        public NetRequest(HttpMethodOptions method) : this() => Method = method;

        public NetRequest(string resource) : this(resource, HttpMethodOptions.Get) { }

        public NetRequest(string resource, HttpMethodOptions method) : this()
        {
            Resource = resource;
            Method = method;
        }

        public NetRequest(Uri resource) : this(resource, HttpMethodOptions.Get) { }

        public NetRequest(Uri resource, HttpMethodOptions method) : this(resource.IsAbsoluteUri ? resource.AbsolutePath + resource.Query : resource.OriginalString, method) { }

        public HttpMethodOptions Method { get; set; }

        public string Resource { get; set; }

        public ICollection<Parameter> Parameters { get; }

        public INetRequest AddHeader(string name, string value) => AddParameter(name, value, ParameterOptions.Headers);

        public INetRequest AddCookie(string name, string value) => AddParameter(name, value, ParameterOptions.Cookie);

        public INetRequest AddBody(string body)
        {
            AddHeader(HttpR.ContentTypeKey, HttpR.TextContentType);
            AddParameter(new Parameter("", body, ParameterOptions.Body));

            return this;
        }

        public INetRequest AddBody(string body, DataFormatOptions dataFormat)
        {
            switch (dataFormat)
            {
                case DataFormatOptions.Json:
                    AddHeader(HttpR.ContentTypeKey, HttpR.JsonContentType);
                    AddParameter(new Parameter("", body, ParameterOptions.Body));
                    break;
                case DataFormatOptions.Xml:
                    AddHeader(HttpR.ContentTypeKey, HttpR.XmlContentType);
                    AddParameter(new Parameter("", body, ParameterOptions.Body));
                    break;
                default:
                    return AddBody(body);
            }

            return this;
        }

        public INetRequest AddParameter(string name, object value) => AddParameter(new Parameter(name, value, ParameterOptions.None));

        public INetRequest AddParameter(string name, object value, ParameterOptions type) => AddParameter(new Parameter(name, value, type));

        public INetRequest AddParameter(string name, object value, string contentType, ParameterOptions type) => AddParameter(new Parameter(name, value, contentType, type));

        public INetRequest AddParameter(Parameter p)
        {
            Parameters.Add(p);

            return this;
        }
    }
}