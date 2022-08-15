using System;
using System.Collections.Generic;
using Product.Ck.Domain.Offer;

namespace Product.Ck.Infrastructure.Offer
{
    public class OfferRepository : IOfferRepository
    {
        private readonly ApplicationContext _applicationContext;

        public OfferRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public Offer GetOffer(Guid OfferId)
        {
            var offerSdf = GetCkOfferSdfData(OfferId);

            return offerSdf;
        }

        private ck_offer GetCkOfferSdfData(Guid OfferId)
        {
            return _applicationContext.Service.Retrieve(ck_offer.EntityLogicalName, OfferId).ToEntity<ck_offer>();
        }

        private ck_product GetCkOfferSdfData(Guid offerId)
        {
            return _applicationContext.Service.Retrieve(ck_product.EntityLogicalName, offerId,
                    new ColumnSet(ck_product.Fields.ck_sygnatura,
                        ck_product.Fields.ck_termin_zlozenia_oferty))
                .ToEntity<ck_product>();
        }

        private List<string> CountriesInsured(Guid productId)
        {
            List<string> countries = new List<string>();
            var countriesIds =
                _applicationContext.Context.ck_produktSet.Where(p =>
                    p.ck_produkt.Value == productId).Select(p => p.ck_countryid).ToList();

            countriesIds.ForEach(id =>
            {
                countries.Add(_applicationContext.Context.ck_countrySet.FirstOrDefault(p => p.Id == id)
                    ?.ck_code_i);
            });

            return countries;
        }

        private decimal GetMaxOwnShare(Guid productId)
        {
            return _applicationContext.Context.ck_pk_prodSet
                .Where(p => p.cck_pk_prodid.Id == productId).ToList().OrderByDescending(t => t.ck_amount.Value).ToList()
                .FirstOrDefault().ck_amount.Value;
        }
    }
}
