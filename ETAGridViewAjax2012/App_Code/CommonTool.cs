using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using log4net;

public enum JobType
{

    AutoPair = 1,
    Configuraotr = 2

}
public enum CTProcessFlag
{
    ErrorProcoessed = -1,
    Unprocessed = 0,
    Processed = 1


}
public class ConfiguratorTransfer
{
    public CTProcessFlag Processed { get; set; }
    public string TSDNumber { get; set; }
    public string OrderNumber { get; set; }
    public int TID { get; set; }
}
/// <summary>
/// Summary description for CommonTool
/// </summary>
public static class CommonTool
{
    public static readonly ILog log = (log4net.ILog)log4net.LogManager.GetLogger("Log");
    private const Int16 MAXAUTOPAIRCOUNT = 200;
    //public const string RESERVEDCHARPATTERN = "[']+";  // "[-()/&#*+-.']+";
    //public static Regex FormcDescRegex = new Regex(
    //  RESERVEDCHARPATTERN,
    //RegexOptions.CultureInvariant
    //| RegexOptions.Compiled
    //);


    //Used For TSD Insert PartList page :PartsListTSDSubSelect.aspx
    public static Regex RexTSDSubPartList = new Regex(
      "^[a-zA-Z0-9]{4}\\([a-zA-Z0-9]{3}\\).*$",
    RegexOptions.IgnoreCase
    | RegexOptions.CultureInvariant
    | RegexOptions.IgnorePatternWhitespace
    | RegexOptions.Compiled
    );

    #region Caching
    /// <summary>
    /// During Minues based 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="duration"></param>
    public static void InsertCacheRecord(string key, object value, double duration)
    {
        HttpRuntime.Cache.Insert(key, value, null, DateTime.Now.AddMinutes(duration), TimeSpan.Zero, CacheItemPriority.High, null);

    }

    public static object GetCacheRecord(string key)
    {
        return HttpRuntime.Cache[key];
    }
    public static void RemoveCacheRecord(string key)
    {
        HttpRuntime.Cache.Remove(key);
    }
    #endregion

    /// <summary>
    /// Converts the data table to XML.
    /// </summary>
    /// <param name="dtBuildSQL">The dt build SQL.</param>
    /// <returns></returns>
    public static string ConvertDataTableToXML(DataTable dtBuildSQL)
    {
        DataSet dsBuildSQL = new DataSet();
        StringBuilder sbSQL;
        StringWriter swSQL;
        string XMLformat;

        sbSQL = new StringBuilder();
        swSQL = new StringWriter(sbSQL);
        dsBuildSQL.Merge(dtBuildSQL, true, MissingSchemaAction.AddWithKey);
        dsBuildSQL.Tables[0].TableName = "Row";
        foreach (DataColumn col in dsBuildSQL.Tables[0].Columns)
        {
            col.ColumnMapping = MappingType.Attribute;
        }
        dsBuildSQL.WriteXml(swSQL, XmlWriteMode.IgnoreSchema);
        XMLformat = sbSQL.ToString();
        return XMLformat;
    }

    /// <summary>
    /// Converts the string to data table.
    /// </summary>
    /// <param name="inputstring">The inputstring.</param>
    /// <param name="separator">The separator.</param>
    /// <returns></returns>
    public static DataTable ConvertStringToDataTable(string inputstring, char separator)
    {

        string[] PartNo = inputstring.Substring(0, inputstring.Length - 2).Split(';');

        // Create a new DataTable.
        System.Data.DataTable table = new DataTable();
        // Declare variables for DataColumn and DataRow objects.
        DataColumn column;


        // Create new DataColumn, set DataType, 
        // ColumnName and add to DataTable.    

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Boolean");
        column.ColumnName = "skipstatus";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "partno";
        table.Columns.Add(column);


        for (int i = 0; i < PartNo.Length - 1; i++)
        {

            table.Rows.Add(false, PartNo[i]);

        }


        return table;

    }


    /// <summary>
    /// Determines whether the specified STR date is date.
    /// </summary>
    /// <param name="strDate">The STR date.</param>
    /// <returns>
    /// 	<c>true</c> if the specified STR date is date; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsDate(string strDate)
    {
        DateTime outDate;
        return DateTime.TryParse(strDate, out outDate);
    }

    //\w{4}[(]\d{3}[)][\w-]*$
    //(^\w{5}[-]\w{5}[-\w]*$)|(^\w{4}[(]\d{3}[)][\w-]*$)
    //(^\w{5}[-]\w{5}[-][\w]*$)|(^\w{4}[(]\d{3}[)][\w-]*$)
    //Regex re = new Regex("^\\w{4}[(]\\d{3}[)][\\w-]*$", RegexOptions.None);
    // MatchCollection mc = re.Matches("text");

    //acceptable part number formats
    //xxxxx-xxxxx-xx (53710-UMX41-71 )
    //xxxxx-xxxxx-xx-x(51130-UM41X-71-C)
    //xxxx(xxx)xxxx(G851(510)0104 ;  NG4N(551)0101)
    //xxxx(xxx)xxx-xx(G837(561)01A-01)

    public static void DisableControl(WebControl control)
    {

        control.Enabled = false;
    }


    public static string FormCPageCodeErrorMessage = "Error : Page Code is not assoicated with assycode and partcode.Please make the change !";
    public static string FormCPairIDErrorMessage = "Error : Pair ID is not correct.Please make the change !";
    public static string FormCRefErrorMessage = "Error: Part numbers in referenced module are not valid .Please make the change !";

    public static Regex RegexForRecursive = new Regex("(^\\w{4}[(]\\w{3}[)][\\w-]*$)", RegexOptions.None);

    public static int MaxValidationRecursiveLevel = 3;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pagecode"></param>
    /// <returns></returns>
    public static bool IsFormCPageCode99(string pagecode)
    {
        bool result = false;
        if (!string.IsNullOrEmpty(pagecode))
        {
            if (pagecode.Substring(pagecode.Length - 2, 2) == "99")
            {

                result = true;
            }
        }

        return result;

    }


    public static List<string> GetFilesRecursive(string b, string dbname)
    {
        // 1.
        // Store results in the file results list.
        List<string> result = new List<string>();

        // 2.
        // Store a stack of our directories.
        Stack<string> stack = new Stack<string>();

        // 3.
        // Add initial directory.
        stack.Push(b);

        // 4.
        // Continue while there are directories to process
        while (stack.Count > 0)
        {
            // A.
            // Get top directory
            string dir = stack.Pop();

            try
            {
                // B
                // Add all files at this directory to the result List.
                result.AddRange(Directory.GetFiles(dir, "*.*"));

                // C
                // Add all directories at this directory.
                foreach (string dn in Directory.GetDirectories(dir))
                {
                    if (dn.Contains(dbname))
                    {
                        stack.Push(dn);
                    }
                }
            }
            catch (Exception err)
            {
                // D
                // Could not open the directory
            }
        }
        return result;
    }



    public static bool ValidatePartnoFromFormC(string partcode, string groupno, string pagecode)
    {

        bool result = false;
        //select * from ETA.dbo.viewFormCItems where pagecode='01 01' and partcode='NG4N' and categoryAddress like '551%' order by designnumber

        if (CommonDB.GetPartNumberInfoFromFormC(pagecode, groupno, pagecode).Tables[0].Rows.Count > 0)
        {
            result = true;
        }

        return result;
    }

    public static string PartNumberCleanUp(string partnumber)
    {
        //remove space
        partnumber = partnumber.Replace(" ", "");

        partnumber = partnumber.Replace("(", "");

        partnumber = partnumber.Replace(")", "");

        // partnumber=partnumber.Replace("-", "");

        return partnumber;


        // partno validation logic

        //clean up partno by removing all separators

        // get the first four character and check whether it is in standard model part lis

        // if yes, need to check with viewstandpartdpartlist

        // if not, need to check with viewpartlists
        // if orignal partnumber's format like "xxxxx-xxxxx", check whether it is from Japan

        //else "SELECT  partnumber,* FROM ETA.dbo.viewPartsList WHERE Partnumber like 'G841-(531)0201%'"
    }
    /// <summary>
    /// keep dash sign for TSD
    /// </summary>
    /// <param name="partnumber"></param>
    /// <returns></returns>
    public static string PartNumberTSDCleanUpUpdate(string partnumber)
    {
        //remove space
        partnumber = partnumber.Replace(" ", "");

        partnumber = partnumber.Replace("(", "");

        partnumber = partnumber.Replace(")", "");

        // partnumber = partnumber.Replace("-", "");

        return partnumber;


        // partno validation logic

        //clean up partno by removing all separators

        // get the first four character and check whether it is in standard model part lis

        // if yes, need to check with viewstandpartdpartlist

        // if not, need to check with viewpartlists
        // if orignal partnumber's format like "xxxxx-xxxxx", check whether it is from Japan

        //else "SELECT  partnumber,* FROM ETA.dbo.viewPartsList WHERE Partnumber like 'G841-(531)0201%'"
    }

    public static string PartNumberCleanUpUpdate(string partnumber)
    {
        //remove space
        partnumber = partnumber.Replace(" ", "");

        partnumber = partnumber.Replace("(", "");

        partnumber = partnumber.Replace(")", "");

        partnumber = partnumber.Replace("-", "");

        return partnumber;


        // partno validation logic

        //clean up partno by removing all separators

        // get the first four character and check whether it is in standard model part lis

        // if yes, need to check with viewstandpartdpartlist

        // if not, need to check with viewpartlists
        // if orignal partnumber's format like "xxxxx-xxxxx", check whether it is from Japan

        //else "SELECT  partnumber,* FROM ETA.dbo.viewPartsList WHERE Partnumber like 'G841-(531)0201%'"
    }


