using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

public partial class EtaAdmin_upload_TiemHistoryUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
             

        Stopwatch sw = new Stopwatch();

        sw.Start();

        DataTable dtname = AccessCommonDB.GetTableValuesByName("ac_Tiem_Name");
        DataTable dtparts = AccessCommonDB.GetTableValuesByName("ac_Tiem_Parts");
        DataTable dtecilist = AccessCommonDB.GetTableValuesByName("ECI_No"); //GetTableValuesByName("ECI_List");
        using (SqlConnection connection = new SqlConnection(CommonDB.ETAConnectionString))
        {
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {

                //delete info of dbo.ac_History_Tiem_Parts ,dbo.ac_History_Tiem_Name, ETA.dbo.ac_ECI_list                                         
                CommonDB.ETATableDelete(transaction, "dbo.ac_Tiem_Parts");
                CommonDB.ETATableDelete(transaction, "dbo.ac_Tiem_Name");
                CommonDB.ETATableDelete(transaction, "dbo.ac_ECI_No"); 
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                {
                    this.txtstatus.Text = string.Empty;

                    SetColumnMapping(dtparts, bulkCopy);
                    //dbo.ac_History_Tiem_Parts
                    bulkCopy.DestinationTableName =
                         "dbo.ac_History_Tiem_Parts";
                    // Write from the source to the destination.
                    bulkCopy.WriteToServer(dtparts);

                    //For dbo.ac_Tiem_Parts
                    bulkCopy.DestinationTableName =
                        "dbo.ac_Tiem_Parts";
                    // Write from the source to the destination.
                    bulkCopy.WriteToServer(dtparts);


                    SetColumnMapping(dtname, bulkCopy);

                    bulkCopy.DestinationTableName =
                      "dbo.ac_Tiem_Name";
                    // Write from the source to the destination.
                    bulkCopy.WriteToServer(dtname);

                                     
                    
                    bulkCopy.DestinationTableName =
                        "dbo.ac_History_Tiem_Name";
                    // Write from the source to the destination.
                    bulkCopy.WriteToServer(dtname);

                   
                    SetColumnMapping(dtecilist, bulkCopy);
                    bulkCopy.DestinationTableName =
                        "dbo.ac_ECI_No";                      
                    // Write from the source to the destination.
                    bulkCopy.WriteToServer(dtecilist);

                }
                transaction.Commit();
                sw.Stop();
                this.txtstatus.Text = string.Format("Status : Success. Running Time : {0}ms", sw.ElapsedMilliseconds);


            }
            catch (Exception err)
            {
                transaction.Rollback();
                this.txtstatus.Text = err.Message;

            }
            connection.Close();

        }
    }
    private static void SetColumnMapping(DataTable dtparts, SqlBulkCopy bulkCopy)
    {
        bulkCopy.ColumnMappings.Clear();
        foreach (DataColumn dc in dtparts.Columns)
        {
            bulkCopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);

        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("HistoryRecordSearch.aspx");
    }
}
