using Yibi.NetSharper.Enums;

namespace Yibi.NetSharper
{
    public class NetParameter
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public ParameterOptions ParamsOptions { get; set; }

        public string ContentType { get; set; }

        public override string ToString()
        {
            return $"{this.Name}={this.Value}";
        }
    }
}