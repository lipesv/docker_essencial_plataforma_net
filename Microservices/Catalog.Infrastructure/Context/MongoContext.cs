using Catalog.Domain.Core.Settings.MongoDbSettings;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Context.Util;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.GenericRepository.Interfaces;

namespace Catalog.Infrastructure.Context
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoClient MongoClient { get; set; }
        private readonly List<Func<Task>> _commands;

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
            MongoClient = new MongoClient(string.Format(configuration.Value.ConnectionString,
                                                        Environment.GetEnvironmentVariable("DB_CONTAINER") ?? "localhost"));

            Database = MongoClient.GetDatabase(configuration.Value.DatabaseName);

            CatalogContextSeed.SeedData(Database.GetCollection<Product>(typeof(Product).Name));
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }

        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }
    }
}