
git .gitignore:
git rm -r --cached .
git add .
git commit -m 'update .gitignore'

Yibi.NetSharperʹ��.net standard2.0������ּ������HttpClient�Լ�System.Net.Http���ʵ�ֽ���rest api��վ����Դ���������棩��������ʡ�
Yibi.NetSharperʵ�ִ���ο�������RestSharp�����ʵ�֡�NuGet��https://www.nuget.org/packages/Yibi.NetSharper/

Yibi. NetSharper was created using. net standard 2.0 to make use of HttpClient and system. Net. Http-related implementations for rest api, site resources (like crawlers) and other requests.

Yibi. NetSharper Implementation Code Reference and Reference.
Yibi. NetSharper implementations refer to and learn from RestSharp implementations.

ʾ����
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



