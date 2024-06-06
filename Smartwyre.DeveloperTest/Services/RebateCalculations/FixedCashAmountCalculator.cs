using Smartwyre.DeveloperTest.Services.RebateCalculations.Interfaces;
using Smartwyre.Entities.BusinessEntities.Requests;
using Smartwyre.Entities.Choices;
using Smartwyre.Entities.DataEntities;
using System.Reflection.Metadata.Ecma335;

namespace Smartwyre.DeveloperTest.Services.RebateCalculations
{
    public class FixedCashAmountCalculator : IRebateCalculator
    {
        public bool Calculable(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            if (rebate == null || product == null || rebate.Incentive != IncentiveType.FixedCashAmount 
                || !product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount) 
                || rebate.Amount == 0)
            {
                return false;
            }
            return true;
        }

        public decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Amount;
        }
    }
}
