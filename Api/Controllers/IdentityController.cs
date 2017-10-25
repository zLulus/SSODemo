// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Api.Services;

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
        public IActionResult Post(UrlResolveService urlResolveService)
        {
            //todo 重定向到了login方法  授权问题？
            //todo fiddler抓包？
            //api去identityserver查询用户信息
            string authorityUrl = urlResolveService.GetAuthorityUrl();
            var client = new HttpClient();
            //todo 
            var content = client.GetAsync($"{authorityUrl}/Account/GetUserInfo").Result;
            var s = content.Content.ReadAsStringAsync().Result;
            return new JsonResult(content);
        }
    }
}