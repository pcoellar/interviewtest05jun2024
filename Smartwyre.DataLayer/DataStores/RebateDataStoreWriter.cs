using Smartwyre.DataLayer.DataStores.BaseClasses;
using Smartwyre.DataLayer.DataStores.Interfaces;
using Smartwyre.Entities.DataEntities;

namespace Smartwyre.DataLayer.DataStores;

public class RebateDataStoreWriter : DataStoreWriter<Rebate>, IRebateDataStoreWriter
{
    public void StoreCalculationResult(Rebate account, decimal rebateAmount)
    {
        // Update account in database, code removed for brevity
    }
}