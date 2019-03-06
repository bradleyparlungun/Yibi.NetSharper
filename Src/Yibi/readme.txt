
HttpClientFactory示例：
https://github.com/aspnet/HttpClientFactory/blob/master/samples/HttpClientFactorySample/Program.cs

启动 HTTP 请求：可以注册 IHttpClientFactory 并将其用于配置和创建应用中的 HttpClient 实例
https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/http-requests?view=aspnetcore-2.2#consumption-patterns

版本：1.0.4
1、NetClientServiceCollectionExtensions.cs
2、项目NetSharper.Test添加完整的测试代码