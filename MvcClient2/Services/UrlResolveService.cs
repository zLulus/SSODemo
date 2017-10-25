using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace MvcClient.Services
{
    /// <summary>
    /// 读取配置文件的服务
    /// </summary>
    public class UrlResolveService
    {
        public static string AuthorityUrl { get; set; }
        public static string ApiUrl { get; set; }

        public string GetAuthorityUrl()
        {
            return AuthorityUrl;
        }
        public string GetApiUrl()
        {
            return ApiUrl;
        }
    }
}
