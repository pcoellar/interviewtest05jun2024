using Smartwyre.Entities.Choices;
using Smartwyre.Entities.DataEntities.BaseClasses;

namespace Smartwyre.Entities.DataEntities;

public class Rebate : DataEntity
{
    public IncentiveType Incentive { get; set; }
    public decimal Amount { get; set; }
    public decimal Percentage { get; set; }
}
