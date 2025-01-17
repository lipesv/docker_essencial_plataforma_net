namespace Common.Domain.Core.Settings.MongoDbSettings.Interfaces
{
    public interface IMongoDbSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}