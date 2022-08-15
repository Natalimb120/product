using Product.BuildingBlocks.Domain;

namespace Product.Ck.Domain.Offer
{
    public class Sum : IDomainValueObject
    {
        public decimal Value { get; }
        public decimal ExchangeRate { get; private set; } = 1;
        public bool Reduced { get; }
        public decimal ReducedValue { get; }
        public string CurrencyCode { get; }

        private Sum(decimal value, decimal reducedValue, string currencyCode)
        {
            Value = value;
            Reduced = true;
            ReducedValue = reducedValue;
            CurrencyCode = currencyCode;
        }
    }
}