using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWithAspNetIdentity.Models
{
    public class IdentityServer4Client
    {
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// client的唯一标志
        /// </summary>
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        /// <summary>
        /// 诸如AllowedGrantTypes、ClientSecrets，以字典形式存入，再序列化
        /// </summary>
        public string DictionaryJson { get; set; }
        public bool RequireConsent { get; set; }
        public bool AllowOfflineAccess { get; set; }
        /// <summary>
        /// 仅保存value部分
        /// </summary>
        //public ICollection<string> ClientSecrets { get; set; }
        //public ICollection<string> RedirectUris { get; set; }
        //public ICollection<string> PostLogoutRedirectUris { get; set; }
        //public ICollection<string> AllowedScopes { get; set; }
        //public ICollection<string> AllowedGrantTypes { get; set; }
    }
}
