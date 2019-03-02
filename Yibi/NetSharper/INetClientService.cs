using System.Threading.Tasks;

namespace Yibi.NetSharper
{
    public interface INetClientService
    {
        Task<NetResponse> ExecuteAsync(NetRequest netRequest);
    }
}