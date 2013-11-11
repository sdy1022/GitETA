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
using System.Xml.Linq;
using System.Diagnostics;

public partial class EngineeringTools_PartRegistry_ETAReleaseAIOServer : System.Web.UI.Page
{
    public string PackageName
    {
        get
        {
            return (string)ViewState["PackageName"];
        }
        set
        {
            this.txtpackage.Text = value;
            ViewState["PackageName"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["Package"]))
            { 
                PackageName = "N121"; 
            }
            else
            {
                PackageName = Request.QueryString["Package"];
            }


            BindGrid();
        }
    }

    private void BindGrid()
    {
        this.myGridView.DataSource = CommonDB.GetFormAAndFormCList(PackageName);
        this.DataBind();
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtpackage.Text))
        {
            PackageName = txtpackage.Text.Trim();
            BindGrid();
        }

    }
    protected void myGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            EngineeringTools_PartRegistry_PartList sdy = (EngineeringTools_PartRegistry_PartList)e.Row.FindControl("PartList1");
            //string packagename = sdy.PackageName;
            //string parent = sdy.PARENTPART;
            //Debug.WriteLine("Page Package:"+ PackageName + " rowdate packageName:" + packagename);
            sdy.BindGridView();
            //   sdy.DataBind();
        }
    }
}
