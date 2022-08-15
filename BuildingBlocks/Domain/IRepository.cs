using System;

namespace Product.BuildingBlocks.Domain
{
    public interface IRepository<TEntity> where TEntity : IAggregateRoot
    {
        
    }
}