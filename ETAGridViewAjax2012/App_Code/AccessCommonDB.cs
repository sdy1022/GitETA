using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;



public static class AccessCommonDB
   {
                //private static String _connectionString;
                //public static String ETAConnectionString
                //{
                //    get
                //    {
                //        if (_connectionString == null)
                //        {
                //            _connectionString = "Provider=Microsoft.JET.OLEDB.4.0;data source=C:\\TOTIEM.MDB";
                //        }
                //        return _connectionString;
                //    }
                //}

               

                public static DataTable GetTableValuesByName(string tablename)
                {

                    DataSet result = null;
          
                    SetAccessHelperConnection();
                    string sql = string.Format("SELECT  * FROM {0}", tablename);
                    result = AccessHelper.ExecuteTextRet(sql, tablename);
                    
                                       

                    return result.Tables[0];




                }

                private static void SetAccessHelperConnection()
                {
                    if (string.IsNullOrEmpty(AccessHelper.ConnectionString))
                    {
                        AccessHelper.ConnectionString = AccessHelper.GetConnectionStringFromConfigFile();
                    }
                }




 }
