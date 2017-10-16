using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityModel.Client;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace WebsiteA.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            Task.Run(async () =>
            {
                // discover endpoints from metadata
                var disco = await DiscoveryClient.GetAsync("http://localhost:5000");

                // request token
                var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
                var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

                if (tokenResponse.IsError)
                {
                    Console.WriteLine(tokenResponse.Error);
                    
                }
                else
                {
                    Console.WriteLine(tokenResponse.Json);
                    Console.WriteLine("\n\n");

                    // call api
                    var client = new HttpClient();
                    client.SetBearerToken(tokenResponse.AccessToken);

                    var response = await client.GetAsync("http://localhost:5001/identity");
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode);
                    }
                    else
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(JArray.Parse(content));
                    }
                }
                

                Console.ReadLine();

            });
            return View();
        }
    }
}