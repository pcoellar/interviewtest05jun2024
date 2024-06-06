using Smartwyre.Entities.DataEntities.BaseClasses;

namespace Smartwyre.DataLayer.DataStores.BaseClasses.Interfaces
{
    public interface IDataStoreReader<T> where T : DataEntity
    {
        T Get(string identifier);
        //TODO: add more generic reader db methods
    }
}
