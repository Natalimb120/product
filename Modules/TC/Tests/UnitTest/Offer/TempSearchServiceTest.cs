using System;
using Product.BuildingBlocks.Domain;
using Product.Ck.Domain.Offer;
using Xunit;

namespace Product.Ck.Test
{
    public class TempSearchServiceTests
    {
        private ConfigurationGenerator _configurationGenerator;
        private BusinessUnitsGenerator _businessUnitsGenerator;
        private OffersGenerator _OffersGenerator;
        private readonly ProductMocksGenerator _productMocksGenerator;
        private readonly CurrencyGenerator _currencyGenerator;

        private readonly APTablesGenerator _APTablesGenerator;
        private readonly APProductsGenerator _APProductsGenerator;
        private readonly ProductTariffGenerator _productTariffGenerator;

        private IGGenerator _IGGenerator;
        private IGProductsGenerator _IGProductsGenerator;

        private ApplicationContext _applicationContext;


        public TempSearchServiceTests()
        {
            _APTablesGenerator = APTablesGenerator
                .Create()
                .AddEntities(APTablesTemplate.Get());

            _productMocksGenerator = ProductMocksGenerator
                .Create()
                .AddEntities(ProductsTemplate.Get());

            _configurationGenerator = ConfigurationGenerator
                .Crete();
               
            _currencyGenerator = CurrencyGenerator
                .Create()
                .AddEntities(CurrencyTemplate.Get());

            _OffersGenerator = OffersGenerator
                .Create();
        }

        internal void InitMocks()
        {
            _applicationContext = ContextBuilder
                .Create()
                .AddEntities(_businessUnitsGenerator.GetEntities())
                .AddEntities(_configurationGenerator.GetEntities())
                .AddEntities(_OffersGenerator.GetEntities())
                .AddEntities(_APTablesGenerator.GetEntities())
                .AddEntities(_APProductsGenerator.GetEntities())
                .AddEntities(_productTariffGenerator.GetEntities())
                .AddEntities(_IGGenerator.GetEntities())
                .AddEntities(_IGProductsGenerator.GetEntities())
                .AddEntities(_currencyGenerator.GetEntities())
                .Build();
        }

        [Fact]
        public void
            SearchTemp_OfferProductExceededValuesAreMatching_ShouldReturnOneTable()
        {
            //Arrange

            var offerSignature = "PP/40/96/TEST";

            _IGGenerator = IGGenerator
                .Create()
                .AddEntity(
                    _OffersGenerator.GetIdByName("Name1", "Surname1"),
                    offerSignature, true, DateTime.Now);

            _IGProductsGenerator = IGProductsGenerator
                .Create()
                .AddExceededEntity(_productMocksGenerator.GetIdByName(ProductsTemplate.K20),
                    _IGGenerator.GetIdBySignature(offerSignature), ProductsTemplate.K20, 1000, false, 1000, 12, 200, 1000, 0.05m);

            InitMocks();
            var OffersSearchService = InitTempSearchService(offerSignature, _applicationContext);


            //Act
            var Temp = OffersSearchService.SearchTemp();

            //Assert

            Assert.Collection(Temp,
                tempOffer => Assert.Equal(_OffersGenerator.GetIdByName("Name3", "Surname3"),
                    tempOffer.OfferId));

            Assert.Collection(Temp,
                tempOffer =>
                    Assert.Equal(_APTablesGenerator.GetIdByName(APTablesTemplate.One),
                        tempOffer.APTableId));
        }

        [Fact]
        public void
            SearchTemp_OfferProductExceededValuesAreMatchingTable_ShouldReturnOneOfferFromSecondTable()
        {
            //Arrange

            var offerSignature = "PP/40/96/TEST";

            _IGGenerator = IGGenerator
                .Create()
                .AddEntity(
                    _OffersGenerator.GetIdByName("Name1", "Surname1"),
                    offerSignature, true, DateTime.Now);

            _IGProductsGenerator = IGProductsGenerator
                .Create()
                .AddExceededEntity(_productMocksGenerator.GetIdByName(ProductsTemplate.K25),
                    _IGGenerator.GetIdBySignature(offerSignature), ProductsTemplate.K25, 1000, false, 1000, 12, 200,
                    1000, 0.05m);

            InitMocks();
            var OffersSearchService = InitTempSearchService(offerSignature, _applicationContext);


            //Act
            var Temp = OffersSearchService.SearchTemp();

            //Assert

            Assert.Collection(Temp,
                tempOffer => Assert.Equal(_OffersGenerator.GetIdByName("Name2", "Surname2"),
                    tempOffer.OfferId));

            Assert.Collection(Temp,
                tempOffer =>
                    Assert.Equal(_APTablesGenerator.GetIdByName(APTablesTemplate.Second),
                        tempOffer.APTableId));

        }
    }
}
