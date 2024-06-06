using Smartwyre.DeveloperTest.Services.RebateCalculations.Interfaces;
using Smartwyre.Entities.BusinessEntities.Requests;
using Smartwyre.Entities.Choices;
using Smartwyre.Entities.DataEntities;

namespace Smartwyre.DeveloperTest.Services.RebateCalculations
{
    public class AmountPerUomCalculator : IRebateCalculator
    {
        public bool Calculable(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            if (rebate == null || product == null || rebate.Incentive != IncentiveType.AmountPerUom
                || !product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom) 
                || rebate.Amount == 0 || request.Volume == 0)
            {
                return false;
            }
            return true;
        }

        public decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Amount * request.Volume;
        }
    }
}
