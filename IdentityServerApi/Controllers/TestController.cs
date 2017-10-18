using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityServerApi.Cache;
using Microsoft.Extensions.Configuration;
using System.IO;
using StackExchange.Redis;
using BLL;

namespace IdentityServerApi.Controllers
{
    public class TestController : Controller
    {

        public IActionResult Redis()
        {
            return View();
        }

        public IActionResult RedisDemo()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379");
            IDatabase db = redis.GetDatabase();
            string value = "abcdefg";
            bool r1= db.StringSet("mykey", value);
            string saveValue = db.StringGet("mykey");
            bool r2= db.StringSet("mykey", "NewValue");
            saveValue = db.StringGet("mykey");
            bool r3 = db.KeyDelete("mykey");
            string uncacheValue = db.StringGet("mykey");
            return Json("");
        }

        public IActionResult AddUser()
        {
            UserManager manager = new UserManager();
            manager.AddUser(new Model.Models.User()
            {
                Account="001",
                Password="aaaaa"
            });
            return Json("");
        }

        public IActionResult QueryUser()
        {
            UserManager manager = new UserManager();
            var u= manager.QueryUserById(2);
            return Json("");
        }
    }
}