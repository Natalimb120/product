using System;
using Product.BuildingBlocks.Application;

namespace Product.Ck.Application.Offer.CreateOffer
{
    public class CreateOfferCommand : IRequest<Either<Exception, CommandResult>>
    {
        public Guid OfferId;

        public CreateOfferCommand(Guid offerId)
        {
            OfferId = offerId;
        }
    }
}
