using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace HRedis
{
    /// <summary>
    /// Redis帮助类
    /// </summary>
    public class RedisHelper
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private readonly string ConnectionString;
        /// <summary>
        /// redis 连接对象
        /// </summary>
        private readonly IConnectionMultiplexer ConnMultiplexer;
        /// <summary>
        /// 默认的 Key 值（用来当作 RedisKey 的前缀）
        /// </summary>
        private readonly string DefaultKey;
        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object Locker = new object();
        /// <summary>
        /// 数据库
        /// </summary>
        private readonly IDatabase _db;

        /// <summary>
        /// 获取 Redis 连接对象
        /// </summary>
        /// <returns></returns>
        public IConnectionMultiplexer GetConnectionRedisMultiplexer()
        {
            if ((ConnMultiplexer == null) || !ConnMultiplexer.IsConnected)
            {
                lock (Locker)
                {
                    if ((ConnMultiplexer == null) || !ConnMultiplexer.IsConnected)
                        ConnectionMultiplexer.Connect(ConnectionString);
                }
            }
            return ConnMultiplexer;
        }
        #region 构造函数

        public RedisHelper(IOptionsSnapshot<RedisSetting> options)
        {
            ConnectionString = options.Value.ConnectionString;
            ConnMultiplexer = ConnectionMultiplexer.Connect(ConnectionString);
            DefaultKey = options.Value.DefaultKey;
            _db = ConnMultiplexer.GetDatabase(options.Value.Db);
        }
        #endregion
        #region String 操作
        /// <summary>
        /// 添加 Key 的前缀
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string AddKeyPrefix(string key)
        {
            var theKey = string.IsNullOrEmpty(DefaultKey) ? key : DefaultKey;
            return key.StartsWith(theKey) ? key : $"{theKey}:{key}";
        }

        /// <summary>
        /// 设置 key 并保存字符串（如果 key 已存在，则覆盖值）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool StringSet(string redisKey, string redisValue, TimeSpan? expiry = null)
        {
            redisKey = AddKeyPrefix(redisKey);
            return _db.StringSet(redisKey, redisValue, expiry);
        }

        /// <summary>
        /// 保存多个 Key-value
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        public bool StringSet(IEnumerable<KeyValuePair<RedisKey, RedisValue>> keyValuePairs)
        {
            keyValuePairs =
                keyValuePairs.Select(x => new KeyValuePair<RedisKey, RedisValue>(AddKeyPrefix(x.Key), x.Value));
            return _db.StringSet(keyValuePairs.ToArray());
        }


        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public string StringGet(string redisKey, TimeSpan? expiry = null)
        {
            redisKey = AddKeyPrefix(redisKey);
            return _db.StringGet(redisKey);

        }

        /// <summary>
        /// 存储一个对象（该对象会被序列化保存）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool StringSet<T>(string redisKey, T redisValue, TimeSpan? expiry = null)
        {
            redisKey = AddKeyPrefix(redisKey);
            var json = JsonConvert.SerializeObject(redisValue);
            return _db.StringSet(redisKey, json, expiry);
        }


        /// <summary>
        /// 获取一个对象（会进行反序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public T StringGet<T>(string redisKey, TimeSpan? expiry = null, Func<T> func = null)
        {
            var result = default(T);
            redisKey = AddKeyPrefix(redisKey);
            var value = _db.StringGet(redisKey);
            if (value.HasValue) result = JsonConvert.DeserializeObject<T>(value);
            else if (func != null)
            {
                result = func.Invoke();
                if (result != null)
                    StringSet(redisKey, result, expiry);
            }
            return result;
        }
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public string StringGet(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return _db.StringGet(redisKey);
        }
        /// <summary>
        /// 获取满足条件的所有key
        /// </summary>
        /// <param name="ruleKeys"></param>
        /// <returns></returns>
        public List<T> StringGetByRuleKey<T>(string ruleKeys, int db = -1)
        {
            ruleKeys = AddKeyPrefix(ruleKeys);
            var endPoint = ConnMultiplexer.GetEndPoints();
            var list = new List<T>();
            foreach (var point in endPoint)
            {
                var server = ConnMultiplexer.GetServer(point);
                list.AddRange(server.Keys(db, ruleKeys).Select(x => StringGet<T>(x)));
            }
            return list;
        }
        #region async

        /// <summary>
        /// 保存一个字符串值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync(string redisKey, string redisValue, TimeSpan? expiry = null)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await _db.StringSetAsync(redisKey, redisValue, expiry);
        }

        /// <summary>
        /// 保存一组字符串值
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync(IEnumerable<KeyValuePair<RedisKey, RedisValue>> keyValuePairs)
        {
            keyValuePairs =
                keyValuePairs.Select(x => new KeyValuePair<RedisKey, RedisValue>(AddKeyPrefix(x.Key), x.Value));
            return await _db.StringSetAsync(keyValuePairs.ToArray());
        }

        /// <summary>
        /// 获取单个值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<string> StringGetAsync(string redisKey, string redisValue, TimeSpan? expiry = null)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await _db.StringGetAsync(redisKey);
        }

        /// <summary>
        /// 存储一个对象（该对象会被序列化保存）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync<T>(string redisKey, T redisValue, TimeSpan? expiry = null)
        {
            redisKey = AddKeyPrefix(redisKey);
            var json = JsonConvert.SerializeObject(redisValue);
            return await _db.StringSetAsync(redisKey, json, expiry);
        }

        /// <summary>
        /// 获取一个对象（会进行反序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<T> StringGetAsync<T>(string redisKey, TimeSpan? expiry = null)
        {
            redisKey = AddKeyPrefix(redisKey);
            return JsonConvert.DeserializeObject<T>(await _db.StringGetAsync(redisKey));
        }
        #endregion

        #endregion
        #region key 操作
        /// <summary>
        /// 移除指定 Key
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public bool KeyDelete(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return _db.KeyDelete(redisKey);
        }
        /// <summary>
        /// 移除指定 Key
        /// </summary>
        /// <param name="redisKeys"></param>
        /// <returns></returns>
        public long KeyDelete(IEnumerable<string> redisKeys)
        {
            var keys = redisKeys.Select(x => (RedisKey)AddKeyPrefix(x));
            return _db.KeyDelete(keys.ToArray());
        }

        /// <summary>
        /// 校验 Key 是否存在
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public bool KeyExists(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return _db.KeyExists(redisKey);
        }

        /// <summary>
        /// 重命名 Key
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisNewKey"></param>
        /// <returns></returns>
        public bool KeyRename(string redisKey, string redisNewKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return _db.KeyRename(redisKey, redisNewKey);
        }

        /// <summary>
        /// 设置 Key 的时间
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool KeyExpire(string redisKey, TimeSpan? expiry)
        {
            redisKey = AddKeyPrefix(redisKey);
            return _db.KeyExpire(redisKey, expiry);
        }

        #region key-async

        /// <summary>
        /// 移除指定 Key
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<bool> KeyDeleteAsync(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await _db.KeyDeleteAsync(redisKey);
        }

        /// <summary>
        /// 移除指定 Key
        /// </summary>
        /// <param name="redisKeys"></param>
        /// <returns></returns>
        public async Task<long> KeyDeleteAsync(IEnumerable<string> redisKeys)
        {
            var keys = redisKeys.Select(x => (RedisKey)AddKeyPrefix(x));
            return await _db.KeyDeleteAsync(keys.ToArray());
        }

        /// <summary>
        /// 校验 Key 是否存在
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<bool> KeyExistsAsync(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await _db.KeyExistsAsync(redisKey);

        }

        /// <summary>
        /// 重命名 Key
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisNewKey"></param>
        /// <returns></returns>
        public async Task<bool> KeyRenameAsync(string redisKey, string redisNewKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await _db.KeyRenameAsync(redisKey, redisNewKey);
        }

        /// <summary>
        /// 设置 Key 的时间
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<bool> KeyExpireAsync(string redisKey, TimeSpan? expiry)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await _db.KeyExpireAsync(redisKey, expiry);
        }


        #endregion key-async


        #endregion key 操作

        #region Hash操作
        /// <summary>
        /// 设置Hash值
        /// </summary>
        /// <param name="hashKey"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public bool SetHash(string hashKey, Dictionary<string, string> dic)
        {
            if (dic.Count < 1) return false;
            var list = new HashEntry[dic.Count];
            var i = 0;
            foreach (var item in dic.Keys)
            {
                var value = dic[item];
                var entry = new HashEntry(item, value);
                list.SetValue(entry, i);
                i++;
            }
            _db.HashSet(hashKey, list);
            return true;
        }
        /// <summary>

        /// 获取Hash值
        /// </summary>
        /// <param name="hashKey"></param>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public string GetHash(string hashKey, string key, Func<string> func = null)
        {
            var value = _db.HashGet(hashKey, key);
            if (value.IsNull && func != null)

            {
                var dic = new Dictionary<string, string>();
                var val = func.Invoke();
                if (!string.IsNullOrEmpty(val))
                {
                    dic[key] = val;
                    SetHash(hashKey, dic);
                    return val;
                }
            }
            return value;
        }
        #endregion
    }
}