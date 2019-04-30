using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Domain.Contracts;

namespace BaseCode.Domain.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Client FindClient(string clientId)
        {
            return _clientRepository.FindClient(clientId);
        }
    }
}
