using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    /// <summary>
    /// 读取配置文件的服务
    /// </summary>
    public class UrlResolveService
    {
        public static string AuthorityUrl { get; set; }

        public string GetAuthorityUrl()
        {
            return AuthorityUrl;
        }
    }
}
