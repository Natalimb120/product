using Product.BuildingBlocks.Domain;
using System;

namespace Product.Ck.Domain.Offer
{
    public class Offer : IDomainEntity, IAggregateRoot
    {
        public Guid OfferId { get; }
        public DateTime OfferDate { get; }
        public string OfferNumber { get; private set; }
      
        private Offer(Guid offerId, DateTime offerDate,
           string offerNumber)
        {
            OfferId = offerId;
            OfferNumber = offerNumber;
            OfferDate = offerDate;
        }
    }
}