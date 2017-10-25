using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWithAspNetIdentity.Services
{
    public interface IIdentityServer4ClientService
    {
        bool InsertClient(Client client);
        List<Client> GetClient();
    }
}
