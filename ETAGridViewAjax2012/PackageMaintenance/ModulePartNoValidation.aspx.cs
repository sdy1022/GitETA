using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PackageMaintenance_ModulePartNoValidation : System.Web.UI.Page
{
    #region Properties
    /// <summary>
    /// PackageName
    /// </summary>
    public string PackageName
    {
        get
        {
            return (string)ViewState["PackageName"];
        }
        set
        {
            ViewState["PackageName"] = value;
        }
    }



    //public bool IsPartsvalid
    //{
    //    get
    //    {
    //        if (ViewState["IsPartsvalid"] == null)
    //        {
    //            return false;
    //        }
    //        else
    //        {
    //            return (bool)ViewState["IsPartsvalid"];
    //        }
    //    }
    //    set
    //    {
    //        ViewState["IsPartsvalid"] = value;
    //    }
    //}
   
   


    /// <summary>
    /// Gets or sets the module.
    /// </summary>
    /// <value>The module.</value>
    public string Module
    {
        get
        {
            return (string)ViewState["Module"];
        }
        set
        {
            ViewState["Module"] = value;
        }
    }

    #endregion



    #region Page Methods

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {



            // get package and module
            PackageName = Request.QueryString["Package"];
            Module = Request.QueryString["SaveAs"];


            //PackageName = "XXX0";
            //Module = "XXX0-31";

            BindGridData();
            SelectNextPage();

        }





        


    }

    /// <summary>
    /// Selects the next page.
    /// </summary>
    private void SelectNextPage()
    {

        if (CheckIsAllPartsValid())
        {
            //("http://colweb01/eta/packageMaintenance/SaveAsModule.asp?Package=" & Package & "&SaveAs=" & Module)
            string url = string.Format("http://colweb01/eta/packageMaintenance/SaveAsModule.asp?Package={0}&SaveAs={1}",PackageName,Module);
            Response.Redirect(url);
        }
    }

    /// <summary>
    /// Checks the is all parts valid.
    /// </summary>
    /// <returns></returns>
    private bool CheckIsAllPartsValid()
    {
        bool result = true;
        DataTable dt=(DataTable) myGridView.DataSource;
        foreach (DataRow dr in dt.Rows)
        {
            if (!(bool)dr["PartNoValidationStatus"])
            {
                result = false;
                break;
            }
        }

        return result;
    }

    /// <summary>
    /// Binds the grid data.
    /// </summary>
    private void BindGridData()
    {

        if (Module.IndexOf('-') == 4)
        {
           
            

           this.myGridView.DataSource = CommonDB.GetPartNumbersByModuleName(Module);
        }

       

        this.myGridView.DataBind();
    }

    #endregion


    #region Grid Methods

    /// <summary>
    /// Handles the RowEditing event of the myGridView control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
    protected void myGridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        myGridView.EditIndex = e.NewEditIndex;
        BindGridData();
    }
    /// <summary>
    /// Handles the RowCancelingEdit event of the myGridView control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCancelEditEventArgs"/> instance containing the event data.</param>
    protected void myGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

        myGridView.EditIndex = -1;
        BindGridData();
    }
    /// <summary>
    /// Handles the RowUpdating event of the myGridView control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewUpdateEventArgs"/> instance containing the event data.</param>
    protected void myGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
       // Label lblpartnumber = (Label)myGridView.Rows[e.RowIndex].FindControl("lblpartnumber") as Label;

        string partno = (myGridView.Rows[e.RowIndex].FindControl("txtpartno") as TextBox).Text.Trim();


        Label lblvaldationstatus = (Label)myGridView.Rows[e.RowIndex].FindControl("lblvaldationstatus") as Label;
        string stringresult = string.Empty;
        if (CommonTool.IsvalidPartnoNew(partno, PackageName,0,ref stringresult ) == -1)
        {
            //still error
            lblvaldationstatus.Text = "INVALID";
        }

        else
        {

            // update the 

            Label lblitemid = (Label)myGridView.Rows[e.RowIndex].FindControl("lblitemid") as Label;


            if (CommonDB.UpdatePartNumberByItemid(partno, lblitemid.Text.Trim())== "Success")
            {
                    lblvaldationstatus.Text = "";
            }

           
        }

        myGridView.EditIndex = -1;
        BindGridData();
        SelectNextPage();
    }

    
    protected void myGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {           
            Label lblPartNoValidationStatus = (Label)e.Row.FindControl("lblPartNoValidationStatus") as Label;

            if (lblPartNoValidationStatus != null)
            {       //lblvaldationstatus

                Label lblvaldationstatus = (Label)e.Row.FindControl("lblvaldationstatus") as Label;
                if (lblPartNoValidationStatus.Text=="False")
                {

                    SetEditButton(e, true);
                    lblvaldationstatus.Text = "INVALID";
                  //  IsPartsvalid = false;

                }
                else
                {

                    SetEditButton(e, false);
                 //   IsPartsvalid = IsPartsvalid & true;


                }

            }
        }
    }

    private static void SetEditButton(GridViewRowEventArgs e , bool result)
    {
        foreach (Control c in e.Row.Cells[0].Controls)
        {
            try
            {
                LinkButton lb = (LinkButton)c;
                if (lb.Text == "Edit")
                {
                    lb.Visible = result;
                    break;

                }
            }
            catch (Exception e1)
            {

            }

        }
    }

    #endregion
}
