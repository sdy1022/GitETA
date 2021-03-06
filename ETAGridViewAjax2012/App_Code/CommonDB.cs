using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web.Caching;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.VisualBasic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

/// <summary>
/// 
/// </summary>
public static class CommonDB
{


    //private static DataSet _categorydataset;
    //public static DataSet CategoryDataSet
    //{
    //    get
    //    {
    //        if (_categorydataset == null)
    //        {
    //            object[] objParams = { 0 };
    //            string sql = string.Format("SELECT ltrim(rtrim(Category)) as Category FROM ETA.dbo.Categories ORDER BY Category");
    //            _categorydataset = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);
    //        }

    //        return _categorydataset;
    //    }
    //}


    /// <summary>
    /// 
    /// </summary>
    //private static DataSet _standardpartcodedataset;
    //public static DataSet StandardPartcodeDataSet
    //{
    //    get
    //    {
    //        if (_standardpartcodedataset == null)
    //        {
    //            object[] objParams = { 0 };
    //            string sql = string.Format("Select distinct(Model) FROM ETA.dbo.viewStandardParts ORDER BY Model");
    //            _standardpartcodedataset = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);
    //        }

    //        return _standardpartcodedataset;
    //    }
    //}

    /// <summary>
    /// 
    /// </summary>
    private static String _connectionString;
    public static String ETAConnectionString
    {
        get
        {
            if (_connectionString == null)
            {
                _connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            }
            return _connectionString;


        }
    }

    private static String _stconnectionString;
    public static String STConnectionString
    {
        get
        {
            if (_stconnectionString == null)
            {
                _stconnectionString = ConfigurationManager.ConnectionStrings["STConnectionString"].ConnectionString;
            }
            return _stconnectionString;


        }
    }



