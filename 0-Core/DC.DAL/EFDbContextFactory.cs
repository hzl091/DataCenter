using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFX.Core;

namespace DC.DAL
{
    public static class EFDbContextFactory
    {
        public static DbContext GetSqlServerDbContext()
        {
            const string dbContextName = "dc_con";
            SqlServerDbContext dbContext = InvokeContext.Get<SqlServerDbContext>(dbContextName);
            if (dbContext == null)
            {
                dbContext = new SqlServerDbContext(dbContextName);
                InvokeContext.Add(dbContextName, dbContext);
            }

            dbContext.Database.Log = Console.WriteLine; //日志监控设置
            return dbContext;
        }
    }
}
