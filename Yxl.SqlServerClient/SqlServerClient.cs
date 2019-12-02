using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Yxl
{
    /// <summary>
    /// SQL Server访问客户端类
    /// </summary>
    public class SqlServerClient
    {
        #region 构造方法
        /// <summary>
        /// SQL Server访问客户端类 构造方法 使用Windows身份登录
        /// </summary>
        /// <param name="server">服务器名 包括实例名 默认连接127.0.0.1</param>
        /// <param name="database">数据库名 默认连接master</param>
        public SqlServerClient(string server="127.0.0.1",string database="master")
        {
            scs = String.Format("Data Source = {0}; Initial Catalog = {1}; Integrated Security = True;"
                ,server,database);
        }

        /// <summary>
        /// SQL Server访问客户端类 构造方法 使用SQL Server身份登录 连接master
        /// </summary>
        /// <param name="server">服务器名 包括实例名</param>
        /// <param name="user">用户名</param>
        /// <param name="password">密码</param>
        public SqlServerClient(string server,string user,string password)
        {
            scs = String.Format("Data Source = {0}; User ID = {1}; Password = {2};"
                , server,user,password);  
        }

        /// <summary>
        /// SQL Server访问客户端类 构造方法 使用SQL Server身份登录
        /// </summary>
        /// <param name="server">服务器名 包括实例名</param>
        /// <param name="database">数据库名</param>
        /// <param name="user">用户名</param>
        /// <param name="password">密码</param>
        public SqlServerClient(string server,string database,string user,string password)
        {
            scs = String.Format("Data Source = {0}; Initial Catalog = {1}; User ID = {2}; Password = {3};"
                , server,database,user,password);  
        }
        #endregion

        #region 字段
        /// <summary>
        /// 连接字符串
        /// </summary>
        private string scs;
        #endregion

        #region 方法
        #region SQL命令
        /// <summary>
        /// 使用SQL命令查询一个数据表
        /// </summary>
        /// <param name="sql">SQL命令文本，{0}、{1}为参数占位符</param>
        /// <param name="args">数量可变的参数数组</param>
        /// <returns>查询结果 数据表</returns>
        public DataTable GetDataTable(string sql,params object[] args)
        {
            using (SqlConnection sc=new SqlConnection(scs))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(String.Format(sql,args),sc))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
            }
        }

        /// <summary>
        /// 使用SQL命令查询查询多个数据表
        /// </summary>
        /// <param name="sql">SQL命令文本，{0}、{1}为参数占位符</param>
        /// <param name="args">数量可变的参数数组</param>
        /// <returns>查询结果 数据集</returns>
        public DataSet GetDataSet(string sql,params object[] args)
        {
            using (SqlConnection sc=new SqlConnection(scs))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(String.Format(sql,args),sc))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    return ds;
                }
            }
        }

        /// <summary>
        /// 使用SQL命令查询一个标量值
        /// <param name="sql">SQL命令文本，{0}、{1}为参数占位符</param>
        /// <param name="args">数量可变的参数数组</param>
        /// <returns>查询结果 标量值</returns>
        public object GetValue(string sql,params object[] args)
        {
            using (SqlConnection sc=new SqlConnection(scs))
            {
                using (SqlCommand cmd = new SqlCommand(String.Format(sql,args),sc))
                {
                    sc.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// 执行SQL命令
        /// <param name="sql">SQL命令文本，{0}、{1}为参数占位符</param>
        /// <param name="args">数量可变的参数数组</param>
        /// <returns>受影响条数</returns>
        public int Execute(string sql,params object[] args)
        {
            using (SqlConnection sc=new SqlConnection(scs))
            {
                using (SqlCommand cmd = new SqlCommand(String.Format(sql,args),sc))
                {
                    sc.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region 存储过程
        /// <summary>
        /// 使用SQL命令查询一个数据表
        /// </summary>
        /// <param name="name">存储过程名称</param>
        /// <param name="args">存储过程参数词典 参数名,参数值</param>
        /// <returns>查询结果 数据表</returns>
        public DataTable GetDataTable(string name, Dictionary<string,object> args)
        {
            using (SqlConnection sc = new SqlConnection(scs))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(name, sc))
                {
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    foreach(KeyValuePair<string,object> kv in args)
                    {
                        sda.SelectCommand.Parameters.AddWithValue(kv.Key, kv.Value);
                    }
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
            }
        }

        /// <summary>
        /// 使用SQL命令查询查询多个数据表
        /// </summary>
        /// <param name="name">存储过程名称</param>
        /// <param name="args">存储过程参数词典 参数名,参数值</param>
        /// <returns>查询结果 数据集</returns>
        public DataSet GetDataSet(string name, Dictionary<string, object> args)
        {
            using (SqlConnection sc = new SqlConnection(scs))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(name, sc))
                {
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    foreach(KeyValuePair<string,object> kv in args)
                    {
                        sda.SelectCommand.Parameters.AddWithValue(kv.Key, kv.Value);
                    }
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    return ds;
                }
            }
        }

        /// <summary>
        /// 使用SQL命令查询一个标量值
        /// <param name="name">存储过程名称</param>
        /// <param name="args">存储过程参数词典 参数名,参数值</param>
        /// <returns>查询结果 标量值</returns>
        public object GetValue(string name, Dictionary<string, object> args)
        {
            using (SqlConnection sc = new SqlConnection(scs))
            {
                using (SqlCommand cmd = new SqlCommand(name, sc))
                {
                    sc.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (KeyValuePair<string, object> kv in args)
                    {
                        cmd.Parameters.AddWithValue(kv.Key, kv.Value);
                    }
                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// 执行SQL命令
        /// <param name="name">存储过程名称</param>
        /// <param name="args">存储过程参数词典 参数名,参数值</param>
        /// <returns>受影响条数</returns>
        public int Execute(string name, Dictionary<string, object> args)
        {
            using (SqlConnection sc = new SqlConnection(scs))
            {
                using (SqlCommand cmd = new SqlCommand(name, sc))
                {
                    sc.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (KeyValuePair<string, object> kv in args)
                    {
                        cmd.Parameters.AddWithValue(kv.Key, kv.Value);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion
        #endregion
    }
}
