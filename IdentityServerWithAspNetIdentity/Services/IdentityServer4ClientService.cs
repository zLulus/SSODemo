using AutoMapper;
using IdentityServer4.Models;
using IdentityServerWithAspNetIdentity.Data;
using IdentityServerWithAspNetIdentity.Models;
using IdentityServerWithAspNetIdentity.Models.ClientViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWithAspNetIdentity.Services
{
    /// <summary>
    /// 管理IdentityServer4的Client
    /// </summary>
    public class IdentityServer4ClientService: IIdentityServer4ClientService
    {
        private ApplicationDbContext dbContext { get; set; }
        public IdentityServer4ClientService(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public bool InsertClient(Client client)
        {
            IdentityServer4Client model= Mapper.Map<Client, IdentityServer4Client>(client);
            Dictionary<string, string> dictionaries = new Dictionary<string, string>();
            dictionaries.Add("AllowOfflineAccess", JsonConvert.SerializeObject(client.AllowOfflineAccess));
            dictionaries.Add("RedirectUris", JsonConvert.SerializeObject(client.RedirectUris));
            dictionaries.Add("PostLogoutRedirectUris", JsonConvert.SerializeObject(client.PostLogoutRedirectUris));
            dictionaries.Add("AllowOfflineAccess", JsonConvert.SerializeObject(client.AllowOfflineAccess));
            List<string> secretsValues = new List<string>();
            foreach (var secret in client.ClientSecrets)
            {
                secretsValues.Add(secret.Value);
            }
            dictionaries.Add("ClientSecrets", JsonConvert.SerializeObject(secretsValues));
            dbContext.IdentityServer4Clients.Add(model);
            return dbContext.SaveChanges() > 0;
        }

        public List<Client> GetClient()
        {
            List<Client> result = new List<Client>();
            List<IdentityServer4Client> list = dbContext.IdentityServer4Clients.ToList();
            foreach(var item in list)
            {
                IdentityServer4ClientViewModel vm = Mapper.Map<IdentityServer4Client, IdentityServer4ClientViewModel>(item);
                Client client = new Client()
                {
                    //ClientId不能重复
                    ClientId = vm.ClientId,
                    ClientName = vm.ClientName,
                    AllowedGrantTypes = GetListString(vm.Dictionaries, "AllowedGrantTypes"),

                    RequireConsent = vm.RequireConsent,

                    RedirectUris = GetListString(vm.Dictionaries, "RedirectUris"),
                    PostLogoutRedirectUris = GetListString(vm.Dictionaries, "PostLogoutRedirectUris"),

                    AllowedScopes = GetListString(vm.Dictionaries, "AllowedScopes"),
                    AllowOfflineAccess = true
                };
                List<string> secretsValues = GetListString(vm.Dictionaries, "ClientSecrets");
                foreach(var secret in secretsValues)
                {
                    client.ClientSecrets.Add(new Secret(secret.Sha256()));
                }
                result.Add(client);
            }
            return result;
        }

        private List<string> GetListString(Dictionary<string, string> Dictionaries, string key)
        {
            var dicValue = Dictionaries[key];
            List<string> dicList = JsonConvert.DeserializeObject<List<string>>(dicValue);
            return dicList;
        }
    }
}
