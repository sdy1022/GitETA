using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using System.Data;
using System.Reflection;

namespace DAL.AS400
{
    public static class A004PFunction
    {

        public static List<AS400A004PEntity> GetOrderListData(string inputdate)
        {
            // ordernumber ="TH57620";
            List<AS400A004PEntity> resultall = new List<AS400A004PEntity>();
            int j = 0;

            IDataReader reader = null;
            using (OdbcConnection AS400connection = new OdbcConnection(AS400DB.ConnectionString))
            {
                AS400connection.Open();
                //string oldcondquery = "SELECT * FROM  LIBDF7.A004P WHERE  (ADODN = '" + ordernumber + "')";
                //string condquery = "select LIBDF7.A021P.AWPDC as TFC,LIBDF7.A004P.* FROM LIBDF7.A021P, LIBDF7.A004P WHERE(LIBDF7.A004P.ADODN ='" + ordernumber + "') And LIBDF7.A021P.AWMDN = LIBDF7.A004P.ADMDN ";
                string condquery = "select LIBDF7.A021P.AWPDC as TFC,LIBDF7.A004P.* FROM LIBDF7.A021P, LIBDF7.A004P WHERE(LIBDF7.A004P.ADCHD ='" + inputdate + "' OR LIBDF7.A004P.ADRCD ='" + inputdate + "') And LIBDF7.A021P.AWMDN = LIBDF7.A004P.ADMDN ";

                //                //select LIBDF7.A021P.AWPDC as TFC ,LIBDF7.A004P.* 
                //FROM    LIBDF7.A021P, LIBDF7.A004P
                //WHERE  (LIBDF7.A004P.ADODN = 'TH57620') And
                //LIBDF7.A021P.AWMDN = LIBDF7.A004P.ADMDN
                // For first result
                reader = (IDataReader)AS400DB.ExecuteReader(AS400connection, condquery);

                while (reader.Read())
                {// // ADODN, ADATT, ADMDN, ADMST, ADEON, ADAON
                    j++;
                    AS400A004PEntity result = new AS400A004PEntity();
                    result.ADODN = GetIReaderValueByName(reader, "ADODN");
                    result.ADATT = GetIReaderValueByName(reader, "ADATT");
                    result.ADMDN = GetIReaderValueByName(reader, "ADMDN");
                    result.ADEON = GetIReaderValueByName(reader, "ADEON");

                    result.ADAON = GetIReaderValueByName(reader, "ADAON");
                    result.ADMST = GetIReaderValueByName(reader, "ADMST");
                    if (result.ADMST.Trim() == "V")
                    {
                        result.ADMST = "STD";
                    }

                    result.TFC = GetIReaderValueByName(reader, "TFC");
                    ////  result.WholeOptions = GetIReaderValueByName(reader, "TFC")+

                    try
                    {
                        for (int i = 1; i <= 40; i++)
                        {
                            if (!string.IsNullOrEmpty(reader["ADOP" + i.ToString("00")].ToString().Trim()))
                            {

                                result.WholeOptions += reader["ADOP" + i.ToString("00")].ToString().Trim() + "|";

                            }
                        }

                        for (int i = 1; i <= 10; i++)
                        {
                            if (!string.IsNullOrEmpty(reader["ADSO" + i.ToString("00")].ToString().Trim()))
                            {

                                result.WholeOptions += reader["ADSO" + i.ToString("00")].ToString().Trim() + "|";
                                result.WholeSalesCode += reader["ADSO" + i.ToString("00")].ToString().Trim() + "|";

                            }
                        }
                        if (!string.IsNullOrEmpty(result.WholeOptions))
                        {
                            result.WholeOptions = result.WholeOptions.Substring(0, result.WholeOptions.Length - 1);
                        }

                        if (!string.IsNullOrEmpty(result.WholeSalesCode)) 
                        {
                            result.WholeSalesCode = result.WholeSalesCode.Substring(0, result.WholeSalesCode.Length - 1);
                        }
                    }
                    catch (Exception err)
                    {
                       

                    }
                    //// remove last "}"
                    resultall.Add(result);

                }


            }

            return resultall;

        }

