using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.Memory;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerApi.Cache.User
{
    /// <summary>
    /// 被Controller调用：生成Token的时候添加缓存，根据token查询userId再查询用户信息
    /// </summary>
    public class TokenCacheManager
    {
        private ConnectionMultiplexer redis { get; set; }
        private IConfiguration Configuration { get; set; }
        private IDatabase db { get; set; }

        public TokenCacheManager()
        {
            //读取配置文件appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string redisConnection = Configuration.GetConnectionString("RedisConnection");
            redis = ConnectionMultiplexer.Connect(redisConnection);
            db = redis.GetDatabase();
        }

        public bool InsertToken(string token, string userId)
        {
            return db.StringSet(token, userId);
        }

        public string GetUserId(string token)
        {
            string userId= db.StringGet(token);
            //如果存在缓存键值对，删去缓存
            if (userId != null)
            {
                bool r= db.KeyDelete(token);
            }
            return userId;
        }
    }
}
