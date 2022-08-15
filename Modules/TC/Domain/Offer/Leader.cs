using Product.BuildingBlocks.Domain;
using System;

namespace Product.Ck.Domain.Offer
{
    public class Leader : IDomainEntity
    {
        public Guid LeaderId { get; }
        public bool isLeadLeader { get; }
    }
}