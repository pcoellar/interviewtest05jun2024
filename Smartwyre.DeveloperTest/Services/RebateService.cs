using Smartwyre.Entities.BusinessEntities.Requests;
using Smartwyre.Entities.BusinessEntities.Responses;
using Smartwyre.DataLayer.DataStores.Interfaces;
using Smartwyre.Entities.DataEntities;
using Smartwyre.DeveloperTest.Services.Interfaces;
using System.Collections.Generic;
using Smartwyre.DeveloperTest.Services.RebateCalculations.Interfaces;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private IRebateDataStoreReader _rebateDataStoreReader;
    private IProductDataStoreReader _productDataStoreReader;
    private IRebateDataStoreWriter _rebateDataStoreWriter;
    private readonly IEnumerable<IRebateCalculator> _rebateCalculators;
    public RebateService(IRebateDataStoreReader rebateDataStoreReader, IProductDataStoreReader productDataStoreReader, IRebateDataStoreWriter rebateDataStoreWriter, IEnumerable<IRebateCalculator> rebateCalculators)
    {
        _rebateDataStoreReader = rebateDataStoreReader;
        _productDataStoreReader = productDataStoreReader;
        _rebateDataStoreWriter = rebateDataStoreWriter;
        _rebateCalculators = rebateCalculators;
    }
    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        Rebate rebate = _rebateDataStoreReader.Get(request.RebateIdentifier);
        Product product = _productDataStoreReader.Get(request.ProductIdentifier);

        CalculateRebateResult result = new CalculateRebateResult();

        foreach (IRebateCalculator calculator in _rebateCalculators)
        {
            if (calculator.Calculable(rebate, product, request))
            {
                var rebateAmount = calculator.Calculate(rebate, product, request);
                _rebateDataStoreWriter.StoreCalculationResult(rebate, rebateAmount);
                result.Success = true;
                return result;
            }
        }

        result.Success = false;
        return result;
    }
}
