using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityServerApi.Dtos;
using IdentityModel.Client;

namespace IdentityServerApi.Controllers
{
    public class UserController : Controller
    {
        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult TryLogIn(LogInDto input)
        {
            if(input.Account=="admin" && input.Password == "123456")
            {
                // discover endpoints from metadata
                var disco = DiscoveryClient.GetAsync("http://localhost:5000").Result;

                // request token
                var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
                var tokenResponse = tokenClient.RequestClientCredentialsAsync("api1").Result;

                if (tokenResponse.IsError)
                {
                    Console.WriteLine(tokenResponse.Error);
                    return Json(new
                    {
                        LogResult = false,
                        Msg = tokenResponse.Error
                    });
                }
                else
                {
                    string url = $"http://localhost:5001/User/CheckToken?Token={tokenResponse.AccessToken}";
                    return Json(new
                    {
                        LogResult = true,
                        Msg = url
                    });
                }
            }
            else
            {
                return Json(new
                {
                    LogResult = false,
                    Msg = "登录失败"
                });
            }
        }
    }
}