﻿using StackExchange.Redis;
using System;
using System.Collections.Generic;
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
        private IDatabase db { get; set; }

        public TokenCacheManager()
        {
            //todo 读取配置文件
            redis = ConnectionMultiplexer.Connect("127.0.0.1:6379");
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
