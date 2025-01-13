using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Domain.Core.Enums;
using MongoDB.Bson;

namespace Microservices.Infrastructure.Helpers
{
    public static class KeyConverter
    {
        public static object ConvertToStorageKey<T>(T key, StorageType storageType)
        {
            return storageType switch
            {
                StorageType.MongoDB => ConvertToMongoKey(key),
                StorageType.Redis => ConvertToRedisKey(key),
                StorageType.SQL => ConvertToSqlKey(key),
                _ => throw new NotSupportedException($"Storage type '{storageType}' is not supported.")
            };
        }

        private static ObjectId ConvertToMongoKey<T>(T key)
        {
            return key switch
            {
                string idString => new ObjectId(idString),
                byte[] idBytes => new ObjectId(idBytes),
                _ => throw new ArgumentException("Invalid key type for MongoDB. Must be a string or byte[].")
            };
        }

        private static string ConvertToRedisKey<T>(T key)
        {
            return key switch
            {
                string idString => idString,
                _ => throw new ArgumentException("Invalid key type for Redis. Must be a string.")
            };
        }

        private static object ConvertToSqlKey<T>(T key)
        {
            return key switch
            {
                int idInt => idInt,
                Guid idGuid => idGuid,
                _ => throw new ArgumentException("Invalid key type for SQL. Must be an int or Guid.")
            };
        }
    }
}