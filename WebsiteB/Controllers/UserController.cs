using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Model.Dtos;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Model.Models;
using Microsoft.AspNetCore.Identity;
using WebsiteB.Helpers;

namespace WebsiteB.Controllers
{
    public class UserController : Controller
    {
        public IActionResult LogIn()
        {
            //重定向到identityserver的登录页面
            return Redirect("http://localhost:5000/User/LogIn");
        }

        public async Task<IActionResult> CheckToken(string token)
        {
            // call api
            var client = new HttpClient();
            client.SetBearerToken(token);

            var response =await client.GetAsync($"http://localhost:5001/identity?token={token}");
            
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
                var content =await response.Content.ReadAsStringAsync();
                //读取用户信息
                GetUserOutput output = JsonConvert.DeserializeObject<GetUserOutput>(content);
                UserDto user = output.User;
                //设置该网站状态为登陆成功
                //记录Session
                HttpContext.Session.Set("CurrentUser", ByteConvertHelper.Object2Bytes(user));
                //跳转到系统首页
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("CurrentUser");
            return RedirectToAction("LogIn", "User");
        }
    }
}