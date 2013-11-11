/****************************************************************************
 * 
 * 
 * Author : Dayang Sun
 * 
 * File   : AS400DB.cs
 * 
 * Date   : 5/10/2010 1:44:41 PM
 * 
 * 
 * ***************************************************************************/


using System;
using System.Configuration;
using System.Data.Odbc;

namespace DAL.AS400
{

    /// <summary>
    /// Summary description for AS400DB
    /// </summary>
    public static class AS400DB
    {

        #region Properties
        private static String _connectionString;
        public static String ConnectionString
        {
            get
            {
                if (_connectionString == null)
                {
                    _connectionString = ConfigurationManager.ConnectionStrings["AS400ConnectionString"].ConnectionString;
                }
                return _connectionString;


            }
        }
        #endregion

        #region Class Methods
        //public static OdbcDataReader GetDataReaderData(string query)
        //{
        //    OdbcDataReader result = null;
        //    using (OdbcConnection AS400connection = new OdbcConnection(ConnectionString))
        //    {
        //        AS400connection.Open();
        //        result = ExecuteReader(AS400connection, query);
        //    }
        //    return result;
        //}

        public static OdbcDataReader ExecuteReader(OdbcConnection connection, string query)
        {
            if (connection == null)
                throw new ArgumentNullException("AS400ConnectionString");

            OdbcDataReader result = null;
            OdbcCommand AS400command = new OdbcCommand(query);
            AS400command.Connection = connection;
            AS400command.CommandTimeout = 0;
            result = AS400command.ExecuteReader();
            return result;

        }


        #endregion


    }
}