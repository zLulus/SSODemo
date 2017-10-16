using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IdentityModel.Client;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace WebsiteA
{
    public class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            //BuildWebHost(args).Run();

            //write a client that requests an access token, and then uses this token to access the API
            //标识服务器上的令牌端点实现OAuth 2.0协议，您可以使用原始HTTP来访问它。然而，我们有一个名为identity model的客户机库，它封装了一个简单的使用API的协议交互。

            //5000
            // discover endpoints from metadata
            //IdentityModel库
            //标识模型包括一个与发现端点一起使用的客户机库。通过这种方式，您只需要知道标识服务器的基本地址—可以从元数据读取实际的端点地址
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");

            //you can use the TokenClient class to request the token. To create an instance you need to pass in the token endpoint address, client id and secret.
            //Next you can use the RequestClientCredentialsAsync method to request a token for your API
            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            //api1是identityserver定义的API资源
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            //5001
            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            //identity指identityController里面的方法，等于identity/index
            var response = await client.GetAsync("http://localhost:5001/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                //返回结果：在缺省情况下，访问令牌将包含关于范围、生存期(nbf和exp)、客户机ID(clientid)和发行方名称(iss)的索赔
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

            Console.ReadLine();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
