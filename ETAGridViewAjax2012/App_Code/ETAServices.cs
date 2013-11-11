using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for ETAServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class ETAServices : System.Web.Services.WebService
{

    public ETAServices()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "Returns JSON array.")]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool ValidateModule(string module)
    {
        return CommonTool.IsvalidRefModule(module);
    }
    [WebMethod(Description = "Returns JSON array of Invalid Part Numbers.")]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetInvalidPartNosFromModule(string module)
    {
        string result=CommonTool.GetInvalidPartNosFromModule(module);
        if ((!string.Equals(result, "SUCCESS"))&&(!string.Equals(result,"Having Invalid Quantity")))
        {
           // create a datatable (dbo.ExclusivePartsListItems)if not "success"

            DataTable dtparts = ConvertStringToDataTableWithValidFormat(result, ';');

            // check this datatable to find any part number with invalid formats

            if (dtparts != null)
            {

                // buik insert the datatabale to ETA
                using (SqlConnection connection = new SqlConnection(CommonDB.ETAConnectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                        {
                            bulkCopy.ColumnMappings.Clear();
                            foreach (DataColumn dc in dtparts.Columns)
                            {
                                bulkCopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                            }

                            bulkCopy.DestinationTableName =
                                "dbo.ExclusivePartsListItems";
                            // Write from the source to the destination.
                            bulkCopy.WriteToServer(dtparts);
                        }
                       transaction.Commit();

                    }
                    catch (Exception err)
                    {
                        transaction.Rollback();
                        result = string.Format(" ExclusiveList Insertion Failed : {0}", result);
                    }
                    connection.Close();
                }

            }
            else
            {
                result = string.Format("INV:{0}", result);
            }
        
        }

        return result;
    

    }



    /// <summary>
    /// Converts the string to data table with valid format.
    /// </summary>
    /// <param name="inputstring">The inputstring.</param>
    /// <param name="separator">The separator.</param>
    /// <returns></returns>
    private DataTable ConvertStringToDataTableWithValidFormat(string inputstring, char separator)
    {

        string[] PartNos = inputstring.Substring(0, inputstring.Length - 2).Split(separator);

        // Create a new DataTable.
        System.Data.DataTable table = new DataTable();
        // Declare variables for DataColumn and DataRow objects.

        DataColumn column;

        // Create new DataColumn, set DataType, 
        // ColumnName and add to DataTable.    

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "PartNumber";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "Package";
        table.Columns.Add(column);
                
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Boolean");
        column.ColumnName = "ActiveStatus";
        table.Columns.Add(column);

        for (int i = 0; i < PartNos.Length - 1; i++)
        {
            string[] array = PartNos[i].Split('@');
            if (CommonTool.IsValidFormatPartNo(array[0]))
            {
                table.Rows.Add(array[0], array[1], false);
            }
            else
            {
                return null;
            }
        }

        return table;
    }



    //test

    [WebMethod(Description = "Returns JSON array of Invalid Part Numbers.")]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetJasonResultTest()
    {


        return "module1:module2";


    }
}

