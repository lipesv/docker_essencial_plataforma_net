using Common.Domain.Core.Settings.MongoDbSettings.Interfaces;

namespace Common.Domain.Core.Settings.MongoDbSettings
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}