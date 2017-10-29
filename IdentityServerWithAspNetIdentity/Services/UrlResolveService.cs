using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWithAspNetIdentity.Services
{
    /// <summary>
    /// 读取配置文件的服务
    /// </summary>
    public class UrlResolveService
    {
        public static string ClientUrls { get; set; }
        public List<string> GetClientUrlList()
        {
            return ClientUrls.Split(';').ToList();
        }
    }
}
