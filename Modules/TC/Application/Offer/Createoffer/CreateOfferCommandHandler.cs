using Product.BuildingBlocks.Application;
using Product.BuildingBlocks.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace Product.Ck.Application.Offer.CreateOffer
{
    public class CreateOfferCommandHandler : IRequestHandler<CreateOfferCommand, Either<Exception, CommandResult>>
    {
        private readonly INewOfferDataBuilder _newOfferDataBuilder;
        private readonly INumeratorService _numeratorService;

        public CreateOfferCommandHandler(INewOfferDataBuilder newOfferDataBuilder, INumeratorService numeratorService)
        {
            _newOfferDataBuilder = newOfferDataBuilder;
            _numeratorService = numeratorService;
        }
        public async Task<Either<Exception, CommandResult>> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tempNumberFromNumerator =
                    _numeratorService.GetNextNumber(NumeratorEnum.CkOfferTemporaryNumber);

                var OfferGuid = _newOfferDataBuilder.CreateOfferFromOffer(request.OfferId,
                    tempNumberFromNumerator);

                return CommandResult.SuccessResult()
                    .WithRecordId(OfferGuid);
            }
            catch (BusinessRuleException businessRuleException)
            {
                return CommandResult.FailedResult()
                    .WithError(businessRuleException.Message);
            }
            catch (Exception exception)
            {
                LoggerFactory.Instance.CreateLogger
                        (LoggerFactory.LoggerNames.Product)
                    .Error(
                        "CreateOfferCommandHandler",
                        "Handle",
                        null, null,
                        exception
                    );

                return exception;
            }
        }
    }
}
