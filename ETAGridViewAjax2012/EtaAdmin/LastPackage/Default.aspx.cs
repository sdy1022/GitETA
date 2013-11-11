using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            BindGridData();

        }

    }

    private void BindGridData()
    {
        this.myGridView.DataSource = CommonDB.GetSmarteam_Transfer().Tables[0];
        this.myGridView.DataBind();
    }

    protected void myGridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        myGridView.EditIndex = e.NewEditIndex;

        BindGridData();
    }
    protected void myGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int pitchid = Int16.Parse(((Label)myGridView.Rows[e.RowIndex].FindControl("lblPID")).Text);
        CommonDB.UpdateSTTID(pitchid,                                 
                                   ((TextBox)myGridView.Rows[e.RowIndex].FindControl("txtpackage")).Text,
                                    ((DropDownList)myGridView.Rows[e.RowIndex].FindControl("ddlstatus")).SelectedValue );

        myGridView.EditIndex = -1;
        BindGridData();

    }
    protected void myGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

      

    }
    protected void myGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        myGridView.EditIndex = -1;
        BindGridData();
    }

    protected void myGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DropDownList ddlstatus = (DropDownList)e.Row.FindControl("ddlstatus") as DropDownList;
        if (ddlstatus != null)
        {
            // DropDownList ddltiretype = (DropDownList)e.Row.FindControl("ddltiretype");
            //ddltiretype.Items.Clear();
            ddlstatus.DataSource = CommonDB.GetSTReturnStatus().Tables[0];
            ddlstatus.DataTextField = "Desc";

            ddlstatus.DataValueField = "ReturnStatusId";
            ddlstatus.DataBind();
            Label lblstatusid = e.Row.FindControl("lblstatusid") as Label;
            ddlstatus.SelectedIndex = CommonTool.SelectDropDownListIndexByText(ddlstatus, lblstatusid.Text);
               

            

        }
    }

    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }




    protected void myGridView_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
