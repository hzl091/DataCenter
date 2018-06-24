using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MyFX.Repository.Ef;
using domain = DC.Domain;

namespace DC.DAL
{
    public class SqlServerDbContext : DbContextBase
    {
        /// <summary>
        /// SqlServerDbContext
        /// </summary>
        public SqlServerDbContext(string name)
            : base(name)
        {

        }

        public DbSet<domain.DataManage.TableInfo> Orders { get; set; }

        /// <summary>
        /// OnModelCreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<DatabaseGeneratedAttributeConvention>();

            #region TableInfo
            modelBuilder.Entity<domain.DataManage.TableInfo>().ToTable("TableInfo");
            modelBuilder.Entity<domain.DataManage.TableInfo>().HasKey(o => o.Id);
            modelBuilder.Entity<domain.DataManage.TableInfo>().Ignore(o => o.Version);

            modelBuilder.Entity<domain.DataManage.TableInfo>()
                .HasMany(t => t.ColumnInfos);
            #endregion

            #region ColumnInfo
            modelBuilder.Entity<domain.DataManage.ColumnInfo>().ToTable("ColumnInfo");
            modelBuilder.Entity<domain.DataManage.ColumnInfo>().HasKey(o => o.Id);
            modelBuilder.Entity<domain.DataManage.ColumnInfo>()
                .HasRequired(c => c.TableInfo);
            #endregion
        }
    }
}
