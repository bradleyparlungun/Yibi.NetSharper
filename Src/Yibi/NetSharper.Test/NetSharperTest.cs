using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Yibi.NetSharper.Test
{
    [TestClass]
    public class NetSharperTest: Startup
    {
        private readonly INetClientService _client;

        public NetSharperTest()
        {
            _client = ServiceProvider.GetRequiredService<INetClientService>();
        }

        [TestMethod]
        public async Task RunTest()
        {
            var request = new NetRequest("https://www.baidu.com/");
            var response = await _client.ExecuteAsync(request);
        }
    }
}
