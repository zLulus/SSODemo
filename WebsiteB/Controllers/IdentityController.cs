using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebsiteB.Controllers
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