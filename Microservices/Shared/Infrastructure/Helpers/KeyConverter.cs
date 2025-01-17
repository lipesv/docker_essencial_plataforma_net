using Common.Domain.Core.Enums;
using MongoDB.Bson;

namespace Common.Infrastructure.Helpers
{
    public static class KeyConverter
    {
        public static object ConvertToStorageKey<T>(T key, StorageType storageType)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key), "Key cannot be null.");

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
                string idString when ObjectId.TryParse(idString, out var objectId) => objectId,
                byte[] idBytes when idBytes.Length == 12 => new ObjectId(idBytes),
                _ => throw new ArgumentException("Invalid key type for MongoDB. Must be a valid string or byte[].")
            };
        }

        private static string ConvertToRedisKey<T>(T key)
        {
            if (key is string idString)
                return idString;

            throw new ArgumentException("Invalid key type for Redis. Must be a string.");
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
