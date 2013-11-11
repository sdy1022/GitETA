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

public partial class EngineeringTools_PartsList_EtagridView_PartListAddTSD : System.Web.UI.Page
{
    #region Property For State Info
    public string Package
    {
        get
        {
            return (string)ViewState["Package"];
        }
        set
        {
            ViewState["Package"] = value;
        }
    }

    public string PackageActiveEci
    {
        get
        {
            return (string)ViewState["PackageActiveEci"];
        }
        set
        {
            ViewState["PackageActiveEci"] = value;
        }
    }

    #endregion

    #region Customized Methods

    #endregion


    #region Page Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Package = Request.QueryString["Package"];
            if (string.IsNullOrEmpty(Package))
            {
                Package = "XXX1";
            }

            // Load dropdownlists
            // Check for released status
            DataTable dt = CommonDB.GetTSD06ModulesByPackage(Package).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ddlinsertnew.DataSource = dt;
                ddlinsertnew.DataTextField = "ModuleNumber";
                ddlinsertnew.DataValueField = "ModuleNumber";

                ddlsublist.DataSource = dt;
                ddlsublist.DataTextField = "ModuleNumber";
                ddlsublist.DataValueField = "ModuleNumber";

            }

            DataTable dt1 = CommonDB.GetExcept0306ModulesByPackage(Package).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                ddlattach.DataSource = dt1;
                ddlattach.DataTextField = "ModuleNumber";
                ddlattach.DataValueField = "ModuleNumber";


                ddlcattach.DataSource = dt1;
                ddlcattach.DataTextField = "ModuleNumber";
                ddlcattach.DataValueField = "ModuleNumber";


            }

            this.DataBind();
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btninsertnewitems_Click(object sender, EventArgs e)
    {
        // Get current droplist selection
        string partlist = ddlinsertnew.SelectedValue;
        if (partlist != "-Select Part List-")
        {

            Response.Redirect(string.Format("PartListInsertTSD.aspx?Package={0}&PartList={1}", Package, partlist));
        }
    }

    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Click(object sender, EventArgs e)
    {
        string partlist = ddlattach.SelectedValue;
        if(partlist!="-Select Part List-")
        {

            Response.Redirect(string.Format("PartListNewTSD.aspx?Package={0}&PartList={1}", Package, partlist));
        }
        

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsublist_Click(object sender, EventArgs e)
    {
        //PartsListTSDSubSelect.aspx
        string partlist = ddlsublist.SelectedValue;
        if (partlist != "-Select Part List-")
        {

            Response.Redirect(string.Format("PartsListTSDSubSelect.aspx?Package={0}&PartList={1}", Package, partlist));
        }
        
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
   
    protected void btncopylist_Click(object sender, EventArgs e)
    {
        // Get values of inputs
        try
        {
            string inputtfc = txtcpkg.Text.Trim();
            string indexparent = txtassy.Text.Trim() + txtpg1.Text.Trim() + txtpg2.Text.Trim() + txtpg3.Text.Trim();
            string dropindexparent = this.ddlcattach.SelectedValue;

            CommonDB.InsertTSDCopyPartList(Package, indexparent, inputtfc, dropindexparent, PackageActiveEci);

            Response.Redirect(string.Format("PartsListTSD.aspx?Package={0}", Package));


        }
        catch (Exception err)
        {
            Response.Redirect("http://colweb01/eta/NoTrans.asp?Message=" + err.Message);
           
        }
       
    }

    #endregion
}
