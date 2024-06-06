using Smartwyre.Entities.DataEntities;
using Smartwyre.DataLayer.DataStores.BaseClasses.Interfaces;

namespace Smartwyre.DataLayer.DataStores.Interfaces
{
    public interface IRebateDataStoreWriter : IDataStoreWriter<Rebate>
    {
        void StoreCalculationResult(Rebate account, decimal rebateAmount);
    }
}
