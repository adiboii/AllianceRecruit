using BaseCode.Data.Models;

namespace BaseCode.Data.Contracts
{
    public interface IClientRepository
    {
        Client FindClient(string clientId);
    }
}