    /// <summary>
    /// Isvalids the partno.
    /// </summary>
    /// <param name="partno">The partno.</param>
    /// <param name="packagename">The packagename.</param>
    /// <returns></returns>

    public static int IsvalidPartnoold(string partno, string packagename)
    {

        //CREATE TABLE #sdy(
        //partno nvarchar(50),
        //model nvarchar(50) )

        //Insert into #sdy(partno,model)
        //select distinct partno,model   from ETA.dbo.viewStandardParts where len(partno)<10 group by partno,model order by partno


        //--select * from #sdy

        //select distinct partno,model from ETA.dbo.viewStandardParts where partno in

        //(

        //select partno from #sdy group by partno having count(model)>1 
        //) order by partno

        //drop table #sdy





        //xxxxx-xxxxx    	 94223-80800	  with len=10
        //xxxxx-xxxxx-x   	94223-80800-C	  with len=10 and minor
        //xxxxx-xxxxx-xx	96364-12240-71	   with len=12
        //xxxxx-xxxxx-xx-x 	51130-UM41X-71-C	with len=12 and minor
        //xxxx(xxx)xxxx     	G851(561)0101	look for combination
        //xxxx(xxx)xxx-xx   	G851(561)01F-04	with len=9






        //partno = "(G851) 561-01)01";

        int result = -1;
        DataTable dt = null;

        try
        {
            // format validation
            Regex re = new Regex("(^\\w{5}[-]\\w{5}[\\w-]*$)|(^\\w{4}[(]\\w{3}[)][\\w-]*$)", RegexOptions.None);
            if (!re.IsMatch(partno))
            {
                result = -1;
                return result;

            }


            string cleanuppartno = PartNumberCleanUp(partno);

            //For partno :   //(xxx)xxxx     : (561)0101  ?
            //xxxxxx-xx     : 56101A-01  ? 
            if (cleanuppartno.Length < 10)
            {
                // query : "Select distinct(PartNo) FROM ETA.dbo.viewStandardParts where  partno like '%{0}%'"         
                if (CommonDB.GetShortStandardPartcodeDataTableFromStandPartsList(cleanuppartno).Tables[0].Rows.Count > 0)
                {

                    result = 1;
                }
                else
                {
                    result = -1;
                }


                return result;
            }


            // Get the first four character to check whether it is from standpart list or not
            string model = cleanuppartno.Substring(0, 4);
            // to check whether this package is in standardpart modellist
            DataRow[] drs = CommonDB.GetStandardPartcodeDataTableFromStandPartsList().Tables[0].Select(string.Format("Model='{0}'", model));

            if (drs.Length > 0)
            {
                //For partno like : 7	xxxx(xxx)xxxx     :G851(561)0101	if from standpart : query
                //                : 9xxxx(xxx)xxx-xx   :G851(561)01F-04	select * from dbo.viewStandardParts  where partno like '%56101F-04%' and model='g851'




                if (cleanuppartno.Contains("-"))
                {
                    //G851(561)01F-04 : Case 9  ;G85156101F-04

                    string newpartno = null;
                    newpartno = cleanuppartno.Substring(4);

                    //if (cleanuppartno.Substring(cleanuppartno.Length - 3, 1) == "-")
                    //{
                    //    //G837-561-01-A-01
                    //    //newpartno = string.Format("{0}{1}", cleanuppartno.Substring(4, cleanuppartno.Length - 8).Replace("-", ""), cleanuppartno.Substring(cleanuppartno.Length - 4));

                    //    newpartno = cleanuppartno.Substring(4);

                    //}
                    //else
                    //{
                    //    //G837-561-0101
                    //    newpartno = cleanuppartno.Substring(4).Replace("-", "");

                    //}

                    //SELECT top 1 * FROM ETA.dbo.viewStandardParts WHERE Partno like '56101F-04%' and model like 'G851%'
                    dt = CommonDB.GetCharacterStandardPartNumberCount(newpartno, model).Tables[0];
                }

                else
                {
                    // 7	xxxx(xxx)xxxx     :G851(561)0101	if from standpart : query
                    //select * from ETA.dbo.viewStandardParts WHERE Model='G851' and GroupNo='561' and CompCode='01' and Vari='01' and Ser='' 
                    dt = CommonDB.GetStandardPartListInfo(model, cleanuppartno.Substring(4, 3), cleanuppartno.Substring(7, 2), cleanuppartno.Substring(9, 2), "").Tables[0];
                }



            }// in standpart model list

            else
            {
                // not from standpart

                if (cleanuppartno.Substring(5, 1) == "-")
                {
                    //1	xxxxx-xxxxx     :94223-80800	 partno like '9422380800%'(viewstandpartlist)
                    //2	xxxxx-xxxxx-x  :94223-80800-C 	parnto like '9422380800%' and minor='c'(viewstandapartlist)
                    //3	xxxxx-xxxxx-xx :53710-UMX41-71	if (UM,UN,9N,9M), check last three digit and warning
                    //4	xxxxx-xxxxx-xx	if not, partno like '537101X4171%' (viewstandpartlist)
                    //5	xxxxx-xxxxx-xx-x : 51130-UM41X-71-C	if (UM,UN,9N,9M), check last three digit and warning
                    //6	xxxxx-xxxxx-xx-x :  96364-12240-71-C	if not, partno like '537101X4171%' (viewstandpartlist) and minor ='c'                          
                    int length = cleanuppartno.Length;
                    string newpartno = null;
                    string minor = null;
                    string[] partarray = cleanuppartno.Split('-');
                    switch (length)
                    {
                        case 11:
                            //1	xxxxx-xxxxx     :94223-80800	 partno like '9422380800%'(viewstandpartlist)
                            newpartno = cleanuppartno.Replace("-", "");
                            break;
                        case 13:
                            //2	xxxxx-xxxxx-x  :94223-80800-C 	parnto like '9422380800%' and minor='c'(viewstandapartlist)
                            newpartno = cleanuppartno.Substring(0, length - 2).Replace("-", "");
                            minor = cleanuppartno.Substring(length - 2, 1);

                            break;
                        case 14:
                            //3	xxxxx-xxxxx-xx :53710-UMX41-71	if (UM,UN,9N,9M), check last three digit and warning
                            //4	xxxxx-xxxxx-xx	if not, partno like '537101X4171%' (viewstandpartlist)


                            if (CheckSpecialPartNo(partarray))
                            {
                                //3	xxxxx-xxxxx-xx :53710-UMX41-71	if (UM,UN,9N,9M), check last three digit and warning

                                //compare with package number . last three
                                if (CompareLastTreePackageCharacters(packagename, partarray))
                                {
                                    result = 1;
                                }
                                else
                                {
                                    result = 0;

                                }

                                return result;
                            }
                            else
                            {
                                //4	xxxxx-xxxxx-xx	if not, partno like '537101X4171%' (viewstandpartlist)
                                newpartno = cleanuppartno.Replace("-", "");

                            }

                            break;
                        case 16:
                            //5	xxxxx-xxxxx-xx-x : 51130-UM41X-71-C	if (UM,UN,9N,9M), check last three digit and warning
                            //6	xxxxx-xxxxx-xx-x :  96364-12240-71-C	if not, partno like '537101X4171%' (viewstandpartlist) and minor ='c'                         

                            if (CheckSpecialPartNo(partarray))
                            {
                                //5	xxxxx-xxxxx-xx-x : 51130-UM41X-71-C	if (UM,UN,9N,9M), check last three digit and warning

                                //compare with package number . last three
                                if (CompareLastTreePackageCharacters(packagename, partarray))
                                {
                                    result = 1;
                                }
                                else
                                {
                                    result = 0;

                                }

                                return result;
                            }
                            else
                            {
                                //6	xxxxx-xxxxx-xx-x :  96364-12240-71-C	if not, partno like '537101X4171%' (viewstandpartlist) and minor ='c'                        
                                newpartno = cleanuppartno.Substring(0, length - 2).Replace("-", "");
                                minor = cleanuppartno.Substring(length - 2, 1);


                            }


                            break;


                        default:
                            break;
                    }


                    dt = CommonDB.GetStandardPartListInfoWithoutModel(newpartno, minor).Tables[0];


                }

                else
                {
                    //8	:xxxx(xxx)xxxx (MC47)(561)(01)01	if not ,  goto partlist page header with MC47 , 561, 01,01
                    //10:xxxx(xxx)xxx-xx	:NK0B(110)(01)E-01	partlist page of Nk0b , check header with 01 ,e01
                    //                    if (cleanuppartno.Length < 11)
                    //                    {
                    //                        //8	: (MC47)(561)(01)01	if not ,  goto partlist page header with MC47 , 561, 01,01

                    //                        //
                    ////SELECT * FROM ETA.dbo.viewPartsList AS m WHERE m.DesignNumber LIKE 'Nk0b%' and code1='110' and code2='01' and code3='E-01'

                    //                        dt = CommonDB.GetNonStanardPartListInfoWithAssyCode(model, cleanuppartno.Substring(4,3),cleanuppartno.Substring(7,2),cleanuppartno.Substring(9)).Tables[0];

                    //                    }
                    //                    else
                    //                    {
                    //                        //10	:NK0B(110)(01)E-01	partlist page of Nk0b , check header with 01 ,e01
                    //                        dt = CommonDB.GetNonStanardPartListInfoWithAssyCode(model, cleanuppartno.Substring(4, 3), cleanuppartno.Substring(7, 2), cleanuppartno.Substring(9)).Tables[0];
                    //                    }

                    dt = CommonDB.GetNonStanardPartListInfoWithAssyCode(model, cleanuppartno.Substring(4, 3), cleanuppartno.Substring(7, 2), cleanuppartno.Substring(9)).Tables[0];

                }




            }

            if (dt.Rows.Count != 0)
            {
                result = 1;

            }
            else
            {

                result = -1;
            }

        } // end try
        catch
        {

            result = -1;
        }

        return result;


    }


