using Smartwyre.Entities.DataEntities.BaseClasses;
using Smartwyre.DataLayer.DataStores.BaseClasses.Interfaces;

namespace Smartwyre.DataLayer.DataStores.BaseClasses
{
    public abstract class DataStoreReader<T> : IDataStoreReader<T> where T : DataEntity, new()
    {
        public T Get(string identifier)
        {
            // Access database to retrieve data entity, code removed for brevity
            return new T();
        }
        //TODO: add more generic reader db methods
    }
}
