using Smartwyre.Entities.DataEntities.BaseClasses;
using Smartwyre.DataLayer.DataStores.BaseClasses.Interfaces;

namespace Smartwyre.DataLayer.DataStores.BaseClasses
{
    public abstract class DataStoreWriter<T> : IDataStoreWriter<T> where T : DataEntity
    {
        //TODO: add generic writer db methods
    }
}
