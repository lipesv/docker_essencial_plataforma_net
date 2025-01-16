using Microservices.Domain.Core.Entities;
using Microservices.Domain.Core.Enums;
using Microservices.Infrastructure.Context.Catalog.Interfaces;
using Microservices.Infrastructure.Helpers;
using Microservices.Infrastructure.Repositories.Base;
using MongoDB.Bson;
using MongoDB.Driver;
using ServiceStack;

namespace Microservices.Infrastructure.Repositories.Catalog
{
    public class CatalogRepository<TEntity> : BaseRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly IMongoContext Context;
        protected readonly IMongoCollection<TEntity> DbSet;

        public CatalogRepository(IMongoContext context) : base(context)
        {
            Context = context;
            DbSet = Context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public override async Task<IEnumerable<TEntity>> GetAll()
        {
            var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return await all.ToListAsync();
        }


        public override async Task<TEntity> GetById(object id)
        {
            var objectId = (ObjectId)KeyConverter.ConvertToStorageKey(id, StorageType.MongoDB);

            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", objectId);

            return await DbSet.FindAsync(filter).Result.FirstOrDefaultAsync();

        }

        public override void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(TEntity).Name + " object is null");
            }

            Context.AddCommand(() => DbSet.InsertOneAsync(entity));
        }

        public override void Update(TEntity entity)
        {
            Context.AddCommand(() => DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", entity.GetId()), entity));
        }

        public override void Delete(object id)
        {
            var objectId = (ObjectId)KeyConverter.ConvertToStorageKey(id, StorageType.MongoDB);
            Context.AddCommand(() => DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", objectId)));
        }

        public override void Dispose()
        {
            Context?.Dispose();
        }
    }
}