    public static bool IsvalidRefModule(string module)
    {
        bool result = true;

        DataTable dt = CommonDB.GetPartNumberByModule(module).Tables[0];

        if (dt.Rows.Count == 0)
        {
            return false;
        }
        string statusresult = string.Empty;
        foreach (DataRow dr in dt.Rows)
        {

            if (IsvalidPartnoNew(dr["partnumber"].ToString(), module.Substring(0, 4), 0, ref statusresult) < 0)
            {

                result = false;
                break;
            }

        }


        return result;

    }


    public static string GetInvalidPartNosFromModule(string module)
    {



        DataTable dt = CommonDB.GetPartNumberByModule(module).Tables[0];

        if (dt.Rows.Count == 0)
        {
            //return "No Module";
            return "SUCCESS";
        }



        StringBuilder result = new StringBuilder();
        foreach (DataRow dr in dt.Rows)
        {
            // check quantity
            if (!IsValidQuantity(dr["subquantity"].ToString().Trim()))
            {
                return "Having Invalid Quantity";
            }
            string statusresult = string.Empty;

            if (IsvalidPartnoNew(dr["partnumber"].ToString().Trim(), module.Substring(0, 4), 0, ref statusresult) < 0)
            {

                // result = false;
                // break;
                result.Append(string.Format("{0}@{1}", dr["partnumber"].ToString(), module.Substring(0, 4)));
                result.Append(";");
            }

        }


        if (result.Length > 0)
        {
            return result.ToString();
        }
        else
        {
            return "SUCCESS";
        }


    }

    public static bool IsValidQuantity(string quantityvalue)
    {
        bool result = false;
        int quantity = 0;
        if (quantityvalue.ToUpper() == "XXX")
        {
            result = true;
        }
        else
        {

            if (Int32.TryParse(quantityvalue, out quantity))
            {

                result = true;

            }
        }


        return result;
    }

    //public static int IsvalidPartno(string partno, string packagename,int recursivelevel)
    //{
    //    if (recursivelevel > 5)
    //    {
    //        return -1;
    //    }
    //    else
    //    {
    //        return IsvalidPartno(partno, packagename);
    //    }
    //}

    /// <summary>
    /// Isvalids the partno.
    /// </summary>
    /// <param name="partno">The partno.</param>
    /// <param name="packagename">The packagename.</param>
    /// <returns></returns>
    public static int IsvalidPartno(string partno, string packagename)
    {
        //xxxxx-xxxxx    	 94223-80800	  with len=10
        //xxxxx-xxxxx-x   	94223-80800-C	  with len=10 and minor
        //xxxxx-xxxxx-xx	96364-12240-71	   with len=12
        //xxxxx-xxxxx-xx-x 	51130-UM41X-71-C	with len=12 and minor
        //xxxx(xxx)xxxx     	G851(561)0101	look for combination
        //xxxx(xxx)xxx-xx   	G851(561)01F-04	with len=9


        //partno = "(G851) 561-01)01";

        int result = -1;
        DataTable dt = null;

        try
        {
            // format validation
            Regex re = new Regex("(^\\w{5}[-]\\w{5}[\\w-]*$)|(^\\w{4}[(]\\w{3}[)][\\w-]*$)", RegexOptions.None);
            if (!re.IsMatch(partno))
            {
                result = -1;
                return result;

            }



            Regex re2 = new Regex("(^\\w{5}[-]\\w{5}[\\w-]*$)", RegexOptions.None);
            if (re2.IsMatch(partno))
            {
                string pa = partno.Substring(6, 2);
                if (pa.ToUpper() == "UN" || pa.ToUpper() == "UM" || pa.ToUpper() == "9N" || pa.ToUpper() == "9M")
                {
                    result = 0;

                    return result;
                }

            }




            string cleanuppartno = PartNumberCleanUpUpdate(partno);
            // handle xxxxx-xxxxx 

            if (partno.Length == 11 && (partno.Substring(5, 1) == "-"))
            {
                //string cleanupno=partno.Substring(0,5)
                if (CommonDB.GetFirstPartNoFromStandPartsList(cleanuppartno).Tables[0].Rows.Count > 0)
                {

                    result = 1;
                }
                else
                {
                    result = -1;
                }


                return result;


            }

            //handle xxxxx-xxxxx-x
            if (partno.Length == 13 && (partno.Substring(5, 1) == "-"))
            {
                if (CommonDB.GetFirstPartNoWithMinorFromStandPartsList(cleanuppartno.Substring(0, 10), cleanuppartno.Substring(10, 1)).Tables[0].Rows.Count > 0)
                {

                    result = 1;
                }
                else
                {
                    result = -1;
                }


                return result;


            }

            //handle xxxxx-xxxxx-xx
            if (partno.Length == 14 && (partno.Substring(5, 1) == "-"))
            {
                if (CommonDB.GetFirstPartNoFromStandPartsList(cleanuppartno).Tables[0].Rows.Count > 0)
                {

                    result = 1;
                }
                else
                {
                    result = -1;
                }


                return result;


            }


            // handle xxxxx-xxxxx-xx-x
            if (partno.Length == 16 && (partno.Substring(5, 1) == "-"))
            {
                if (CommonDB.GetFirstPartNoWithMinorFromStandPartsList(cleanuppartno.Substring(0, 12), cleanuppartno.Substring(12, 1)).Tables[0].Rows.Count > 0)
                {

                    result = 1;
                }
                else
                {
                    result = -1;
                }


                return result;


            }

            //handle xxxx(xxx)xxxx 


            if (partno.Length == 13)
            {
                string model = cleanuppartno.Substring(0, 4);
                string groupno = cleanuppartno.Substring(4, 3);
                string compcode = cleanuppartno.Substring(7, 2);
                string vari = cleanuppartno.Substring(9, 2);
                if (CommonDB.GetStandardPartListInfo(model, groupno, compcode, vari, "").Tables[0].Rows.Count > 0)
                {
                    result = 1;


                }
                else
                {
                    // need to check whether this partnumber be referenced or not , if yes, it is valid
                    if (CommonDB.GetPartListInfoFromViewPartList(partno).Tables[0].Rows.Count > 0)
                    {
                        result = 1;
                    }

                    else
                    {

                        result = -1;
                    }
                }


                return result;
            }


            //handle xxxx(xxx)xxx-xx   	G851(561)01F-04	with len=9
            if (partno.Length == 15)
            {
                string model = cleanuppartno.Substring(0, 4);
                //GetStandardPartListInfoWithMino
                string po = cleanuppartno.Substring(4, 6) + "-" + cleanuppartno.Substring(10, 2);
                if (CommonDB.GetStandardPartListInfoWithMinor(model, po).Tables[0].Rows.Count > 0)
                {
                    result = 1;
                }
                else
                {
                    result = -1;
                }


                return result;
            }



        } // end try
        catch
        {

            result = -1;
        }

        return result;


    }
    /// <summary>
    /// Compares the last tree package characters.
    /// </summary>
    /// <param name="packagename">The packagename.</param>
    /// <param name="partarray">The partarray.</param>
    /// <returns></returns>
    private static bool CompareLastTreePackageCharacters(string packagename, string[] partarray)
    {
        return partarray[1].Substring(2, 3).ToUpper() == packagename.Substring(packagename.Length - 3, 3);
    }

    /// <summary>
    /// Checks the special part no.
    /// </summary>
    /// <param name="partarray">The partarray.</param>
    /// <returns></returns>
    private static bool CheckSpecialPartNo(string[] partarray)
    {
        return partarray[1].Substring(0, 2).ToUpper() == "UN" || partarray[1].Substring(0, 2).ToUpper() == "UM" || partarray[1].Substring(0, 2).ToUpper() == "9N" || partarray[1].Substring(0, 2).ToUpper() == "9M";
    }