    /// <summary>
    /// ETAEs the delete.
    /// Delete Table with transaction
    /// </summary>
    /// <param name="trans">The trans.</param>
    /// <param name="tablename">The tablename.</param>
    public static void ETATableDelete(SqlTransaction trans, string tablename)
    {
        string sql = string.Format("Delete from {0}", tablename);
        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);
    }



    #region  ac_History_tiem_parts
    public static DataSet GetInfoFromHistoryParts(string partno)
    {
        DataSet result = null;
        object[] objParams = { 0 };
        //SELECT     * FROM  ac_History_Tiem_Parts WHERE  ac_History_Tiem_Parts.PartNo  like +@partno+'%' AND ac_History_Tiem_Parts.DATE = @date 
        string sql = string.Format("SELECT   Date , Partno FROM  ac_History_Tiem_Parts WHERE  ac_History_Tiem_Parts.PartNo  like '{0}%'  order by Date ", partno);
        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);
        return result;
    }

    #endregion

    #region FormC

    /// <summary>
    /// Gets all parts by module.
    /// </summary>
    /// <param name="package">The package.</param>
    /// <param name="module">The module.</param>
    /// <returns></returns>
    public static DataSet GetAllPartsByModule(string package, string module)
    {
        DataSet result = null;
        object[] objParams = { 0 };

        string sql = string.Format("SELECT  LTRIM(RTRIM(CategoryAddress)) AS CategoryAddress ,KeyNumber ,[key] ,AssyCode , Treatment ,PartCode ,PageCode ,Description,  viewFormCItems.Citemid,PartNumber FROM    viewFormCItems INNER JOIN viewPartsList ON viewFormCItems.CItemId = viewPartsList.CItemId WHERE   ( viewFormCItems.DesignNumber = '{0}' ) AND ( viewFormCItems.ModuleLocation = '{1}' ) OrDER BY CategoryAddress ,viewFormCItems.CItemId", package, module);

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);
        return result;
    }



    /// <summary>
    /// Gets the part number info from form C.
    /// </summary>
    /// <param name="pagecode">The pagecode.</param>
    /// <param name="groupno">The groupno.</param>
    /// <param name="partcode">The partcode.</param>
    /// <returns></returns>
    public static DataSet GetPartNumberInfoFromFormC(string pagecode, string groupno, string partcode)
    {
        DataSet result = null;
        object[] objParams = { 0 };

        string sql = string.Format("select top 1 partcode from ETA.dbo.viewFormCItems where pagecode='{0}' and partcode='{1}' and replace(categoryAddress,' ','' )  like '{2}%'", pagecode, partcode, groupno);
        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);
        return result;
    }


    public static DataSet GetCurrentRevByPackage(string package)
    {
        DataSet result = null;
        object[] objParams = { 0 };

        string sql = "SELECT Rev FROM  eci.dbo.Eci WHERE DesignNumber='" + package + "' ORDER BY Rev DESC";
        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);


        return result;
    }


    /// <summary>
    /// Gets the header info.
    /// </summary>
    /// <param name="package">The package.</param>
    /// <returns></returns>
    public static DataSet GetHeaderInfo(string package)
    {
        DataSet result = null;
        object[] objParams = { 0 };

        string sql = string.Format("SELECT Model,Mast,Attachment FROM ETA.dbo.viewHeaderInfo WHERE DesignNumber='{0}'", package);

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);


        return result;
    }
    /// <summary>
    /// Add on 10/02/2013 for TSD FormC Update
    /// </summary>
    /// <param name="cid"></param>
    /// <returns></returns>
    public static string UpdateTSDFormCWithTransactionForEdit(string cid, string aid, string addtfc, string finaladdindex, string deltfc, string delindex, string vDescription, string fromeci, string toeci, string vkey)
    {
        string ReturnValue = null;

        // get modelcode


        string sql = string.Format(
            "UPDATE  ETA.dbo.[03_TSD] SET AItemId={8}, ADD_TFC ='{1}', ADD_INDEX ='{2}', DEL_TFC ='{3}', DEL_INDEX ='{4}', Description ='{5}', FROM_ECI ='{6}', TO_ECI ='{7}' where ID={0}", cid, addtfc, finaladdindex, deltfc, delindex, vDescription, fromeci, toeci, vkey);

        using (SqlConnection conn = new SqlConnection(ETAConnectionString))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {


                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                    trans.Commit();
                    ReturnValue = "Success";
                }//end try
                catch (Exception err)
                {
                    trans.Rollback();
                    ReturnValue = err.Message;
                }
            }
        }// end using sqlconnection
        return ReturnValue;

    }

    public static string InsertFormCWithTransactionForEdit(string formcitemid, string formcpairitemid, string ecimode, string ecinumber, string sinital, string package, bool vLog, string vRev, string vCategory, string vKey, string vAssyCode, string vTreatment, string vPartCode, string vPageCode, string vDescription, string ModuleValue, string EciAcid, string AitemId, string pairidlist)
    {
        string ReturnValue = null;
        string sql = null;
        using (SqlConnection conn = new SqlConnection(ETAConnectionString))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    string keya = null;
                    DataSet ds = null;


                    sql = "SELECT Rev FROM  eci.dbo.Eci WHERE DesignNumber='" + package + "' ORDER BY Rev DESC";
                    ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);

                    string currentrev = null;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        currentrev = ds.Tables[0].Rows[0][0].ToString().Trim();
                    }


                    //''**Get KeyA and increment
                    DataTable dtkeya = GetKeyAByEciNumber(ecinumber).Tables[0];

                    if (dtkeya.Rows.Count > 0)
                    {
                        if (string.IsNullOrEmpty(dtkeya.Rows[0][0].ToString().Trim()))
                        {
                            keya = "A";
                        }
                        else
                        {

                            keya = CommonTool.Chr(CommonTool.Asc(dtkeya.Rows[0][0].ToString().Trim()) + 1);
                        }

                    }

                    else
                    {
                        keya = "A";
                    }



                    //'  If ECI Mode, Log data

                    // '**Add records to ChangeLogC if ECI mode


                    if (ecimode == "on" && vLog)
                    {


                        //'**Check Change Log for existing log to item

                        sql = "SELECT Rev FROM ETA.dbo.ChangeLogC WHERE CitemId='" + formcitemid + "' AND Rev='" + currentrev + "'";

                        ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);

                        if (ds.Tables[0].Rows.Count == 0)
                        {

                            //'**Log change
                            sql = "INSERT ETA.dbo.ChangeLogC (CitemId,Rev) VALUES (" + formcitemid + ",'" + currentrev + "')";

                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                        }



                        // '**Get ECI header info
                        sql = "SELECT EciNumber FROM ETA.dbo.EciHeadParts WHERE EciNumber='" + ecinumber + "'";

                        ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            // insert '**Create Header
                            sql = "INSERT ETA.dbo.EciHeadC (DesignNumber,EciNumber,RevInitials) VALUES ('" + package + "','" + ecinumber + "','" + sinital + "')";
                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);
                        }


                        //'**Get old Form A and Form C items information

                        sql = string.Format("SELECT * FROM ETA.dbo.FormCitems as c JOIN ETA.dbo.FormAitems as a ON c.AitemId=a.AitemId WHERE CitemId={0}", formcitemid);
                        DataSet dsoldA = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);

                        string ChangeA = null;
                        string KeyA = null;
                        string MajorA = null;
                        string MinorA = null;
                        string CommentA = null;

                        DataSet dsold = null;
                        //'**Form A or ECI data source
                        if (!string.IsNullOrEmpty(EciAcid))
                        {
                            //'**find item in ECI form to assign this to
                            sql = string.Format("SELECT ChangeA,KeyA,MajorA,MinorA,CommentA FROM eci.dbo.EciACitems WHERE EciAcId={0}", EciAcid);

                            dsold = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);
                            ChangeA = dsold.Tables[0].Rows[0]["ChangeA"].ToString();
                            KeyA = dsold.Tables[0].Rows[0]["KeyA"].ToString();
                            MajorA = dsold.Tables[0].Rows[0]["MajorA"].ToString();
                            MinorA = dsold.Tables[0].Rows[0]["MinorA"].ToString();
                            CommentA = dsold.Tables[0].Rows[0]["CommentA"].ToString().Replace("'", "''");

                        }
                        else
                        {
                            //	'**Get data from Package

                            ChangeA = "No Change";
                            MajorA = dsoldA.Tables[0].Rows[0]["Major"].ToString();
                            MinorA = dsoldA.Tables[0].Rows[0]["Minor"].ToString();
                            CommentA = dsoldA.Tables[0].Rows[0]["Comment"].ToString().Replace("'", "''");
                        }


                        string AssyC = dsoldA.Tables[0].Rows[0]["CategoryAddress"].ToString().Substring(0, 3);

                        string PartCodeC = null;
                        string PageCodeC = null;
                        int assycint = 0;
                        if (Int32.TryParse(dsoldA.Tables[0].Rows[0]["CategoryAddress"].ToString(), out assycint))
                        {

                            PartCodeC = package;
                            PageCodeC = string.Format("{0} {1}", dsoldA.Tables[0].Rows[0]["CategoryAddress"].ToString(), dsoldA.Tables[0].Rows[0]["PartCode"].ToString());
                        }
                        else
                        {
                            PartCodeC = dsoldA.Tables[0].Rows[0]["PartCode"].ToString();
                            PageCodeC = dsoldA.Tables[0].Rows[0]["PageCode"].ToString();

                        }

                        string Description = dsoldA.Tables[0].Rows[0]["Description"].ToString();
                        //  string KeyC = "Z8";
                        string ChangeC = "Deleted";

                        //'**Log Old Data to EciACitems




                        sql = "INSERT eci.dbo.EciACitems (EciNumber,ChangeA,KeyA,MajorA,MinorA,CommentA,KeyC,ChangeC,AssyC,PartCodeC,PageCodeC,DescriptionC) " + "VALUES ('" + ecinumber +
                       "','" + ChangeA +
                       "','" + KeyA +
                       "','" + MajorA +
                       "','" + MinorA +
                       "','" + CommentA + "'," +
                       "'Z8','" + ChangeC + "','" + AssyC +
                       "','" + PartCodeC +
                       "','" + PageCodeC +
                       "','" + Description + "')";

                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                        //KeyC="Z9";
                        ChangeC = "New Adoption";


                        //'**Reformat data, if needed.

                        string NewPartCodeC = null;
                        string NewPageCodeC = null;

                        if (Int32.TryParse(vAssyCode, out assycint))
                        {

                            NewPartCodeC = package;
                            NewPageCodeC = string.Format("{0} {1}", vAssyCode, vPartCode);
                        }
                        else
                        {
                            NewPartCodeC = vPartCode;
                            NewPageCodeC = vPageCode;

                        }

                        //  '**Log new information to EciACitems


                        sql = "INSERT eci.dbo.EciACitems (EciNumber,ChangeA,KeyA," +
                          "MajorA,MinorA,CommentA,KeyC,ChangeC,AssyC," +
                          "PartCodeC,PageCodeC,DescriptionC) " +
                          "VALUES ('" + ecinumber +
                          "','" + ChangeA +
                          "','" + KeyA +
                          "','" + MajorA +
                          "','" + MinorA +
                          "','" + CommentA + "'," +
                          "'Z9','" + ChangeC + "','" + vCategory.Substring(0, 3) +
                          "','" + NewPartCodeC +
                          "','" + NewPageCodeC +
                          "','" + vDescription + "')";

                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);


                    } //end of Vlog

                    // add by dayang on 01/08 to add pageid field
                    // revised on 02/28 to update empty filed
                    if (string.IsNullOrEmpty(formcpairitemid))
                    {
                        sql = "UPDATE ETA.dbo.FormCItems " +
               "SET CategoryAddress='" + vCategory + "'," +
                   "AitemId='" + vKey + "'," +
                   "AssyCode='" + vAssyCode + "'," +
                   "Treatment='" + vTreatment + "'," +
                            // "PCItemId=" + formcpairitemid + "," +
                   " PartCode='" + vPartCode + "'," +
                   "PageCode='" + vPageCode + "'," +
                   "Description='" + vDescription.Replace("'", "''") + "'," +
                   "ModuleLocation='" + ModuleValue + "' WHERE CItemId=" + formcitemid;

                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);
                    }

                    else
                    {
                        sql = "UPDATE ETA.dbo.FormCItems " +
                 "SET CategoryAddress='" + vCategory + "'," +
                     "AitemId='" + vKey + "'," +
                     "AssyCode='" + vAssyCode + "'," +
                     "Treatment='" + vTreatment + "'," +
                     "PCItemId=" + formcpairitemid + "," +
                     " PartCode='" + vPartCode + "'," +
                     "PageCode='" + vPageCode + "'," +
                     "Description='" + vDescription.Replace("'", "''") + "'," +
                     "ModuleLocation='" + ModuleValue + "' WHERE CItemId=" + formcitemid;

                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);


                        // also need to update formcpairitemid row for citemid =formcitemid

                        // changed by dayang on 03/08/2013
                        //    sql = "UPDATE ETA.dbo.FormCItems SET PCItemId=" + formcitemid + "WHERE CItemId=" + formcpairitemid;

                        //     SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);
                        // */

                    }

                    // changed by dayang on 03/08/2013 , bulk update formc pairid
                    sql = string.Format("EXECUTE [ETA].[dbo].[BulkUpdateFormCItems] '{0}'", pairidlist);
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                    trans.Commit();
                    ReturnValue = "Success";
                }//end try
                catch (Exception err)
                {
                    trans.Rollback();
                    ReturnValue = err.Message;
                }
            }
        }// end using sqlconnection
        return ReturnValue;


    }

    public static string InsertNewFormCWithTransactionForMutipleInserts(string ecinumber, string sinital, string package, bool vLog, string vRev, DataTable dt, string EciAcid, string AitemId)
    {
        string ReturnValue = null;
        string sql = null;
        using (SqlConnection conn = new SqlConnection(ETAConnectionString))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    string keya = null;
                    DataSet ds = null;
                    if (!string.IsNullOrEmpty(ecinumber))
                    {


                        // '**Get ECI header info
                        sql = "SELECT EciNumber FROM ETA.dbo.EciHeadParts WHERE EciNumber='" + ecinumber + "'";

                        ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            // insert '**Create Header
                            sql = "INSERT ETA.dbo.EciHeadC (DesignNumber,EciNumber,RevInitials) VALUES ('" + package + "','" + ecinumber + "','" + sinital + "')";
                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);
                        }


                        //'**Get Next Key from EciAcItems
                        DataTable dtkeya = GetKeyAByEciNumber(ecinumber).Tables[0];

                        if (dtkeya.Rows.Count > 0)
                        {
                            // keya = CommonTool.Chr(CommonTool.Asc(dtkeya.Rows[0][0].ToString().Trim()) + 1);
                            if (string.IsNullOrEmpty(dtkeya.Rows[0][0].ToString().Trim()))
                            {
                                keya = "A";
                            }
                            else
                            {

                                keya = CommonTool.Chr(CommonTool.Asc(dtkeya.Rows[0][0].ToString().Trim()) + 1);
                            }

                        }

                        else
                        {
                            keya = "A";
                        }


                    }//end ecinumber

                    foreach (DataRow row in dt.Rows)
                    {

                        // 
                        if (string.IsNullOrEmpty(row["key"].ToString()))
                        {


                        }

                        else
                        {
                            //ID, AItemId, TFC, ATT, KEYCODE, ITEMCODE, MODELCODE, ADD_TFC, ADD_INDEX, DEL_TFC, DEL_INDEX, Description, FROM_ECI, TO_ECI, FROM_DATE, TO_DATE


                            string vCategory = row["assyname"].ToString();
                            string vKey = row["Aitemid"].ToString();
                            //txtcode
                            string vAssyCode = row["assycode"].ToString();
                            string vTreatment = row["treatment"].ToString();

                            string vPartCode = row["partcode"].ToString();
                            string vPageCode = CommonTool.GetFormattedPageCodeUpdate(row["pagecode"].ToString());
                            string vDescription = CommonTool.ProcessedFormCDesc(row["description"].ToString());

                            // add by dayang on 01/09/2013
                            string pairid = row["pcitemid"].ToString();


                            //'**Get Module Number for selected AItem
                            sql = string.Format("SELECT ModuleNumber FROM ETA.dbo.FormAItems where AItemId={0}", vKey);
                            string vModule = null;
                            ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);
                            if (ds.Tables[0].Rows.Count != 0)
                            {

                                vModule = ds.Tables[0].Rows[0]["ModuleNumber"].ToString();
                            }




                            //'**Insert record into form C table

                            //sql = "INSERT ETA.dbo.FormCItems (DesignNumber,CategoryAddress,AItemId,AssyCode,Treatment,PartCode,PageCode,Description,ModuleLocation) VALUES('" + package + "','" + vCategory + "'," + vKey + ",'"+ vAssyCode + "','"  + vTreatment + "','" + vPartCode + "','" + vPageCode + "','" + vDescription + "','" + vModule + "')";

                            //sql = string.Format("INSERT ETA.dbo.FormCItems (DesignNumber,CategoryAddress,AItemId,AssyCode,Treatment,PartCode,PageCode,Description,ModuleLocation,PCItemId) VALUES('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}',{9})", package, vCategory, vKey, vAssyCode, vTreatment, vPartCode, vPageCode, vDescription, vModule,pairid);

                            sql = string.Format("INSERT ETA.dbo.FormCItems (DesignNumber,CategoryAddress,AItemId,AssyCode,Treatment,PartCode,PageCode,Description,ModuleLocation) VALUES('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}')", package, vCategory, vKey, vAssyCode, vTreatment, vPartCode, vPageCode, vDescription, vModule);

                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                            sql = "SELECT IDENT_CURRENT ('ETA.dbo.FormCItems') as Id";
                            ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);

                            string CitemId = ds.Tables[0].Rows[0][0].ToString();
                            // add by dayang on 01/09/2013 , need to update the citemid=pairid 's pairid=Citemid
                            // update citemid 
                            // also need to update formcpairitemid row for citemid =formcitemid
                            // sql = "UPDATE ETA.dbo.FormCItems SET PCItemId=" +CitemId +" WHERE CItemId=" + pairid;
                            // SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                            //log

                            if (vLog)
                            {


                                //'**Add item to ChangeLogC

                                sql = string.Format("INSERT ETA.dbo.ChangeLogC VALUES ({0},'{1}')", CitemId, vRev);
                                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                                string ChangeA = null;
                                string KeyA = null;
                                string MajorA = null;
                                string MinorA = null;
                                string CommentA = null;

                                DataSet dsold = null;
                                //'**Form A or ECI data source
                                if (!string.IsNullOrEmpty(EciAcid))
                                {
                                    //'**find item in ECI form to assign this to
                                    sql = string.Format("SELECT ChangeA,KeyA,MajorA,MinorA,CommentA FROM eci.dbo.EciACitems WHERE EciAcId={0}", EciAcid);

                                    dsold = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);
                                    ChangeA = dsold.Tables[0].Rows[0]["ChangeA"].ToString();
                                    KeyA = dsold.Tables[0].Rows[0]["KeyA"].ToString();
                                    MajorA = dsold.Tables[0].Rows[0]["MajorA"].ToString();
                                    MinorA = dsold.Tables[0].Rows[0]["MinorA"].ToString();
                                    CommentA = dsold.Tables[0].Rows[0]["CommentA"].ToString();

                                }
                                else
                                {
                                    // eciacid is null
                                    //'**find item in Form A to assign this to


                                    sql = string.Format("SELECT Major,Minor,Comment FROM ETA.dbo.FormAitems WHERE AitemId={0}", AitemId);
                                    dsold = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);
                                    ChangeA = "No Change";
                                    MajorA = dsold.Tables[0].Rows[0]["Major"].ToString();
                                    MinorA = dsold.Tables[0].Rows[0]["Minor"].ToString();
                                    CommentA = dsold.Tables[0].Rows[0]["Comment"].ToString().Replace("'", "''");
                                }

                                if (dsold.Tables[0].Rows.Count > 0)
                                {
                                    string PartCodeC = null;
                                    string PageCodeC = null;
                                    int assycint = 0;
                                    if (Int32.TryParse(vAssyCode, out assycint))
                                    {

                                        PartCodeC = package;
                                        PageCodeC = string.Format("{0} {1}", vAssyCode, vPartCode);
                                    }
                                    else
                                    {
                                        PartCodeC = vPartCode;
                                        PageCodeC = vPageCode;

                                    }
                                    string vChangeCode = null;
                                    if (vTreatment.Trim() == "S")
                                    {
                                        vChangeCode = "New Adoption";
                                    }
                                    else
                                    {
                                        vChangeCode = "Deleted";

                                    }

                                    sql = "INSERT eci.dbo.EciACitems (EciNumber,ChangeA,KeyA," +
                             "MajorA,MinorA,CommentA,KeyC,ChangeC,AssyC," +
                             "PartCodeC,PageCodeC,DescriptionC) " +
                             "VALUES ('" + ecinumber +
                             "','" + ChangeA +
                             "','" + KeyA +
                             "','" + MajorA +
                             "','" + MinorA +
                             "','" + CommentA + "'," +
                             "'Z9','" + vChangeCode + "','" + vCategory.Substring(0, 3) +
                             "','" + PartCodeC +
                             "','" + PageCodeC +
                             "','" + vDescription + "')";

                                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);


                                }


                            }//Vlog end
                        }//end if datarow empty

                    } //end foreach

                    trans.Commit();
                    ReturnValue = "Success";
                }//end try
                catch (Exception err)
                {


                    trans.Rollback();

                    ReturnValue = err.Message;
                }
            }
        }// end using sqlconnection
        return ReturnValue;


    }

    /// <summary>
    /// For new 03 FormC Insertion
    /// </summary>
    /// <param name="ecinumber"></param>
    /// <param name="sinital"></param>
    /// <param name="package"></param>
    /// <param name="vLog"></param>
    /// <param name="vRev"></param>
    /// <param name="dt"></param>
    /// <param name="EciAcid"></param>
    /// <param name="AitemId"></param>
    /// <returns></returns>

    public static string Insert03FormCWithTransactionForMutipleInserts(string ecinumber, string sinital, string package, bool vLog, string vRev, DataTable dt, string EciAcid, string AitemId)
    {



        string ReturnValue = null;
        string sql = null;

        // Add by Dayang on 10/01/2013 to get max keycode
        DataTable dtkeycode = Get03TSDMaxKeyCode(package);
        int maxkeycode = 0;
        if (dtkeycode.Rows.Count > 0)
        {

            maxkeycode = Convert.ToInt16(dtkeycode.Rows[0]["maxindex"]);
        }

        using (SqlConnection conn = new SqlConnection(ETAConnectionString))
        {
            conn.Open();




            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    string keya = null;
                    DataSet ds = null;
                    if (!string.IsNullOrEmpty(ecinumber))
                    {


                        // '**Get ECI header info
                        sql = "SELECT EciNumber FROM ETA.dbo.EciHeadParts WHERE EciNumber='" + ecinumber + "'";

                        ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            // insert '**Create Header
                            sql = "INSERT ETA.dbo.EciHeadC (DesignNumber,EciNumber,RevInitials) VALUES ('" + package + "','" + ecinumber + "','" + sinital + "')";
                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);
                        }


                        //'**Get Next Key from EciAcItems
                        DataTable dtkeya = GetKeyAByEciNumber(ecinumber).Tables[0];

                        if (dtkeya.Rows.Count > 0)
                        {
                            // keya = CommonTool.Chr(CommonTool.Asc(dtkeya.Rows[0][0].ToString().Trim()) + 1);
                            if (string.IsNullOrEmpty(dtkeya.Rows[0][0].ToString().Trim()))
                            {
                                keya = "A";
                            }
                            else
                            {

                                keya = CommonTool.Chr(CommonTool.Asc(dtkeya.Rows[0][0].ToString().Trim()) + 1);
                            }

                        }

                        else
                        {
                            keya = "A";
                        }


                    }//end ecinumber



                    foreach (DataRow row in dt.Rows)
                    {
                        maxkeycode++;
                        // 
                        if (string.IsNullOrEmpty(row["key"].ToString()))
                        {


                        }

                        else
                        {
                            //ID, AItemId, TFC, ATT, KEYCODE, ITEMCODE, MODELCODE, ADD_TFC, ADD_INDEX, DEL_TFC, DEL_INDEX, Description, FROM_ECI, TO_ECI, FROM_DATE, TO_DATE


                            // // ID, 
                            //??AItemId, TFC,
                            //ATT="", 
                            //KEYCODE=packge+id
                            //, ITEMCODE=A, 
                            //MODELCODE=forma.Model by package
                            //ADD_TFC, ADD_INDEX, DEL_TFC, DEL_INDEX, Description, FROM_ECI, TO_ECI, FROM_DATE, TO_DATE

                            // Aitemid ?
                            // TFC : package ?
                            //ATT: 
                            // 

                            string aitemid = row["Aitemid"].ToString();
                            string tfc = package;
                            string keycode = package + "_" + maxkeycode.ToString();
                            string itemcode = "A";
                            //'**Get Module Number for selected AItem
                            sql = string.Format("SELECT ModuleNumber FROM ETA.dbo.FormAItems where AItemId={0}", aitemid);
                            string vModule = null;
                            ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);
                            if (ds.Tables[0].Rows.Count != 0)
                            {

                                vModule = ds.Tables[0].Rows[0]["ModuleNumber"].ToString();
                            }
                            //modelcode is vmodule
                            string assycode = row["assyname"].ToString().Substring(0, 3);
                            string addtfc = row["addtfc"].ToString();
                            string addindex = assycode + row["addindex"].ToString();
                            string deltfc = row["deltfc"].ToString();
                            string delindex = assycode + row["delindex"].ToString();
                            string description = CommonTool.ProcessedFormCDesc(row["description"].ToString());
                            string fromeci = row["fromeci"].ToString();


                            sql = string.Format("INSERT INTO [ETA].[dbo].[03_TSD](AItemId,TFC,ATT,KEYCODE,ITEMCODE,MODELCODE,ADD_TFC,ADD_INDEX,DEL_TFC,DEL_INDEX,Description,FROM_ECI,FROM_DATE) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',getdate())", aitemid, package, "", keycode, "A", vModule, addtfc, addindex, deltfc, delindex, description, fromeci);

                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);


                            //string vCategory = row["assyname"].ToString();

                            //string vKey = row["Aitemid"].ToString();
                            ////txtcode
                            //string vAssyCode = row["assycode"].ToString();
                            //string vTreatment = row["treatment"].ToString();

                            //string vPartCode = row["partcode"].ToString();
                            //string vPageCode = CommonTool.GetFormattedPageCodeUpdate(row["pagecode"].ToString());
                            //string vDescription = CommonTool.ProcessedFormCDesc(row["description"].ToString());

                            //// add by dayang on 01/09/2013
                            //string pairid = row["pcitemid"].ToString();

                            ////'**Insert record into form C table


                            //sql = string.Format("INSERT ETA.dbo.FormCItems (DesignNumber,CategoryAddress,AItemId,AssyCode,Treatment,PartCode,PageCode,Description,ModuleLocation) VALUES('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}')", package, vCategory, vKey, vAssyCode, vTreatment, vPartCode, vPageCode, vDescription, vModule);

                            //   SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                            sql = "SELECT IDENT_CURRENT ('ETA.dbo.FormCItems') as Id";
                            ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);

                            string CitemId = ds.Tables[0].Rows[0][0].ToString();
                            // add by dayang on 01/09/2013 , need to update the citemid=pairid 's pairid=Citemid
                            // update citemid 
                            // also need to update formcpairitemid row for citemid =formcitemid
                            // sql = "UPDATE ETA.dbo.FormCItems SET PCItemId=" +CitemId +" WHERE CItemId=" + pairid;
                            // SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                            //log

                            if (vLog)
                            {
                                /*

                                //'**Add item to ChangeLogC

                                sql = string.Format("INSERT ETA.dbo.ChangeLogC VALUES ({0},'{1}')", CitemId, vRev);
                                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                                string ChangeA = null;
                                string KeyA = null;
                                string MajorA = null;
                                string MinorA = null;
                                string CommentA = null;

                                DataSet dsold = null;
                                //'**Form A or ECI data source
                                if (!string.IsNullOrEmpty(EciAcid))
                                {
                                    //'**find item in ECI form to assign this to
                                    sql = string.Format("SELECT ChangeA,KeyA,MajorA,MinorA,CommentA FROM eci.dbo.EciACitems WHERE EciAcId={0}", EciAcid);

                                    dsold = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);
                                    ChangeA = dsold.Tables[0].Rows[0]["ChangeA"].ToString();
                                    KeyA = dsold.Tables[0].Rows[0]["KeyA"].ToString();
                                    MajorA = dsold.Tables[0].Rows[0]["MajorA"].ToString();
                                    MinorA = dsold.Tables[0].Rows[0]["MinorA"].ToString();
                                    CommentA = dsold.Tables[0].Rows[0]["CommentA"].ToString();

                                }
                                else
                                {
                                    // eciacid is null
                                    //'**find item in Form A to assign this to


                                    sql = string.Format("SELECT Major,Minor,Comment FROM ETA.dbo.FormAitems WHERE AitemId={0}", AitemId);
                                    dsold = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);
                                    ChangeA = "No Change";
                                    MajorA = dsold.Tables[0].Rows[0]["Major"].ToString();
                                    MinorA = dsold.Tables[0].Rows[0]["Minor"].ToString();
                                    CommentA = dsold.Tables[0].Rows[0]["Comment"].ToString().Replace("'", "''");
                                }

                                if (dsold.Tables[0].Rows.Count > 0)
                                {
                                    string PartCodeC = null;
                                    string PageCodeC = null;
                                    int assycint = 0;
                                    if (Int32.TryParse(vAssyCode, out assycint))
                                    {

                                        PartCodeC = package;
                                        PageCodeC = string.Format("{0} {1}", vAssyCode, vPartCode);
                                    }
                                    else
                                    {
                                        PartCodeC = vPartCode;
                                        PageCodeC = vPageCode;

                                    }
                                    string vChangeCode = null;
                                    if (vTreatment.Trim() == "S")
                                    {
                                        vChangeCode = "New Adoption";
                                    }
                                    else
                                    {
                                        vChangeCode = "Deleted";

                                    }

                                    sql = "INSERT eci.dbo.EciACitems (EciNumber,ChangeA,KeyA," +
                             "MajorA,MinorA,CommentA,KeyC,ChangeC,AssyC," +
                             "PartCodeC,PageCodeC,DescriptionC) " +
                             "VALUES ('" + ecinumber +
                             "','" + ChangeA +
                             "','" + KeyA +
                             "','" + MajorA +
                             "','" + MinorA +
                             "','" + CommentA + "'," +
                             "'Z9','" + vChangeCode + "','" + vCategory.Substring(0, 3) +
                             "','" + PartCodeC +
                             "','" + PageCodeC +
                             "','" + vDescription + "')";

                                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);


                                }

*/
                            }//Vlog end
                        }//end if datarow empty

                    } //end foreach

                    trans.Commit();
                    ReturnValue = "Success";
                }//end try
                catch (Exception err)
                {


                    trans.Rollback();

                    ReturnValue = err.Message;
                }
            }
        }// end using sqlconnection
        return ReturnValue;


    }

    //vRev, vCategory, vKey, vAssyCode, vTreatment, vPartCode, vPageCode, vDescription);
    /// <summary>
    /// Inserts the new form C with transaction for insert.
    /// </summary>
    /// <param name="ecinumber">The ecinumber.</param>
    /// <param name="sinital">The sinital.</param>
    /// <param name="package">The package.</param>
    /// <param name="vLog">if set to <c>true</c> [v log].</param>
    /// <param name="vRev">The v rev.</param>
    /// <param name="vCategory">The v category.</param>
    /// <param name="vKey">The v key.</param>
    /// <param name="vAssyCode">The v assy code.</param>
    /// <param name="vTreatment">The v treatment.</param>
    /// <param name="vPartCode">The v part code.</param>
    /// <param name="vPageCode">The v page code.</param>
    /// <param name="vDescription">The v description.</param>
    /// <param name="EciAcid">The eci acid.</param>
    /// <param name="AitemId">The aitem id.</param>
    /// <returns></returns>
    public static string InsertNewFormCWithTransactionForInsert(string ecinumber, string sinital, string package, bool vLog, string vRev, string vCategory, string vKey, string vAssyCode, string vTreatment, string vPartCode, string vPageCode, string vDescription, string EciAcid, string AitemId)
    {
        string ReturnValue = null;
        string sql = null;
        using (SqlConnection conn = new SqlConnection(ETAConnectionString))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    string keya = null;
                    DataSet ds = null;
                    if (!string.IsNullOrEmpty(ecinumber))
                    {


                        // '**Get ECI header info
                        sql = "SELECT EciNumber FROM ETA.dbo.EciHeadParts WHERE EciNumber='" + ecinumber + "'";

                        ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            // insert '**Create Header
                            sql = "INSERT ETA.dbo.EciHeadC (DesignNumber,EciNumber,RevInitials) VALUES ('" + package + "','" + ecinumber + "','" + sinital + "')";
                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);
                        }


                        //'**Get Next Key from EciAcItems
                        DataTable dtkeya = GetKeyAByEciNumber(ecinumber).Tables[0];

                        if (dtkeya.Rows.Count > 0)
                        {
                            //keya = CommonTool.Chr(CommonTool.Asc(dtkeya.Rows[0][0].ToString().Trim()) + 1);
                            if (string.IsNullOrEmpty(dtkeya.Rows[0][0].ToString().Trim()))
                            {
                                keya = "A";
                            }
                            else
                            {

                                keya = CommonTool.Chr(CommonTool.Asc(dtkeya.Rows[0][0].ToString().Trim()) + 1);
                            }


                        }

                        else
                        {
                            keya = "A";
                        }



                    }//end ecinumber



                    //'**Get Module Number for selected AItem
                    sql = string.Format("SELECT ModuleNumber FROM ETA.dbo.FormAItems where AItemId={0}", vKey);
                    string vModule = null;
                    ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);
                    if (ds.Tables[0].Rows.Count != 0)
                    {

                        vModule = ds.Tables[0].Rows[0]["ModuleNumber"].ToString();
                    }

                    //'**Insert record into form C table

                    //sql = "INSERT ETA.dbo.FormCItems (DesignNumber,CategoryAddress,AItemId,AssyCode,Treatment,PartCode,PageCode,Description,ModuleLocation) VALUES('" + package + "','" + vCategory + "'," + vKey + ",'"+ vAssyCode + "','"  + vTreatment + "','" + vPartCode + "','" + vPageCode + "','" + vDescription + "','" + vModule + "')";

                    sql = string.Format("INSERT ETA.dbo.FormCItems (DesignNumber,CategoryAddress,AItemId,AssyCode,Treatment,PartCode,PageCode,Description,ModuleLocation) VALUES('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}')", package, vCategory, vKey, vAssyCode, vTreatment, vPartCode, vPageCode, vDescription, vModule);

                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                    //log

                    if (vLog)
                    {

                        sql = "SELECT IDENT_CURRENT ('ETA.dbo.FormCItems') as Id";
                        ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);

                        string CitemId = ds.Tables[0].Rows[0][0].ToString();
                        //'**Add item to ChangeLogC

                        sql = string.Format("INSERT ETA.dbo.ChangeLogC VALUES ({0},'{1}')", CitemId, vRev);
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                        string ChangeA = null;
                        string KeyA = null;
                        string MajorA = null;
                        string MinorA = null;
                        string CommentA = null;

                        DataSet dsold = null;
                        //'**Form A or ECI data source
                        if (!string.IsNullOrEmpty(EciAcid))
                        {
                            //'**find item in ECI form to assign this to
                            sql = string.Format("SELECT ChangeA,KeyA,MajorA,MinorA,CommentA FROM eci.dbo.EciACitems WHERE EciAcId={0}", EciAcid);

                            dsold = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);
                            ChangeA = dsold.Tables[0].Rows[0]["ChangeA"].ToString();
                            KeyA = dsold.Tables[0].Rows[0]["KeyA"].ToString();
                            MajorA = dsold.Tables[0].Rows[0]["MajorA"].ToString();
                            MinorA = dsold.Tables[0].Rows[0]["MinorA"].ToString();
                            CommentA = dsold.Tables[0].Rows[0]["CommentA"].ToString();

                        }
                        else
                        {
                            // eciacid is null
                            //'**find item in Form A to assign this to


                            sql = string.Format("SELECT Major,Minor,Comment FROM ETA.dbo.FormAitems WHERE AitemId={0}", AitemId);
                            dsold = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);
                            ChangeA = "No Change";
                            MajorA = dsold.Tables[0].Rows[0]["Major"].ToString();
                            MinorA = dsold.Tables[0].Rows[0]["Minor"].ToString();
                            CommentA = dsold.Tables[0].Rows[0]["Comment"].ToString().Replace("'", "''");
                        }

                        if (dsold.Tables[0].Rows.Count > 0)
                        {
                            string PartCodeC = null;
                            string PageCodeC = null;
                            int assycint = 0;
                            if (Int32.TryParse(vAssyCode, out assycint))
                            {

                                PartCodeC = package;
                                PageCodeC = string.Format("{0} {1}", vAssyCode, vPartCode);
                            }
                            else
                            {
                                PartCodeC = vPartCode;
                                PageCodeC = vPageCode;

                            }
                            string vChangeCode = null;
                            if (vTreatment.Trim() == "S")
                            {
                                vChangeCode = "New Adoption";
                            }
                            else
                            {
                                vChangeCode = "Deleted";

                            }

                            sql = "INSERT eci.dbo.EciACitems (EciNumber,ChangeA,KeyA," +
                     "MajorA,MinorA,CommentA,KeyC,ChangeC,AssyC," +
                     "PartCodeC,PageCodeC,DescriptionC) " +
                     "VALUES ('" + ecinumber +
                     "','" + ChangeA +
                     "','" + KeyA +
                     "','" + MajorA +
                     "','" + MinorA +
                     "','" + CommentA + "'," +
                     "'Z9','" + vChangeCode + "','" + vCategory.Substring(0, 3) +
                     "','" + PartCodeC +
                     "','" + PageCodeC +
                     "','" + vDescription + "')";

                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);


                        }


                    }//Vlog end




                    trans.Commit();
                    ReturnValue = "Success";
                }//end try
                catch (Exception err)
                {


                    trans.Rollback();

                    ReturnValue = err.Message;
                }
            }
        }// end using sqlconnection
        return ReturnValue;


    }
    /// <summary>
    /// Gets the non stanard part list info with assy code.
    /// </summary>
    /// <param name="package">The package.</param>
    /// <param name="assyname">The assyname.</param>
    /// <param name="compcode">The compcode.</param>
    /// <param name="vari">The vari.</param>
    /// <returns></returns>
    public static DataSet GetNonStanardPartListInfoWithAssyCode(string package, string assyname, string assycode, string partcode)
    {
        // N2E8(531)0101
        DataSet result = null;
        object[] objParams = { 0 };

        //string sql = string.Format("SELECT top 1 * FROM   viewPartsList WHERE  CODE2 = '{0}' And CODE3 = '{1}' AND CODE1 = '{2}' And DESIGNNUMBER LIKE '{3}%'",  compcode, vari, assyname,package);



        string sql = string.Format("SELECT top 1 * FROM viewFormCItems INNER JOIN viewPartsList ON viewFormCItems.ModuleLocation = viewPartsList.DesignNumber WHERE  viewFormCItems.DesignNumber ='{0}' AND viewPartsList.CODE2 = '{1}' And viewPartsList.CODE3 = '{2}' AND viewPartsList.CODE1 = '{3}'", package, assycode, partcode, assyname);

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);


        return result;
    }

    public static DataSet GetNonStandPartListInfo(string partnumber)
    {
        // N2E8(531)0101
        DataSet result = null;
        object[] objParams = { 0 };

        string sql = string.Format("select  partnumber  FROM ETA.dbo.viewPartsList where partnumber like '{0}%'", partnumber);

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);


        return result;


    }
    /// <summary>
    /// Gets the non stanard part list info without assy code.
    /// </summary>
    /// <param name="partcode">The partcode.</param>
    /// <param name="assyname">The assyname.</param>
    /// <param name="compcode">The compcode.</param>
    /// <param name="vari">The vari.</param>
    /// <returns></returns>
    public static DataSet GetNonStanardPartListInfoWithoutAssyCode(string partcode, string assyname, string compcode, string vari)
    {
        DataSet result = null;
        object[] objParams = { 0 };
        //                SELECT     * FROM viewFormCItems INNER JOIN
        //                      viewPartsList ON viewFormCItems.ModuleLocation = viewPartsList.DesignNumber
        //WHERE     viewFormCItems.PartCode = 'M204' AND viewPartsList.CODE2 = '01' AND viewPartsList.CODE3 = '04'

        // AND viewPartsList.CODE1 = '611'
        // 07/14 version
        //string sql = string.Format("SELECT * FROM viewFormCItems INNER JOIN viewPartsList ON viewFormCItems.ModuleLocation = viewPartsList.DesignNumber WHERE  viewFormCItems.PartCode ='{0}' AND viewPartsList.CODE2 = '{1}' And viewPartsList.CODE3 = '{2}' AND viewPartsList.CODE1 = '{3}'", partcode,  compcode, vari,assyname);

        string sql = string.Format("SELECT top 1 * FROM viewFormCItems INNER JOIN viewPartsList ON viewFormCItems.ModuleLocation = viewPartsList.DesignNumber WHERE  viewFormCItems.DesignNumber ='{0}' AND viewPartsList.CODE2 = '{1}' And viewPartsList.CODE3 = '{2}' AND viewPartsList.CODE1 = '{3}'", partcode, compcode, vari, assyname);

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);


        return result;
    }

    /// <summary>
    /// Gets the standard part list info.
    /// </summary>
    /// <param name="partcode">The partcode.</param>
    /// <param name="assyname">The assyname.</param>
    /// <param name="compcode">The compcode.</param>
    /// <param name="vari">The vari.</param>
    /// <param name="ser">The ser.</param>
    /// <returns></returns>
    public static DataSet GetStandardPartListInfo(string partcode, string assyname, string compcode, string vari, string ser)
    {
        DataSet result = null;
        object[] objParams = { 0 };
        string sql = string.Format("select top 1 * from ETA.dbo.viewStandardParts WHERE Model='{0}' and  GroupNo='{1}' and CompCode='{2}' and Vari='{3}' and Ser='{4}' ", partcode, assyname, compcode, vari, ser);

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);


        return result;
    }


    /// <summary>
    /// Gets the part lists info.
    /// </summary>
    /// <param name="partcode">The partcode.</param>
    /// <param name="assyname">The assyname.</param>
    /// <param name="code1">The code1.</param>
    /// <param name="code2">The code2.</param>
    /// <returns></returns>
    public static DataSet GetPartListsInfo(string partcode, string assyname, string code1, string code2)
    {
        //SELECT distinct PartNumber FROM  viewPartsList where code1='51u' and code2='01' and code3='04' and Num1='MB37'
        DataSet result = null;
        object[] objParams = { 0 };
        string sql = string.Format("SELECT distinct PartNumber FROM  viewPartsList where code1='{0}' and code2='{1}' and code3='{2}' and Num1='{3}' ", assyname, code1, code2, partcode);

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);


        return result;
    }

    /// <summary>
    /// Gets the part list info from view part list.
    /// </summary>
    /// <param name="partno">The partno.</param>
    /// <returns></returns>
    public static DataSet GetPartListInfoFromViewPartList(string partno)
    {
        DataSet result = null;
        object[] objParams = { 0 };
        string sql = string.Format("select top 1  partnumber from dbo.viewPartsList where PartNumber = '{0}' ", partno);

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);


        return result;
    }


    public static DataSet GetStandardPartListInfoWithMinor(string model, string partno)
    {
        DataSet result = null;
        object[] objParams = { 0 };
        string sql = string.Format("select top 1 * from ETA.dbo.viewStandardParts WHERE Model='{0}' and  partno like '{1}%'", model, partno);

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);


        return result;
    }

    /// <summary>
    /// Gets the standard part list info without model.
    /// </summary>
    /// <param name="partno">The partno.</param>
    /// <param name="minor">The minor.</param>
    /// <returns></returns>
    public static DataSet GetStandardPartListInfoWithoutModel(string partno, string minor)
    {
        DataSet result = null;
        object[] objParams = { 0 };


        string sql = null;
        if (string.IsNullOrEmpty(minor))
        {
            sql = string.Format("select top 1  partnumber from ETA.dbo.viewStandardParts WHERE  partnumber like '{0}%' ", partno);
        }
        else
        {
            sql = string.Format("select top 1  partnumber from ETA.dbo.viewStandardParts WHERE  partnumber like '{0}%' and minor='{1}' ", partno, minor);

        }



        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);


        return result;
    }


    /// <summary>
    /// Gets the ref part lists.
    /// </summary>
    /// <param name="package">The package.</param>
    /// <param name="code1">The code1.</param>
    /// <param name="code2">The code2.</param>
    /// <param name="code3">The code3.</param>
    /// <returns></returns>
    public static DataSet GetRefPartLists(string package, string code1, string code2, string code3)
    {
        DataSet result = null;
        object[] objParams = { 0 };

        string sql = string.Format("SELECT partnumber FROM ETA.dbo.viewPartsList AS m WHERE m.DesignNumber LIKE '{0}%' and Code1='{1}' and code2='{2}' and code3='{3}'", package, code1, code2, code3);
        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);


        return result;
    }




    /// <summary>
    /// Gets the name of the part list header info by package.
    /// </summary>
    /// <param name="packagename">The packagename.</param>
    /// <returns></returns>
    public static DataSet GetPartListHeaderInfoByPackageName(string packagename)
    {
        DataSet result = null;
        object[] objParams = { 0 };

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, "SELECT Rev,RevComment,c.EciNumber,CommentDate,RevInitials FROM ETA.dbo.EciHeadC as c JOIN eci.dbo.Eci as e ON c.EciNumber=e.EciNumber WHERE c.DesignNumber='" + packagename + "' ORDER BY Rev");


        return result;
    }

    /// <summary>
    /// Gets the category.
    /// Use Caching
    /// </summary>
    /// <returns></returns>
    public static DataSet GetCategory()
    {

        // 

        DataSet result = null;
        object[] objParams = { 0 };

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, "SELECT ltrim(rtrim(Category)) as Category FROM ETA.dbo.Categories ORDER BY Category");

        //result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, "SELECT distinct ltrim(rtrim(CategoryAddress)) as Category  FROM viewFormCItems order by Category");
        //SELECT distinct ltrim(rtrim(CategoryAddress)) as Category  FROM viewFormCItems order by Category
        return result;
    }

    /// <summary>
    /// Validates the part code from standard part list.
    /// </summary>
    /// <param name="partcode">The partcode.</param>
    /// <returns></returns>
    public static DataSet ValidatePartCodeFromStandardPartList(string partcode)
    {
        DataSet result = null;
        object[] objParams = { 0 };

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, string.Format("SELECT DISTINCT Model FROM ETA.dbo.ac_Tiem_Parts  where Model='{0}'", partcode));


        return result;

    }

    public static bool GetConfiguratorResult(string orderno, string cmbtext)
    {

        bool result = false;

        string sql =
            string.Format(
                "SELECT Order_Number from dbo.ConfiguratorValidation where Order_Number='{0}' and  ltrim(rtrim([ADD_TFC]))+replace([ADD_INDEX],' ','')='{1}'",
                orderno, cmbtext);

        DataTable dt = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql).Tables[0];
        if (dt.Rows.Count > 0)
            result = true;

        return result;

    }

    /// <summary>
    /// 
    /// </summary>
    public static void AutoPairFormCFromQueue()
    {
        SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, "EXECUTE  [ETA].[dbo].[AutoPairFormCFromQueue] ");

    }
    /// <summary>
    /// 
    /// </summary>
    public static void RunConfiguratorFromQueue()
    {

        //SqlHelper.ExecuteDataset(STConnectionString, CommandType.Text, "EXECUTE  [ST_SUPPORT].[dbo].[ConfiguratorFromQueue]");

        // Changed on 04/10/2013. because of timeout error, can not use sqlhelper

        SqlConnection conn = null;
        try
        {
            conn = new SqlConnection(STConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("[dbo].[ConfiguratorFromQueue]", conn);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            if (conn != null)
            {
                conn.Close();
            }

        }

    }

    public static DataTable GetConfiguratorQueue()
    {
        var result =
            SqlHelper.ExecuteDataset(STConnectionString, CommandType.Text,
                                     "select JobValue from  [ST_SUPPORT].[dbo].[JobAS400OrderInfo]").Tables[0];

        return result;

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="typeid"></param>
    /// <param name="isdelete"></param>
    //public static void ProcessETAJobQueue(string value, int typeid, int isdelete)
    //{

    //    string sql = string.Format("EXECUTE  [ST_SUPPORT].[dbo].[ConfiguratorFromQueue]  {0} ,{1},{2} ", value, typeid, isdelete);

    //    SqlHelper.ExecuteNonQuery(STConnectionString, CommandType.Text, sql);

    //}

    public static DataSet AutoPairFormCByPackage(string package)
    {
        DataSet result = null;
        object[] objParams = { 0 };

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, string.Format("EXECUTE  [ETA].[dbo].[AutoPairFormCByPackage] {0} ", package));


        return result;
    }

    public static void LogMessageToDB(string message)
    {

        string sql = string.Format("INSERT INTO [ETA].[dbo].[test1] ([step]) VALUES ('{0}')  ", message);
        SqlHelper.ExecuteNonQuery(ETAConnectionString, CommandType.Text, sql);

    }

    public static DataSet GetTSDFormAKey(string package)
    {
        DataSet result = null;
        object[] objParams = { 0 };

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, string.Format("SELECT AItemId,[key] FROM ETA.dbo.FormAItemsTSD WHERE DesignNumber='{0}' ORDER BY [key]", package));


        return result;
    }

    public static DataSet GetKey(string package)
    {
        DataSet result = null;
        object[] objParams = { 0 };

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, string.Format("SELECT AItemId,[key] FROM ETA.dbo.FormAItems WHERE DesignNumber='{0}' ORDER BY [key]", package));


        return result;
    }



    public static DataSet GetFormCInfoByPackageNameAndModule(string packagename, string module, bool moduleflag)
    {
        DataSet result = null;
        object[] objParams = { 0 };
        if (moduleflag)
        {
            string sql = string.Format("SELECT DesignNumber, ltrim(rtrim(CategoryAddress)) as CategoryAddress, ModuleLocation, KeyNumber, [key], AssyCode, Treatment, PartCode, PageCode, Description, CItemId,PCItemId,AItemId, dbo.GetRevFromChangeLogCByCItemid(Citemid) as Rev FROM viewFormCItems WHERE  (DesignNumber = '{0}') AND (ModuleLocation = '{1} ') ORDER BY CategoryAddress, CItemId", packagename, module);


            result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);

            //"SELECT * FROM ETA.dbo.viewFormCItems left outer  JOIN    ChangeLogC ON viewFormCItems.CItemId = ChangeLogC.CItemId WHERE DesignNumber='" + packagename + "' AND ModuleLocation='" + module + " ' ORDER BY viewFormCItems.CategoryAddress, viewFormCItems.CItemId");
        }
        else
        {
            string sql = string.Format("SELECT DesignNumber, ltrim(rtrim(CategoryAddress)) as CategoryAddress, ModuleLocation, KeyNumber, [key], AssyCode, Treatment, PartCode, PageCode, Description, CItemId, PCItemId,AItemId, dbo.GetRevFromChangeLogCByCItemid(Citemid) as Rev FROM viewFormCItems WHERE (DesignNumber = '{0}') ORDER BY CategoryAddress, CItemId", packagename);

            result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);
            //"SELECT * FROM ETA.dbo.viewFormCItems left outer  JOIN    ChangeLogC ON viewFormCItems.CItemId = ChangeLogC.CItemId WHERE DesignNumber='" + packagename + "' ORDER BY viewFormCItems.CategoryAddress, viewFormCItems.CItemId");

        }

        return result;
    }

    #endregion

    #region PartsList Table

    public static DataSet GetPartNumberByModule(string module)
    {

        DataSet result = null;
        object[] objParams = { 0 };
        string sql = string.Format("select  distinct partnumber , subquantity from dbo.PartsListItems where designnumber='{0}'", module);
        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);
        //select  distinct partnumber from dbo.PartsListItems where designnumber='xxx0-29'


        return result;

    }

    public static DataSet temp(string sql)
    {
        DataSet result = null;
        object[] objParams = { 0 };

        //string sql = string.Format("select  partnumber ,designnumber,dbo.GetModelByPartNo(substring(designnumber,1,4)),itemid from  partslistitems where len(partnumber)={0} and substring(partnumber,1,1)='(' and substring(partnumber,5,1)=')' ; ", length);

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);


        return result;

    }

    public static DataSet temp_9(int length)
    {
        DataSet result = null;
        object[] objParams = { 0 };

        string sql = string.Format("select  partnumber ,designnumber,dbo.GetModelByPartNo(substring(designnumber,1,4)),itemid from  partslistitems where len(partnumber)={0} and substring(partnumber,1,1)='(' and substring(partnumber,5,1)=')' ; ", length);
        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);
        return result;

    }


    public static DataSet GetModelListByPartno(string partno)
    {
        DataSet result = null;
        object[] objParams = { 0 };

        string sql = string.Format("SELECT  distinct Model,partno  FROM ETA.dbo.viewStandardParts where partno like '%{0}%' ", partno);

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);


        return result;

    }

    public static DataSet GetSubStandardPartItemsByvalues(string partno)
    {

        DataSet result = null;
        object[] objParams = { 0 };

        string sql = string.Format("SELECT top 5 * FROM ETA.dbo.ac_tiem_parts WHERE model ='{0}' AND Index_ = '{1}' ORDER by order_", partno.Split('-')[0], string.Format("{0} {1}", partno.Split('-')[1].Substring(0, 3), partno.Split('-')[1].Substring(3)));

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);


        return result;
    }

    public static DataSet GetStandardPartItemsByvalues(string Model, string Assy, string Pagecode1, string Pagecode2, string Pagecode3)
    {

        DataSet result = null;
        object[] objParams = { 0 };

        string sql = string.Format("SELECT Level_,PartNo,Minor,PartName,Qty,Dwg,Material1,Material2,PartNoCode1,PartNoComment1, PartNoCode2,PartNoComment2,FromPartNoECI FROM ETA.dbo.viewStandardParts WHERE Model='{0} ' AND GroupNo='{1}' AND CompCode='{2}' AND Vari='{3}'  AND Ser='{4}' order by order_", Model, Assy, Pagecode1, Pagecode2, Pagecode3);

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);


        return result;
    }

    public static DataSet GetIdentityModelByPartnoAndDesignNumber(string partno, string designnumber)
    {
        DataSet result = null;
        object[] objParams = { 0 };

        string sql = string.Format("select distinct dbo.GetModelByPartNo(substring(designnumber,1,4)) as identifymodel,designnumber from  partslistitems where partnumber='{0}' and designnumber='{1}' ", partno, designnumber);

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);


        return result;

    }




    public static DataSet GetPartListINfoByLength(string length, bool sideflag)
    {
        DataSet result = null;
        object[] objParams = { 0 };

        string sql = null;
        if (!sideflag)
        {
            sql = string.Format("select top 100 partnumber,designnumber,dbo.GetModelByPartNo(substring(designnumber,1,4)),itemid   from  partslistitems where len(partnumber)={0} ", length);
        }
        else
        {
            sql = string.Format("select top 100 partnumber,designnumber,dbo.GetModelByPartNo(substring(designnumber,1,4)),itemid   from  partslistitems where len(partnumber){0} ", length);

        }
        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);


        return result;
    }
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <returns></returns>
    //public static DataSet GetCachedCategorySet()
    //{

    //    object temp = CommonTool.GetCacheRecord("categoryset");

    //    if (temp == null)
    //    {
    //        DataSet ds = GetCategory();
    //        CommonTool.InsertCacheRecord("categoryset", ds, 60);
    //        return ds;

    //    }

    //    else
    //    {

    //        return temp as DataSet;
    //    }

    //}


    /// <summary>
    /// Gets the part code from stand parts list.
    /// </summary>
    /// <param name="partcode">The partcode.</param>
    /// <returns></returns>
    public static DataSet GetStandardPartcodeDataTableFromStandPartsList()
    {

        object temp = CommonTool.GetCacheRecord("stmodellistdataset");

        if (temp == null)
        {
            // need to get 
            string sql = string.Format("Select distinct(Model) FROM ETA.dbo.viewStandardParts ORDER BY Model");
            DataSet spbdataset = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);
            CommonTool.InsertCacheRecord("stmodellistdataset", spbdataset, 60);
            return spbdataset;

        }

        else
        {

            return temp as DataSet;
        }


        //DataSet result = null;
        //object[] objParams = { 0 };

        //string sql = string.Format("Select distinct(Model) FROM ETA.dbo.viewStandardParts ORDER BY Model");

        //result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);

        //return result;
    }


    public static DataSet GetShortStandardPartcodeDataTableFromStandPartsList(string partno)
    {
        DataSet result = null;
        object[] objParams = { 0 };

        string sql = string.Format("Select distinct(PartNo) FROM ETA.dbo.viewStandardParts where  partno like '%{0}%'", partno);

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);

        return result;
    }

    public static DataSet GetFirstPartNoFromStandPartsList(string partno)
    {
        DataSet result = null;
        object[] objParams = { 0 };

        string sql = string.Format("Select distinct(PartNo) FROM ETA.dbo.viewStandardParts where  partno like '{0}%'", partno);

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);

        return result;
    }

    /// <summary>
    /// Add On 10/29/2013 For TSD Partno Validation From 05
    /// </summary>
    /// <param name="partno"></param>
    /// <returns></returns>
    public static bool IsValidTSDPartno_05(string partno)
    {

        //  bool result = false;

        string dbpartno = string.Empty;

        string sql = string.Format("EXECUTE  [dbo].[GetTSDValidPartNumberFrom05]  '{0}'", partno);

        SqlDataReader dreader = SqlHelper.ExecuteReader(ETAConnectionString, CommandType.Text, sql);

        while (dreader.Read())
        {
            dbpartno = ((IDataRecord)dreader)[0].ToString();
        }

        // Call Close when done reading.
        dreader.Close();

        if (dbpartno == "Valid")
        {

            return true;
        }
        else
        {

            return false;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="partno"></param>
    /// <returns></returns>
    public static bool IsValidTSDPartno_06(string tfc, string partno)
    {

        //  bool result = false;

        string dbpartno = string.Empty;

        string sql = string.Format("EXECUTE  [dbo].[GetTSDValidPartNumberFrom06]  '{0}', '{1}'", tfc, partno);

        SqlDataReader dreader = SqlHelper.ExecuteReader(ETAConnectionString, CommandType.Text, sql);

        while (dreader.Read())
        {
            dbpartno = ((IDataRecord)dreader)[0].ToString();
        }

        // Call Close when done reading.
        dreader.Close();

        if (dbpartno == "Valid")
        {

            return true;
        }
        else
        {

            return false;
        }

    }

    public static DataSet GetFirstPartNoWithMinorFromStandPartsList(string partno, string minor)
    {
        DataSet result = null;
        object[] objParams = { 0 };

        string sql = string.Format("Select distinct(PartNo) FROM ETA.dbo.viewStandardParts where  partno like '{0}%' and Minor='{1}'", partno, minor);

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);

        return result;
    }

    public static DataSet GetInfoFromPartsListItems(string itemid)
    {
        DataSet result = null;
        object[] objParams = { 0 };
        string sSql = string.Format("SELECT LineNumber,HeaderId,DesignNumber FROM ETA.dbo.PartsListItems WHERE ItemId={0} ", itemid);
        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sSql);

        return result;
    }


    public static DataSet GetFormCItemstatusInfo(string citemid)
    {
        DataSet result = null;
        object[] objParams = { 0 };
        string sSql = string.Format("SELECT ModuleLocation FROM ETA.dbo.FormCItems WHERE CitemId={0} ", citemid);
        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sSql);

        return result;
    }

    public static DataSet GetPackageLockStatusInfo(string packagename)
    {
        DataSet result = null;
        object[] objParams = { 0 };
        //string sSql = "SELECT count(*) from dbo.viewFormCItems WHERE DesignNumber='" + packagename + "' and left(CategoryAddress, 3)='" + txtcode1 + "' and assycode='" + txtcode2 + "' and partcode='" + txtcode3 + "' and pagecode='" + txtcode4 + "'";
        string sSql = string.Format("SELECT EditLock FROM ETA.dbo.PackageLog WHERE DesignNumber='{0}'", packagename);
        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sSql);

        return result;
    }

    public static DataSet GetCitemInfo(string packagename, string module)
    {
        DataSet result = null;
        object[] objParams = { 0 };
        string sSql = string.Format("SELECT CategoryAddress+ ' | ' + CONVERT(varchar (10), [key])+' | '+ AssyCode+' | '+Treatment+' | '+PartCode+ ' | ' + PageCode as TextValue, CitemId,CategoryAddress,[key],AssyCode,Treatment,PartCode,PageCode FROM ETA.dbo.viewFormCitems WHERE DesignNumber='{0}' AND ModuleLocation='{1}' ORDER BY CategoryAddress ", packagename, module);
        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sSql);

        return result;
    }





    public static int UpdatePartListHeader(int headerid, string tname1, string tname2, string tname3, string tname4, string tcode1, string tcode2, string tcode3, string tcode4)
    {

        int ReturnValue;
        //  object[] objParams = { 0 };
        string sSql = "UPDATE ETA.dbo.PartsListHeaders " +
                      "SET Name1='" + tname1 + "'," +
                          "Name2='" + tname2 + "'," +
                          "Name3='" + tname3 + "'," +
                          "Name4='" + tname4 + "'," +
                          "Code1='" + tcode1 + "'," +
                          "Code2='" + tcode2 + "'," +
                          "Code3='" + tcode3 + "'," +
                          "Code4='" + tcode4 + "' " +
                      "WHERE HeaderId=" + headerid;


        using (SqlConnection conn = new SqlConnection(ETAConnectionString))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    //
                    ReturnValue = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sSql);

                    // throw new Exception("error");
                    trans.Commit();

                }
                catch (Exception err)
                {
                    trans.Rollback();
                    ReturnValue = -1;
                }
            }
        }



        return ReturnValue;


    }



    /// <summary>
    /// Gets the part list header validate result.
    /// </summary>
    /// <param name="packagename">The packagename.</param>
    /// <param name="txtcode1">The txtcode1.</param>
    /// <param name="txtcode2">The txtcode2.</param>
    /// <param name="txtcode3">The txtcode3.</param>
    /// <param name="txtcode4">The txtcode4.</param>
    /// <returns></returns>
    public static DataSet GetPartListHeaderValidateResult(string packagename, string txtcode1, string txtcode2, string txtcode3, string txtcode4)
    {
        DataSet result = null;
        object[] objParams = { 0 };
        //string sSql = "SELECT count(*) from dbo.viewFormCItems WHERE DesignNumber='" + packagename + "' and left(CategoryAddress, 3)='" + txtcode1 + "' and assycode='" + txtcode2 + "' and partcode='" + txtcode3 + "' and pagecode='" + txtcode4 + "'";
        string sSql = string.Format("SELECT count(*) from dbo.viewFormCItems WHERE DesignNumber='{0}' and left(CategoryAddress, 3)='{1}' and assycode='{2}' and partcode='{3}' and pagecode='{4}'", packagename, txtcode1, txtcode2, txtcode3, txtcode4);
        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sSql);

        return result;
    }

    /// <summary>
    /// Gets the name of the ECI status info by package.
    /// </summary>
    /// <param name="packagename">The packagename.</param>
    /// <returns></returns>
    public static DataSet GetECIStatusInfoByPackageName(string packagename)
    {
        DataSet result = null;
        object[] objParams = { 0 };

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, "SELECT EciStart,EciNumber FROM eci.dbo.Eci WHERE DesignNumber='" + packagename + "' AND EciFrom IS NULL");


        return result;
    }

    /// <summary>
    /// Gets the name of the lock info by package.
    /// </summary>
    /// <param name="packagename">The packagename.</param>
    /// <returns></returns>
    public static DataSet GetLockInfoByPackageName(string packagename)
    {
        DataSet result = null;
        object[] objParams = { 0 };

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, "SELECT EditLock,ReleaseDate FROM ETA.dbo.PackageLog WHERE DesignNumber='" + packagename + "'");


        return result;
    }
    /// <summary>
    /// Gets the name of the part list info by package.
    /// </summary>
    /// <param name="packagename">The packagename.</param>
    /// <returns></returns>
    public static DataSet GetPartListInfoByPackageName(string packagename)
    {
        DataSet result = null;
        object[] objParams = { 0 };
        //SELECT headerid ,code1 FROM ETA.dbo.viewPartsList AS m WHERE m.DesignNumber LIKE 'XXX0%' group by  code1,headerid order by code1
        string sql = string.Format("SELECT headerid ,code1 FROM ETA.dbo.viewPartsList AS m WHERE m.DesignNumber LIKE '{0}%' group by  code1,headerid order by code1", packagename);
        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);
        //"SELECT distinct Headerid FROM ETA.dbo.viewPartsList AS m WHERE m.DesignNumber LIKE '" + packagename + "%' ORDER BY m.HeaderId");


        return result;
    }

    public static DataSet GetPartListInfoByModuleName(string module)
    {
        DataSet result = null;
        object[] objParams = { 0 };
        string sql = string.Format("SELECT  headerid ,code1 FROM ETA.dbo.viewPartsList AS m WHERE m.DesignNumber = '{0}' group by headerid, code1 order by code1", module);
        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);
        //"SELECT distinct Headerid FROM ETA.dbo.viewPartsList AS m WHERE m.DesignNumber = '" + module + "' ORDER BY m.HeaderId");


        return result;
    }


    public static DataTable GetPartNumbersByModuleName(string module)
    {
        DataSet result = null;
        object[] objParams = { 0 };
        string sql = string.Format("SELECT  partnumber,designnumber,itemid FROM ETA.dbo.viewPartsList AS m WHERE m.DesignNumber = '{0}' order by itemid desc", module);
        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);
        //"SELECT distinct Headerid FROM ETA.dbo.viewPartsList AS m WHERE m.DesignNumber = '" + module + "' ORDER BY m.HeaderId");

        DataTable dt = result.Tables[0];

        DataColumn dc = new DataColumn("PartNoValidationStatus", System.Type.GetType("System.Boolean"));


        dt.Columns.Add(dc);
        string stringresult = string.Empty;
        foreach (DataRow dr in dt.Rows)
        {

            if (CommonTool.IsvalidPartnoNew(dr["partnumber"].ToString(), dr["designnumber"].ToString().Substring(0, 4), 0, ref stringresult) == -1)
            {

                dr["PartNoValidationStatus"] = false;
            }
            else
            {
                dr["PartNoValidationStatus"] = true;

            }
        }

        return dt;


    }
    /// <summary>
    /// Gets the part list info by package name and header id.
    /// </summary>
    /// <param name="packagename">The packagename.</param>
    /// <param name="headerid">The headerid.</param>
    /// <param name="moduleflag">if set to <c>true</c> [moduleflag].</param>
    /// <returns></returns>
    public static DataSet GetPartListInfoByPackageNameAndHeaderId(string packagename, string headerid, bool moduleflag)
    {
        //          SELECT     TOP (100) PERCENT dbo.PartsListHeaders.HeaderId, dbo.PartsListHeaders.STARTINGPAGE, dbo.PartsListHeaders.CODE1, 
        //                      dbo.PartsListHeaders.DESIGNNUMBER, dbo.PartsListHeaders.HEADING, dbo.PartsListHeaders.ORIGINAL, dbo.PartsListHeaders.NAME1, 
        //                      dbo.PartsListHeaders.NAME2, dbo.PartsListHeaders.NAME3, dbo.PartsListHeaders.NAME4, dbo.PartsListHeaders.CODE2, 
        //                      dbo.PartsListHeaders.CODE3, dbo.PartsListHeaders.CODE4, dbo.PartsListHeaders.CODE5, dbo.PartsListHeaders.CItemId, 
        //                      dbo.PartsListHeaders.SubOfItem, dbo.PartsListItems.ItemId, dbo.PartsListItems.HeaderId AS ItemHeaderId, 
        //                      dbo.PartsListItems.DesignNumber AS ItemDesignNumber, dbo.PartsListItems.LineNumber, dbo.PartsListItems.[Level], dbo.PartsListItems.PartNumber, 
        //                      dbo.PartsListItems.PartName, dbo.PartsListItems.SubQuantity, dbo.PartsListItems.Material, dbo.PartsListItems.MaterialSize, 
        //                      dbo.PartsListItems.Drawing, dbo.PartsListItems.Sp, dbo.PartsListItems.Comment1, dbo.PartsListItems.Comment2, 
        //                      LEFT(dbo.PartsListHeaders.DESIGNNUMBER, 4) AS NUM1, dbo.ChangeLogParts.Rev
        //FROM         dbo.PartsListHeaders INNER JOIN
        //                      dbo.PartsListItems ON dbo.PartsListHeaders.HeaderId = dbo.PartsListItems.HeaderId LEFT OUTER JOIN
        //                      dbo.ChangeLogParts ON dbo.PartsListItems.ItemId = dbo.ChangeLogParts.ItemId
        //ORDER BY NUM1, dbo.PartsListHeaders.CODE1, dbo.PartsListHeaders.CODE2, dbo.PartsListHeaders.CODE3, dbo.PartsListHeaders.CODE4, 
        //                      dbo.PartsListHeaders.CODE5, dbo.PartsListHeaders.HeaderId, dbo.PartsListItems.LineNumber

        DataSet result = null;
        object[] objParams = { 0 };
        if (moduleflag)
        {
            //viewPartsList_DY
            result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, "SELECT * FROM ETA.dbo.viewPartsList AS m WHERE m.DesignNumber = '" + packagename + "' and  HeaderId=" + headerid + " ORDER BY m.NUM1, m.CODE1, m.CODE2, m.CODE3, m.CODE4, m.CODE5, m.HeaderId, m.LineNumber");
        }
        else
        {
            result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, "SELECT * FROM ETA.dbo.viewPartsList AS m WHERE m.DesignNumber LIKE '" + packagename + "%' and  HeaderId=" + headerid + " ORDER BY m.NUM1, m.CODE1, m.CODE2, m.CODE3, m.CODE4, m.CODE5, m.HeaderId, m.LineNumber");

        }

        return result;
    }

    /// <summary>
    /// Gets the part list header info by package name and header id.
    /// </summary>
    /// <param name="packagename">The packagename.</param>
    /// <param name="headerid">The headerid.</param>
    /// <returns></returns>
    public static DataSet GetPartListHeaderInfoByPackageNameAndHeaderId(string packagename, string headerid)
    {
        DataSet result = null;
        object[] objParams = { 0 };

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, "SELECT Rev,RevComment,p.EciNumber,CommentDate,RevInitials FROM ETA.dbo.EciHeadParts as p JOIN eci.dbo.Eci as e ON p.EciNumber=e.EciNumber WHERE left(p.EciNumber,4)='" + packagename + "' and  HeaderId=" + headerid + " ORDER BY Rev");

        //result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, "SELECT Rev,RevComment,p.EciNumber,CommentDate,RevInitials FROM ETA.dbo.EciHeadParts as p JOIN eci.dbo.Eci as e ON p.EciNumber=e.EciNumber WHERE left(p.EciNumber,4)='" + packagename + "' ORDER BY Rev");


        return result;
    }




    /// <summary>
    /// Gets the key A by eci number.
    /// </summary>
    /// <param name="ecinumber">The ecinumber.</param>
    /// <returns></returns>
    public static DataSet GetKeyAByEciNumber(string ecinumber)
    {
        DataSet result = null;

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, "SELECT KeyA FROM eci.dbo.EciACitems WHERE EciNumber='" + ecinumber + "' ordER BY KeyA DESC");


        return result;
    }

    /// <summary>
    /// Gets the character standard part number count.
    /// G851 (561)(01)C-01
    /// </summary>
    /// <param name="partno">The partno.</param>
    /// <param name="model">The model.</param>
    /// <returns></returns>
    public static DataSet GetCharacterStandardPartNumberCount(string partno, string model)
    {
        DataSet result = null;
        object[] objParams = { 0 };
        //SELECT   *  FROM ETA.dbo.viewStandardParts WHERE Partno like '56101C-01%' and model like 'G851%'
        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, "SELECT  top 1 *  FROM ETA.dbo.viewStandardParts WHERE Partno like '" + partno + "%' and model like '" + model + "%'");
        return result;
    }

    /// <summary>
    /// Gets the part number count.
    /// </summary>
    /// <param name="partno">The partno.</param>
    /// <returns></returns>
    public static DataSet GetPartNumberCount(string partno, bool removedash)
    {
        DataSet result = null;
        object[] objParams = { 0 };
        //"SELECT  top 1 *  FROM ETA.dbo.viewStandardParts WHERE Partno like '" & Replace(partnumber,"-","") & "%'" 
        if (removedash)
        {
            result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, "SELECT  top 1 *  FROM ETA.dbo.viewStandardParts WHERE Partno like '" + partno.Replace("-", "") + "%'");
        }
        else
        {
            result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, "SELECT  top 1 *  FROM ETA.dbo.viewStandardParts WHERE Partno like '" + partno + "%'");
        }

        return result;
    }


    public static DataSet GetECINumberCountByItemId(string itemid, string ecinumber)
    {
        DataSet result = null;
        //object[] objParams = { 0 };
        //"SELECT  top 1 *  FROM ETA.dbo.viewStandardParts WHERE Partno like '" & Replace(partnumber,"-","") & "%'" 
        // result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, "SELECT HeaderId FROM ETA.dbo.PartsListItems WHERE ItemId=" + itemid);
        //SELECT    EciHeadParts.EciNumber FROM   PartsListItems inner JOIN
        //                      EciHeadParts ON PartsListItems.HeaderId = EciHeadParts.HeaderId where 
        //EciHeadParts.EciNumber = 'N9JUE0001'
        //and PartsListItems.ItemId = 3870588

        string sql = string.Format("SELECT    EciHeadParts.EciNumber FROM   PartsListItems inner JOIN  EciHeadParts ON PartsListItems.HeaderId = EciHeadParts.HeaderId where EciHeadParts.EciNumber = '{0}' and PartsListItems.ItemId ={1} ", ecinumber, itemid);

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);

        return result;
    }


    public static DataSet GetPartListHeaderIDByItemId(string itemid)
    {
        DataSet result = null;
        object[] objParams = { 0 };
        //"SELECT  top 1 *  FROM ETA.dbo.viewStandardParts WHERE Partno like '" & Replace(partnumber,"-","") & "%'" 
        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, "SELECT HeaderId FROM ETA.dbo.PartsListItems WHERE ItemId=" + itemid);
        //SELECT    EciHeadParts.EciNumber FROM   PartsListItems inner JOIN
        //                      EciHeadParts ON PartsListItems.HeaderId = EciHeadParts.HeaderId where 
        //EciHeadParts.EciNumber = 'N9JUE0001'
        //and PartsListItems.ItemId = 3870588

        //string sql = string.Format("SELECT    EciHeadParts.EciNumber FROM   PartsListItems inner JOIN  EciHeadParts ON PartsListItems.HeaderId = EciHeadParts.HeaderId where EciHeadParts.EciNumber = '{0}' and PartsListItems.ItemId ={1} ", ecinumber, itemid);

        //result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);

        return result;
    }


    public static string InsertNewPartListWithTransactionForInsert(ref string result2, string package, string ecinumber, string headerid, string sinital, int insertstart, DataTable dt, string trapfile, string module, string ecimode, string eciacid, string traperror, bool isbefore)
    {



        string ReturnValue = null;
        string sql = null;
        using (SqlConnection conn = new SqlConnection(ETAConnectionString))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    sql = "SELECT Rev FROM  eci.dbo.Eci WHERE DesignNumber='" + package + "' ORDER BY Rev DESC";
                    DataSet ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);

                    string currentrev = null;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        currentrev = ds.Tables[0].Rows[0][0].ToString().Trim();
                    }

                    // '**Get ECI header info
                    sql = "SELECT EciNumber FROM ETA.dbo.EciHeadParts WHERE EciNumber='" + ecinumber + "' AND HeaderId=" + headerid;

                    ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        // insert '**Create Header
                        sql = "INSERT ETA.dbo.EciHeadParts (HeaderId,EciNumber,RevInitials) VALUES ('" + headerid + "','" + ecinumber + "','" + sinital + "')";
                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);
                    }


                    //	'**Loop through text boxes to find new records

                    int AddCount = 0;
                    int LineNumber = insertstart;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        //'**********   Start Trap   **********

                        string txtValue = dt.Rows[i]["partno"].ToString();

                        bool logstatus = Convert.ToBoolean(dt.Rows[i]["ecilog"]);
                        string Vlevel = dt.Rows[i]["level"].ToString();
                        string VQty = dt.Rows[i]["qty"].ToString();
                        string VPartName = dt.Rows[i]["partname"].ToString();
                        VPartName = VPartName.Replace("'", "`");
                        VPartName = VPartName.Replace("\"", "``");

                        string VDwg = dt.Rows[i]["dwg"].ToString();
                        string VSize = dt.Rows[i]["size"].ToString();
                        string vMaterial = dt.Rows[i]["material"].ToString();


                        CommonTool.WriteToFile(trapfile, string.Format("{0}AddCount={1}", System.Environment.NewLine, AddCount), true);
                        CommonTool.WriteToFile(trapfile, string.Format("{0}LineNumber={1}", System.Environment.NewLine, LineNumber), true);
                        CommonTool.WriteToFile(trapfile, string.Format("{0}i={1}", System.Environment.NewLine, i), true);
                        CommonTool.WriteToFile(trapfile, string.Format("{0}txtItem=txtPartNum{1}", System.Environment.NewLine, i), true);
                        CommonTool.WriteToFile(trapfile, string.Format("{0}txtValue={1}", System.Environment.NewLine, txtValue), true);


                        if (!string.IsNullOrEmpty(txtValue))
                        {
                            CommonTool.WriteToFile(trapfile, string.Format("{0}Item=selLevel{1}", System.Environment.NewLine, i), true);
                            CommonTool.WriteToFile(trapfile, string.Format("{0}vLevel={1}", System.Environment.NewLine, Vlevel), true);

                            CommonTool.WriteToFile(trapfile, string.Format("{0}Item=txtPartNum{1}", System.Environment.NewLine, i), true);
                            CommonTool.WriteToFile(trapfile, string.Format("{0}vLevel={1}", System.Environment.NewLine, txtValue), true);

                            CommonTool.WriteToFile(trapfile, string.Format("{0}Item=txtPartName{1}", System.Environment.NewLine, i), true);
                            CommonTool.WriteToFile(trapfile, string.Format("{0}vPartName={1}", System.Environment.NewLine, VPartName), true);


                            CommonTool.WriteToFile(trapfile, string.Format("{0}Item=txtQty{1}", System.Environment.NewLine, i), true);
                            CommonTool.WriteToFile(trapfile, string.Format("{0}vQty={1}", System.Environment.NewLine, VQty), true);

                            CommonTool.WriteToFile(trapfile, string.Format("{0}Item=txtMaterial{1}", System.Environment.NewLine, i), true);
                            CommonTool.WriteToFile(trapfile, string.Format("{0}vMaterial={1}", System.Environment.NewLine, vMaterial), true);

                            CommonTool.WriteToFile(trapfile, string.Format("{0}Item=txtSize{1}", System.Environment.NewLine, i), true);
                            CommonTool.WriteToFile(trapfile, string.Format("{0}vSize={1}", System.Environment.NewLine, VSize), true);

                            CommonTool.WriteToFile(trapfile, string.Format("{0}Item=selDwg{1}", System.Environment.NewLine, i), true);
                            CommonTool.WriteToFile(trapfile, string.Format("{0}vDwg={1}", System.Environment.NewLine, VDwg), true);

                            CommonTool.WriteToFile(trapfile, string.Format("{0}Item=txtaComment{1}", System.Environment.NewLine, i), true);
                            CommonTool.WriteToFile(trapfile, string.Format("{0}vComment={1}", System.Environment.NewLine, dt.Rows[i]["comments"].ToString()), true);

                            CommonTool.WriteToFile(trapfile, string.Format("{0}Item=chkLog{1}", System.Environment.NewLine, i), true);
                            CommonTool.WriteToFile(trapfile, string.Format("{0}vLog={1}", System.Environment.NewLine, logstatus), true);


                            // change by dayang on 12/16/09
                            // Reset all line numbers with this headerid 
                            if (isbefore)
                            {
                                //UPDATE    PartsListItems SET LineNumber = LineNumber - 1 WHERE  (HeaderId = 1061119) and (DesignNumber = 'XXX0-18') and Linenumber<=40
                                sql = string.Format("UPDATE    PartsListItems SET LineNumber = LineNumber + 1 WHERE  (HeaderId ={0} ) and (DesignNumber = '{1}') and Linenumber>={2} ", headerid, module, LineNumber);

                            }
                            else
                            {

                                sql = string.Format("UPDATE  PartsListItems SET LineNumber = LineNumber - 1 WHERE  (HeaderId ={0} ) and (DesignNumber = '{1}') and Linenumber<={2} ", headerid, module, LineNumber);
                            }


                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);
                            //'**Insert New Items	
                            sql = "INSERT ETA.dbo.PartsListItems (DesignNumber,HeaderId,LineNumber ,[level],PartNumber,PartName,SubQuantity,Material,MaterialSize,Drawing,Comment1) VALUES ('" + module + "'," +
                                          headerid + "," + LineNumber + "," +
                                          Vlevel + "," +
                                          "'" + txtValue + "'," +
                                          "'" + VPartName + "'," +
                                          "'" + VQty + "'," +
                                          "'" + vMaterial.Replace("'", "''") + "'," +
                                          "'" + VSize.Replace("'", "''") + "'," +
                                          "'" + VDwg + "'," +
                                          "'" + dt.Rows[i]["comments"].ToString() + "')";


                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                            AddCount += 1;

                            CommonTool.WriteToFile(trapfile, string.Format("{0}sql=={1}", System.Environment.NewLine, sql), true);
                            CommonTool.WriteToFile(trapfile, string.Format("{0}vLevel={1}", System.Environment.NewLine, dt.Rows[i]["level"].ToString()), true);


                            //'**ECI Logging chkecilog

                            if (ecimode == "on" && logstatus == true)
                            {
                                sql = "SELECT IDENT_CURRENT ('ETA.dbo.PartsListItems') as Id";
                                ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    string pitemid = ds.Tables[0].Rows[0][0].ToString();
                                    //'**Check Change Log for existing log to item

                                    sql = "SELECT Rev FROM ETA.dbo.ChangeLogParts WHERE ItemId='" + pitemid + "' AND Rev='" + currentrev + "'";
                                    ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);
                                    if (ds.Tables[0].Rows.Count == 0)
                                    {
                                        sql = "INSERT ETA.dbo.ChangeLogParts (ItemId,Rev) VALUES (" + pitemid + ",'" + currentrev + "')";
                                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);
                                    }

                                    //'**Log New Data to EciPLitems
                                    if (!string.IsNullOrEmpty(eciacid))
                                    {
                                        sql = "INSERT eci.dbo.EciPLitems (EciAcId,NewPartLevel,NewPartNumber,NewQty,NewPartName,NewInstruction,NewDwg,NewMaterial,NewSize) VALUES (" +
                                                  eciacid + "," + Vlevel + ",'" + txtValue + "','" + VQty + "','" +
                                                  VPartName + "','" + "NA','" + VDwg + "','" + vMaterial.Replace("'", "''") + "','" + VSize.Replace("'", "''") + "')";

                                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                                    }//!string.IsNullOrEmpty(eciacid)
                                    else
                                    {
                                        //'**Get Form A, Form C, and Parts List items

                                        sql = "SELECT * FROM ETA.dbo.viewItemsChain WHERE ItemId=" + pitemid;
                                        ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);

                                        string ChangeA = "No Change";
                                        string MajorA = ds.Tables[0].Rows[0]["Major"].ToString();
                                        string MinorA = ds.Tables[0].Rows[0]["Minor"].ToString();
                                        string CommentA = ds.Tables[0].Rows[0]["Comment"].ToString().Replace("'", "''");

                                        //'**Form C info from Package

                                        string ChangeC = "No Change";
                                        string AssyC = ds.Tables[0].Rows[0]["CategoryAddress"].ToString().Substring(0, 3);

                                        //'**Reformat data, if needed.


                                        //'**Form A info from Package
                                        string PartCodeC = null;
                                        string PageCodeC = null;
                                        int assycint = 0;
                                        if (Int32.TryParse(AssyC, out assycint))
                                        {

                                            PartCodeC = package;
                                            PageCodeC = string.Format("{0} {1}", ds.Tables[0].Rows[0]["AssyCode"].ToString(), ds.Tables[0].Rows[0]["PartCode"].ToString());
                                        }
                                        else
                                        {
                                            PartCodeC = ds.Tables[0].Rows[0]["PartCode"].ToString();
                                            PageCodeC = ds.Tables[0].Rows[0]["PageCode"].ToString();

                                        }
                                        string DescriptionC = ds.Tables[0].Rows[0]["Description"].ToString().Replace("'", "''");

                                        sql = "SELECT KeyA FROM eci.dbo.EciACitems WHERE EciNumber='" + ecinumber + "' ORDER BY KeyA DESC";
                                        ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);

                                        string KeyA = null;
                                        if (ds.Tables[0].Rows.Count > 0)
                                        {
                                            // KeyA = CommonTool.Chr(CommonTool.Asc(ds.Tables[0].Rows[0][0].ToString().Trim()) + 1);
                                            if (string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString().Trim()))
                                            {
                                                KeyA = "A";
                                            }
                                            else
                                            {

                                                KeyA = CommonTool.Chr(CommonTool.Asc(ds.Tables[0].Rows[0][0].ToString().Trim()) + 1);
                                            }


                                        }
                                        else
                                        {
                                            KeyA = "A";

                                        }
                                        //'**Log Package Data to EciACitems
                                        sql = "INSERT eci.dbo.EciACitems (EciNumber,ChangeA,KeyA,MajorA,MinorA,CommentA," +
                                              "KeyC,ChangeC,AssyC,PartCodeC,PageCodeC,DescriptionC) " +
                                              "VALUES ('" + ecinumber + "','" +
                                                  ChangeA + "','" +
                                                  KeyA + "','" +
                                                  MajorA + "','" +
                                                  MinorA + "','" +
                                                  CommentA + "','" +
                                                  "Z9','" +
                                                  ChangeC + "','" +
                                                  AssyC + "','" +
                                                  PartCodeC + "','" +
                                                  PageCodeC + "','" +
                                                  DescriptionC + "')";

                                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                                        //'**Get EciACid
                                        sql = "SELECT IDENT_CURRENT ('eci.dbo.EciACitems') as Id";
                                        ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);
                                        if (ds.Tables[0].Rows.Count > 0)
                                        {
                                            eciacid = ds.Tables[0].Rows[0][0].ToString();
                                        }

                                        //'**Log New Data to EciPLitems
                                        sql = "INSERT eci.dbo.EciPLitems (EciAcId,NewPartLevel,NewPartNumber,NewQty,NewPartName,NewInstruction,NewDwg,NewMaterial,NewSize) VALUES (" +
                                                 eciacid + "," + Vlevel + ",'" + txtValue + "','" + VQty + "','" +
                                                 VPartName + "','" + "NA','" + VDwg + "','" + vMaterial.Replace("'", "''") + "','" + VSize.Replace("'", "''") + "')";
                                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);


                                    }//string.IsNullOrEmpty(eciacid)

                                }// ds.Tables[0].Rows.Count > 0

                            }//ecimode == "on" && logstatus == true
                            else
                            {
                                if ((AddCount == 0) && (i == 6))
                                {
                                    result2 = "<FONT size=4 color=red><STRONG>No Input Error.<br>No information added.<br><br>  Go Back to Add Items.</STRONG></FONT>";
                                }

                            }///ecimode == "on" && logstatus == true
                        }

                    }       // end for i=6           


                    //'************************************************************************************	
                    //'**************Resequence Line Numbers and split to new headers if needed************
                    //'**Get variables for Page Group
                    sql = "SELECT HeaderId,Code1,Code2,Code3,Code4,Code5,Name1,Name2,Name3 FROM ETA.dbo.PartsListHeaders WHERE HeaderId = '" + headerid + "'";
                    ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);


                    string Code1 = null;
                    string Code2 = null;
                    string Code3 = null;
                    string Code4 = null;
                    string Name1 = null;
                    string Name2 = null;
                    string Name3 = null;


                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Code1 = ds.Tables[0].Rows[0]["Code1"].ToString();
                        Code2 = ds.Tables[0].Rows[0]["Code2"].ToString();
                        Code3 = ds.Tables[0].Rows[0]["Code3"].ToString();
                        Code4 = ds.Tables[0].Rows[0]["Code4"].ToString();
                        Name1 = ds.Tables[0].Rows[0]["Name1"].ToString();
                        Name2 = ds.Tables[0].Rows[0]["Name2"].ToString();
                        Name3 = ds.Tables[0].Rows[0]["Name3"].ToString();

                        if (!string.IsNullOrEmpty(Code4))
                        {
                            Code4 = string.Format("='{0}')", Code4);
                        }
                        else
                        {
                            Code4 = " IS NULL OR Code4='')";
                        }



                    }//end ds.Tables[0].Rows.Count
                    CommonTool.WriteToFile(trapfile, string.Format("{0}Line 301 sSql={1}", System.Environment.NewLine, sql), true);
                    CommonTool.WriteToFile(trapfile, string.Format("{0}Code1={1}", System.Environment.NewLine, Code1), true);
                    CommonTool.WriteToFile(trapfile, string.Format("{0}Code2={1}", System.Environment.NewLine, Code2), true);
                    CommonTool.WriteToFile(trapfile, string.Format("{0}Code3={1}", System.Environment.NewLine, Code3), true);
                    CommonTool.WriteToFile(trapfile, string.Format("{0}Code4={1}", System.Environment.NewLine, Code4), true);
                    CommonTool.WriteToFile(trapfile, string.Format("{0}Name1={1}", System.Environment.NewLine, Name1), true);
                    CommonTool.WriteToFile(trapfile, string.Format("{0}Name2={1}", System.Environment.NewLine, Name2), true);
                    CommonTool.WriteToFile(trapfile, string.Format("{0}Name3={1}", System.Environment.NewLine, Name3), true);
                    CommonTool.WriteToFile(trapfile, string.Format("{0}Code4={1}", System.Environment.NewLine, Code4), true);

                    //  '**Get Headers for this Page series***************************************
                    sql = "SELECT HeaderId,Code1,Code2,Code3,Code4,Code5,CitemId,SubOfItem,DesignNumber,Name1,Name2,Name3 FROM ETA.dbo.PartsListHeaders WHERE DesignNumber = '" +
                                module + "' AND Code1='" + Code1 + "' AND Code2='" + Code2 + "' AND Code3='" + Code3 + "' AND (Code4" + Code4 + " ORDER BY Code1,Code2,Code3,Code4,Code5";

                    DataSet dsHeader = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);

                    int HeaderCount = 0;
                    //'**Open Headers recordset object
                    //'**Need to change cursor type to allow SQL query update method.  The rs.Update 
                    //'	is not valid with SQL Server 2005. KDL
                    //'rsHeaders.Open sSql,cn,adOpenKeyset,adLockPessimistic


                    // '**Find All HeaderIds
                    //'**Build string of Headers for SQL string
                    //'First HeaderId


                    if (ds.Tables[0].Rows.Count > 0)
                    {


                        string strHeaderIds = dsHeader.Tables[0].Rows[HeaderCount]["HeaderId"].ToString();
                        string CitemId = dsHeader.Tables[0].Rows[HeaderCount]["CitemId"].ToString();
                        string SubOfItem = dsHeader.Tables[0].Rows[HeaderCount]["SubOfItem"].ToString();
                        if (string.IsNullOrEmpty(SubOfItem))
                        {
                            SubOfItem = "NULL";
                        }

                        CommonTool.WriteToFile(trapfile, string.Format("{0}Line 337 sSql={1}", System.Environment.NewLine, sql), true);
                        CommonTool.WriteToFile(trapfile, string.Format("{0}={1}", System.Environment.NewLine, strHeaderIds), true);

                        for (int i = 1; i < dsHeader.Tables[0].Rows.Count; i++)
                        {
                            strHeaderIds = strHeaderIds + " OR HeaderId=" + dsHeader.Tables[0].Rows[i]["HeaderId"].ToString();
                            // '**********   Start Trap   **********344
                            CommonTool.WriteToFile(trapfile, string.Format("{0}Line 344 Do Loop", System.Environment.NewLine), true);
                            CommonTool.WriteToFile(trapfile, string.Format("{0}={1}", System.Environment.NewLine, strHeaderIds), true);

                        }// end for

                        // '**Get Items for this Page series******************************************
                        sql = "SELECT DesignNumber,HeaderId,LineNumber,ItemId FROM ETA.dbo.PartsListItems WHERE DesignNumber='" + module + "' AND (HeaderId=" + strHeaderIds + ") ORDER BY LineNumber";

                        ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);

                        //'**Commence resequence*****************************************************
                        // '**Set Initial Values
                        int LineCount = 10;
                        string Page = "1";
                        int ipage = 1;

                        int ItemPosition = 1;
                        CommonTool.WriteToFile(trapfile, string.Format("{0}Line 361 sSql={1}", System.Environment.NewLine, sql), true);
                        CommonTool.WriteToFile(trapfile, string.Format("{0}LineCount={1}", System.Environment.NewLine, LineCount), true);
                        CommonTool.WriteToFile(trapfile, string.Format("{0}Page={1}", System.Environment.NewLine, Page), true);
                        CommonTool.WriteToFile(trapfile, string.Format("{0}CitemId={1}", System.Environment.NewLine, CitemId), true);
                        CommonTool.WriteToFile(trapfile, string.Format("{0}SubOfItem={1}", System.Environment.NewLine, SubOfItem), true);
                        CommonTool.WriteToFile(trapfile, string.Format("{0}ItemPosition={1}", System.Environment.NewLine, ItemPosition), true);

                        // '**Loop through records, resetting Header Page Numbers and Item HeaderIds
                        //Do until ItemPosition=rsItems.RecordCount + 1


                        do
                        {

                            //'**Set Line Numbers incremented by 10
                            int ItemCount = 1;
                            //  '**Set Page number
                            //'rsHeaders("Code5")=Page
                            string sHeadSql = "UPDATE ETA.dbo.PartsListHeaders SET Code5='" + Page + "' WHERE HeaderID=" + dsHeader.Tables[0].Rows[HeaderCount]["HeaderId"].ToString();
                            //'response.write sHeadSql & "<BR>"
                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sHeadSql);
                            CommonTool.WriteToFile(trapfile, string.Format("{0}Line 378 Do Loop", System.Environment.NewLine), true);
                            CommonTool.WriteToFile(trapfile, string.Format("{0}rsItems.RecordCount={1}", System.Environment.NewLine, ds.Tables[0].Rows.Count), true);
                            CommonTool.WriteToFile(trapfile, string.Format("{0}ItemCount={1}", System.Environment.NewLine, ds.Tables[0].Rows.Count), true);
                            CommonTool.WriteToFile(trapfile, string.Format("{0}Line 386 sHeadSql={1}", System.Environment.NewLine, sHeadSql), true);

                            //Do Until ItemCount=14 OR ItemPosition=rsItems.RecordCount + 1 Or rsItems.eof




                            do
                            {

                                string sItemSql = "UPDATE ETA.dbo.PartsListItems SET LineNumber=" + LineCount + " WHERE ItemId=" + ds.Tables[0].Rows[ItemPosition - 1]["ItemId"].ToString();
                                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sHeadSql);
                                CommonTool.WriteToFile(trapfile, string.Format("{0}Line 393 sItemSql={1}", System.Environment.NewLine, sItemSql), true);
                                CommonTool.WriteToFile(trapfile, string.Format("{0}ItemCount={1}", System.Environment.NewLine, ItemCount), true);

                                //'**set header ID
                                //'rsItems("HeaderId")=rsHeaders.Fields("HeaderId").Value

                                sItemSql = "UPDATE ETA.dbo.PartsListItems SET HeaderId=" + dsHeader.Tables[0].Rows[HeaderCount]["HeaderId"].ToString() + " WHERE ItemId=" + ds.Tables[0].Rows[ItemPosition - 1]["ItemId"].ToString();

                                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                                ItemPosition = ItemPosition + 1;
                                LineCount = LineCount + 10;
                                ItemCount = ItemCount + 1;

                                CommonTool.WriteToFile(trapfile, string.Format("{0}ItemCount={1}", System.Environment.NewLine, ItemCount), true);
                                CommonTool.WriteToFile(trapfile, string.Format("{0}ItemPosition={1}", System.Environment.NewLine, ItemPosition), true);
                                CommonTool.WriteToFile(trapfile, string.Format("{0}LineCount={1}", System.Environment.NewLine, LineCount), true);
                                CommonTool.WriteToFile(trapfile, string.Format("{0}Page={1}", System.Environment.NewLine, Page), true);

                                //	If ItemPosition=rsItems.RecordCount + 1 AND Left(Page,1)<>"E" THEN

                                if (ItemPosition == ds.Tables[0].Rows.Count && (Page.ToString().Substring(0, 1) != "E"))
                                //   Page.ToString().Substring(0, 1) != "E"))
                                {
                                    //   Page = "E" + Page;

                                    Page = string.Format("E{0}", Page);

                                    sHeadSql = "UPDATE ETA.dbo.PartsListHeaders SET Code5='" + Page +
                         "' WHERE HeaderID=" + dsHeader.Tables[0].Rows[HeaderCount]["HeaderId"].ToString();
                                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);
                                    CommonTool.WriteToFile(trapfile, string.Format("{0}Line 414 sHeadSql={1}", System.Environment.NewLine, sHeadSql), true);

                                }



                            }
                            while (ItemCount < 14 && (ItemPosition - 1) < ds.Tables[0].Rows.Count && ds.Tables[0].Rows.Count == 0);

                            //'rsHeaders.Update
                            if (dsHeader.Tables[0].Rows.Count == 0)
                            {
                                result2 += "rsHeaders.eof or bof <BR>";
                            }

                            else
                            {
                                HeaderCount += 1;
                            }

                            if (Page.ToString().Substring(0, 1) != "E")
                            {
                                ipage += 1;
                            }
                            if (dsHeader.Tables[0].Rows.Count == 0 && ItemPosition < ds.Tables[0].Rows.Count + 1)
                            {
                                //Page="E" & Page
                                Page = "E" + ipage.ToString();

                                sHeadSql = "INSERT ETA.dbo.PartsListHeaders (DesignNumber,Name1,Name2,Name3,Code1,Code2,Code3,Code5,CitemId,SubOfItem) " +
                 "VALUES('" + module + "','" +
                 Name1 + "','" +
                 Name2 + "','" +
                 Name3 + "','" +
                 Code1 + "','" +
                 Code2 + "','" +
                 Code3 + "','" +
                 Page + "'," +
                 CitemId + "," +
                 SubOfItem + ")";

                                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                                CommonTool.WriteToFile(trapfile, string.Format("{0}Line 462 sHeadSql=={1}", System.Environment.NewLine, sHeadSql), true);


                            }


                        } while ((ItemPosition - 1) < ds.Tables[0].Rows.Count && HeaderCount < dsHeader.Tables[0].Rows.Count);









                    }//end ds.Tables[0].Rows.Count





                    trans.Commit();
                    ReturnValue = "Success";
                }//end try
                catch (Exception err)
                {
                    //Note: not implement the logic for the following code in asp( i can not be 6 at all)
                    //  ElseIf AddCount=0 AND i=6 THEN
                    //    cn.RollbackTrans
                    //    Response.Write("<FONT size=4 color=red><STRONG>No Input Error.<br>  No information added.<br><br>  Go Back to Add Items.</STRONG></FONT>")
                    //Else

                    trans.Rollback();
                    CommonTool.MoveFile(trapfile, traperror);

                    ReturnValue = err.Message;
                }
            }
        }// end using sqlconnection
        return ReturnValue;


    }

    /// <summary>
    /// First Insert
    /// </summary>
    /// <param name="result2"></param>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static string InsertTSDNewPartListWithTransactionForInsert(ref string result2, DataTable dt)
    {



        string ReturnValue = null;
        string sql = null;
        using (SqlConnection conn = new SqlConnection(ETAConnectionString))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    /*
                     INSERT INTO [ETA].[dbo].[06_PartsStructureList_TSD]
           ([Indicator]
           ,[TFC]
           ,[INDEX_PARENTPART]
           ,[GP]
           ,[PARENT_CHILDPART]
           ,[QTY]
           ,[SEL]
           ,[CC1]
           ,[COMMENT1]
           ,[CC2]
           ,[COMMENT2]
           ,[CC3]
           ,[COMMENT3]
           ,[CC4]
           ,[COMMENT4]
           ,[CC5]
           ,[COMMENT5]
           ,[FROM_ECI]
           ,[TO_ECI]
           ,[FROM_DATE]
           ,[TO_DATE])
     VALUES
           (<Indicator, nvarchar(50),>
           ,<TFC, nvarchar(50),>
           ,<INDEX_PARENTPART, nvarchar(50),>
           ,<GP, nvarchar(50),>
           ,<PARENT_CHILDPART, nvarchar(50),>
           ,<QTY, nvarchar(50),>
           ,<SEL, nvarchar(50),>
           ,<CC1, nvarchar(50),>
           ,<COMMENT1, nvarchar(50),>
           ,<CC2, nvarchar(50),>
           ,<COMMENT2, nvarchar(50),>
           ,<CC3, nvarchar(50),>
           ,<COMMENT3, nvarchar(50),>
           ,<CC4, nvarchar(50),>
           ,<COMMENT4, nvarchar(50),>
           ,<CC5, nvarchar(50),>
           ,<COMMENT5, nvarchar(50),>
           ,<FROM_ECI, nvarchar(50),>
           ,<TO_ECI, nvarchar(50),>
           ,current time
           ,'0') 
                     */
                    foreach (DataRow dr in dt.Rows)
                    {
                        // string partno = dr["partno"].ToString();
                        //DataTable dtpartno = CommonDB.GetTSDPartentPartnoByChildPart("partno").Tables[0];
                        //string parentpartno = dtpartno.Rows[0][""].ToString();

                        sql =
                            string.Format(
                                "INSERT INTO [ETA].[dbo].[06_PartsStructureList_TSD] ([TFC],[INDEX_PARENTPART],[PARENT_CHILDPART],[QTY],[COMMENT1],[FROM_ECI],[FROM_DATE],[TO_DATE]) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                                dr["tfc"].ToString(), dr["parentpartno"].ToString(), dr["partno"].ToString(), dr["qty"].ToString(),
                                dr["comments"].ToString(), dr["fromeci"].ToString(), dr["fromdate"].ToString(), dr["todate"].ToString());

                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);
                    }
                    trans.Commit();
                    ReturnValue = "Success";
                } //end try
                catch (Exception err)
                {
                    //Note: not implement the logic for the following code in asp( i can not be 6 at all)
                    //  ElseIf AddCount=0 AND i=6 THEN
                    //    cn.RollbackTrans
                    //    Response.Write("<FONT size=4 color=red><STRONG>No Input Error.<br>  No information added.<br><br>  Go Back to Add Items.</STRONG></FONT>")
                    //Else

                    trans.Rollback();


                    ReturnValue = err.Message;
                }

            }
        }
        return ReturnValue;



    }

    /// <summary>
    /// Second Insert
    /// </summary>
    /// <param name="result2"></param>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static string InsertTSDAttachPartListWithTransactionForInsert(ref string result2, DataTable dt, string package, string txtcode1, string txtcode2, string txtcode3, string txtcode4, string txtname2, string txtname3, string txtname4)
    {



        string ReturnValue = null;
        string sql = null;
        using (SqlConnection conn = new SqlConnection(ETAConnectionString))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    // Insert values to Header Table
                    sql =
                          string.Format(
                              "INSERT INTO [ETA].[dbo].[PartsListHeaders]([HEADING],[NAME2],[NAME3],[NAME4],[CODE1],[CODE2],[CODE3],[CODE4],[DESIGNNUMBER],[CItemId]) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','',30508)",
                              package, txtname2, txtname3, txtname4, txtcode1, txtcode2, txtcode3, txtcode4);
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                    // Insert values to PartList table one by one
                    foreach (DataRow dr in dt.Rows)
                    {
                        // string partno = dr["partno"].ToString();
                        //DataTable dtpartno = CommonDB.GetTSDPartentPartnoByChildPart("partno").Tables[0];
                        //string parentpartno = dtpartno.Rows[0][""].ToString();

                        sql =
                            string.Format(
                                "INSERT INTO [ETA].[dbo].[06_PartsStructureList_TSD] ([TFC],[INDEX_PARENTPART],[PARENT_CHILDPART],[QTY],[COMMENT1],[FROM_ECI],[FROM_DATE],[TO_DATE]) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                                dr["tfc"].ToString(), dr["parentpartno"].ToString(), dr["partno"].ToString(), dr["qty"].ToString(),
                                dr["comments"].ToString(), dr["fromeci"].ToString(), dr["fromdate"].ToString(), dr["todate"].ToString());

                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);
                    }
                    trans.Commit();
                    ReturnValue = "Success";
                } //end try
                catch (Exception err)
                {

                    trans.Rollback();


                    ReturnValue = err.Message;
                }

            }
        }
        return ReturnValue;



    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Module"></param>
    /// <param name="Major"></param>
    /// <param name="Name2"></param>
    /// <param name="Name3"></param>
    /// <param name="Code1"></param>
    /// <param name="Code2"></param>
    /// <param name="Code3"></param>
    /// <param name="CitemId"></param>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static string InsertNewPartListWithTransactionForNew(string Module, string Major, string Name2, string Name3, string Code1, string Code2, string Code3, string CitemId, DataTable dt)
    {
        string ReturnValue = null;
        string sql = null;
        using (SqlConnection conn = new SqlConnection(ETAConnectionString))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    // should use string.format 
                    sql = "INSERT ETA.dbo.PartsListHeaders (DesignNumber,Name1,Name2,Name3,Code1,Code2,Code3,Code5,CitemId) VALUES('"
                        + Module + "','" + Major + "','" + Name2 + "','" + Name3 + "','" + Code1 + "','" + Code2 + "','" + Code3 + "','E1'," + CitemId + ")" + "SELECT SCOPE_IDENTITY()";
                    //  SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                    //   sql = "SELECT SCOPE_IDENTITY()";
                    DataSet ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);
                    string headerid = null;
                    int linenumber = 0;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        headerid = ds.Tables[0].Rows[0][0].ToString();
                        // insert all checked rows , upto 13 
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            linenumber += 10;
                            sql = string.Format("INSERT ETA.dbo.PartsListItems (DesignNumber,HeaderId,LineNumber,[Level],PartNumber,PartName,SubQuantity,Material,MaterialSize,Drawing,Comment1)VALUES('{0}',{1},{2},{3},'{4}','{5}','{6}','{7}','{8}','{9}','{10}')", Module, headerid, linenumber, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][6].ToString(), dt.Rows[i][7].ToString());


                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);


                        }

                    }


                    //trans.Rollback();

                    trans.Commit();

                    ReturnValue = "Success";
                }
                catch (Exception err)
                {
                    trans.Rollback();


                    //  Response.Redirect("http://colweb01/test/eta/NoTrans.asp?Message=" + err.Message());
                    ReturnValue = err.Message;
                }
            }
        }// end using sqlconnection
        return ReturnValue;


    }

    public static string InsertPartListWithTransactionForEdit(string itemid, string sinital, string ecinumber, string currentrev, string eciacid, string packagename, string keya, string LevelValue, string PartNumberValue, string QtyValue, string PartNameValue, string DwgValue, string MaterialValue, string SizeValue, string CommentValue, bool vlog, string ecimode)
    {
        string ReturnValue = null;
        string sql = null;
        using (SqlConnection conn = new SqlConnection(ETAConnectionString))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    // get headerid 

                    if (vlog && ecimode == "on")
                    {

                        sql = "SELECT HeaderId FROM ETA.dbo.PartsListItems WHERE ItemId=" + itemid;
                        DataSet ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);

                        string headerid = ds.Tables[0].Rows[0][0].ToString();

                        sql = "SELECT EciNumber FROM ETA.dbo.EciHeadParts WHERE EciNumber='" + ecinumber + "' AND HeaderId=" + headerid;

                        //  sql = string.Format("SELECT    EciHeadParts.EciNumber FROM   PartsListItems inner JOIN  EciHeadParts ON PartsListItems.HeaderId = EciHeadParts.HeaderId where EciHeadParts.EciNumber = '{0}' and PartsListItems.ItemId ={1} ", ecinumber, itemid);
                        ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            // insert '**Create Header
                            sql = "INSERT ETA.dbo.EciHeadParts (HeaderId,EciNumber,RevInitials) VALUES ('" + headerid + "','" + ecinumber + "','" + sinital + "')";
                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);
                        }


                        //'**Check Change Log for existing log to item

                        sql = "SELECT Rev FROM ETA.dbo.ChangeLogParts WHERE ItemId='" + itemid + "' AND Rev='" + currentrev + "'";
                        ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            //'**Log change
                            sql = "INSERT ETA.dbo.ChangeLogParts (ItemId,Rev) VALUES (" + itemid + ",'" + currentrev + "')";
                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);
                        }


                        //'**Get old Form A, Form C, and Parts List items
                        sql = "SELECT * FROM ETA.dbo.viewItemsChain WHERE ItemId=" + itemid;

                        ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);

                        string DescriptionC = null;
                        string levelPP = null;
                        string PartNumberPP = null;

                        string qtyPPP = null;


                        if (String.IsNullOrEmpty(eciacid))
                        {
                            //'**Form A info from Package
                            string ChangeA = "No Change";
                            string MajorA = ds.Tables[0].Rows[0]["Major"].ToString();
                            string MinorA = ds.Tables[0].Rows[0]["Minor"].ToString();
                            string CommentA = ds.Tables[0].Rows[0]["Comment"].ToString().Replace("'", "''");
                            string ChangeC = "No Change";
                            string AssyC = ds.Tables[0].Rows[0]["CategoryAddress"].ToString().Substring(0, 3);
                            string PartCodeC = null;
                            string PageCodeC = null;
                            int assycint = 0;
                            if (Int32.TryParse(AssyC, out assycint))
                            {

                                PartCodeC = packagename;
                                PageCodeC = string.Format("{0} {1}", ds.Tables[0].Rows[0]["AssyCode"].ToString(), ds.Tables[0].Rows[0]["PartCode"].ToString());
                            }
                            else
                            {
                                PartCodeC = ds.Tables[0].Rows[0]["PartCode"].ToString();
                                PageCodeC = ds.Tables[0].Rows[0]["PageCode"].ToString();

                            }
                            DescriptionC = ds.Tables[0].Rows[0]["Description"].ToString().Replace("'", "''");

                            levelPP = ds.Tables[0].Rows[0]["Level"].ToString();

                            PartNumberPP = ds.Tables[0].Rows[0]["PartNumber"].ToString();

                            qtyPPP = ds.Tables[0].Rows[0]["PartNumber"].ToString();

                            // string PartNumberPP = ds.Tables[0].Rows[0]["PartNumber"].ToString();

                            sql = "INSERT eci.dbo.EciACitems (EciNumber,ChangeA,KeyA,MajorA,MinorA,CommentA," +
                                      "KeyC,ChangeC,AssyC,PartCodeC,PageCodeC,DescriptionC) " +
                                      "VALUES ('" + ecinumber + "','" +
                                          ChangeA + "','" +
                                          keya + "','" +
                                          MajorA + "','" +
                                          MinorA + "','" +
                                          CommentA + "','" +
                                          "Z9','" +
                                          ChangeC + "','" +
                                          AssyC + "','" +
                                          PartCodeC + "','" +
                                          PageCodeC + "','" +
                                          DescriptionC + "')";

                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);


                            //'**Get EciACid
                            sql = "SELECT IDENT_CURRENT ('eci.dbo.EciACitems') as Id";
                            DataSet ds1 = SqlHelper.ExecuteDataset(trans, CommandType.Text, sql);
                            eciacid = ds1.Tables[0].Rows[0][0].ToString();


                            //string eciid=
                        } //end acid=""


                        levelPP = ds.Tables[0].Rows[0]["Level"].ToString();
                        PartNumberPP = ds.Tables[0].Rows[0]["PartNumber"].ToString();
                        string qtyPP = ds.Tables[0].Rows[0]["SubQuantity"].ToString();
                        string PartNamePP = ds.Tables[0].Rows[0]["PartName"].ToString();
                        string IntrPP = "NULL";
                        string IntrNP = "NULL";
                        string DwgPP = null;

                        if (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Drawing"].ToString()))
                        {
                            DwgPP = "";
                        }
                        else
                        {
                            DwgPP = ds.Tables[0].Rows[0]["Drawing"].ToString();
                        }

                        string MaterialPP = null;
                        if (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Material"].ToString()))
                        {
                            MaterialPP = "";
                        }
                        else
                        {
                            MaterialPP = ds.Tables[0].Rows[0]["Material"].ToString();
                        }

                        string SizePP = null;
                        if (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MaterialSize"].ToString()))
                        {
                            SizePP = "";
                        }
                        else
                        {
                            SizePP = ds.Tables[0].Rows[0]["MaterialSize"].ToString();
                        }



                        //'**Log Old and New Data to EciPLitems

                        sql = "INSERT eci.dbo.EciPLitems (EciAcId,PrevPartLevel,NewPartLevel," +
                                  "PrevPartNumber,NewPartNumber,PrevQty,NewQty,PrevPartName,NewPartName," +
                                  "PrevInstruction,NewInstruction,PrevIntr,NewIntr,PrevDwg,NewDwg,PrevMaterial," +
                                  "NewMaterial,PrevSize,NewSize) " +
                                  "VALUES (" +
                                      eciacid + ",'" +
                                      levelPP + "','" +
                                      LevelValue + "','" +
                                      PartNumberPP + "','" +
                                      PartNumberValue + "','" +
                                      qtyPP + "','" +
                                      QtyValue + "','" +
                                      PartNamePP + "','" +
                                      PartNameValue + "','" +
                                      "TD','" +
                                      "NA'," +
                                      IntrPP + "," +
                                      IntrNP + ",'" +
                                      DwgPP + "','" +
                                      DwgValue + "','" +
                                      MaterialPP + "','" +
                                      MaterialValue + "','" +
                                      SizePP + "','" +
                                      SizeValue + "')";


                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                    }
                    sql = "UPDATE ETA.dbo.PartsListItems " +
                            "SET [level]='" + LevelValue + "'," +
                                "PartNumber='" + PartNumberValue + "'," +
                                "PartName='" + PartNameValue.Replace("'", "''") + "'," +
                                "SubQuantity='" + QtyValue + "'," +
                                "Material='" + MaterialValue.Replace("'", "''") + "'," +
                                "MaterialSize='" + SizeValue.Replace("'", "''") + "'," +
                                "Drawing='" + DwgValue + "'," +
                                "Comment1='" + CommentValue.Replace("'", "''") + "'" +
                            "WHERE ItemId=" + itemid;
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                    trans.Commit();

                    ReturnValue = "Success";
                }
                catch (Exception err)
                {
                    trans.Rollback();
                    ReturnValue = err.Message;
                }
            }
        }// end using sqlconnection
        return ReturnValue;
    }

    public static string UpdateTSDPartListWithTransactionForEdit(string itemid, string parentpartno, string partno, string qty, string fromeci, string toeci)
    {
        string ReturnValue = null;
        string sql = null;
        using (SqlConnection conn = new SqlConnection(ETAConnectionString))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {// get headerid 


                    sql = "UPDATE [ETA].[dbo].[06_PartsStructureList_TSD] " +
                            "SET [INDEX_PARENTPART]='" + parentpartno + "'," +
                                "[PARENT_CHILDPART]='" + partno + "'," +
                                "QTY='" + qty.Replace("'", "''") + "'," +
                                "[FROM_ECI]='" + fromeci.Replace("'", "''") + "'," +
                                "[TO_ECI]='" + toeci.Replace("'", "''") + "'WHERE ID=" + itemid;
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                    trans.Commit();

                    ReturnValue = "Success";
                }
                catch (Exception err)
                {
                    trans.Rollback();
                    ReturnValue = err.Message;
                }
            }
        }// end using sqlconnection
        return ReturnValue;
    }

    public static string DeleteTSDPartListWithTransaction(string itemid)
    {
        string ReturnValue = null;
        string sql = null;
        using (SqlConnection conn = new SqlConnection(ETAConnectionString))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {// get headerid 


                    sql = "DELETE [ETA].[dbo].[06_PartsStructureList_TSD] WHERE ID=" + itemid;
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                    trans.Commit();

                    ReturnValue = "Success";
                }
                catch (Exception err)
                {
                    trans.Rollback();
                    ReturnValue = err.Message;
                }
            }
        }// end using sqlconnection
        return ReturnValue;
    }

    public static string LinkModuleByPackage(string Packatge, string Module)
    {
        string ReturnValue = null;
        string sql = null;
        using (SqlConnection conn = new SqlConnection(ETAConnectionString))
        {

            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {

                    //sql = string.Format("exec eta.[dbo].[LinkModuleByPackage] '{0}' ,'{1}'", Packatge, Module);
                    //SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);
                    //trans.Commit();


                    //     DataTable dt = null;

                    SqlParameter[] parm = new SqlParameter[2];
                    parm[0] = new SqlParameter("@package", SqlDbType.NVarChar, 50);
                    parm[0].Value = Packatge;
                    parm[0].Direction = ParameterDirection.Input;
                    parm[1] = new SqlParameter("@module", SqlDbType.NVarChar, 50);
                    parm[1].Value = Module;
                    parm[1].Direction = ParameterDirection.Input;

                    SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure,
               "LinkModuleByPackage", parm);


                    trans.Commit();

                    ReturnValue = "Success";
                }
                catch (Exception err)
                {
                    trans.Rollback();
                    ReturnValue = err.Message;

                }
            }
        }

        return ReturnValue;

    }

    public static string UpdatePartNumberByItemid(string partno, string itemid)
    {

        string ReturnValue = null;
        string sql = null;
        using (SqlConnection conn = new SqlConnection(ETAConnectionString))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    sql = string.Format("UPDATE PartsListItems SET Partnumber = '{0}' WHERE    ItemId = {1}", partno, itemid);
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);
                    trans.Commit();

                    ReturnValue = "Success";
                }
                catch (Exception err)
                {
                    trans.Rollback();
                    ReturnValue = err.Message;
                }
            }
        }// end using sqlconnection
        return ReturnValue;

    }

    //public static DataSet GetProductBatteryInfo()
    //{
    //    DataSet result = null;
    //    object[] objParams = { 0 };

    //    result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, "SELECT distinct Headerid FROM ETA.dbo.viewPartsList AS m WHERE m.DesignNumber LIKE 'N9JU%'");


    //    return result;
    //}

    //public static int InsertProductBatteryInfo(string batteryid, string weightlb, string weigthkg)
    //{
    //    int ReturnValue;
    //    object[] objParams = { 0, batteryid, weightlb, weigthkg };
    //    ReturnValue = SqlHelper.ExecuteNonQuery(ETAConnectionString, "InsertProductBatteryInfo", objParams);

    //    return ReturnValue;
    //}

    ////

    //public static int UpdateProductBatteryByTID(int tid, string batteryid, string weightlb, string weigthkg)
    //{

    //    int ReturnValue;
    //    object[] objParams = { 0, tid, batteryid,weightlb,weigthkg};
    //    ReturnValue = SqlHelper.ExecuteNonQuery(ETAConnectionString, "UpdateProductBatteryByTID", objParams);

    //    return ReturnValue;


    //}

    //public static int DeleteProductBatteryByTID(int tid)
    //{
    //    int ReturnValue;
    //    object[] objParams = { 0, tid };
    //    ReturnValue = SqlHelper.ExecuteNonQuery(ETAConnectionString, "DeleteProductBatteryByTID", objParams);

    //    return ReturnValue;

    //}


    #endregion

    #region PackageLog

    public static DataSet GetPackageLogInfoByPackage(string package)
    {
        DataSet result = null;
        object[] objParams = { 0 };

        string sql = string.Format("SELECT EditLock,ReleaseDate FROM ETA.dbo.PackageLog WHERE DesignNumber= '{0}' ", package);
        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);
        //SELECT EditLock,ReleaseDate FROM ETA.dbo.PackageLog " & _"WHERE DesignNumber='" & Package & "'"

        return result;
    }


    #endregion

    #region eci.dbo.Ecr

    public static DataSet GetActiveStatusByPackage(string package)
    {
        DataSet result = null;
        object[] objParams = { 0 };

        string sql = string.Format("SELECT Status FROM eci.dbo.Ecr WHERE DesignNumber='{0}'", package);
        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);
        return result;
    }

    #endregion

    #region ExclusivePartListItems


    /// <summary>
    /// Inserts the exclusive part list items.
    /// </summary>
    /// <param name="partno">The partno.</param>
    /// <param name="package">The package.</param>
    /// <returns></returns>
    public static string InsertExclusivePartListItems(string partno, string package)
    {
        string ReturnValue = null;
        string sql = null;
        using (SqlConnection conn = new SqlConnection(ETAConnectionString))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    sql = string.Format("INSERT INTO ExclusivePartsListItems (PartNumber, Package, ActiveStatus) VALUES ('{0}','{1}', 0 )", partno, package);

                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                    trans.Commit();

                    ReturnValue = "Success";
                }
                catch (Exception err)
                {
                    trans.Rollback();
                    ReturnValue = err.Message;
                }
            }
        }// end using sqlconnection
        return ReturnValue;
    }



    /// <summary>
    /// Determines whether [is partno in exclusive table] [the specified partno].
    /// </summary>
    /// <param name="partno">The partno.</param>
    /// <returns>
    /// 	<c>true</c> if [is partno in exclusive table] [the specified partno]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsPartnoInExclusiveTable(string partno)
    {

        bool result = false;
        object[] objParams = { 0 };
        string sql = string.Format("select * from dbo.ExclusivePartsListItems where PartNumber='{0}'", partno);
        DataSet ds = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);

        if (ds.Tables[0].Rows.Count > 0)
        {

            result = true;
        }
        else
        {
            result = false;
        }


        return result;

    }
    #endregion


    #region Smarteam_Transfer_01

    /// <summary>
    /// Updates the STTID.
    /// </summary>
    /// <param name="tid">The tid.</param>
    /// <param name="pname">The pname.</param>
    /// <param name="status">The status.</param>
    /// <returns></returns>
    public static int UpdateSTTID(int tid, string pname, string status)
    {
        int ReturnValue;

        ReturnValue = SqlHelper.ExecuteNonQuery(ETAConnectionString, CommandType.Text, string.Format("UPDATE Smarteam_Transfer_01 SET Package_Number = '{0}', Status = {1}  WHERE     (TID= {2})", pname, status, tid));

        return ReturnValue;


    }

    /// <summary>
    /// Gets the smarteam_ transfer.
    /// </summary>
    /// <returns></returns>
    public static DataSet GetSmarteam_Transfer()
    {

        DataSet result = null;

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, "SELECT  Smarteam_Transfer_01.TID,Smarteam_Transfer_01.Package_Number, Smarteam_Transfer_01.Status, Smarteam_Transfer_ReturnStatus.[Desc] as statusvalue FROM  Smarteam_Transfer_01 INNER JOIN  Smarteam_Transfer_ReturnStatus ON Smarteam_Transfer_01.Status = Smarteam_Transfer_ReturnStatus.ReturnStatusId ");

        return result;
    }

    public static DataSet GetSTReturnStatus()
    {

        DataSet result = null;

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, "SELECT   * from Smarteam_Transfer_ReturnStatus ");

        return result;
    }


    #endregion

    #region SmartTeamAllPartsInfo

    public static List<STPartInfoObject> GetPartsInfoByNumber(string partnumber)
    {
        string sql = string.Format("select * from SmartTeamAllPartsInfo where CN_PART_NUMBER='{0}' order by CN_PART_NUMBER", partnumber);

        DataTable dt = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql).Tables[0];

        List<STPartInfoObject> list = new List<STPartInfoObject>();
        foreach (DataRow dr in dt.Rows)
        {

            STPartInfoObject partinfo = new STPartInfoObject();
            partinfo.CLASS_NAME = dr["CLASS_NAME"].ToString();
            partinfo.CN_PART_NUMBER = dr["CN_PART_NUMBER"].ToString();
            partinfo.FILE_NAME = dr["TDM_DESCRIPTION"].ToString();
            partinfo.FILE_NAME = dr["REVISION"].ToString();
            partinfo.FILE_NAME = dr["TDM_ID"].ToString();
            partinfo.FILE_NAME = dr["FILE_NAME"].ToString();
            partinfo.MODIFICATION_DATE = Convert.ToDateTime(dr["FILE_NAME"]);

            list.Add(partinfo);

        }


        return list;

    }

    /// <summary>
    /// Gets the DT parts info by number.
    /// </summary>
    /// <param name="wildpartnumber">The wildpartnumber.</param>
    /// <returns></returns>
    public static DataTable GetDTPartsInfoByNumber(string wildpartnumber)
    {




        string sql = string.Format("SELECT  CN_PART_NUMBER, CLASS_NAME, FILE_NAME, MODIFICATION_DATE, REVISION, TDM_DESCRIPTION, TDM_ID FROM  SmartTeamAllPartsInfo WHERE (CN_PART_NUMBER LIKE '{0}') GROUP BY CN_PART_NUMBER, CLASS_NAME, FILE_NAME, MODIFICATION_DATE, REVISION, TDM_DESCRIPTION, TDM_ID", wildpartnumber);

        DataTable dt = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql).Tables[0];


        return dt;

    }

    /// <summary>
    /// Gets the DT parts info by numbers.
    /// </summary>
    /// <param name="xmlstring">The xmlstring.</param>
    /// <returns></returns>
    public static DataTable GetDTPartsInfoByNumbers(string xml)
    {





        DataTable dt = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.StoredProcedure, "SmartTeamSOABulkQuery", new SqlParameter("@rowlist", xml)).Tables[0];



        return dt;

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderno"></param>
    /// <param name="cmbtext"></param>
    /// <returns></returns>
    public static bool IsvalidByConfigurator(string orderno, string cmbtext)
    {
        bool result = true;
        string sql =
           string.Format(
               "select * from dbo.ConfiguratorValidation where Order_Number='{0}' and ADD_TFC+';'+ADD_INDEX={1}'", orderno, cmbtext);
        DataTable dt = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql).Tables[0];

        if (dt.Rows.Count == 0)
        {
            result = false;
        }
        return result;

    }

    #endregion

    #region BOM_List_Transfer
    /// <summary>
    /// Get first order no from FormA 
    /// </summary>
    /// <param name="package"></param>
    /// <returns></returns>
    public static string GetFirstOrderNoByPackage(string package)
    {
        string orderno = string.Empty;

        // should add caching

        //if (HttpRuntime.Cache[package + ":fo"] != null)
        //{
        //    orderno = HttpRuntime.Cache[package + ":fo"].ToString();
        //    return orderno;
        //}
        object cre = CommonTool.GetCacheRecord(package + ":fo");

        if (cre != null)
        {
            orderno = cre.ToString();
            return orderno;

        }

        //System.Web.Caching.Cache assd=HttpR

        string sql = string.Format("SELECT top 1 OrderNumber FROM ETA.dbo.OrderNumbers WHERE DesignNumber='{0}'", package);

        //DataTable dt = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql).Tables[0];
        SqlDataReader dreader = SqlHelper.ExecuteReader(ETAConnectionString, CommandType.Text, sql);

        while (dreader.Read())
        {
            orderno = ((IDataRecord)dreader)[0].ToString();
        }

        // Call Close when done reading.
        dreader.Close();

        //cachine
        //HttpRuntime.Cache.Insert(package + ":fo", orderno, null, DateTime.Now.AddMinutes(60), TimeSpan.Zero, CacheItemPriority.High, null);
        CommonTool.InsertCacheRecord(package + ":fo", orderno, 60);

        return orderno;

    }

    /// <summary>
    /// get result from db directly 
    /// </summary>
    /// <param name="package"></param>
    /// <returns></returns>
    public static DataTable GetConfiguratorValidationDataTableByPackageFromDB(string package)
    {

        DataTable dt = null;
        // try to get result from cache with package value
        // if true , use it


        //else, get from database and save to cache

        string forderno = GetFirstOrderNoByPackage(package);
        // need to changed back when in server
        // forderno = "0532203";
        if (!string.IsNullOrEmpty(forderno))
        {
            // changed by dayang on 03/13
            // 04/01
            //   string sql = string.Format("SELECT * FROM [dbo].[ConfiguratorValidation] where Order_Number='{0}' and List_Level!=3 ", forderno);
            //    dt = SqlHelper.ExecuteDataset(STConnectionString, CommandType.Text, sql).Tables[0];
            ////  /*
            //    DataTable dt = null;



            //     SqlParameter[] parm = new SqlParameter[2];
            //     parm[0] = new SqlParameter("@orderno", SqlDbType.NVarChar, 50);
            //     parm[0].Value = forderno;
            //     parm[0].Direction = ParameterDirection.Input;
            //     parm[1] = new SqlParameter("@isfromdw", SqlDbType.Int, 4);
            //     parm[1].Direction = ParameterDirection.Output;
            //     dt = SqlHelper.ExecuteDataset(STConnectionString, CommandType.StoredProcedure,
            //"ConfiguratorSummaryByOrderNo",
            //parm).Tables[0]; ;


            string tableresultstatus = string.Empty;

            dt = GetConfiguratorSummaryByOrderResult(forderno, ref tableresultstatus);

            // add on 04/02/2013
            // need to filter out level 3
            if (dt.Rows.Count > 0)
            {
                var rows = dt.Select("List_Level<> 3");

                //.CopyToDataTable();

                if (rows.Length != 0)
                {

                    dt = rows.CopyToDataTable();
                }
            }



            // Save to cache
            CommonTool.InsertCacheRecord(package + ":cfdt", dt, 60);

        }


        return dt;
    }
    /// <summary>
    /// Get result from Historical Table (ST_Support)
    /// </summary>
    /// <param name="orderno"></param>
    /// <returns></returns>
    public static DataTable GetDateList(string orderno)
    {

        string sql = string.Format("select distinct CONVERT(VARCHAR(10),PROCESSED_DATE,111) as pdate from dbo.BOM_Lists_Transfer where Order_Number='{0}' order by CONVERT(VARCHAR(10),PROCESSED_DATE,111) desc", orderno);

        DataTable dt = SqlHelper.ExecuteDataset(STConnectionString, CommandType.Text, sql).Tables[0];
        return dt;


    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static DataTable IsItemExistInJobQueue(JobType type, string value)
    {


        string sql = string.Format("select TID from dbo.JobAS400OrderInfo where Order_Number='{1}'", Convert.ToInt16(type),
                                   value);
        return SqlHelper.ExecuteDataset(
            STConnectionString, CommandType.Text, sql).Tables[0];


    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderno"></param>
    /// <param name="pdate"></param>
    /// <returns></returns>
    public static DataTable GetPakcageProcessDateResult(string orderno, string pdate, bool isfromdw)
    {
        DataTable dt = null;
        string dbtablename = string.Empty;
        if (isfromdw)
        {
            dbtablename = "dbo.BOM_Lists_Transfer";

            string sql =
                string.Format(
                    "select * from {2} where Order_Number='{0}' and CONVERT(VARCHAR(10),PROCESSED_DATE,111)='{1}'",
                    orderno, pdate, dbtablename);

            dt = SqlHelper.ExecuteDataset(STConnectionString, CommandType.Text, sql).Tables[0];
        }
        else // should from eta working table
        {
            dbtablename = "dbo.ConfiguratorValidation";

            string sql =
                string.Format(
                    "select * from {2} where Order_Number='{0}' and CONVERT(VARCHAR(10),PROCESSED_DATE,111)='{1}'",
                    orderno, pdate, dbtablename);

            dt = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql).Tables[0];

        }
        return dt;


    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderno"></param>
    /// <returns></returns>
    public static DataTable GetConfiguratorSummaryByOrderResult(string orderno, ref string tableresultstatus)
    {
        DataTable dt = null;

        SqlParameter[] parm = new SqlParameter[2];
        parm[0] = new SqlParameter("@orderno", SqlDbType.NVarChar, 50);
        parm[0].Value = orderno;
        parm[0].Direction = ParameterDirection.Input;
        parm[1] = new SqlParameter("@isfromdw", SqlDbType.Int, 4);
        parm[1].Direction = ParameterDirection.Output;

        dt = SqlHelper.ExecuteDataset(STConnectionString, CommandType.StoredProcedure,
   "ConfiguratorSummaryByOrderNo",
   parm).Tables[0]; ;

        //string sql =
        //    string.Format(
        //        "exec   [dbo].[ConfiguratorSummaryByOrderNo]  '{0}'",
        //        orderno);

        //dt = SqlHelper.ExecuteDataset(STConnectionString, CommandType.Text, sql).Tables[0];

        tableresultstatus = parm[1].Value.ToString();

        return dt;


    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static void InsertJobAS400OrderInfo(string sql)
    {
        DataTable dt = null;


        //string sql =
        //    string.Format(
        //        "exec   [dbo].[ConfiguratorSummaryByOrderNo]  '{0}'",
        //        orderno);

        SqlHelper.ExecuteNonQuery(STConnectionString, CommandType.Text, sql);




    }


    public static bool IsValid(string orderno, string pdate, string inputvalue, DataTable cachingdt)
    {
        bool result = false;



        DataTable dt = null;
        if (cachingdt == null)
        {

            dt = GetPakcageProcessDateResult(orderno, pdate, false);
            //if(dt.Rows.Count==0)
            //{

            //    dt = GetPakcageProcessDateResult(orderno, pdate, true);
            //}
        }
        else
        {
            dt = cachingdt;
        }
        // find result from datatable
        string sql =
            string.Format(
                "select * from dbo.ConfiguratorValidation where Order_Number='{0}' and CONVERT(VARCHAR(10),PROCESSED_DATE,111)='{1}'", "SD", "sd");

        throw new NotImplementedException();

        return result;


    }


    #endregion

    #region ETA Configurator Validation
    /// <summary>
    /// get package validation result ; if cached , use it ; or get new one from db and save to cache
    /// </summary>
    /// <param name="package"></param>
    /// <returns></returns>
    public static DataTable GetConfiguratorValidationDataTableByPackage(string package)
    {

        DataTable dt = null;
        // try to get result from cache with package value
        // if true , use it
        object temp = CommonTool.GetCacheRecord(package + ":cfdt");
        if (temp != null)
        {
            dt = temp as DataTable;
        }
        else
        {
            //else, get from database and save to cache

            dt = GetConfiguratorValidationDataTableByPackageFromDB(package);

        }
        return dt;
    }


    public static void BulkUpdateFormCItems(string inputs)
    {
        SqlHelper.ExecuteNonQuery(ETAConnectionString, CommandType.StoredProcedure, "BulkUpdateFormCItems", new SqlParameter("@pairids", inputs));






    }

    #endregion

    #region Page Registry
    //Query
    /// <summary>
    /// General SqL 
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="pagesize"></param>
    /// <param name="pagenum"></param>
    /// <returns></returns>
    public static DataTable GetPagingDataTableFromDB(string sql, int pagesize, int pagenum, ref string counts, ref string pagecounts)
    {

        DataTable dt = null;
        SqlParameter[] parm = new SqlParameter[5];
        parm[0] = new SqlParameter("@Sql", SqlDbType.NVarChar, 4000);
        parm[0].Value = sql;
        //"select * from dbo.PartsListItems where DesignNumber like ''"+ package+"%'' order by Itemid desc";
        parm[0].Direction = ParameterDirection.Input;

        parm[1] = new SqlParameter("@CurrentPage", SqlDbType.Int, 4);
        parm[1].Value = pagenum;
        parm[1].Direction = ParameterDirection.Input;

        parm[2] = new SqlParameter("@PageSize", SqlDbType.Int, 4);
        parm[2].Value = pagesize;
        parm[2].Direction = ParameterDirection.Input;

        parm[3] = new SqlParameter("@counts", SqlDbType.Int, 4);
        parm[3].Direction = ParameterDirection.Output;

        parm[4] = new SqlParameter("@pageCount", SqlDbType.Int, 4);
        parm[4].Direction = ParameterDirection.Output;


        dt = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.StoredProcedure,
   "GetGeneralPagingResult",
   parm).Tables[0]; ;

        counts = parm[3].Value.ToString();
        pagecounts = parm[4].Value.ToString();
        return dt;
    }

    //Query
    /// <summary>
    /// General SqL 
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="pagesize"></param>
    /// <param name="pagenum"></param>
    /// <returns></returns>
    public static DataTable GetPagingDataTableFromST(string sql, int pagesize, int pagenum, ref string counts, ref string pagecounts)
    {

        DataTable dt = null;
        SqlParameter[] parm = new SqlParameter[5];
        parm[0] = new SqlParameter("@Sql", SqlDbType.NVarChar, 4000);
        parm[0].Value = sql;
        //"select * from dbo.PartsListItems where DesignNumber like ''"+ package+"%'' order by Itemid desc";
        parm[0].Direction = ParameterDirection.Input;

        parm[1] = new SqlParameter("@CurrentPage", SqlDbType.Int, 4);
        parm[1].Value = pagenum;
        parm[1].Direction = ParameterDirection.Input;

        parm[2] = new SqlParameter("@PageSize", SqlDbType.Int, 4);
        parm[2].Value = pagesize;
        parm[2].Direction = ParameterDirection.Input;

        parm[3] = new SqlParameter("@counts", SqlDbType.Int, 4);
        parm[3].Direction = ParameterDirection.Output;

        parm[4] = new SqlParameter("@pageCount", SqlDbType.Int, 4);
        parm[4].Direction = ParameterDirection.Output;


        dt = SqlHelper.ExecuteDataset(STConnectionString, CommandType.StoredProcedure,
   "GetGeneralPagingResult",
   parm).Tables[0]; ;

        counts = parm[3].Value.ToString();
        pagecounts = parm[4].Value.ToString();
        return dt;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="partno"></param>
    /// <param name="minor"></param>
    /// <param name="desc"></param>
    /// <param name="tmhuview"></param>
    /// <param name="fromeci"></param>
    /// <param name="toeci"></param>
    /// <returns></returns>
    public static string InsertPrNewEntry(string partno, string minor, string desc, int tmhuview,string m1,string m2,string comment,string modfrom ,string drw, string fromeci, string toeci)
    {
        string ReturnValue = null;
        string sql = null;
        using (SqlConnection conn = new SqlConnection(ETAConnectionString))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    if (!string.IsNullOrEmpty(toeci))
                    //sql = string.Format("SELECT ID as TID,PARTSNO as PartNo ,[MAINOR] as [Minor],[PARTSNAME] as [Description],[TMHU_View],[MATERIAL1],[MATERIAL2],[DRW],[COMMENT1],[From_ECI],[To_ECI],[FROM_DATE],[TO_DATE],Mod_From FROM  [ETA].[dbo].[05_GPN_TSD] where  PARTSNO  like '%{0}%' order by ID", inputpackage);
                    {
                        sql =
                            string.Format(
                                "INSERT INTO [ETA].[dbo].[05_GPN_TSD] ( [PARTSNO], [MAINOR],[PARTSNAME],[MATERIAL1],[MATERIAL2],[DRW],[COMMENT1],[FROM_ECI],[TO_ECI],[TMHU_View],[Mod_From],[TO_DATE]) VALUES ('{0}','{1}', '{2}','{3}','{4}','{5}','{6}','{7}','{8}',{9},'{10}','{11}')",
                                partno, minor, desc, m1, m2, drw, comment, fromeci, toeci, tmhuview, modfrom,
                                DateTime.Now.ToShortDateString());

                    }
                    else
                    {
                        sql =
                            string.Format(
                                "INSERT INTO [ETA].[dbo].[05_GPN_TSD] ( [PARTSNO], [MAINOR],[PARTSNAME],[MATERIAL1],[MATERIAL2],[DRW],[COMMENT1],[FROM_ECI],[TO_ECI],[TMHU_View],[Mod_From]) VALUES ('{0}','{1}', '{2}','{3}','{4}','{5}','{6}','{7}','{8}',{9},'{10}')",
                                partno, minor, desc, m1, m2, drw, comment, fromeci, toeci, tmhuview, modfrom); 

                    }
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);
                    trans.Commit();
                    ReturnValue = "OK";
                }
                catch (Exception err)
                {
                    trans.Rollback();
                    ReturnValue = err.Message;
                }
            }
        }// end using sqlconnection
        return ReturnValue;
    }


    /// <summary>
    /// Update Registry
    /// </summary>
    /// <param name="tid"></param>
    /// <param name="partno"></param>
    /// <param name="minor"></param>
    /// <param name="desc"></param>
    /// <param name="tmhuview"></param>
    /// <param name="fromeci"></param>
    /// <param name="toeci"></param>
    /// <returns></returns>
    public static string UpdatePrNewEntry(int tid, string partno, string minor, string desc, int tmhuview, string m1,string m2,string comment,string modfrom ,string drw,string fromeci, string toeci)
    //(string partno, string minor, string desc, int tmhuview,string m1,string m2,string comment,string modfrom ,string drw, string fromeci, string toeci)
    {
        string ReturnValue = null;
        string sql = null;
        using (SqlConnection conn = new SqlConnection(ETAConnectionString))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    sql = string.Format("UPDATE [ETA].[dbo].[05_GPN_TSD] set [PARTSNO]='{0}', [MAINOR]='{1}',[PARTSNAME]='{2}',TMHU_View={3},From_ECI='{4}',To_ECI='{5}',[MATERIAL1]='{7}', [MATERIAL2]='{8}', [COMMENT1]='{9}' , [Mod_From]='{10}', [DRW]='{11}' where ID={6}", partno, minor, desc, tmhuview, fromeci, toeci, tid, m1, m2, comment, modfrom, drw);

                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                    trans.Commit();

                    ReturnValue = "OK";
                }
                catch (Exception err)
                {
                    trans.Rollback();
                    ReturnValue = err.Message;
                }
            }
        }// end using sqlconnection
        return ReturnValue;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="tids"></param>
    /// <returns></returns>
    public static string DeletePrNewEntry(string tids)
    {
        string ReturnValue = null;
        string sql = null;
        using (SqlConnection conn = new SqlConnection(ETAConnectionString))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    sql = string.Format("DELETE FROM [ETA].[dbo].[05_GPN_TSD] Where  ID in ({0})", tids);

                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

                    trans.Commit();

                    ReturnValue = "OK";
                }
                catch (Exception err)
                {
                    trans.Rollback();
                    ReturnValue = err.Message;
                }
            }
        }// end using sqlconnection
        return ReturnValue;
    }
    #endregion

    #region Configurator Transfer
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static DataTable GetInvalidConfiguratorTransferList()
    {
        //   List<ConfiguratorTransfer> result = new List<ConfiguratorTransfer>();

        string sql = "SELECT *  FROM Configurator_Transfer  where Processed=-1";

        DataSet ds = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);


        if (ds.Tables.Count > 0)
        {
            return ds.Tables[0];

        }
        else
        {

            return null;
        }

        //foreach (DataRow dr in ds.Tables[0].Rows)
        //{
        //    ConfiguratorTransfer cf = new ConfiguratorTransfer();
        //    cf.TID = (int)dr["TID"];
        //    cf.OrderNumber = dr["OrderNumber"].ToString();
        //    cf.Processed = CTProcessFlag.ErrorProcoessed;
        //    cf.TSDNumber = dr["TSDNumber"].ToString();

        //    result.Add(cf);
        //}




    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="tid"></param>
    /// <returns></returns>
    public static bool UpdateConfiguratorTransferFlagById(int tid)
    {
        try
        {
            DataTable result = null;
            string sql = string.Format("UPDATE Configurator_Transfer SET  Processed =0 WHERE TID = {0}", tid);

            SqlHelper.ExecuteScalar(ETAConnectionString, CommandType.Text, sql);
            return true;
        }
        catch (Exception)
        {

            return false;
        }





    }

    #endregion
    #region ST_Support Release

    public static DataTable GetFormAAndFormCList(string package)
    {
        string sql =
          string.Format(
              "select * FROM [03_TSD] INNER JOIN FormAItems ON [03_TSD].AItemId = FormAItems.AItemId and designnumber='{0}' ",
              package);
        DataSet ds = SqlHelper.ExecuteDataset(STConnectionString, CommandType.Text, sql);


        if (ds.Tables.Count > 0)
        {
            return ds.Tables[0];

        }
        else
        {

            return null;
        }

    }


    public static DataTable Get06PartsStructure_05PRList_TSDList(string tfc, string partentpart)
    {

        string sql =
            string.Format(
                "SELECT  * FROM [dbo].[06_PartsStructureList_TSD] WHERE TFC='{0}' and replace(INDEX_PARENTPART,' ','')='{1}' ",
                tfc, partentpart);


        sql =
            string.Format(
                "EXECUTE  [ST_SUPPORT].[dbo].[GetRecursive06PartList] '{0}' , '{1}' ",
                tfc, partentpart);

        DataSet ds = SqlHelper.ExecuteDataset(STConnectionString, CommandType.Text, sql);


        if (ds.Tables.Count > 0)
        {
            return ds.Tables[0];

        }
        else
        {

            return null;
        }

    }

    #endregion


    #region TSD

    public static DataSet GetTSDFormCInfoByPackageNameAndModule(string packagename, string module, bool moduleflag)
    {
        DataSet result = null;
        object[] objParams = { 0 };
        string sql = string.Empty;
        if (moduleflag)
        {
            sql = string.Format("exec [dbo].[GetTSDFormCByPackageAndModule] '{0}',{1}", packagename, module);



            //"SELECT * FROM ETA.dbo.viewFormCItems left outer  JOIN    ChangeLogC ON viewFormCItems.CItemId = ChangeLogC.CItemId WHERE DesignNumber='" + packagename + "' AND ModuleLocation='" + module + " ' ORDER BY viewFormCItems.CategoryAddress, viewFormCItems.CItemId");
        }
        else
        {
            sql = string.Format("exec [dbo].[GetTSDFormCByPackageAndModule] '{0}',''", packagename);
            // result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);
            //"SELECT * FROM ETA.dbo.viewFormCItems left outer  JOIN    ChangeLogC ON viewFormCItems.CItemId = ChangeLogC.CItemId WHERE DesignNumber='" + packagename + "' ORDER BY viewFormCItems.CategoryAddress, viewFormCItems.CItemId");

        }
        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);

        return result;
    }

    public static DataSet GetNewFormCRecord(string addtfc, string addindex)
    {
        DataSet result = null;
        //object[] objParams = { 0 };
        //                SELECT     * FROM viewFormCItems INNER JOIN
        //                      viewPartsList ON viewFormCItems.ModuleLocation = viewPartsList.DesignNumber
        //WHERE     viewFormCItems.PartCode = 'M204' AND viewPartsList.CODE2 = '01' AND viewPartsList.CODE3 = '04'

        // AND viewPartsList.CODE1 = '611'
        // 07/14 version
        //string sql = string.Format("SELECT * FROM viewFormCItems INNER JOIN viewPartsList ON viewFormCItems.ModuleLocation = viewPartsList.DesignNumber WHERE  viewFormCItems.PartCode ='{0}' AND viewPartsList.CODE2 = '{1}' And viewPartsList.CODE3 = '{2}' AND viewPartsList.CODE1 = '{3}'", partcode,  compcode, vari,assyname);

        string sql = string.Format("SELECT top 1 ID FROM ETA.dbo.[03_TSD] WHERE  ADD_TFC = '{0}' AND ADD_INDEX = '{1}'", addtfc, addindex);

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);


        return result;
    }

    public static DataTable Get03TSDMaxKeyCode(string package)
    {
        DataTable result = null;
        //object[] objParams = { 0 };
        //                SELECT     * FROM viewFormCItems INNER JOIN
        //                      viewPartsList ON viewFormCItems.ModuleLocation = viewPartsList.DesignNumber
        //WHERE     viewFormCItems.PartCode = 'M204' AND viewPartsList.CODE2 = '01' AND viewPartsList.CODE3 = '04'

        // AND viewPartsList.CODE1 = '611'
        // 07/14 version
        //string sql = string.Format("SELECT * FROM viewFormCItems INNER JOIN viewPartsList ON viewFormCItems.ModuleLocation = viewPartsList.DesignNumber WHERE  viewFormCItems.PartCode ='{0}' AND viewPartsList.CODE2 = '{1}' And viewPartsList.CODE3 = '{2}' AND viewPartsList.CODE1 = '{3}'", partcode,  compcode, vari,assyname);

        string sql = string.Format("SELECT  top 1  convert(int,substring(KEYCODE,6,len(KEYCODE)-5)) as maxindex FROM [ETA].[dbo].[03_TSD] where TFC='{0}' order by maxindex desc", package);

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql).Tables[0];


        return result;
    }


    public static DataSet GetPartListTSDDropDownByPackageName(string packagename)
    {
        DataSet result = null;
        object[] objParams = { 0 };
        //SELECT headerid ,code1 FROM ETA.dbo.viewPartsList AS m WHERE m.DesignNumber LIKE 'XXX0%' group by  code1,headerid order by code1
        string sql = string.Format("SELECT distinct [ADD_INDEX] FROM [ETA].[dbo].[03_TSD] where TFC='{0}' and ADD_TFC='{0}' order by ADD_INDEX", packagename);
        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);
        //"SELECT distinct Headerid FROM ETA.dbo.viewPartsList AS m WHERE m.DesignNumber LIKE '" + packagename + "%' ORDER BY m.HeaderId");


        return result;
    }

    public static DataSet GetPartListInfoTSDByPackageName(string packagename, string revision)
    {
        DataSet result = null;
        object[] objParams = { 0 };
        //SELECT headerid ,code1 FROM ETA.dbo.viewPartsList AS m WHERE m.DesignNumber LIKE 'XXX0%' group by  code1,headerid order by code1
        string sql = string.Format("SELECT top 1 HeaderID FROM ETA.dbo.viewPartsListTSD where DESIGNNUMBER like '{0}%' and CINDEX_PARENTPART='{1}'", packagename, revision);
        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, sql);
        //"SELECT distinct Headerid FROM ETA.dbo.viewPartsList AS m WHERE m.DesignNumber LIKE '" + packagename + "%' ORDER BY m.HeaderId");
        if (result.Tables[0].Rows.Count == 0)
        {
            result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text, "select -1 as HeaderID  ");

        }


        return result;
    }



    public static DataSet GetPartListInfoTSDByPackageNameAndParentNumber(string packagename, string indexparent, bool isall)
    {


        DataSet result;

        /*new DataSet();


    SqlConnection scn = new SqlConnection(ETAConnectionString);

    string sp;
    if (isall)
    {
        sp = "GetTSDRecursivePartList";
    }
    else
    {
        sp = "GetTSDRecursivePartListECI";
    }
    SqlCommand cmd = new SqlCommand();
    cmd.Parameters.Add("@tfc", SqlDbType.NVarChar,50);
    cmd.Parameters["@tfc"].Value = packagename;

    cmd.Parameters.Add("@indexparent", SqlDbType.NVarChar, 50);
    cmd.Parameters["@indexparent"].Value = indexparent;


    cmd.CommandText = sp;
    cmd.CommandType = CommandType.StoredProcedure;
    cmd.Connection = scn;
    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
    adapter.Fill(result);

    if (isall)
    {
        string test = result.Tables[0].Rows[0]["FROM_ECI"].ToString();
    }

    return result;*/
        /*
        string sql = null;
        if (isall)
        {
            sql = string.Format("EXECUTE  [dbo].[GetTSDRecursivePartList] '{0}','{1}'", packagename, indexparent);
        }

        else
        {
            sql = string.Format("EXECUTE  [dbo].[GetTSDRecursivePartListECI] '{0}','{1}'", packagename, indexparent);
            
        }
        CommandType.StoredProcedure
        //viewPartsList_DY
        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.Text,sql);
        if (isall)
        {
            string test = result.Tables[0].Rows[0]["FROM_ECI"].ToString();
        }
         * */

        SqlParameter[] parm = new SqlParameter[2];
        parm[0] = new SqlParameter("@tfc", SqlDbType.NVarChar, 50);
        parm[0].Value = packagename;
        //"select * from dbo.PartsListItems where DesignNumber like ''"+ package+"%'' order by Itemid desc";
        parm[0].Direction = ParameterDirection.Input;

        parm[1] = new SqlParameter("@indexparent", SqlDbType.NVarChar, 50);
        parm[1].Value = indexparent;
        parm[1].Direction = ParameterDirection.Input;

        if (isall)
        {

            result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.StoredProcedure,
                                              "GetTSDRecursivePartList",
                                              parm);
        }
        else
        {
            result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.StoredProcedure,
                                              "GetTSDRecursivePartListECI",
                                              parm);

        }

        return result;
    }


    public static DataSet GetTSDHeaderNamesInfo(string packagename, string code1, string code2, string code3, string code4)
    {


        DataSet result;



        SqlParameter[] parm = new SqlParameter[5];
        parm[0] = new SqlParameter("@package", SqlDbType.NVarChar, 50);
        parm[0].Value = packagename;
        //"select * from dbo.PartsListItems where DesignNumber like ''"+ package+"%'' order by Itemid desc";
        parm[0].Direction = ParameterDirection.Input;

        parm[1] = new SqlParameter("@code1", SqlDbType.NVarChar, 50);
        parm[1].Value = code1;
        parm[1].Direction = ParameterDirection.Input;

        parm[2] = new SqlParameter("@code2", SqlDbType.NVarChar, 50);
        parm[2].Value = code2;
        parm[2].Direction = ParameterDirection.Input;


        parm[3] = new SqlParameter("@code3", SqlDbType.NVarChar, 50);
        parm[3].Value = code3;
        parm[3].Direction = ParameterDirection.Input;

        if (string.IsNullOrEmpty(code4))
        {

            code4 = "";
        }
        parm[4] = new SqlParameter("@code4", SqlDbType.NVarChar, 50);
        parm[4].Value = code4;
        parm[4].Direction = ParameterDirection.Input;




        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.StoredProcedure,
                                          "GetTSDHeaderNamesInfo",
                                          parm);



        return result;
    }

    /// <summary>
    /// TSD PartList ADD Page
    /// Created On 10/30/2013
    /// </summary>
    /// <param name="packagename"></param>
    /// <returns></returns>
    public static DataSet GetTSD06ModulesByPackage(string packagename)
    {
        DataSet result = null;
        SqlParameter[] parm = new SqlParameter[5];
        parm[0] = new SqlParameter("@package", SqlDbType.NVarChar, 50);
        parm[0].Value = packagename;
        //"select * from dbo.PartsListItems where DesignNumber like ''"+ package+"%'' order by Itemid desc";
        parm[0].Direction = ParameterDirection.Input;

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.StoredProcedure,
                                         "GetTSD06ModulesByPackage",
                                         parm);

        return result;
    }

    public static DataSet GetExcept0306ModulesByPackage(string packagename)
    {
        DataSet result = null;
        SqlParameter[] parm = new SqlParameter[5];
        parm[0] = new SqlParameter("@package", SqlDbType.NVarChar, 50);
        parm[0].Value = packagename;
        //"select * from dbo.PartsListItems where DesignNumber like ''"+ package+"%'' order by Itemid desc";
        parm[0].Direction = ParameterDirection.Input;

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.StoredProcedure,
                                         "GetExcept0306ModulesByPackage",
                                         parm);

        return result;
    }

    public static DataSet GetTSDAll05ResultByPartno(string partno)
    {
        DataSet result = null;
        SqlParameter[] parm = new SqlParameter[5];
        parm[0] = new SqlParameter("@partno", SqlDbType.NVarChar, 50);
        parm[0].Value = partno;
        //"select * from dbo.PartsListItems where DesignNumber like ''"+ package+"%'' order by Itemid desc";
        parm[0].Direction = ParameterDirection.Input;

        result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.StoredProcedure,
                                         "GetTSDAll05ResultByPartno",
                                         parm);

        return result;
    }

    public static void InsertTSDCopyPartList(string tfc, string indexparent, string inputtfc, string ddindexparent, string peci)
    {
        //  DataSet result = null;
        SqlParameter[] parm = new SqlParameter[5];
        parm[0] = new SqlParameter("@tfc", SqlDbType.NVarChar, 50);
        parm[0].Value = tfc;
        parm[0].Direction = ParameterDirection.Input;

        parm[1] = new SqlParameter("@indexparent", SqlDbType.NVarChar, 50);
        parm[1].Value = indexparent;
        parm[1].Direction = ParameterDirection.Input;



        parm[2] = new SqlParameter("@uinputtfc", SqlDbType.NVarChar, 50);
        parm[2].Value = inputtfc;
        parm[2].Direction = ParameterDirection.Input;


        parm[3] = new SqlParameter("uselectiindexparent", SqlDbType.NVarChar, 50);
        parm[3].Value = ddindexparent;
        parm[3].Direction = ParameterDirection.Input;


        parm[4] = new SqlParameter("@pkgactiveeci", SqlDbType.NVarChar, 50);
        parm[4].Value = peci;
        parm[4].Direction = ParameterDirection.Input;

        SqlHelper.ExecuteNonQuery(ETAConnectionString, CommandType.StoredProcedure,
                                        "InsertTSDCopyPartList",
                                        parm);

        //return result;
    }

    //public static DataSet GetTSDPartentPartnoByChildPart(string partno)
    //{
    //    DataSet result = null;
    //    SqlParameter[] parm = new SqlParameter[5];
    //    parm[0] = new SqlParameter("@cpartno", SqlDbType.NVarChar, 50);
    //    parm[0].Value = partno;
    //    //"select * from dbo.PartsListItems where DesignNumber like ''"+ package+"%'' order by Itemid desc";
    //    parm[0].Direction = ParameterDirection.Input;

    //    result = SqlHelper.ExecuteDataset(ETAConnectionString, CommandType.StoredProcedure,
    //                                     "GetTSDPartentPartnoByChildPart",
    //                                     parm);

    //    return result;
    //}    

    #endregion
    #region Shared DB Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="package"></param>
    /// <param name="ReleaseDate"></param>
    /// <param name="ecistart"></param>
    /// <param name="EciNumber"></param>
    public static void AssignReleaseDateByPackage(string package, out string ReleaseDate, out string ecistart, out string EciNumber)
    {
        //Check ECI Status and set ecinumber
        DataTable dtecistatus = GetECIStatusInfoByPackageName(package).Tables[0];
        //  var ecistart = AssignReleaseDateByPackage(dtecistatus);
        //  string ecistart = null;
        // string ecistart = null;

        if (dtecistatus.Rows.Count == 0)
        {
            ecistart = "NoEci";
            ReleaseDate = null;
            EciNumber = null;
        }
        else
        {
            if (dtecistatus.Rows[0][0] == null)
            {
                ecistart = null;
            }
            else
            {
                ecistart = dtecistatus.Rows[0][0].ToString();
            }

            if (dtecistatus.Rows[0][1] == null)
            {
                ReleaseDate = null;
            }
            else
            {
                ReleaseDate = dtecistatus.Rows[0][1].ToString();
            }

            EciNumber = dtecistatus.Rows[0][1].ToString();
        }
        // return ecistart;
    }

    #endregion

}

