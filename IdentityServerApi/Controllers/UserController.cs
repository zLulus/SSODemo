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
using BLL;
using BLL.Encrypting;

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
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");

            // request token
            input.Password= MD5Encrypting.MD5Encoding(input.Password);
            var tokenClient = new TokenClient(disco.TokenEndpoint, input.Account, input.Password);
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

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
                UserManager manager = new UserManager();
                User user = manager.QueryUserByAccount(input.Account);
                TokenCacheManager tokenCacheManager = new TokenCacheManager();
                //todo 如何缓存失败呢？
                //缓存token-userId
                bool r = tokenCacheManager.InsertToken(tokenResponse.AccessToken, user.Id.ToString());
                string url = $"http://localhost:5001/User/CheckToken?token={tokenResponse.AccessToken}";
                return Json(new
                {
                    LogResult = true,
                    Msg = url
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
            UserManager manager = new UserManager();
            User u = manager.QueryUserById(long.Parse(userId));
            UserDto dto= Mapper.Map<User, UserDto>(u);
            return Json(new GetUserOutput()
            {
                IsExist = userId != null,
                UserId = userId,
                User= dto
            });
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult TryAddUser(AddUserDto input)
        {
            UserManager manager = new UserManager();
            User existUser= manager.QueryUserByAccount(input.Account);
            if (existUser != null)
            {
                return Json(new
                {
                    AddUserResult = false,
                    Msg = "当前用户名已存在"
                });
            }
            User user= Mapper.Map<AddUserDto, User>(input);
            bool result= manager.AddUser(user);
            //todo 新增用户之后，要更新Clients列表
            return Json(new
            {
                AddUserResult = result,
                Msg = result? "/User/LogIn": "注册失败"
            });
        }
    }
}