    /// <summary>
    /// Selects the drop down list index by value.
    /// </summary>
    /// <param name="ddl">The DDL.</param>
    /// <param name="ddlvalue">The ddlvalue.</param>
    /// <returns></returns>
    public static int SelectDropDownListIndexByText(DropDownList ddl, string ddlvalue)
    {
        return ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(ddlvalue));
    }

    public static int SelectDropDownListIndexByValue(DropDownList ddl, string ddlvalue)
    {
        return ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(ddlvalue));
    }
    /// <summary>
    /// Ascs the specified character.
    /// </summary>
    /// <param name="character">The character.</param>
    /// <returns></returns>
    public static int Asc(string character)
    {
        if (character.Length == 1)
        {
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            int intAsciiCode = (int)asciiEncoding.GetBytes(character)[0];
            return (intAsciiCode);
        }
        else
        {
            throw new Exception("Character is not valid.");
        }

    }
    /// <summary>
    /// CHRs the specified ASCII code.
    /// </summary>
    /// <param name="asciiCode">The ASCII code.</param>
    /// <returns></returns>
    public static string Chr(int asciiCode)
    {
        if (asciiCode >= 0 && asciiCode <= 255)
        {
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            byte[] byteArray = new byte[] { (byte)asciiCode };
            string strCharacter = asciiEncoding.GetString(byteArray);
            return (strCharacter);
        }
        else
        {
            throw new Exception("ASCII Code is not valid.");
        }
    }

    /// <summary>
    /// Isvalids the partno.
    /// </summary>
    /// <param name="partno">The partno.</param>
    /// <param name="packagename">The packagename.</param>
    /// <returns></returns>
    public static int IsvalidPartnoOld(string partno, string packagename)
    {

        int result = 1;
        // partno = "56011-U2291-71";

        string[] partarray = partno.Split('-');
        //Step 1 : split the input to array , check length >0 means at least xxx-xxx

        // first step to make sure the number has xxx-xxx
        if (partarray.Length > 1)
        {
            // Step 2:: check the first and two items are 5
            if ((partarray[0].Length == 5) && (partarray[1].Length == 5))
            {
                //Step 3 : if second item start with "UN, UM , 9N, 9M", compare with package number and lead to warning
                if (partarray[1].Substring(0, 2).ToUpper() == "UN" || partarray[1].Substring(0, 2).ToUpper() == "UM" || partarray[1].Substring(0, 2).ToUpper() == "9N" || partarray[1].Substring(0, 2).ToUpper() == "9M")
                {
                    //compare with package number . last three
                    if (partarray[1].Substring(2, 3).ToUpper() == packagename.Substring(packagename.Length - 3, 3))
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 0;

                    }

                }// not UN
                else
                {
                    // ' standpard partno
                    // Validate all the input header info
                    //"SELECT  top 1 *  FROM ETA.dbo.viewStandardParts WHERE Partno like '" & Replace(partnumber,"-","") & "%'" 
                    // check whether it is in database 
                    //if yes
                    DataTable dt = CommonDB.GetPartNumberCount(partno, true).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        result = 1;
                    }
                    else
                    {
                        result = -1;
                    }

                }
            }
            else
            {
                result = -1;

            }

        } // end length>2
        else
        {


            result = -1;


        }

        return result;

    }



    public static int IsvalidPartnoold2(string partno, string packagename)
    {
        partno = partno.Replace(" ", "").Trim();


        int result = 1;

        DataTable dt = null;

        // partno = "(G851) (561)(01)C-01";//G851(531)0201 

        // partno = "G851(531)0201 ";
        //partno = "N2E8(531)0101";
        // partno = "56011-U2291-71";  


        //        SELECT   *  FROM ETA.dbo.viewStandardParts WHERE Partno like '56101C-01%' and model like 'G851%'

        //select partno from ETA.dbo.viewStandardParts WHERE Model='G851' and  GroupNo='531' and CompCode='02' and Vari='01' and Ser=''

        //SELECT   *  FROM ETA.dbo.viewStandardParts WHERE Partno like '56011U229171%' 


        if (partno.Contains("("))
        {


            string newpartno = null;
            //  partno should like : G851(531)0201 ; G851 (561)(01)C-01;N2E8(531)0101
            string[] partarray = partno.Split('(');

            // get Model number 
            string model = null;

            int length = partarray.Length;
            if (length > 2)
            {
                // partno like G851 (561)(01)C-01
                model = partarray[length - 3].Replace(")", "").Trim();
                newpartno = string.Format("{0}{1}", partarray[length - 2].Replace(")", "").Trim(), partarray[length - 1].Replace(")", "").Trim());
            }
            else
            {
                //partno like  G851(531)0201 
                newpartno = string.Format("{0}", partarray[length - 1].Replace(")", "").Trim());
                model = partarray[length - 2].Replace(")", "").Trim();
            }


            DataRow[] drs = CommonDB.GetStandardPartcodeDataTableFromStandPartsList().Tables[0].Select(string.Format("Model='{0}'", model));
            if (drs.Length > 0)
            {
                // // partno should like : G851(531)0201 ; G851 (561)(01)C-01

                if (length > 2)
                {
                    // G851 (561)(01)C-01
                    // SELECT   *  FROM ETA.dbo.viewStandardParts WHERE Partno like '56101C-01%' and model like 'G851%'
                    dt = CommonDB.GetCharacterStandardPartNumberCount(newpartno, model).Tables[0];

                }

                else
                {
                    // G851(531)0201

                    // /select partno from ETA.dbo.viewStandardParts WHERE Model='G851' and  GroupNo='531' and CompCode='02' and Vari='01' and Ser=''

                    // GetStandardPartListInfo

                    dt = CommonDB.GetStandardPartListInfo(model, partarray[1].Split(')')[0], partarray[1].Split(')')[1].Substring(0, 2), partarray[1].Split(')')[1].Substring(2), "").Tables[0];


                }


            }
            else
            {
                //// N2E8(531)0101
                //(MB0M)(561)(09)(A-01)
                // SELECT * FROM   viewPartsList where DESIGNNUMBER LIKE 'N2E8%' and code1='531' and code2=
                // GetNonStanardPartListInfoWithAssyCode

                dt = CommonDB.GetNonStanardPartListInfoWithAssyCode(model, partarray[1].Split(')')[0], partarray[1].Split(')')[1].Substring(0, 2), partarray[1].Split(')')[1].Substring(2)).Tables[0];


            }


            if (dt.Rows.Count != 0)
            {
                result = 1;

            }
            else
            {

                result = -1;
            }


        }
        else
        {
            //partno should like : "56582-U2100-71 ";  
            string[] partarray = partno.Split('-');
            //Step 1 : split the input to array , check length >0 means at least xxx-xxx

            // first step to make sure the number has xxx-xxx

            if (partarray.Length > 1)
            {
                // Step 2:: check the first and two items are 5
                if ((partarray[0].Length == 5) && (partarray[1].Length == 5))
                {
                    //Step 3 : if second item start with "UN, UM , 9N, 9M", compare with package number and lead to warning
                    if (partarray[1].Substring(0, 2).ToUpper() == "UN" || partarray[1].Substring(0, 2).ToUpper() == "UM" || partarray[1].Substring(0, 2).ToUpper() == "9N" || partarray[1].Substring(0, 2).ToUpper() == "9M")
                    {
                        //compare with package number . last three
                        if (partarray[1].Substring(2, 3).ToUpper() == packagename.Substring(packagename.Length - 3, 3))
                        {
                            result = 1;
                        }
                        else
                        {
                            result = 0;

                        }

                    }// not UN
                    else
                    {
                        // ' standpard partno
                        // Validate all the input header info
                        //"SELECT  top 1 *  FROM ETA.dbo.viewStandardParts WHERE Partno like '" & Replace(partnumber,"-","") & "%'" 
                        // check whether it is in database 
                        //if yes
                        dt = CommonDB.GetPartNumberCount(partno, true).Tables[0];

                        if (dt.Rows.Count > 0)
                        {
                            result = 1;
                        }
                        else
                        {
                            result = -1;
                        }

                    }
                }
                else
                {
                    result = -1;

                }

            } // end length>2
            else
            {


                result = -1;


            }




        }

        return result;




    }

    //public static bool IsvalidRefModulePartnumbers(string partcode, string code1, string code2, string assyname)
    //{

    //    //SELECT distinct PartNumber FROM  viewPartsList where code1='51u' and code2='01' and code3='04' and Num1='MB37'

    //    bool result = true;


    //    DataTable dt = CommonDB.GetPartListsInfo(partcode, assyname, code1, code2).Tables[0];

    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        string partno = dt.Rows[i]["PartNumber"].ToString().Trim();
    //        if(IsvalidPartno(partno,partcode)==-1)
    //        {
    //            result = false;

    //            break;
    //        }
    //    }

    //    return result;
    //}



    //public static bool IsvalidRecursiveRefModulePartnumbersold(string partcode, string code1, string code2, string assyname)
    //{

    //    bool result = true;
    //    //  Get List
    //    DataTable dt = CommonDB.GetPartListsInfo(partcode, assyname, code1, code2).Tables[0];



    //    // Check whether list have referred partno or not
    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        string partno = dt.Rows[i]["PartNumber"].ToString().Trim();
    //        if (RegexForRecursive.IsMatch(partno))
    //        {
    //            DataRow[] drs = CommonDB.StandardPartcodeDataSet.Tables[0].Select(string.Format("Model='{0}'", partcode));

    //            if (drs.Length <= 0)
    //            {
    //                // find the referred number , call recursive function

    //                //N4E0(561)01A-01
    //                if (partno.Length == 15)
    //                {
    //                    result = IsvalidRefModulePartnumbers(partno.Substring(0, 4), partno.Substring(9, 2), partno.Substring(11, 4), partno.Substring(5, 3));
    //                }
    //                else
    //                {
    //                    //N4E0(561)0101
    //                    result = IsvalidRefModulePartnumbers(partno.Substring(0, 4), partno.Substring(9, 2), partno.Substring(11, 2), partno.Substring(5, 3));
    //                }
    //                if (result == false)
    //                {
    //                    //  result = false;
    //                    break;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            // no referred partno
    //            if (IsvalidPartnoNew(partno, partcode,0) == -1)
    //            {
    //                result = false;
    //                break;
    //            }
    //        }




    //    }



    //    return result;
    //}

    public static bool IsvalidRefModulePartnumbers(string partcode, string code1, string code2, string assyname, ref string stringresult)
    {
        bool result = true;
        //  Get List
        DataTable dt = CommonDB.GetPartListsInfo(partcode, assyname, code1, code2).Tables[0];

        // Check whether list have referred partno or not
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string partno = dt.Rows[i]["PartNumber"].ToString().Trim();

            if (IsvalidPartnoNew(partno, partcode, 0, ref stringresult) == -1)
            {

                result = false;
                break;
            }

        }
        return result;
    }

    public static bool IsValidByConfigurator(string orderno, string cmbtext)
    {




        return CommonDB.GetConfiguratorResult(orderno, cmbtext);
    }



    /// <summary>
    /// Isvalids the pagecode.
    /// </summary>
    /// <param name="pagecode">The pagecode.</param>
    /// <param name="partcode">The partcode.</param>
    /// <param name="assyname">The assyname.</param>
    /// <returns></returns>
    public static int IsvalidPagecode(string pagecode, string partcode, string assyname, string assycode, string packagename, DataTable spartcodelist)
    {
        int result = 1;
        if (string.IsNullOrEmpty(pagecode) || !string.IsNullOrEmpty(assycode))
        {
            result = 1;
            return result;
        }
        DataTable dt = null;

        string GroupNo = assyname.Split("".ToCharArray())[0];

        // changed by dayang on 03/14/2013 with no space inside pagecode




        if (pagecode.Length != 2 && pagecode.Length != 4 && pagecode.Length != 6)
        {
            //  invalid 
            return -1;
        }
        string CompCode = null;
        string Vari = null;
        string Ser = null;

        CompCode = pagecode.Substring(0, 2);
        Vari = pagecode.Substring(2, 2);
        Ser = pagecode.Substring(4, pagecode.Length - 4) ?? "";
        /*
        string[] pagecodearray = pagecode.Split("".ToCharArray());

        switch (pagecodearray.Length)
        {
            case 1:
                CompCode = pagecodearray[0];
                Vari = "";
                Ser = "";
                break;
            case 2:
                CompCode = pagecodearray[0];
                Vari = pagecodearray[1];
                Ser = "";
                break;
            default:
                CompCode = pagecodearray[0];
                Vari = pagecodearray[1];
                Ser = pagecodearray[2];
                break;
        }
         * */
        // First Step : check the partcode is in standard  part list or not
        DataRow[] drs = spartcodelist.Select(string.Format("Model='{0}'", partcode));
        if (drs.Length > 0)
        {
            // if yes, go to viewstandpard to validate
            //SELECT Level_,PartNo,Minor,PartName,Qty,Dwg,Material1,Material2,PartNoCode1,PartNoComment1, PartNoCode2,PartNoComment2 FROM ETA.dbo.viewStandardParts WHERE Model='G851' AND GroupNo='110' AND CompCode='01' AND Vari='03' AND Ser='' ORDER BY Order_
            // Build SQL
            dt = CommonDB.GetStandardPartListInfo(partcode, GroupNo, CompCode, Vari, Ser).Tables[0];
            if (dt.Rows.Count != 0)
            {// valid
                result = 1;
            }
            else
            {
                result = -1;
            }
        }
        else
        {
            //    //step 2: check assycode is blank or not
            //viewFormCItems and viewPartsList
            if (string.IsNullOrEmpty(assycode.Trim()))
            {
                dt = CommonDB.GetNonStanardPartListInfoWithoutAssyCode(partcode, GroupNo, CompCode, Vari).Tables[0];

            }
            else
            {
                //viewPartsList
                // asscode; partcode
                dt = CommonDB.GetNonStanardPartListInfoWithAssyCode(packagename, GroupNo, partcode, assycode).Tables[0];

            }
            if (dt.Rows.Count != 0)
            {
                //valid
                result = 1;
            }
            else
            {
                result = -1;
            }
        }
        return result;
    }

    /// <summary>
    /// Writes to file.
    /// </summary>
    /// <param name="FileName">Name of the file.</param>
    /// <param name="Contents">The contents.</param>
    /// <param name="IsAppend">if set to <c>true</c> [is append].</param>
    public static void WriteToFile(string FileName, string Contents, bool IsAppend)
    {

        return;
        //StreamWriter swLog=null;


        //if (IsAppend)
        //{
        //    if (File.Exists(FileName))
        //    {
        //        swLog = File.AppendText(FileName);

        //    }
        //}
        //else
        //{

        //    swLog = new StreamWriter(FileName);
        //}

        //swLog.Write(Contents);
        //swLog.Close();
    }

    /// <summary>
    /// Moves the file.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="dest">The dest.</param>
    public static void MoveFile(string source, string dest)
    {
        return;

        // Ensure that the target does not exist.
        if (File.Exists(dest))
            File.Delete(dest);

        // Move the file.
        File.Move(source, dest);

    }

    //public static string GetFormattedPageCode(string input)
    //{

    //    string result = null;

    //    if (string.IsNullOrEmpty(input))
    //    {

    //        return result;

    //    }

    //    if (input.Length == 1)
    //    {
    //        result=input;
    //        return result;
    //    }

    //    input = input.Replace(" ", "");
    //    int length = 0;
    //    bool isodd = false;
    //    if (input.Length % 2 == 0)
    //    {
    //        isodd = false;
    //        length = input.Length / 2;
    //    }
    //    else
    //    {
    //        isodd = true;
    //        length = input.Length / 2 + 1;
    //    }

    //    if (length > 1)
    //    {
    //        result = input.Substring(0, 2);

    //        for (int i = 1; i < length; i++)
    //        {
    //            if (i == length - 1 && isodd)
    //            {
    //                result = string.Format("{0} {1}", result, input.Substring(2 * i));
    //            }
    //            else
    //            {
    //                result = string.Format("{0} {1}", result, input.Substring(2 * i, 2));
    //            }
    //        }
    //    }
    //    else
    //    {
    //        //lenth<=1
    //        result = input;

    //    }
    //    return result;

    //}
    public static string GetFormattedPageCodeUpdate(string input)
    {
        // changed on 03/13/2013 by dayang by requiremenet



        string result = null;

        if (string.IsNullOrEmpty(input))
        {

            return result;

        }



        input = input.Replace(" ", "").Trim();

        return input;
        if (input.Length < 3)
        {
            result = input;
            return result;
        }


        if (input.Length > 4)
        {
            result = string.Format("{0} {1} {2}", input.Substring(0, 2), input.Substring(2, 2), input.Substring(4));
        }
        else
        {
            result = string.Format("{0} {1}", input.Substring(0, 2), input.Substring(2));
        }

        return result;

    }


    //public static bool IsReferencePartNoValid(string partno,int recursivelevel)
    //{
    //    bool result = true;

    //    if (recursivelevel > 5)
    //    {

    //        result = false;
    //    }
    //    else
    //    {
    //        //First check partno is from standpard browser or not

    //        if (true)
    //        {

    //            result = true;
    //        }
    //        else
    //        {

    //            // Get all sub part no with this list
    //            string package = partno.Substring(0, 4);
    //            string code1 = partno.Substring(5, 3);
    //            string code2 = partno.Substring(9, 2);
    //            string code3 = partno.Substring(11);

    //            //// SELECT * FROM ETA.dbo.viewPartsList AS m WHERE m.DesignNumber LIKE 'MB0M%' and Code1='561' and code2='09' and code3='A-01'
    //            DataTable dt = CommonDB.GetRefPartLists(package, code1, code2, code3).Tables[0];

    //            foreach (DataRow dr in dt.Rows)
    //            {
    //               // dr["partnumber"]

    //                if (IsvalidPartno(dr["partnumber"].ToString().Trim(),package)<0)
    //                {
    //                    result = false;
    //                    break;
    //                }
    //            }


    //            //if one part failed validation ,return false;



    //        }



    //    }




    //    return result;


    //}

    /// <summary>
    /// Determines whether [is valid format part no] [the specified partno].
    /// </summary>
    /// <param name="partno">The partno.</param>
    /// <returns>
    /// 	<c>true</c> if [is valid format part no] [the specified partno]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsValidFormatPartNo(string partno)
    {
        bool result = false;
        Regex re = new Regex("(^\\w{5}[-]\\w{5}[\\w-]*$)|(^\\w{4}[(]\\w{3}[)][\\w-]*$)", RegexOptions.None);


        if (!re.IsMatch(partno))
        {
            result = false;
        }
        else
        {
            result = true;
        }
        return result;
    }


    /// <summary>
    /// Isvalids the partno new.
    /// </summary>
    /// <param name="partno">The partno.</param>
    /// <param name="packagename">The packagename.</param>
    /// <param name="recursivelevel">The recursivelevel.</param>
    /// <returns></returns> 
    public static int IsvalidPartnoNew(string partno, string packagename, int recursivelevel, ref string statusresult)
    {
        //xxxxx-xxxxx    	 94223-80800	  with len=10
        //xxxxx-xxxxx-x   	94223-80800-C	  with len=10 and minor
        //xxxxx-xxxxx-xx	96364-12240-71	   with len=12
        //xxxxx-xxxxx-xx-x 	51130-UM41X-71-C	with len=12 and minor
        //xxxx(xxx)xxxx     	G851(561)0101	look for combination
        //xxxx(xxx)xxx-xx   	G851(561)01F-04	with len=9


        //partno = "(G851) 561-01)01";




#if   DEBUG
            
            System.Diagnostics.Debug.WriteLine(partno + ";" + recursivelevel);
 

#else

#endif

        // check part no is from Exclusivelist or not
        if (CommonDB.IsPartnoInExclusiveTable(partno))
        {
            statusresult = string.Empty;
            return 1;
        }


        if (recursivelevel >= MaxValidationRecursiveLevel)
        {
            statusresult = "Recursive Level Error";
            return -1;
        }

        int result = 1;

        try
        {
            // format validation
            //Regex re = new Regex("(^\\w{5}[-]\\w{5}[\\w-]*$)|(^\\w{4}[(]\\w{3}[)][\\w-]*$)", RegexOptions.None);
            //if (!re.IsMatch(partno))

            if (!IsValidFormatPartNo(partno))
            {
                result = -1;
                statusresult = partno;
                return result;

            }


            Regex re2 = new Regex("(^\\w{5}[-]\\w{5}[\\w-]*$)", RegexOptions.None);
            if (re2.IsMatch(partno))
            {
                string pa = partno.Substring(6, 2);
                if (pa.ToUpper() == "UN" || pa.ToUpper() == "UM" || pa.ToUpper() == "9N" || pa.ToUpper() == "9M")
                {
                    result = 0;
                    statusresult = string.Empty;
                    return result;
                }

            }

            string cleanuppartno = PartNumberCleanUpUpdate(partno);
            // handle xxxxx-xxxxx 

            if (partno.Length == 11 && (partno.Substring(5, 1) == "-"))
            {
                //string cleanupno=partno.Substring(0,5)
                if (CommonDB.GetFirstPartNoFromStandPartsList(cleanuppartno).Tables[0].Rows.Count > 0)
                {
                    statusresult = string.Empty;
                    result = 1;
                }
                else
                {
                    statusresult = partno;
                    result = -1;
                }


                return result;


            }

            //handle xxxxx-xxxxx-x
            if (partno.Length == 13 && (partno.Substring(5, 1) == "-"))
            {
                if (CommonDB.GetFirstPartNoWithMinorFromStandPartsList(cleanuppartno.Substring(0, 10), cleanuppartno.Substring(10, 1)).Tables[0].Rows.Count > 0)
                {

                    statusresult = string.Empty;
                    result = 1;
                }
                else
                {
                    statusresult = partno;
                    result = -1;
                }


                return result;


            }

            //handle xxxxx-xxxxx-xx
            if (partno.Length == 14 && (partno.Substring(5, 1) == "-"))
            {
                if (CommonDB.GetFirstPartNoFromStandPartsList(cleanuppartno).Tables[0].Rows.Count > 0)
                {

                    statusresult = string.Empty;
                    result = 1;
                }
                else
                {
                    statusresult = partno;
                    result = -1;
                }


                return result;


            }


            // handle xxxxx-xxxxx-xx-x
            if (partno.Length == 16 && (partno.Substring(5, 1) == "-"))
            {
                if (CommonDB.GetFirstPartNoWithMinorFromStandPartsList(cleanuppartno.Substring(0, 12), cleanuppartno.Substring(12, 1)).Tables[0].Rows.Count > 0)
                {

                    statusresult = string.Empty;
                    result = 1;
                }
                else
                {
                    statusresult = partno;
                    result = -1;
                }


                return result;


            }

            //handle xxxx(xxx)xxxx 


            if (partno.Length == 13)
            {
                string model = cleanuppartno.Substring(0, 4);
                string groupno = cleanuppartno.Substring(4, 3);
                string compcode = cleanuppartno.Substring(7, 2);
                string vari = cleanuppartno.Substring(9, 2);
                if (CommonDB.GetStandardPartListInfo(model, groupno, compcode, vari, "").Tables[0].Rows.Count > 0)
                {
                    statusresult = string.Empty;
                    result = 1;


                }
                else
                {
                    DataTable dt1 = CommonDB.GetRefPartLists(model, groupno, compcode, vari).Tables[0];
                    int sdy = recursivelevel + 1;
                    foreach (DataRow dr in dt1.Rows)
                    {
                        // dr["partnumber"]

                        if (IsvalidPartnoNew(dr["partnumber"].ToString().Trim(), model, sdy, ref statusresult) < 0)
                        {

                            result = -1;
                            break;
                        }
                    }




                }

                return result;
            }


            //handle xxxx(xxx)xxx-xx   	G851(561)01F-04	with len=9
            if (partno.Length == 15)
            {
                string model = partno.Substring(0, 4);
                string code1 = partno.Substring(5, 3);
                string code2 = partno.Substring(9, 2);
                string code3 = partno.Substring(11);
                //GetStandardPartListInfoWithMino
                string po = cleanuppartno.Substring(4, 6) + "-" + cleanuppartno.Substring(10, 2);
                if (CommonDB.GetStandardPartListInfoWithMinor(model, po).Tables[0].Rows.Count > 0)
                {
                    statusresult = string.Empty;
                    result = 1;
                }
                else
                {
                    DataTable dt1 = CommonDB.GetRefPartLists(model, code1, code2, code3).Tables[0];
                    int sdy = recursivelevel + 1;
                    foreach (DataRow dr in dt1.Rows)
                    {
                        // dr["partnumber"]

                        if (IsvalidPartnoNew(dr["partnumber"].ToString().Trim(), model, sdy, ref statusresult) < 0)
                        {

                            result = -1;
                            break;
                        }
                    }
                }


                return result;
            }



        } // end try
        catch
        {
            statusresult = "Exception Error";
            result = -1;
        }



        return result;


    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="partno"></param>
    /// <param name="packagename"></param>
    /// <param name="recursivelevel"></param>
    /// <returns></returns>
    public static PartNoValidationResult IsvalidPartnoWithEnu(string partno, string packagename, int recursivelevel)
    {
        //**xxxxx-xxxxx    	 94223-80800	  with len=10
        //xxxxx-xxxxx-x   	94223-80800-C	  with len=10 and minor
        //**xxxxx-xxxxx-xx	96364-12240-71	   with len=12
        //xxxxx-xxxxx-xx-x 	51130-UM41X-71-C	with len=12 and minor
        //**xxxx(xxx)xxxx     	G851(561)0101	look for combination
        //**xxxx(xxx)xxx-xx   	G851(561)01F-04	with len=9


        //partno = "(G851) 561-01)01";




#if   DEBUG
            
            System.Diagnostics.Debug.WriteLine(partno + ";" + recursivelevel);
 

#else

#endif

        // check part no is from Exclusivelist or not
        if (CommonDB.IsPartnoInExclusiveTable(partno))
        {
            return PartNoValidationResult.Valid;
        }


        if (recursivelevel >= MaxValidationRecursiveLevel)
        {

            return PartNoValidationResult.BeyondRecursiveLevel;
        }


        PartNoValidationResult result2 = PartNoValidationResult.Valid;

        try
        {
            // format validation
            //Regex re = new Regex("(^\\w{5}[-]\\w{5}[\\w-]*$)|(^\\w{4}[(]\\w{3}[)][\\w-]*$)", RegexOptions.None);
            //if (!re.IsMatch(partno))

            if (!IsValidFormatPartNo(partno))
            {

                return PartNoValidationResult.InvalidFormat;

            }


            Regex re2 = new Regex("(^\\w{5}[-]\\w{5}[\\w-]*$)", RegexOptions.None);
            if (re2.IsMatch(partno))
            {
                string pa = partno.Substring(6, 2);
                if (pa.ToUpper() == "UN" || pa.ToUpper() == "UM" || pa.ToUpper() == "9N" || pa.ToUpper() == "9M")
                {


                    return PartNoValidationResult.Warning;
                }

            }




            string cleanuppartno = PartNumberCleanUpUpdate(partno);
            // handle xxxxx-xxxxx 

            if (partno.Length == 11 && (partno.Substring(5, 1) == "-"))
            {
                //string cleanupno=partno.Substring(0,5)
                if (CommonDB.GetFirstPartNoFromStandPartsList(cleanuppartno).Tables[0].Rows.Count > 0)
                {

                    return PartNoValidationResult.Valid;
                }
                else
                {

                    CommonDB.InsertExclusivePartListItems(partno, packagename);
                    return PartNoValidationResult.InvalidValue;
                }



            }

            //handle xxxxx-xxxxx-x
            if (partno.Length == 13 && (partno.Substring(5, 1) == "-"))
            {
                if (CommonDB.GetFirstPartNoWithMinorFromStandPartsList(cleanuppartno.Substring(0, 10), cleanuppartno.Substring(10, 1)).Tables[0].Rows.Count > 0)
                {

                    return PartNoValidationResult.Valid;
                }
                else
                {
                    CommonDB.InsertExclusivePartListItems(partno, packagename);
                    return PartNoValidationResult.InvalidValue;
                }





            }

            //handle xxxxx-xxxxx-xx
            if (partno.Length == 14 && (partno.Substring(5, 1) == "-"))
            {
                if (CommonDB.GetFirstPartNoFromStandPartsList(cleanuppartno).Tables[0].Rows.Count > 0)
                {

                    return PartNoValidationResult.Valid;
                }
                else
                {
                    CommonDB.InsertExclusivePartListItems(partno, packagename);
                    return PartNoValidationResult.InvalidValue;

                }




            }


            // handle xxxxx-xxxxx-xx-x
            if (partno.Length == 16 && (partno.Substring(5, 1) == "-"))
            {
                if (CommonDB.GetFirstPartNoWithMinorFromStandPartsList(cleanuppartno.Substring(0, 12), cleanuppartno.Substring(12, 1)).Tables[0].Rows.Count > 0)
                {

                    return PartNoValidationResult.Valid;
                }
                else
                {
                    CommonDB.InsertExclusivePartListItems(partno, packagename);
                    return PartNoValidationResult.InvalidValue;
                }





            }

            //handle xxxx(xxx)xxxx 



            if (partno.Length == 13)
            {
                string model = cleanuppartno.Substring(0, 4);
                string groupno = cleanuppartno.Substring(4, 3);
                string compcode = cleanuppartno.Substring(7, 2);
                string vari = cleanuppartno.Substring(9, 2);
                if (CommonDB.GetStandardPartListInfo(model, groupno, compcode, vari, "").Tables[0].Rows.Count > 0)
                {
                    return PartNoValidationResult.Valid;


                }
                else
                {


                    DataTable dt1 = CommonDB.GetRefPartLists(model, groupno, compcode, vari).Tables[0];
                    int sdy = recursivelevel + 1;
                    foreach (DataRow dr in dt1.Rows)
                    {
                        // dr["partnumber"]
                        PartNoValidationResult temp = IsvalidPartnoWithEnu(dr["partnumber"].ToString().Trim(), model, sdy);
                        if (temp < 0)
                        {
                            //if it is 

                            if (temp == PartNoValidationResult.InvalidValue)
                            {
                                //  can insert this part number to tracking table 
                                CommonDB.InsertExclusivePartListItems(dr["partnumber"].ToString().Trim(), model);
                            }

                            result2 = temp;
                            break;
                        }
                    }

                    return result2;



                }



            }


            //handle xxxx(xxx)xxx-xx   	G851(561)01F-04	with len=9
            if (partno.Length == 15)
            {
                string model = partno.Substring(0, 4);
                string code1 = partno.Substring(5, 3);
                string code2 = partno.Substring(9, 2);
                string code3 = partno.Substring(11);
                //GetStandardPartListInfoWithMino
                string po = cleanuppartno.Substring(4, 6) + "-" + cleanuppartno.Substring(10, 2);
                if (CommonDB.GetStandardPartListInfoWithMinor(model, po).Tables[0].Rows.Count > 0)
                {
                    result2 = PartNoValidationResult.Valid;
                }
                else
                {
                    DataTable dt1 = CommonDB.GetRefPartLists(model, code1, code2, code3).Tables[0];
                    int sdy = recursivelevel + 1;
                    foreach (DataRow dr in dt1.Rows)
                    {
                        // dr["partnumber"]
                        PartNoValidationResult temp = IsvalidPartnoWithEnu(dr["partnumber"].ToString().Trim(), model, sdy);
                        if (temp < 0)
                        {
                            if (temp == PartNoValidationResult.InvalidValue)
                            {
                                //  can insert this part number to tracking table 
                                CommonDB.InsertExclusivePartListItems(dr["partnumber"].ToString().Trim(), model);
                            }

                            result2 = temp;
                            break;
                        }
                    }
                }


                return result2;
            }



        } // end try
        catch
        {

            result2 = PartNoValidationResult.ExceptionError;
        }



        return result2;


    }

    /// <summary>
    /// Add in 10/23/2013
    /// </summary>
    /// <param name="partno"></param>
    /// <param name="packagename"></param>
    /// <param name="recursivelevel"></param>
    /// <returns></returns>
    public static PartNoValidationResult IsvalidTSDPartnoWithEnu(string partno, string packagename, int recursivelevel)
    {
        //**xxxxx-xxxxx    	 94223-80800	  with len=10
        //**xxxxx-xxxxx-xx	96364-12240-71	   with len=12
        //**xxxx(xxx)xxxx     	G851(561)0101	look for combination
        //**xxxx(xxx)xxx-xx   	G851(561)01F-04	with len=9


        //partno = "(G851) 561-01)01";




#if   DEBUG
            
            System.Diagnostics.Debug.WriteLine(partno + ";" + recursivelevel);
 

#else

#endif



        if (recursivelevel >= MaxValidationRecursiveLevel)
        {

            return PartNoValidationResult.BeyondRecursiveLevel;
        }


        PartNoValidationResult result2 = PartNoValidationResult.Valid;

        try
        {
            // format validation
            //Regex re = new Regex("(^\\w{5}[-]\\w{5}[\\w-]*$)|(^\\w{4}[(]\\w{3}[)][\\w-]*$)", RegexOptions.None);
            //if (!re.IsMatch(partno))

            if (!IsValidFormatPartNo(partno))
            {

                return PartNoValidationResult.InvalidFormat;

            }


            string cleanuppartno =
            PartNumberTSDCleanUpUpdate(partno);
            // handle xxxxx-xxxxx 

            if (partno.Length == 11 && (partno.Substring(5, 1) == "-"))
            {
                //string cleanupno=partno.Substring(0,5)
                // from two 05 table , 
                //  remove space , dash
                // check if any record exist in 05 or 05_GPN exist

                if (CommonDB.IsValidTSDPartno_05(cleanuppartno))
                {

                    return PartNoValidationResult.Valid;
                }
                else
                {

                    // CommonDB.InsertExclusivePartListItems(partno, packagename);
                    return PartNoValidationResult.InvalidValue;
                }



            }



            //handle xxxxx-xxxxx-xx
            if (partno.Length == 14 && (partno.Substring(5, 1) == "-"))
            {
                // from two 05 table , 
                //  remove space , dash
                // check if any record exist in 05 or 05_GPN exist
                if (CommonDB.IsValidTSDPartno_05(cleanuppartno))
                {

                    return PartNoValidationResult.Valid;
                }
                else
                {
                    // CommonDB.InsertExclusivePartListItems(partno, packagename);
                    return PartNoValidationResult.InvalidValue;

                }




            }

            //handle xxxx(xxx)xxxx or   //handle xxxx(xxx)xxx-xx   	G851(561)01F-04	with len=9
            if (partno.Length == 13 || partno.Length == 15)
            {
                string tfc = cleanuppartno.Substring(0, 4);

                if (CommonDB.IsValidTSDPartno_06(tfc, cleanuppartno.Substring(4, cleanuppartno.Length - 4)))
                {

                    return PartNoValidationResult.Valid;
                }
                else
                {
                    // CommonDB.InsertExclusivePartListItems(partno, packagename);
                    return PartNoValidationResult.InvalidValue;

                }
            }


        } // end try
        catch
        {

            result2 = PartNoValidationResult.ExceptionError;
        }



        return result2;


    }
    /// <summary>
    /// Add to get pairid of id ; 
    /// Can be used for validation
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string GetPairID(DataTable dt, string id)
    {
        string result = string.Empty;

        foreach (DataRow dr in dt.Rows)
        {
            if (dr["CItemid"].ToString() == id)
            {
                if (string.IsNullOrEmpty(dr["PCItemid"].ToString())) // if pairid is null
                {
                    if (dr.RowState == DataRowState.Unchanged)
                    {

                        //     get currenet cittemid 
                        //  string cp=dr["CItemId"].ToString().Trim()+

                        // Get pathc count
                        string citemid = dr["CItemid"].ToString();

                        if (dr["CategoryAddress"].ToString().Length < 3)
                        {
                            continue;
                        }
                        string f2partcode = string.Empty;
                        if (dr["AssyCode"].ToString().Length > 0)
                        {
                            f2partcode = dr["AssyCode"].ToString();
                        }
                        else
                        {
                            if (dr["PageCode"].ToString().Length < 2)
                            {
                                continue;
                            }
                            else
                            {
                                f2partcode = dr["PageCode"].ToString().Substring(0, 2);
                            }
                        }


                        string groupno = dr["CategoryAddress"].ToString().Substring(0, 3);
                        int key = Convert.ToInt16(dr["key"].ToString());

                        IEnumerable<DataRow> rows = null;
                        rows = (from p in dt.AsEnumerable()
                                where
                                    (p.Field<int>("key") == key) &&
                                    (p.Field<string>("CategoryAddress").Substring(0, 3) == groupno) &&
                                    (p.Field<string>("AssyCode") == f2partcode ||
                                     p.Field<string>("PageCode").Substring(0,
                                                                           Math.Min(p.Field<string>("PageCode").Length, 2)) ==
                                     f2partcode)
                                //orderby p.Field<string>("Treatment") ascending
                                select p).AsEnumerable();
                        var list1 = rows.ToList();
                        if (list1.Count == 2)
                        {
                            // at this point , the paired count is 2 , still  could have error like  s, s
                            string id1 = ((DataRow)list1[0])["CItemid"].ToString();
                            string id2 = ((DataRow)list1[1])["CItemid"].ToString();
                            string joinedtreatment = ((DataRow)list1[0])["Treatment"].ToString().Trim() +
                                                     ((DataRow)list1[1])["Treatment"].ToString().Trim();

                            if (joinedtreatment == "DS" || joinedtreatment == "SD")
                            {
                                // paired

                                //  string pid = rows.ElementAt(0)["CitemId"].ToString();
                                //builder.Append(string.Format("{0},{1};{1},{0};", id1, id2));
                                int index = dt.Rows.IndexOf(rows.ToList()[1]);
                                dt.Rows[index].AcceptChanges();
                                dt.Rows[index].SetModified();
                                return id2;
                            }


                        }

                    }
                }
                else
                {

                    return dr["PCItemid"].ToString();
                }
            }



        }


        return result;


    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static string GetFormCAutoPairStringByDataTable(DataTable dt)
    {
        // Get all non null records

        //  DataTable dt = dt1.Copy();

        // Get ID and other parameter
        //DesignNumber, ltrim(rtrim(CategoryAddress)) as CategoryAddress, ModuleLocation, KeyNumber, [key], AssyCode, Treatment, PartCode, PageCode, Description, CItemId,PCItemId,AItemId

        //        declare @str varchar(100)
        //set @str = '123456,123457;343443,345656;29874,29875;29875,29874'

        //exec  [dbo].[BulkUpdateFormCItems]  @str
        StringBuilder builder = new StringBuilder(4000);

        foreach (DataRow dr in dt.Rows)
        {
            var sdy = dr["CItemid"].ToString();


            if (string.IsNullOrEmpty(dr["PCItemid"].ToString())) // if pairid is null
            {
                if (dr.RowState == DataRowState.Unchanged)
                {

                    //     get currenet cittemid 
                    //  string cp=dr["CItemId"].ToString().Trim()+

                    // Get pathc count
                    string citemid = dr["CItemid"].ToString();

                    if (dr["CategoryAddress"].ToString().Length < 3)
                    {
                        continue;
                    }
                    string f2partcode = string.Empty;
                    if (dr["AssyCode"].ToString().Length > 0)
                    {
                        f2partcode = dr["AssyCode"].ToString();
                    }
                    else
                    {
                        if (dr["PageCode"].ToString().Length < 2)
                        {
                            continue;
                        }
                        else
                        {
                            f2partcode = dr["PageCode"].ToString().Substring(0, 2);
                        }
                    }


                    string groupno = dr["CategoryAddress"].ToString().Substring(0, 3);
                    int key = Convert.ToInt16(dr["key"].ToString());

                    IEnumerable<DataRow> rows = null;
                    rows = (from p in dt.AsEnumerable()
                            where
                                (p.Field<int>("key") == key) &&
                                (p.Field<string>("CategoryAddress").Substring(0, 3) == groupno) &&
                                (p.Field<string>("AssyCode") == f2partcode ||
                                 p.Field<string>("PageCode").Substring(0,
                                                                       Math.Min(p.Field<string>("PageCode").Length, 2)) ==
                                 f2partcode)
                            //orderby p.Field<string>("Treatment") ascending
                            select p).AsEnumerable();
                    var list1 = rows.ToList();
                    if (list1.Count == 2)
                    {
                        // at this point , the paired count is 2 , still  could have error like  s, s
                        string id1 = ((DataRow)list1[0])["CItemid"].ToString();
                        string id2 = ((DataRow)list1[1])["CItemid"].ToString();
                        string joinedtreatment = ((DataRow)list1[0])["Treatment"].ToString().Trim() +
                                                 ((DataRow)list1[1])["Treatment"].ToString().Trim();

                        if (joinedtreatment == "DS" || joinedtreatment == "SD")
                        {
                            // paired

                            //  string pid = rows.ElementAt(0)["CitemId"].ToString();
                            builder.Append(string.Format("{0},{1};{1},{0};", id1, id2));
                            int index = dt.Rows.IndexOf(rows.ToList()[1]);
                            dt.Rows[index].AcceptChanges();
                            dt.Rows[index].SetModified();
                        }

                        //   IEnumerable<DataRow> rows1 = null;
                        //if (dr["Treatment"].ToString() == "S")
                        //{
                        //    //rows = (from p in dt.AsEnumerable()
                        //    //        where
                        //    //            (p.Field<string>("CategoryAddress").Substring(0, 3) == groupno) &&
                        //    //            (p.Field<string>("AssyCode") == f2partcode || p.Field<string>("PageCode").Substring(0, Math.Min(p.Field<string>("PageCode").Length, 2)) == f2partcode) &&
                        //    //            (p.Field<string>("Treatment") == "D")
                        //    //        select p).AsEnumerable().ToList();


                        //}
                    }
                    //if (dr["Treatment"].ToString() == "S")
                    //{
                    //    rows = (from p in dt.AsEnumerable()
                    //            where
                    //                (p.Field<string>("CategoryAddress").Substring(0, 3) == groupno) &&
                    //                (p.Field<string>("AssyCode") == f2partcode || p.Field<string>("PageCode").Substring(0, Math.Min(p.Field<string>("PageCode").Length, 2)) == f2partcode) &&
                    //                (p.Field<string>("Treatment") == "D")
                    //            select p).AsEnumerable().ToList();

                    //}

                    //else
                    //{

                    //    rows = (from p in dt.AsEnumerable()
                    //            where
                    //                (p.Field<string>("CategoryAddress").Substring(0, 3) == groupno) &&
                    //                (p.Field<string>("AssyCode") == f2partcode || p.Field<string>("PageCode").Substring(0, Math.Min(p.Field<string>("PageCode").Length, 2)) == f2partcode) &&
                    //                (p.Field<string>("Treatment") == "S")
                    //            select p).AsEnumerable().ToList();


                    //}

                    //if (rows.ToList().Count == 1)
                    //{
                    //    //  build id parameter
                    //    // get cid

                    //    string pid = rows.ElementAt(0)["CitemId"].ToString();
                    //    builder.Append(string.Format("{0},{1};{1},{0};", citemid, pid));
                    //    // set row processed
                    //    int index = dt.Rows.IndexOf(rows.ToList()[0]);
                    //    dt.Rows[index].AcceptChanges();
                    //    dt.Rows[index].SetModified();

                    //}
                }
            }
        }

        // check builder to total size which should smaller than 4000
        string dbinputs = builder.ToString();
        // the last character will be ;, so remove it 
        return dbinputs;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="package"></param>
    /// <param name="errormessage"></param>
    /// <returns></returns>
    public static bool AutoPairFormCItemByPackage(string package, ref string errormessage)
    {
        bool result = false;
        try
        {

            DataTable dt = CommonDB.GetFormCInfoByPackageNameAndModule(package, null, false).Tables[0];
            string dbinputs = GetFormCAutoPairStringByDataTable(dt);

            List<String> subinputs = dbinputs.Split(';').ToList();
            // Will bulk update per 200 

            float count = subinputs.Count;
            int times = (int)Math.Ceiling(count / MAXAUTOPAIRCOUNT);

            string dbinputs2 = string.Empty;
            for (int i = 0; i < times; i++)
            {
                // get dbinputs2
                string[] currentinputs = subinputs.Skip(i * MAXAUTOPAIRCOUNT).Take(MAXAUTOPAIRCOUNT).ToArray();
                // convert to string
                dbinputs2 = string.Join(";", currentinputs);



                if (dbinputs.Length > 0)
                {
                    if (dbinputs2.Length >= 4000)
                    {
                        // this.btnpair.Enabled = false;
                        errormessage = "Error: input is more than 4000 ";
                        result = false;

                    }
                    else
                    {
                        CommonDB.BulkUpdateFormCItems(dbinputs2);
                        result = true;
                    }
                }
            }





        }
        catch (Exception err)
        {

            errormessage = err.Message;

        }


        return result;
    }


    public static string ProcessedFormCDesc(string input)
    {

        return input.Replace("'", "''");
        ;

    }


    public static int IsvalidPKGTFC(string addtfc, string finaladdindex)
    {
        int result = 1;

        if (CommonDB.GetNewFormCRecord(addtfc, finaladdindex).Tables[0].Rows.Count == 0)
        {
            result = 1;
        }
        else
        {
            result = -1;
        }
        return result;

    }

    public static int IsvalidSTDTFC(string addtfc, string assycode, string assyname)
    {
        int result = 1;

        string CompCode = null;
        string Vari = null;
        string Ser = null;

        CompCode = assycode.Substring(0, 2);
        Vari = assycode.Substring(2, 2);
        Ser = assycode.Substring(4, assycode.Length - 4) ?? "";

        DataTable dt = CommonDB.GetStandardPartListInfo(addtfc, assyname, CompCode, Vari, Ser).Tables[0];
        if (dt.Rows.Count != 0)
        {// valid
            result = 1;
        }
        else
        {
            result = -1;
        }
        return result;
    }

    #region Caching Record
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static DataTable GetCategory()
    {
        object dt = GetCacheRecord("CategoryTable");

        if (dt != null)
        {
            return (DataTable)dt;
        }
        else
        {
            DataTable dbdt = CommonDB.GetCategory().Tables[0];
            // caching record for 60 minutes
            InsertCacheRecord("CategoryTable", dbdt, 60);

            return dbdt;
        }

    }


    #endregion
}

/// <summary>
/// 
/// </summary>
public class CollectionHelper
{
    private CollectionHelper()
    {
    }

    public static DataTable ConvertTo<T>(IList<T> list)
    {
        DataTable table = CreateTable<T>();
        Type entityType = typeof(T);
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

        foreach (T item in list)
        {
            DataRow row = table.NewRow();

            foreach (PropertyDescriptor prop in properties)
            {
                row[prop.Name] = prop.GetValue(item);
            }

            table.Rows.Add(row);
        }

        return table;
    }

    public static IList<T> ConvertTo<T>(IList<DataRow> rows)
    {
        IList<T> list = null;

        if (rows != null)
        {
            list = new List<T>();

            foreach (DataRow row in rows)
            {
                T item = CreateItem<T>(row);
                list.Add(item);
            }
        }

        return list;
    }

    public static IList<T> ConvertTo<T>(DataTable table)
    {
        if (table == null)
        {
            return null;
        }

        List<DataRow> rows = new List<DataRow>();

        foreach (DataRow row in table.Rows)
        {
            rows.Add(row);
        }

        return ConvertTo<T>(rows);
    }

    public static T CreateItem<T>(DataRow row)
    {
        T obj = default(T);
        if (row != null)
        {
            obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in row.Table.Columns)
            {
                PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                try
                {
                    object value = row[column.ColumnName];
                    prop.SetValue(obj, value, null);
                }
                catch
                {
                    // You can log something here
                    throw;
                }
            }
        }

        return obj;
    }

    public static DataTable CreateTable<T>()
    {
        Type entityType = typeof(T);
        DataTable table = new DataTable(entityType.Name);
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

        foreach (PropertyDescriptor prop in properties)
        {
            table.Columns.Add(prop.Name, prop.PropertyType);
        }

        return table;
    }


}
