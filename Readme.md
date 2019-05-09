
git .gitignore:
git rm -r --cached .
git add .
git commit -m 'update .gitignore'

Yibi.NetSharper使用.net standard2.0创建，旨在利用HttpClient以及System.Net.Http相关实现进行rest api、站点资源（类似爬虫）等请求访问。
Yibi.NetSharper实现代码参考与借鉴了RestSharp代码的实现。NuGet：https://www.nuget.org/packages/Yibi.NetSharper/

Yibi. NetSharper was created using. net standard 2.0 to make use of HttpClient and system. Net. Http-related implementations for rest api, site resources (like crawlers) and other requests.

Yibi. NetSharper Implementation Code Reference and Reference.
Yibi. NetSharper implementations refer to and learn from RestSharp implementations.

示例：
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



