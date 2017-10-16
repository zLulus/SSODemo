using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebsiteB.Dtos;

namespace WebsiteB.Controllers
{
    public class UserController : Controller
    {
        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult TryLogIn(LogInDto input)
        {
            //从WebisiteB的用户表里面查询
            //如果登录成功，登录成功
            if (false)
            {
                return Json("登录成功");
            }
            //登录失败，WebisiteB的用户表里面没有该用户
            else
            {
                //重定向到identityserver的登录页面
                return Redirect("www.baidu.com");
            }
        }
    }
}