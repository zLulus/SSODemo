using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;

namespace MvcClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secure()
        {
            ViewData["Message"] = "查看授权页面";

            return View();
        }

        public async Task Logout()
        {
            //使用的scheme
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> CallApiUsingClientCredentials(IConfiguration configuration)
        {
            var authorityUrl = configuration["AuthorityUrl"];
            var apiUrl = configuration["ApiUrl"];
            var tokenClient = new TokenClient($"{authorityUrl}/connect/token", "mvc", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("jwellApi");

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            //get
            var content = await client.GetStringAsync($"{apiUrl}/identity");

            ViewBag.Json = JArray.Parse(content).ToString();
            return View("json");
        }

        public async Task<IActionResult> CallApiUsingUserAccessToken(IConfiguration configuration)
        {
            var apiUrl = configuration["ApiUrl"];
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            //get
            var content = await client.GetStringAsync($"{apiUrl}/identity");

            ViewBag.Json = JArray.Parse(content).ToString();
            return View("json");
        }

        public async Task<IActionResult> CallApiGetUserInfo(IConfiguration configuration)
        {
            //todo
            var apiUrl = configuration["ApiUrl"];
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            //post
            var content = await client.PostAsync($"{apiUrl}/identity/GetUserInfo",new StringContent(""));

            ViewBag.Json = JArray.Parse(content.Content.ReadAsStringAsync().Result).ToString();
            return View("json");
        }
    }
}