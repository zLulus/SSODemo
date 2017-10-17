using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using Newtonsoft.Json;
using Model.Dtos;

namespace WebsiteB.Controllers
{
    [Route("[controller]")]
    //在controller上添加   [Authorize]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        //check user
        [HttpGet]
        public IActionResult Get(string token)
        {
            //使用该控制器来测试授权需求，并通过API的眼睛对索赔标识进行可视化
            //获得token传给服务器5000的GetUser，查询用户
            var client = new HttpClient();
            var response = client.GetAsync($"http://localhost:5000/User/GetUser?token={token}").Result;
            var str = response.Content.ReadAsStringAsync().Result;
            GetUserOutput output = JsonConvert.DeserializeObject<GetUserOutput>(str);
            return new JsonResult(output);
        }
    }
}