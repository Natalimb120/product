using Product.BuildingBlocks.Application;
using Product.BuildingBlocks.Domain;
using Product.Ck.Domain.Offer;
using Product.Ck.Infrastructure.Offer.RepositoryServices;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Ck.Application.Offer.Application.Offer.AssignNumber
{
    public class AssignOfferNumberCommandHandler : IRequestHandler<AssignOfferNumberCommand, Either<Exception, CommandResult>>
    {
        private readonly INumeratorService _numeratorService;
        private readonly IOfferRepository _offerRepository;

        public AssignOfferNumberCommandHandler(INumeratorService numeratorService, IOfferRepository OfferRepository)
        {
            _numeratorService = numeratorService;
            _offerRepository = OfferRepository;
        }

        public async Task<Either<Exception, CommandResult>> Handle(AssignOfferNumberCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var nextOfferNumberFromNumerator =
                    _numeratorService.GetNextNumber(NumeratorEnum.CkOfferNumber);

                var Offer = OfferBuilderService.CreateOfferBuilderService(_offerRepository)
                    .BasedOnSdfOfferData(request.OfferId)
                    .BuildOffer();

                Offer.AssignOfferNumberAndBankAccount(nextOfferNumberFromNumerator);

                _offerRepository.SaveOfferNumber(Offer);

                return CommandResult.SuccessResult()
                    .WithInformation("Ok");
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
                        "AssignOfferNumberCommandHandler",
                        "Handle",
                        null, null,
                        exception
                    );

                return exception;
            }
        }
    }
}
