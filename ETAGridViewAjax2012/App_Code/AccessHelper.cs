using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

    /**//**//**//// <summary>
    /// AccessHelper 的摘要说明。
    /// </summary>
    public class AccessHelper
    {
        private AccessHelper()
        {
            //静态构造函数，表示该类不可以被实例化
            //所以的成员都是static类型的

           
        }

        private static string m_connection = "";
        
        /**//**//**//// <summary>
        /// ConnectionString的属性
        /// </summary>
        public static string ConnectionString
        {
            set
            {
                m_connection = value;
            }
            get
            {
                return m_connection;
            }
        }

        /**//**//**//// <summary>
        /// 判断OleDbConnection是否已经连接
        /// </summary>
        /// <returns>true 表示OleDbConnection已经连接，或者设置连接成功； 
        /// false 表示OleDbConnection尝试连接失败</returns>
        public static bool IsConnected()
        {
            OleDbConnection conn = new OleDbConnection(m_connection);
            
            try
            {
                conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }
    

        /**//**//**//// <summary>
        /// 执行一条Sql命令
        /// </summary>
        /// <param name="cmdText">Sql语句</param>
        /// <returns>表示影响的行数</returns>
        public static int ExecuteText(string cmdText)
        {
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                PrepareCommand(cmd,CommandType.Text,cmdText);
                try
                {
                    return cmd.ExecuteNonQuery();
                }

                catch (OleDbException e)
                {
                    throw (new Exception(e.Message));;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DisposeCmd(cmd);
            }
        }

        /**//**//**//// <summary>
        /// 执行一条Sql命令（ExecuteScalar），返回一个结果值（int, string, float等类型）。如果不存在记录，返回为null。
        /// </summary>
        /// <param name="cmdText">Sql语句</param>
        /// <returns>返回值（int, string, float等类型）。如果不存在记录，返回为null。</returns>
        public static object ExecuteTextRet(string cmdText)
        {
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                PrepareCommand(cmd, CommandType.Text, cmdText);
                try
                {
                    return cmd.ExecuteScalar();
                }
                catch (OleDbException e)
                {
                    throw (new Exception(e.Message));
                }
            }            
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DisposeCmd(cmd);
            }
        }

        /**//**//**//// <summary>
        /// 执行一个Sql命令，返回一个DataSet结果集。
        /// </summary>
        /// <param name="cmdText">Sql语句</param>
        /// <param name="TableName">Fill入DataSet中的表名</param>
        /// <returns>DataSet结果集</returns>
        public static DataSet ExecuteTextRet(string cmdText, string TableName)
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter adp = new OleDbDataAdapter();
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                PrepareCommand(cmd, CommandType.Text, cmdText);
                try
                {
                    adp.SelectCommand = cmd;
                    adp.Fill(ds,TableName);
                    return ds;
                }
                catch (OleDbException e)
                {
                    throw (new Exception(e.Message));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DisposeCmd(cmd);
            }

        }    

        /**//**//**//// <summary>
        /// 把一个DataSet更新入数据库（可以修改，添加，删除）
        /// 但是，这个表的设计中必须要有主键
        /// </summary>
        /// <param name="cmdText">select语句，和要更新的表对应</param>
        /// <param name="ds">要更新入数据库的DataSet</param>
        /// <param name="TableName">DataSet中的源表名</param>
        /// <returns>影响的行数</returns>
        public static int ExecuteUpdate(string cmdText,DataSet ds,string TableName)
        {
            OleDbDataAdapter adp = new OleDbDataAdapter();
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                PrepareCommand(cmd, CommandType.Text, cmdText);
                adp.SelectCommand = cmd;
                OleDbCommandBuilder builder = new OleDbCommandBuilder(adp);
                return adp.Update(ds,TableName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DisposeCmd(cmd);
                adp.Dispose();
            }
        }

        /**//**//**//// <summary>
        /// 设置OleDbCommand的参数
        /// </summary>
        /// <param name="cmd">OleDbCommand对象</param>
        /// <param name="cmdType">类型</param>
        /// <param name="cmdText">Sql语句</param>        
        private static void PrepareCommand(OleDbCommand cmd, CommandType cmdType, string cmdText)
        {    
            OleDbConnection    conn = new OleDbConnection(m_connection);
            try
            {
                conn.Open();
    
                if (cmd != null)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = cmdText;
                    cmd.CommandType = cmdType;
                }                
            }

            catch (OleDbException e)
            {
                throw (new Exception(e.Message));
            }
        }
        

        /**//**//**//// <summary>
        /// 处理OleDbCommand，关闭连接，并且释放内存。
        /// </summary>
        /// <param name="cmd">要处理的OleDbCommand对象</param>
        private static void DisposeCmd(OleDbCommand cmd)
        {
            if (cmd.Connection != null)
            {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
            cmd.Dispose();
        }

      //  填加段填加段
       #region 填加段
        /**//**//**//// <summary>
        /// 根据app.config获取连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionStringFromConfigFile()
        {
            DataSet ds = new DataSet();
            ds.ReadXml(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);            
            string result = ConfigurationSettings.AppSettings["DataSource"];
            ds.Clear();
            ds.Dispose();

            result = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + result;        
            return result;
        }

        /**//**//**//// <summary>
        /// 根据app.config获取数据源的信息
        /// </summary>
        /// <returns></returns>
        public static string GetDataSourceFromConfigFile()
        {
            DataSet ds = new DataSet();
            ds.ReadXml(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);            
            string result = ds.Tables["appSettings"].Rows[0]["DateSource"].ToString();
            ds.Clear();
            ds.Dispose();
            
            return result;
            
        }

        /**//**//**//// <summary>
        /// 设置app.config的数据源信息
        /// </summary>
        /// <param name="dataSource"></param>
        public static void SetDataSourceToConfigFile(string dataSource)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);            
            ds.Tables["appSettings"].Rows[0]["DateSource"]=dataSource;
            ds.AcceptChanges();
            ds.WriteXml(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            ds.Clear();
            ds.Dispose();
        }

        /**//**//**//// <summary>
        /// 测试能否连接(打开并关闭)
        /// </summary>
        /// <returns></returns>
        public static bool IsCanConnected()
        {
            OleDbConnection conn = new OleDbConnection(m_connection);
            bool result = false;
            try
            {
                conn.Open();
                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        #endregion
    }

