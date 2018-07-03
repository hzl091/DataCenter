using MyFX.Repository.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DC.DAL
{
    public class DCUnitOfWork : EFUnitOfWork
    {
        public override DbContext Context
        {
            get { return EFDbContextFactory.GetSqlServerDbContext(); }
            set {}
        }
    }
}
