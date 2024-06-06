using Smartwyre.Entities.Choices;
using Smartwyre.Entities.DataEntities.BaseClasses;

namespace Smartwyre.Entities.DataEntities;

public class Product : DataEntity
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public string Uom { get; set; }
    public SupportedIncentiveType SupportedIncentives { get; set; }
}
