using System.Collections.Generic;
using Yibi.NetSharper.Enums;

namespace Yibi.NetSharper
{
    public interface INetRequest
    {
        INetRequest AddHeader(string name, string value);

        INetRequest AddCookie(string name, string value);

        INetRequest AddBody(string body);

        INetRequest AddBody(string body, DataFormatOptions DataFormat);

        INetRequest AddParameter(string name, object value);

        INetRequest AddParameter(string name, object value, ParameterOptions type);

        INetRequest AddParameter(string name, object value, string contentType, ParameterOptions type);

        INetRequest AddParameter(Parameter p);

        ICollection<Parameter> Parameters { get; }
    }
}