using DC.Common.DataManage;
using MyFX.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.Domain.DataManage
{
    /// <summary>
    /// 表信息
    /// </summary>
    public class TableInfo : AggregateRoot<int>
    {
        private List<ColumnInfo> _columnInfos = null;

        public TableInfo()
        {
            AddTime = DateTime.Now;
            _columnInfos = new List<ColumnInfo>();
        }

        public TableInfo(string name, string desc)
            : this()
        {
            this.Name = name;
            this.Desc = desc;
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 列集合信息
        /// </summary>
        public virtual ICollection<ColumnInfo> ColumnInfos
        {
            get { return _columnInfos; }
            set { _columnInfos = value.ToList(); }
        }

        /// <summary>
        /// 带排序的列集合信息
        /// </summary>
        public IEnumerable<ColumnInfo> SortColumnInfos
        {
            get{ return ColumnInfos.OrderBy(c => c.Sort).ToList(); }
        }

        #region 方法
        /// <summary>
        /// 获取列信息拼接的sql
        /// </summary>
        /// <returns></returns>
        public string GetColumnSql()
        {
            return string.Join(",", SortColumnInfos.Select(c => string.Format( "[{0}]", c.Name )));
        }

        /// <summary>
        /// 获取指定列名的描述信息
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        public string GetColumnDesc(string colName)
        {
            if (colName.ToLower() == "rownum")
            {
                return "编号";
            }
            return this.ColumnInfos.Single(c => c.Name.Equals(colName)).Desc;
        }

        /// <summary>
        /// 检测指定的列是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool CheckColumnExist(string name)
        {
            return this.ColumnInfos.Any(c => c.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// 检测主键列是否存在
        /// </summary>
        /// <returns></returns>
        private bool CheckPrimaryKey()
        {
            return this.ColumnInfos.Any(c => c.IsPrimaryKey);
        }

        /// <summary>
        /// 获取最大排序数
        /// </summary>
        /// <returns></returns>
        public int GetMaxSort()
        {
            return this.ColumnInfos.Any() ? this.ColumnInfos.Max(c => c.Sort) : 0;
        }

        /// <summary>
        /// 添加列信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="formItemType"></param>
        /// <param name="desc"></param>
        /// <param name="isPrimaryKey"></param>
        /// <param name="isSystem"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public ColumnInfo AddColumnInfo(string name, FormItemType formItemType, string desc, bool isPrimaryKey, bool isSystem, int sort = 0)
        {
            name = name.Replace(" ", ""); //去空格处理

            #region 数据校验
            if (string.IsNullOrEmpty( name ))
            {
                throw new MyFX.Core.Exceptions.DomainException("添加失败，列名不能为空");
            }

            if(this.CheckColumnExist(name))
            {
                throw new MyFX.Core.Exceptions.DomainException(string.Format("添加失败，已存在相同的列[{0}]", name));
            }

            if (isPrimaryKey && this.CheckPrimaryKey())
            {
                throw new MyFX.Core.Exceptions.DomainException(string.Format("不能将[{0}]设为主键列，主键列已存在", name));
            }
            #endregion

            ColumnInfo info = new ColumnInfo();
            info.Name = name;
            info.Desc = desc;
            info.IsPrimaryKey = isPrimaryKey;
            info.IsSystem = isSystem;
            info.FormItemType = formItemType;
            
            #region 适配表单项
            switch (formItemType)
            { 
                case FormItemType.Text:
                    info.Type = "nvarchar(200)";
                    break;
                case FormItemType.BigText:
                    info.Type = "nvarchar(4000)";
                    break;
                case FormItemType.Number:
                    info.Type = "int";
                    break;
                case FormItemType.Double:
                    info.Type = "double";
                    break;
                case FormItemType.DateTime:
                    info.Type = "datetime";
                    break;
                case FormItemType.Money:
                    info.Type = "decimal";
                    break;
                case FormItemType.RadioButtonList:
                    info.Type = "nvarchar(50)";
                    break;
                case FormItemType.CheckBoxList:
                    info.Type = "nvarchar(50)";
                    break;
                case FormItemType.DropDownList:
                    info.Type = "nvarchar(50)";
                    break;
                case FormItemType.Image:
                    info.Type = "nvarchar(200)";
                    break;
                case FormItemType.File:
                    info.Type = "nvarchar(200)";
                    break;
                default:
                    throw new MyFX.Core.Exceptions.DomainException("不支持的表单项类型");
            }
            #endregion

            #region 设置排序值
            info.Sort = sort <= 0 ? GetMaxSort() + 1 : sort;
            #endregion

            _columnInfos.Add(info);
            return info;
        }

        /// <summary>
        /// 编辑列信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public ColumnInfo EditColumnInfo(string name, string desc, int sort)
        {
            var columnInfo = GetColumnForBeforeEdit(name);
            columnInfo.Desc = desc;
            if (sort != 0)
            {
                columnInfo.Sort = sort;
            }
            return columnInfo;
        }

        /// <summary>
        /// 列排序设置
        /// </summary>
        /// <param name="name"></param>
        /// <param name="moveType"></param>
        public void ColumnMove(string name, MoveType moveType)
        {
            var columnInfo = GetColumnForBeforeEdit(name);
            int sort = columnInfo.Sort;
            int minSort = this.ColumnInfos.Min(c => c.Sort);
            int maxSort = this.ColumnInfos.Max(c => c.Sort);

            int index = -1;
            var colInfo = GetColumnIndex(SortColumnInfos.ToArray(), name, out index);
            int currentColumnSort = colInfo.Sort;
            
            if (moveType == MoveType.Up)
            {
                if (sort == minSort) return;
                if (index == -1) return;
                var preColumnInfo = this.SortColumnInfos.ToArray()[index - 1];
                int preColumnSort = preColumnInfo.Sort;
                colInfo.Sort = preColumnSort;
                preColumnInfo.Sort = currentColumnSort;
            }
            else
            {
                if (sort == maxSort) return;
                if (index == -1) return;
                var nextColumnInfo = this.SortColumnInfos.ToArray()[index + 1];
                int nextColumnSort = nextColumnInfo.Sort;
                colInfo.Sort = nextColumnSort;
                nextColumnInfo.Sort = currentColumnSort;
            }
        }


        private ColumnInfo GetColumnForBeforeEdit(string colName)
        {
            colName = colName.Replace(" ", ""); //去空格处理

            #region 数据校验
            if (string.IsNullOrEmpty(colName))
            {
                throw new MyFX.Core.Exceptions.DomainException("编辑失败，列名不能为空");
            }

            if (!this.CheckColumnExist(colName))
            {
                throw new MyFX.Core.Exceptions.DomainException(string.Format("编辑失败，不存在列[{0}]", colName));
            }
            #endregion

            var columnInfo = ColumnInfos.Single(c => c.Name == colName);
            return columnInfo;
        }

        private ColumnInfo GetColumnIndex(ColumnInfo[] cols, string colName, out int index)
        {
            for (int i = 0; i < cols.Count(); i++)
            {
                if (cols[i].Name == colName)
                {
                    index = i;
                    return cols[i];
                }
            }

            index = - 1;
            return null;
        }

        #endregion
    }
}
