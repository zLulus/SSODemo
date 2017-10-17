using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Model.Dtos;
using Newtonsoft.Json;

namespace WebsiteB.Controllers
{
    public class UserController : Controller
    {
        public IActionResult LogIn()
        {
            //重定向到identityserver的登录页面
            return Redirect("http://localhost:5000/User/LogIn");
        }

        public IActionResult CheckToken(string token)
        {
            // call api
            var client = new HttpClient();
            client.SetBearerToken(token);

            var response = client.GetAsync($"http://localhost:5001/identity?token={token}").Result;
            
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                return Json(new
                {
                    CheckTokenResult = false,
                    Msg = response.StatusCode
                });
            }
            else
            {
                //登录成功
                var content = response.Content.ReadAsStringAsync().Result;
                //读取用户信息
                GetUserOutput output = JsonConvert.DeserializeObject<GetUserOutput>(content);
                

                //todo 设置该网站状态为登陆成功
                return Json(new
                {
                    CheckTokenResult = true,
                    Msg = JArray.Parse(content)
                });
            }
        }
    }
}