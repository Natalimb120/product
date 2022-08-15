using Product.BuildingBlocks.Domain;
using System;

namespace Product.Ck.Domain.Offer
{
    public interface IOfferRepository : IRepository<Offer>
    {
        Offer GetOffer(Guid OfferId);
        Guid SaveOffer(Offer Offer);
    }
}