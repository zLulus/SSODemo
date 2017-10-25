using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWithAspNetIdentity.Models.ClientViewModels
{
    public class IdentityServer4ClientViewModel
    {
        public long Id { get; set; }
        /// <summary>
        /// client的唯一标志
        /// </summary>
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        /// <summary>
        /// 诸如AllowedGrantTypes、ClientSecrets，以字典形式存入，再序列化
        /// </summary>
        public Dictionary<string,string> Dictionaries
        {
            get
            {
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(DictionaryJson);
            }
        }
        public bool RequireConsent { get; set; }
        public bool AllowOfflineAccess { get; set; }
        public string DictionaryJson { get; set; }
    }
}
