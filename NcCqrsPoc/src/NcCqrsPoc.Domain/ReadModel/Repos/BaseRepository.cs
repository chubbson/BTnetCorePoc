﻿using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Domain.ReadModel.Repos
{
    /// <summary>
    /// This class is BaseRepositoryClass. 
    /// It provides methods by which items can be 
    ///   retrieved from or saved to Redis Data Store
    /// TODO: encapsulate Redis to enable other Data Stores / Event Stores. 
    /// </summary>
    public class BaseRepository
    {
        private readonly IConnectionMultiplexer _redisConnection;

        /// <summary>
        /// The Namespace is the first part of any key created by this Repository, ex "subsidiary" or "employee"
        /// </summary>
        private readonly string _namespace;

        public BaseRepository(IConnectionMultiplexer redis, string nameSpace)
        {
            _redisConnection = redis;
            _namespace = nameSpace;
        }

        public T Get<T>(int id)
        {
            return Get<T>(id.ToString());
        }

        public T Get<T>(string keySuffix)
        {
            var key = MakeKey(keySuffix);
            var database = _redisConnection.GetDatabase();
            var serializedObject = database.StringGet(key);
            if (serializedObject.IsNullOrEmpty) throw new ArgumentNullException(String.Format("connection Multiplexer redis database have not found a key '{0}'", key));
            return JsonConvert.DeserializeObject<T>(serializedObject.ToString());
        }

        public List<T> GetMultiple<T>(List<int> ids)
        {
            var database = _redisConnection.GetDatabase();
            List<RedisKey> keys = new List<RedisKey>();
            foreach (int id in ids)
            {
                keys.Add(MakeKey(id));
            }
            var serializedItems = database.StringGet(keys.ToArray(), CommandFlags.None);
            List<T> items = new List<T>();
            foreach (var item in serializedItems)
            {
                items.Add(JsonConvert.DeserializeObject<T>(item.ToString()));
            }
            return items;
        }

        public bool Exists(int id)
        {
            return Exists(id.ToString());
        }

        // probably marked as private. not used in IBaseRepo interface, which is not an interface of this class!
        public bool Exists(string keySuffix)
        {
            var key = MakeKey(keySuffix);
            var database = _redisConnection.GetDatabase();
            var serializedObject = database.StringGet(key);
            return !serializedObject.IsNullOrEmpty;
        }

        public void Save(int id, object entity)
        {
            Save(id.ToString(), entity);
        }

        public void Save(string keySuffix, object entity)
        {
            var key = MakeKey(keySuffix);
            var database = _redisConnection.GetDatabase();
            database.StringSet(MakeKey(key), JsonConvert.SerializeObject(entity));
        }

        private string MakeKey(int id)
        {
            return MakeKey(id.ToString());
        }

        private string MakeKey(string keySuffix)
        {
            if (!keySuffix.StartsWith(_namespace + ":"))
            {
                return _namespace + ":" + keySuffix;
            }
            else return keySuffix; //Key is already prefixed with namespace
        }

    }
}
