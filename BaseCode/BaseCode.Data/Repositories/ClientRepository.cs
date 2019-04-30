using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using System.Linq;

namespace BaseCode.Data.Repositories
{
    public class ClientRepository : BaseRepository, IClientRepository
    {
        public ClientRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Client FindClient(string clientId)
        {
            return GetDbSet<Client>().FirstOrDefault(x => x.ClientID == clientId);
        }
    }
}
