using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using MvcClient.Services;

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

        public async Task<IActionResult> CallApiUsingClientCredentials(UrlResolveService urlResolveService)
        {
            var authorityUrl = urlResolveService.GetAuthorityUrl();
            var apiUrl = urlResolveService.GetApiUrl();
            var tokenClient = new TokenClient($"{authorityUrl}/connect/token", "mvc", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("jwellApi");

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            //get
            var response = await client.GetAsync($"{apiUrl}/Identity/Get");
            var content = await response.Content.ReadAsStringAsync();
            ViewBag.Json = JArray.Parse(content).ToString();
            return View("json");
        }

        public async Task<IActionResult> CallApiUsingUserAccessToken(UrlResolveService urlResolveService)
        {
            var apiUrl = urlResolveService.GetApiUrl();
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            //get
            var content = await client.GetStringAsync($"{apiUrl}/Identity/Get");

            ViewBag.Json = JArray.Parse(content).ToString();
            return View("json");
        }

        public async Task<IActionResult> CallApiGetUserInfo(UrlResolveService urlResolveService)
        {
            var apiUrl = urlResolveService.GetApiUrl();
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            //post
            var content = await client.PostAsync($"{apiUrl}/Identity/Post", new StringContent(""));
            string str = await content.Content.ReadAsStringAsync();
            ViewBag.Json = JArray.Parse(str).ToString();
            return View("json");
        }
    }
}