using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerApi
{
    public class Config
    {
        // scopes define the API resources in your system
        //范围定义了您系统中的API资源
        public static IEnumerable<ApiResource> GetApiResources()
        {
            //定义API
            //范围定义了您想要保护的系统中的资源
            //由于我们使用的是这个演练的内存配置——您需要做的就是添加一个API，就是创建一个API类型的对象，并设置适当的属性
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }

        // clients want to access resources (aka scopes)
        //客户端想要访问资源（也就是范围）
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            //定义可以访问该API的客户端
            //对于这个场景，客户端将不会有一个交互的用户，并将使用标识服务器的所谓客户端机密进行身份验证
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    //这里是允许访问的资源
                    AllowedScopes = { "api1" }
                }
            };
        }
    }
}
