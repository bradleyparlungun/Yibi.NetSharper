using Yibi.NetSharper.Enums;

namespace Yibi.NetSharper
{
    public class Parameter
    {
        public Parameter()
        {
        }

        public Parameter(string name, object value, ParameterOptions type) : this()
        {
            Name = name;
            Value = value;
            ParamsOptions = type;
        }

        public Parameter(string name, object value, string contentType, ParameterOptions type) : this(name, value, type)
        {
            ContentType = contentType;
        }

        public string Name { get; set; }

        public object Value { get; set; }

        public string ContentType { get; set; }

        public ParameterOptions ParamsOptions { get; set; }

        public DataFormatOptions DataFormat { get; set; } = DataFormatOptions.None;

        public override string ToString() => $"{Name}={Value}";
    }

    //public class XmlParameter : Parameter
    //{
    //    public XmlParameter(string name, object value) : base(name, value, ParameterOptions.Body)
    //    {
    //        DataFormat = DataFormatOptions.Xml;
    //        ContentType = HttpR.XmlContentType;
    //    }
    //}

    //public class JsonParameter : Parameter
    //{
    //    public JsonParameter(string name, object value) : base(name, value, ParameterOptions.Body)
    //    {
    //        DataFormat = DataFormatOptions.Json;
    //        ContentType = HttpR.JsonContentType;
    //    }
    //}
}