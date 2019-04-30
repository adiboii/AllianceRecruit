using BaseCode.Data.Models;

namespace BaseCode.Domain.Contracts
{
    public interface IClientService
    {
        Client FindClient(string clientId);
    }
}
