using Smartwyre.Entities.BusinessEntities.Requests;
using Smartwyre.Entities.DataEntities;

namespace Smartwyre.DeveloperTest.Services.RebateCalculations.Interfaces;

public interface IRebateCalculator
{
    bool Calculable(Rebate rebate, Product product, CalculateRebateRequest request);
    decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request);
}
