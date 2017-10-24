// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        /// <summary>
        /// 获得授权情况
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        /// <summary>
        /// 获得用户基本信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post()
        {
            //api去identityserver查询用户信息
            var client = new HttpClient();
            //var content = client.PostAsync($"http://localhost:5000/Account/GetUserInfo",new StringContent("")).Result;
            //todo 
            var content = client.GetAsync($"http://localhost:5000/Account/GetUserInfo").Result;
            var s = content.Content.ReadAsStringAsync().Result;
            return new JsonResult(content);
        }
    }
}