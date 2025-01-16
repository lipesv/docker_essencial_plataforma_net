using Catalog.Domain.Entities;
using Infrastructure.Context.Mongo.Interfaces;
using Infrastructure.Context.Util;
using Microservices.Domain.Core.Settings.MongoDbSettings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Context.Mongo
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoClient MongoClient { get; set; }
        private readonly List<Func<Task>> _commands;
        private bool _disposed = false;

        public MongoContext(IOptions<MongoDbSettings> configuration)
        {
            // Every command will be stored and it'll be processed at SaveChanges
            _commands = new List<Func<Task>>();

            ConfigureMongo(configuration);
        }

        public async Task<int> SaveChanges()
        {
            using (Session = await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await Session.CommitTransactionAsync();
            }

            return _commands.Count;
        }

        private void ConfigureMongo(IOptions<MongoDbSettings> configuration)
        {
            if (MongoClient != null)
            {
                return;
            }

            // Configure mongo (You can inject the config, just to simplify)
            var connStr = string.Format(configuration.Value.ConnectionString,
                                                Environment.GetEnvironmentVariable("DB_CONTAINER") ?? "localhost");

            MongoClient = new MongoClient(connStr);

            Database = MongoClient.GetDatabase(configuration.Value.DatabaseName);

            ContextSeed.SeedData(Database.GetCollection<Product>(typeof(Product).Name));
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            if (Session != null)
            {
                Session.Dispose();
                Session = null;
            }

            GC.SuppressFinalize(this);
            _disposed = true;
        }
    }
}