using Product.BuildingBlocks.Domain;

namespace Product.BuildingBlocks.Application
{
    public interface IDomainEventHandler<in T> where T : IDomainEvent
    {
        void Handle(T domainEvent);
    }
}
