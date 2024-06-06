using Smartwyre.DataLayer.DataStores.Interfaces;
using Smartwyre.DeveloperTest.Services.RebateCalculations.Interfaces;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.Entities.BusinessEntities.Requests;
using Smartwyre.Entities.Choices;
using Smartwyre.Entities.DataEntities;
using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using Smartwyre.Entities.BusinessEntities.Responses;
using Smartwyre.DeveloperTest.Services.RebateCalculations;

namespace Smartwyre.DeveloperTest.Tests;

public class RebateServiceTests
{
    private Mock<IRebateDataStoreReader> _rebateDataStoreReaderMock;
    private Mock<IProductDataStoreReader> _productDataStoreReaderMock;
    private Mock<IRebateDataStoreWriter> _rebateDataStoreWriterMock;
    private RebateService _rebateService;

    public RebateServiceTests()
    {
        _rebateDataStoreReaderMock = new Mock<IRebateDataStoreReader>();
        _productDataStoreReaderMock = new Mock<IProductDataStoreReader>();
        _rebateDataStoreWriterMock = new Mock<IRebateDataStoreWriter>();
        _rebateService = new RebateService(_rebateDataStoreReaderMock.Object,
                                            _productDataStoreReaderMock.Object,
                                            _rebateDataStoreWriterMock.Object,
                                            new List<IRebateCalculator>() { new FixedCashAmountCalculator(), new FixedRateRebateCalculator(), new AmountPerUomCalculator()});
    }


    [Fact]
    public void Calculate_Returns_Success_Result_For_Valid_Data()
    {
        // Arrange
        CalculateRebateRequest request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebateIdentifier",
            ProductIdentifier = "productIdentifier",
            Volume = 10
        };

        Rebate rebate = new Rebate
        {
            Incentive = IncentiveType.FixedCashAmount,
            Amount = 50 // Assuming a valid rebate amount
        };

        Product product = new Product
        {
            SupportedIncentives = SupportedIncentiveType.FixedCashAmount // Assuming the product supports FixedCashAmount incentive
        };

        _rebateDataStoreReaderMock.Setup(m => m.Get(request.RebateIdentifier)).Returns(rebate);
        _productDataStoreReaderMock.Setup(m => m.Get(request.ProductIdentifier)).Returns(product);

        CalculateRebateResult result = _rebateService.Calculate(request);
        // check StoreCalculationResult was not called once
        _rebateDataStoreWriterMock.Verify(m => m.StoreCalculationResult(rebate, It.IsAny<decimal>()), Times.Once);
        // Assert
        Assert.True(result.Success);

        rebate = new Rebate
        {
            Incentive = IncentiveType.FixedRateRebate,
            Percentage = 5 // Assuming a valid rebate percentaje
        };
        product = new Product
        {
            Price = 10, // Assuming a valid product price
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate // Assuming the product supports FixedRateRebate incentive
        };

        _rebateDataStoreReaderMock.Setup(m => m.Get(request.RebateIdentifier)).Returns(rebate);
        _productDataStoreReaderMock.Setup(m => m.Get(request.ProductIdentifier)).Returns(product);

        result = _rebateService.Calculate(request);
        // check StoreCalculationResult was not called once
        _rebateDataStoreWriterMock.Verify(m => m.StoreCalculationResult(rebate, It.IsAny<decimal>()), Times.Once);
        // Assert
        Assert.True(result.Success);

        rebate = new Rebate
        {
            Incentive = IncentiveType.AmountPerUom,
            Amount = 50 // Assuming a valid rebate amount
        };
        product = new Product
        {
            SupportedIncentives = SupportedIncentiveType.AmountPerUom // Assuming the product supports AmountPerUom incentive
        };
        
        _rebateDataStoreReaderMock.Setup(m => m.Get(request.RebateIdentifier)).Returns(rebate);
        _productDataStoreReaderMock.Setup(m => m.Get(request.ProductIdentifier)).Returns(product);

        result = _rebateService.Calculate(request);
        // check StoreCalculationResult was not called once
        _rebateDataStoreWriterMock.Verify(m => m.StoreCalculationResult(rebate, It.IsAny<decimal>()), Times.Once);
        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public void Calculate_Returns_Fail_Result_For_Invalid_Data()
    {
        // Arrange
        CalculateRebateRequest request = new CalculateRebateRequest
        {
            RebateIdentifier = "rebateIdentifier",
            ProductIdentifier = "productIdentifier",
            Volume = 10
        };

        Rebate rebate = new Rebate
        {
            Incentive = IncentiveType.FixedCashAmount,
            Amount = 50
        };

        Product product = new Product
        {
            SupportedIncentives = SupportedIncentiveType.AmountPerUom // Assuming incorrect SupportedIncentiveType
        };

        _rebateDataStoreReaderMock.Setup(m => m.Get(request.RebateIdentifier)).Returns(rebate);
        _productDataStoreReaderMock.Setup(m => m.Get(request.ProductIdentifier)).Returns(product);

        CalculateRebateResult result = _rebateService.Calculate(request);
        // check StoreCalculationResult was not called
        _rebateDataStoreWriterMock.Verify(m => m.StoreCalculationResult(rebate, It.IsAny<decimal>()), Times.Never);
        // Assert
        Assert.False(result.Success);

        rebate = new Rebate
        {
            Incentive = IncentiveType.FixedRateRebate,
            Percentage = 0 // Assuming incorrect rebate percentaje
        };
        product = new Product
        {
            Price = 10, 
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate 
        };

        _rebateDataStoreReaderMock.Setup(m => m.Get(request.RebateIdentifier)).Returns(rebate);
        _productDataStoreReaderMock.Setup(m => m.Get(request.ProductIdentifier)).Returns(product);

        result = _rebateService.Calculate(request);
        // check StoreCalculationResult was not called
        _rebateDataStoreWriterMock.Verify(m => m.StoreCalculationResult(rebate, It.IsAny<decimal>()), Times.Never);
        // Assert
        Assert.False(result.Success);

        rebate = new Rebate
        {
            Incentive = IncentiveType.AmountPerUom,
            Amount = 0 // Assuming a invalid rebate amount
        };
        product = new Product
        {
            SupportedIncentives = SupportedIncentiveType.AmountPerUom 
        };

        _rebateDataStoreReaderMock.Setup(m => m.Get(request.RebateIdentifier)).Returns(rebate);
        _productDataStoreReaderMock.Setup(m => m.Get(request.ProductIdentifier)).Returns(product);

        result = _rebateService.Calculate(request);
        // check StoreCalculationResult was not called
        _rebateDataStoreWriterMock.Verify(m => m.StoreCalculationResult(rebate, It.IsAny<decimal>()), Times.Never);
        // Assert
        Assert.False(result.Success);
    }
}
