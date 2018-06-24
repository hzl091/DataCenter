using MyFX.Core.Domain;
using MyFX.Core.Domain.Entities;
using MyFX.Repository.Ef;

namespace DC.DAL
{
    public abstract class RepositoryBase<TEntity,TKey> : EFRepository<TEntity, TKey>
        where TEntity : EntityBase<TKey>, IAggregateRoot<TKey>
    {
        public RepositoryBase()
        {
        }
    }
}
