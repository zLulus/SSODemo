// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("[controller]")]
    //在controller上添加   [Authorize]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            //使用该控制器来测试授权需求，并通过API的眼睛对索赔标识进行可视化
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}