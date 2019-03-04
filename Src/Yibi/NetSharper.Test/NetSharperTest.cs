using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yibi.NetSharper;

namespace Yibi.NetSharper.Test
{
    [TestClass]
    public class NetSharperTest
    {
        private readonly INetClientService _client;

        public NetSharperTest(INetClientService client)
        {
            _client = client;
        }

        public async Task EurekaRestfulTest()
        {
            var request = new NetRequest("https://www.baidu.com/");
            var response = await _client.ExecuteAsync(request);

            Console.WriteLine(JsonConvert.SerializeObject(response));
        }
    }
}
