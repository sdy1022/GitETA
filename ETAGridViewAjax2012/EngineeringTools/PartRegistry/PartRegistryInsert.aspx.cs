using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class EngineeringTools_PartRegistry_PartRegistryInsert : System.Web.UI.Page
{
    #region Prop
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

    public DataTable NewDataTable
    {
        get
        {
            return (DataTable)ViewState["NewDataTable"];
        }
        set
        {
            ViewState["NewDataTable"] = value;
        }
    }
    #endregion


    #region Customized Methods

    private string GetTextBoxValue(int editindex, string controlname)
    {
        TextBox txtbox = (TextBox)NewGvwpartlist.Rows[editindex].FindControl(controlname) as TextBox;
        string result = "";

        if (!string.IsNullOrEmpty(txtbox.Text))
        {
            result = txtbox.Text.Trim();
        }

        return result;
    }
    /// <summary>
    /// Binds the new grid view.
    /// </summary>
    private void BindNewGridView()
    {
        if (NewDataTable == null)
        {

            NewDataTable = MakeDataTable();
        }

        NewGvwpartlist.DataSource = NewDataTable;
    }

    private DataTable MakeDataTable()
    {

        DataTable table = new DataTable();

        table.Columns.Add("rowid", typeof(int));
        table.Columns.Add("PartNo", typeof(string));
        table.Columns.Add("Minor", typeof(string));

        table.Columns.Add("Description", typeof(string));
        table.Columns.Add("TMHU_View", typeof(int));
        table.Columns.Add("From_Eci", typeof(string));
        table.Columns.Add("To_ECI", typeof(string));

        table.Rows.Add(0, null, "", null, 0, null, null);
        //table.Rows.Add(1, null, null, null, 0, null, null);
        //table.Rows.Add(2, null, null, null, 0, null, null);
        //table.Rows.Add(3, null, null, null, 0, null, null);
        //table.Rows.Add(4, null, null, null, 0, null, null);
        //table.Rows.Add(5, null, null, null, 0, null, null);


        return table;


    }



    #endregion 
    #region Form Methods
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            PackageName = Request.QueryString["Package"];
            if (string.IsNullOrEmpty(PackageName))
            {
                PackageName = "XXX0";
            }
        //    BindNewGridView();
            this.DataBind();
        }


    }


    
  

    protected void NewGvwpartlist_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
       

        NewGvwpartlist.EditIndex = -1;
        BindNewGridView();
        NewGvwpartlist.DataBind();
    }
    protected void NewGvwpartlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        return;

        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
           


        //    Button btnsubmit = e.Row.Cells[0].FindControl("btnsubmit") as Button;


        //    if (btnsubmit != null)
        //    {
        //        if (!string.IsNullOrEmpty(NewDataTable.Rows[0][3].ToString()) || !string.IsNullOrEmpty(NewDataTable.Rows[1][3].ToString()))
        //        {

        //            btnsubmit.Attributes.Add("onClick", "javascript:InvokePop('" + this.hiddenfield.ClientID + "','" + PackageName + "','" + EciNumber + "');");
        //            btnsubmit.Enabled = true;
        //        }

        //        else
        //        {
        //            btnsubmit.Enabled = false;
        //        }


        //    }

        //}

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkecilog = (CheckBox)e.Row.FindControl("chkecilog") as CheckBox;
            if (chkecilog != null)
            {
                Label lblecilog = e.Row.FindControl("lblecilog") as Label;


                if (lblecilog != null)
                {
                    // chkecilog.Enabled = Convert.ToBoolean(lblecilog.Text);

                    chkecilog.Checked = Convert.ToBoolean(lblecilog.Text);
                }

            }


            Image imgrev = (Image)e.Row.FindControl("imgrev") as Image;
            if (imgrev != null)
            {

                DataRowView dview = (DataRowView)e.Row.DataItem as DataRowView;


                if (!string.IsNullOrEmpty(dview.Row["Rev"].ToString().Trim()))
                {
                    imgrev.Visible = true;
                    imgrev.ImageUrl = string.Format("http://colweb01/eta/images/Rev{0}.gif", dview.Row["Rev"].ToString().Trim());
                }
                else
                {
                    imgrev.Visible = false;
                }
            }

           

           

            DropDownList ddltreatment = (DropDownList)e.Row.FindControl("ddltreatment") as DropDownList;
            if (ddltreatment != null)
            {
                Label lbltreatment = e.Row.FindControl("lbltreatment") as Label;
                if (lbltreatment != null)
                {
                    ddltreatment.SelectedIndex = CommonTool.SelectDropDownListIndexByText(ddltreatment, lbltreatment.Text.Trim());
                }
            }

        }


    }
   
    protected void NewGvwpartlist_RowEditing(object sender, GridViewEditEventArgs e)
    {
        NewGvwpartlist.EditIndex = e.NewEditIndex;
        // NewDataTable.Rows[NewGvwpartlist.EditIndex]["ecilog"] = "True";
        BindNewGridView();
        NewGvwpartlist.DataBind();

    }
    protected void NewGvwpartlist_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        // update datatable


        NewDataTable.Rows[NewGvwpartlist.EditIndex]["ecilog"] = ((CheckBox)NewGvwpartlist.Rows[NewGvwpartlist.EditIndex].FindControl("chkecilog") as CheckBox).Checked.ToString();



        NewDataTable.Rows[NewGvwpartlist.EditIndex]["assyname"] = ((DropDownList)NewGvwpartlist.Rows[NewGvwpartlist.EditIndex].FindControl("ddlassyname") as DropDownList).SelectedItem.ToString();

        NewDataTable.Rows[NewGvwpartlist.EditIndex]["key"] = ((DropDownList)NewGvwpartlist.Rows[NewGvwpartlist.EditIndex].FindControl("ddlkey") as DropDownList).SelectedItem.Text;//

        NewDataTable.Rows[NewGvwpartlist.EditIndex]["Aitemid"] = ((DropDownList)NewGvwpartlist.Rows[NewGvwpartlist.EditIndex].FindControl("ddlkey") as DropDownList).SelectedValue;//

        NewDataTable.Rows[NewGvwpartlist.EditIndex]["assycode"] = GetTextBoxValue(NewGvwpartlist.EditIndex, "txtcode");
        NewDataTable.Rows[NewGvwpartlist.EditIndex]["treatment"] = ((DropDownList)NewGvwpartlist.Rows[NewGvwpartlist.EditIndex].FindControl("ddltreatment") as DropDownList).SelectedItem.ToString().Trim();

        NewDataTable.Rows[NewGvwpartlist.EditIndex]["partcode"] = GetTextBoxValue(NewGvwpartlist.EditIndex, "txtpartcode");
        // add by dayang on 03/12/2013
        NewDataTable.Rows[NewGvwpartlist.EditIndex]["pagecode"] = GetTextBoxValue(NewGvwpartlist.EditIndex, "txtpagecode").Replace(" ", "");
        NewDataTable.Rows[NewGvwpartlist.EditIndex]["description"] = GetTextBoxValue(NewGvwpartlist.EditIndex, "txtdesc");
        // add by dayang on 01/09/2013
       
        NewGvwpartlist.EditIndex = -1;
        NewGvwpartlist.DataSource = NewDataTable;
        NewGvwpartlist.DataBind();


    }
  

    protected void btnsubmit_Click(object sender, EventArgs e)
    {

    }

    #endregion
}
