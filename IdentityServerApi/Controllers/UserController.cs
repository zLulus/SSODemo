using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityServerApi.Dtos;
using IdentityModel.Client;
using System.Data.SqlClient;
using IdentityServerApi.Cache.User;
using Model.Dtos;
using Model.Models;
using AutoMapper;

namespace IdentityServerApi.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// 统一登录页面
        /// </summary>
        /// <returns></returns>
        public IActionResult LogIn()
        {
            return View();
        }

        /// <summary>
        /// 点击登录按钮
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> TryLogIn(LogInDto input)
        {
            if(input.Account=="admin" && input.Password == "123456")
            {
                // discover endpoints from metadata
                var disco =await DiscoveryClient.GetAsync("http://localhost:5000");

                //todo 应该把user加载到client列表里面，然后查询
                // request token
                //var tokenClient = new TokenClient(disco.TokenEndpoint, input.Account, input.Password);
                var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
                var tokenResponse =await tokenClient.RequestClientCredentialsAsync("api1");

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
                    //登录成功
                    TokenCacheManager tokenCacheManager = new TokenCacheManager();
                    //todo 如何缓存失败呢？
                    bool r = tokenCacheManager.InsertToken(tokenResponse.AccessToken, "admin");
                    string url = $"http://localhost:5001/User/CheckToken?token={tokenResponse.AccessToken}";
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

        /// <summary>
        /// 其他系统调用添加用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IActionResult AddUser(AddUserDto input)
        {
            
            return null;
        }

        /// <summary>
        /// 根据Token获得用户信息
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public IActionResult GetUser(string token)
        {
            TokenCacheManager tokenCacheManager = new TokenCacheManager();
            string userId= tokenCacheManager.GetUserId(token);
            //查询用户信息
            //todo 数据库
            User u = new User()
            {
                Id = 1,
                Account = "admin"
            };
            UserDto dto= Mapper.Map<User, UserDto>(u);
            return Json(new GetUserOutput()
            {
                IsExist = userId != null,
                UserId = userId,
                User= dto
            });
        }
    }
}