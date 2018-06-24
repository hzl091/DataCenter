using MyFX.Core.DI;
using MyFX.Core.Domain.Repositories;
using DC.Domain.DataManage;

namespace DC.DAL.IRepository
{
    public interface ITableInfoRepository : IRepository<TableInfo, int>, IDependency
    {
    }
}
