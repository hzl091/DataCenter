using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DC.Common;

namespace DC.DAL
{
    public static class SqlHelper
    {
        public static string ConString {
            get {
                return MyFX.Core.Base.ConfigManager.GetConnectionString("dc_con");
            }
        }

        public static T ExecuteCommand<T>(Func<SqlConnection, T> func)
        {
            var con = new SqlConnection(ConString);
            T rs;

            try
            {
                con.Open();
                rs = func(con);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }

            return rs;
        }

        public static DataTable GetData(string sql)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, ConString);
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        public static int ExecuteNonQuery(string sql)
        {
            return ExecuteCommand((con) =>
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                return cmd.ExecuteNonQuery();
            });
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, SqlParameter[] parameters, string tableName)
        {
            return ExecuteCommand((con) =>
            {
                DataSet dataSet = new DataSet();
                SqlDataAdapter sqlDa = new SqlDataAdapter
                {
                    SelectCommand = BuildQueryCommand(con, storedProcName, parameters)
                };
                sqlDa.Fill(dataSet, tableName);
                return dataSet;
            });
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>DataTable</returns>
        public static DataTable RunPagerProcedure(SqlParameter[] parameters, out int pageCount, out int recordCount)
        {
            int pCount = 0;
            int rCount = 0;

            DataTable tb = ExecuteCommand((con) =>
            {
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter();
                sqlDa.SelectCommand = BuildQueryCommand(con, "Pager", parameters);
                sqlDa.Fill(dt);
                pCount = Convert.ToInt32(sqlDa.SelectCommand.Parameters["@PageCount"].Value);
                rCount = Convert.ToInt32(sqlDa.SelectCommand.Parameters["@RecordCount"].Value);
                return dt;
            });

            pageCount = pCount;
            recordCount = rCount;
            return tb;
        }

        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection) {CommandType = CommandType.StoredProcedure};
            foreach (var dataParameter in parameters)
            {
                var parameter = dataParameter;
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }


        public static PagerInfo GetPagerData(string tableName, string columns, string where, string orderBy, int pageIndex, int pageSize)
        {
            List<SqlParameter> ps = new List<SqlParameter>
            {
                new SqlParameter("@TableName", tableName) {DbType = DbType.String},
                string.IsNullOrEmpty(columns)
                    ? new SqlParameter("@Columns", "*") {DbType = DbType.String}
                    : new SqlParameter("@Columns", columns) {DbType = DbType.String},
                string.IsNullOrEmpty(@where)
                    ? new SqlParameter("@Condition", "1=1") {DbType = DbType.String}
                    : new SqlParameter("@Condition", @where) {DbType = DbType.String},
                new SqlParameter("@OrderBy", orderBy) {DbType = DbType.String},
                new SqlParameter("@PageNum", pageIndex) {DbType = DbType.Int32},
                new SqlParameter("@PageSize", pageSize) {DbType = DbType.Int32},
                new SqlParameter("@PageCount", SqlDbType.Int) {Direction = ParameterDirection.Output},
                new SqlParameter("@RecordCount", SqlDbType.BigInt) {Direction = ParameterDirection.Output}
            };

            int pageCount = 0;
            int recordCount = 0;
            var dt = RunPagerProcedure(ps.ToArray(), out pageCount, out recordCount);

            PagerInfo pagerInfo = new PagerInfo
            {
                PagerData = dt,
                PageCount = pageCount,
                RecordCount = recordCount
            };

            return pagerInfo;
        }
    }
}