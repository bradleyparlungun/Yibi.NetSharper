
�� ASP.NET Core ��ʹ�� IHttpClientFactory ���� HTTP ����
https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/http-requests?view=aspnetcore-2.2

HttpClientFactory in ASP.NET Core 2.1 (Part 3)
https://www.stevejgordon.co.uk/httpclientfactory-aspnetcore-outgoing-request-middleware-pipeline-delegatinghandlers

�Զ���ѹ��ʾ����
var client = new HttpClient(new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                });

HttpClientFactoryʾ����
https://github.com/aspnet/HttpClientFactory/blob/master/samples/HttpClientFactorySample/Program.cs

���� HTTP ���󣺿���ע�� IHttpClientFactory �������������úʹ���Ӧ���е� HttpClient ʵ��
https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/http-requests?view=aspnetcore-2.2#consumption-patterns

�汾��1.0.4
1��NetClientServiceCollectionExtensions.cs
2����ĿNetSharper.Test��������Ĳ��Դ���