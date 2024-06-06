using Smartwyre.Entities.DataEntities.BaseClasses;
using Smartwyre.DataLayer.DataStores.BaseClasses.Interfaces;

namespace Smartwyre.DataLayer.DataStores.BaseClasses
{
    public abstract class DataStoreRemover<T> : IDataStoreRemover<T> where T : DataEntity
    {
        //TODO: add generic remover db methods
    }
}
