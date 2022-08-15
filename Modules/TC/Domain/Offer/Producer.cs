using System;
using Product.BuildingBlocks.Domain;

namespace Product.Ck.Domain.Offer
{
    public class Producer : IDomainEntity
    {
        public Guid OfferProducerId { get; }
        public Guid AccountId { get; }
        public string Name { get; }
        public bool IsMainProducer { get; }

        private Producer(Guid accountId, string name, bool isMainProducer)
        {
            AccountId = accountId;
            Name = name;
            IsMainProducer = isMainProducer;
        }

        public static Producer CreateProducer(Guid accountId, string name, bool isMainProducer)
        {
            return new Producer(accountId, name, isMainProducer);
        }
    }
}