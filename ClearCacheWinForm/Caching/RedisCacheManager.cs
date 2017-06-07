using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;
using ClearCacheWinForm.Utils;

namespace ClearCacheWinForm.Caching
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly IRedisConnectionWrapper _connectionWrapper;
        private readonly IDatabase _db;

        public RedisCacheManager(IRedisConnectionWrapper connectionWrapper)
        {
            _connectionWrapper = connectionWrapper;
            _db = _connectionWrapper.Database();
        }

        public T Get<T>(string key)
        {
            var json = _db.StringGet(key);
            if (!json.HasValue)
                return default(T);

            var result = JsonHelper.Deserialize<T>(json);

            return result;
        }
        public string Get(string key)
        {
            var json = _db.StringGet(key);
            return json;
        }

        public void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            var json = JsonHelper.Serialize(data);
            _db.StringSet(key, json, TimeSpan.FromMinutes(cacheTime));
        }

        public bool IsSet(string key)
        {
            return _db.KeyExists(key);
        }

        public void Remove(string key)
        {
            _db.KeyDelete(key);
        }

        public void RemoveByPattern(string pattern)
        {
            foreach (var ep in _connectionWrapper.GetEndpoints())
            {
                var server = _connectionWrapper.Server(ep);
                if (!server.IsConnected) 
                    continue;

                var keys = server.Keys(pattern: "*" + pattern + "*").ToArray();
                foreach (var key in keys)
                    _db.KeyDelete(key);
            }
        }

        public virtual void Clear()
        {
            foreach (var ep in _connectionWrapper.GetEndpoints())
            {
                var server = _connectionWrapper.Server(ep);
                if (!server.IsConnected)
                    continue;

                //we can use the code below (commented)
                //but it requires administration permission - ",allowAdmin=true"
                //server.FlushDatabase();

                //that's why we simply interate through all elements now
                var keys = server.Keys();
                foreach (var key in keys)
                    _db.KeyDelete(key);
            }
        }


        public List<string> GetAllKeys()
        {
            var list=new List<string>();
            foreach (var ep in _connectionWrapper.GetEndpoints())
            {
                var server = _connectionWrapper.Server(ep);
                if (!server.IsConnected)
                    continue;
                var keys = server.Keys().Select(e => e.ToString());
                list.AddRange(keys);
            }
            return list;
        }
    }
}
