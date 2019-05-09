using System;
using System.Collections.Generic;
using Yibi.NetSharper.Extensions;

namespace Yibi.NetSharper
{
    public class NetResponse
    {
        public Uri ResponseUri { get; set; }

        public int StatusCode{get;set;}

        public bool IsSuccessful { get; set; }

        public Dictionary<string,IEnumerable<string>> Headers{get;set;}

        public string ContentType { get; set; }

        public long? ContentLength { get; set; }

        public string ContentEncoding { get; set; }

        public string Content => this._content ?? (this._content = this.RawBytes.AsString().Result);

        public byte[] RawBytes { get; set; }

        public string CookieAppend { get; set; }

        private string _content;
    }
}