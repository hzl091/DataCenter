/****************************************************************************************
 * 文件名：OrderRepository
 * 作者：huangzl
 * 创始时间：2018/1/17 13:57:42
 * 创建说明：
****************************************************************************************/

using System;
using DC.Domain.DataManage;
using DC.DAL.IRepository;

namespace DC.DAL.Repository
{
    public class TableInfoRepository : RepositoryBase<TableInfo, int>, ITableInfoRepository
    {
        public override TableInfo GetByKey(int id)
        {
            return this.Rs.Find(id);
        }
    }
}