        public static List<string> GetNewChangedOrderList(string inputdate)
        {

            List<string> result = new List<string>();

            IDataReader reader = null;
            using (OdbcConnection AS400connection = new OdbcConnection(AS400DB.ConnectionString))
            {
                AS400connection.Open();
               // string condquery = "SELECT  ADODN FROM  LIBDF7.A004P WHERE  (ADCHD = '20110331') OR (ADRCD='20110331')";

                string condquery = "SELECT  ADODN FROM  LIBDF7.A004P WHERE  (ADCHD = '"+inputdate+"') OR (ADRCD='"+inputdate +"')";

                reader = (IDataReader)AS400DB.ExecuteReader(AS400connection, condquery);

                while (reader.Read())
                {
                    result.Add(GetIReaderValueByName(reader, "ADODN"));

                }
            }

            return result;

        }
     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ordernumber"></param>
        /// <returns></returns>
        public static AS400A004PEntity GetOrderData(string ordernumber)
        {
            // ordernumber ="TH57620";
            AS400A004PEntity result = null;


            IDataReader reader = null;
            using (OdbcConnection AS400connection = new OdbcConnection(AS400DB.ConnectionString))
            {
                AS400connection.Open();
                string oldcondquery = "SELECT * FROM  LIBDF7.A004P WHERE  (ADODN = '" + ordernumber + "')";
                string condquery = "select LIBDF7.A021P.AWPDC as TFC,LIBDF7.A004P.* FROM LIBDF7.A021P, LIBDF7.A004P WHERE(LIBDF7.A004P.ADODN ='" + ordernumber + "') And LIBDF7.A021P.AWMDN = LIBDF7.A004P.ADMDN ";
               // string newquery = "SELECT  LIBDF7.A021P.AWPDC AS TFC, LIBDF7.S032L01.* FROM  LIBDF7.A021P, LIBDF7.S032L01 WHERE  LIBDF7.A021P.AWMDN = LIBDF7.S032L01.BDMDN AND (LIBDF7.S032L01.BDPRC = '')"; 
                // querystring = "select * from LIBDF7.S032P where LIBDF7.S032P.BDODN='" & orderno & "' order by LIBDF7.S032P.BDDT desc,LIBDF7.S032P.BDTM desc fetch first 1 row only"
                condquery = "select * from LIBDF7.A004P where LIBDF7.A004P.ADODN='" + ordernumber + "' fetch first 1 row only";

                //                //select LIBDF7.A021P.AWPDC as TFC ,LIBDF7.A004P.* 
                //FROM    LIBDF7.A021P, LIBDF7.A004P
                //WHERE  (LIBDF7.A004P.ADODN = 'TH57620') And
                //LIBDF7.A021P.AWMDN = LIBDF7.A004P.ADMDN
                // For first result
                reader = (IDataReader)AS400DB.ExecuteReader(AS400connection, condquery);

                while (reader.Read())
                {// // ADODN, ADATT, ADMDN, ADMST, ADEON, ADAON
                    result = new AS400A004PEntity();
                    result.ADODN = GetIReaderValueByName(reader, "ADODN");
                    result.ADATT = GetIReaderValueByName(reader, "ADATT");
                    result.ADMDN = GetIReaderValueByName(reader, "ADMDN");
                    result.ADEON = GetIReaderValueByName(reader, "ADEON");

                    result.ADAON = GetIReaderValueByName(reader, "ADAON");
                    result.ADMST = GetIReaderValueByName(reader, "ADMST");
                    if (result.ADMST.Trim() == "V")
                    {
                        result.ADMST = "STD";
                    }

                    result.TFC = GetIReaderValueByName(reader, "ADPDC");
                    //  result.WholeOptions = GetIReaderValueByName(reader, "TFC")+
                    for (int i = 1; i <= 40; i++)
                    {
                        if (!string.IsNullOrEmpty(reader["ADOP" + i.ToString("00")].ToString().Trim()))
                        {

                            result.WholeOptions += reader["ADOP" + i.ToString("00")].ToString().Trim() + "|";

                        }
                    }

                    for (int i = 1; i <= 10; i++)
                    {
                        if (!string.IsNullOrEmpty(reader["ADSO" + i.ToString("00")].ToString().Trim()))
                        {

                            result.WholeOptions += reader["ADSO" + i.ToString("00")].ToString().Trim() + "|";
                            result.WholeSalesCode += reader["ADSO" + i.ToString("00")].ToString().Trim() + "|";

                        }
                    }
                    // remove last "}"
                    if (!string.IsNullOrEmpty(result.WholeOptions))
                    {
                        result.WholeOptions = result.WholeOptions.Substring(0, result.WholeOptions.Length - 1);
                    }

                    if (!string.IsNullOrEmpty(result.WholeSalesCode))
                    {
                        result.WholeSalesCode = result.WholeSalesCode.Substring(0, result.WholeSalesCode.Length - 1);
                    } 
                    break;
                }


            }

            return result;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ordernumber"></param>
        /// <returns></returns>
        public static AS400A004PEntity GetOrderDataS032P(string ordernumber)
        {
            // ordernumber ="TH57620";
            AS400A004PEntity result = null;


            IDataReader reader = null;
            using (OdbcConnection AS400connection = new OdbcConnection(AS400DB.ConnectionString))
            {
                AS400connection.Open();
               // string oldcondquery = "SELECT * FROM  LIBDF7.A004P WHERE  (ADODN = '" + ordernumber + "')";
               // string condquery =
                    //"select LIBDF7.A021P.AWPDC as TFC,LIBDF7.A004P.* FROM LIBDF7.A021P, LIBDF7.A004P WHERE(LIBDF7.A004P.ADODN ='" + ordernumber + "') And LIBDF7.A021P.AWMDN = LIBDF7.A004P.ADMDN ";
                // string newquery = "SELECT  LIBDF7.A021P.AWPDC AS TFC, LIBDF7.S032L01.* FROM  LIBDF7.A021P, LIBDF7.S032L01 WHERE  LIBDF7.A021P.AWMDN = LIBDF7.S032L01.BDMDN AND (LIBDF7.S032L01.BDPRC = '')"; 
                string condquery = "select * from LIBDF7.S032P where LIBDF7.S032P.BDODN='" + ordernumber +
                                   "' order by LIBDF7.S032P.BDDT desc,LIBDF7.S032P.BDTM desc fetch first 1 row only";


                //                //select LIBDF7.A021P.AWPDC as TFC ,LIBDF7.A004P.* 
                //FROM    LIBDF7.A021P, LIBDF7.A004P
                //WHERE  (LIBDF7.A004P.ADODN = 'TH57620') And
                //LIBDF7.A021P.AWMDN = LIBDF7.A004P.ADMDN
                // For first result
                reader = (IDataReader)AS400DB.ExecuteReader(AS400connection, condquery);

                while (reader.Read())
                {// // ADODN, ADATT, ADMDN, ADMST, ADEON, ADAON
                    result = new AS400A004PEntity();
                    result.ADODN = GetIReaderValueByName(reader, "BDODN");
                    result.ADATT = GetIReaderValueByName(reader, "BDATT");
                    result.ADMDN = GetIReaderValueByName(reader, "BDMDN");
                    result.ADEON = GetIReaderValueByName(reader, "BDEON");

                    result.ADAON = GetIReaderValueByName(reader, "BDAON");
                    result.ADMST = GetIReaderValueByName(reader, "BDMST");
                    if (result.ADMST.Trim() == "V")
                    {
                        result.ADMST = "STD";
                    }

                    result.TFC = GetIReaderValueByName(reader, "BDPDC");
                    //  result.WholeOptions = GetIReaderValueByName(reader, "TFC")+
                    for (int i = 1; i <= 40; i++)
                    {
                        if (!string.IsNullOrEmpty(reader["BDOP" + i.ToString("00")].ToString().Trim()))
                        {

                            result.WholeOptions += reader["BDOP" + i.ToString("00")].ToString().Trim() + "|";

                        }
                    }

                    for (int i = 1; i <= 10; i++)
                    {
                        if (!string.IsNullOrEmpty(reader["BDSO" + i.ToString("00")].ToString().Trim()))
                        {

                            result.WholeOptions += reader["BDSO" + i.ToString("00")].ToString().Trim() + "|";
                            result.WholeSalesCode += reader["BDSO" + i.ToString("00")].ToString().Trim() + "|";

                        }
                    }

                    for (int i = 1; i <= 10; i++)
                    {
                        if (!string.IsNullOrEmpty(reader["BDEX" + i.ToString("00")].ToString().Trim()))
                        {

                            result.WholeOptions += reader["BDEX" + i.ToString("00")].ToString().Trim() + "|";
                            result.WholeSalesCode += reader["BDEX" + i.ToString("00")].ToString().Trim() + "|";

                        }
                    }
                    // remove last "}"
                    if (!string.IsNullOrEmpty(result.WholeOptions))
                    {
                        result.WholeOptions = result.WholeOptions.Substring(0, result.WholeOptions.Length - 1);
                    }

                    if (!string.IsNullOrEmpty(result.WholeSalesCode))
                    {
                        result.WholeSalesCode = result.WholeSalesCode.Substring(0, result.WholeSalesCode.Length - 1);
                    }
                    break;
                }


            }

            return result;

        }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
        public static List<string> GetTFCList()
        {
            List<string> result = new List<string>();

            IDataReader reader = null;
            using (OdbcConnection AS400connection = new OdbcConnection(AS400DB.ConnectionString))
            {
                AS400connection.Open();
                string condquery = "SELECT DISTINCT AWPDC FROM LIBDF7.A021L01 ORDER BY AWPDC ";


                reader = (IDataReader)AS400DB.ExecuteReader(AS400connection, condquery);

                while (reader.Read())
                {
                    result.Add(GetIReaderValueByName(reader, "AWPDC"));

                }
            }

            return result;

        }

        public static List<string> GetMastTypeList()
        {
            List<string> result = new List<string>();

            IDataReader reader = null;
            using (OdbcConnection AS400connection = new OdbcConnection(AS400DB.ConnectionString))
            {
                AS400connection.Open();
                string condquery = "SELECT DISTINCT AYMST FROM LIBDF7.A023L03 ORDER BY AYMST ";


                //                //select LIBDF7.A021P.AWPDC as TFC ,LIBDF7.A004P.* 
                //FROM    LIBDF7.A021P, LIBDF7.A004P
                //WHERE  (LIBDF7.A004P.ADODN = 'TH57620') And
                //LIBDF7.A021P.AWMDN = LIBDF7.A004P.ADMDN
                // For first result
                reader = (IDataReader)AS400DB.ExecuteReader(AS400connection, condquery);

                while (reader.Read())
                {
                    result.Add(GetIReaderValueByName(reader, "AYMST"));

                }
            }

            return result;

        }

        public static List<string> GetAttHostingList()
        {
            List<string> result = new List<string>();

            IDataReader reader = null;
            using (OdbcConnection AS400connection = new OdbcConnection(AS400DB.ConnectionString))
            {
                AS400connection.Open();
                string condquery = "SELECT DISTINCT AXATT FROM  LIBDF7.A022L01 ORDER BY AXATT ";


                //                //select LIBDF7.A021P.AWPDC as TFC ,LIBDF7.A004P.* 
                //FROM    LIBDF7.A021P, LIBDF7.A004P
                //WHERE  (LIBDF7.A004P.ADODN = 'TH57620') And
                //LIBDF7.A021P.AWMDN = LIBDF7.A004P.ADMDN
                // For first result
                reader = (IDataReader)AS400DB.ExecuteReader(AS400connection, condquery);

                while (reader.Read())
                {
                    result.Add(GetIReaderValueByName(reader, "AXATT"));

                }
            }

            return result;

        }

        public static List<string> GetModel()
        {
            List<string> result = new List<string>();

            IDataReader reader = null;
            using (OdbcConnection AS400connection = new OdbcConnection(AS400DB.ConnectionString))
            {
                AS400connection.Open();
                string condquery = "SELECT DISTINCT AWMDN FROM  LIBDF7.A021L01 order by AWMDN";


                //                //select LIBDF7.A021P.AWPDC as TFC ,LIBDF7.A004P.* 
                //FROM    LIBDF7.A021P, LIBDF7.A004P
                //WHERE  (LIBDF7.A004P.ADODN = 'TH57620') And
                //LIBDF7.A021P.AWMDN = LIBDF7.A004P.ADMDN
                // For first result
                reader = (IDataReader)AS400DB.ExecuteReader(AS400connection, condquery);

                while (reader.Read())
                {
                    result.Add(GetIReaderValueByName(reader, "AWMDN"));

                }
            }

            return result;

        }

        public static String GetTFCByModel(string model)
        {
            string result = string.Empty;


            return result;


        }


        private static string GetIReaderValueByName(IDataReader reader, string name)
        {
            return reader[name].ToString().Trim();
        }
    }
}
