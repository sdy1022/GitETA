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

public partial class EtaAdmin_TSDRFlag_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindGridview();
            this.DataBind();
        }
    }

    private void BindGridview()
    {
               


        Gvwpartlist.DataSource = CommonDB.GetInvalidConfiguratorTransferList();
    }
   
    protected void Gvwpartlist_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Gvwpartlist.EditIndex = e.NewEditIndex;
        BindGridview();
        Gvwpartlist.DataBind();

    }
    protected void Gvwpartlist_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Gvwpartlist.EditIndex = -1;
        BindGridview();
        Gvwpartlist.DataBind();
    }
    protected void Gvwpartlist_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        BindGridview();
        Gvwpartlist.DataBind();
    }
}
