using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EtaAdmin_dataupload_HistoryRecordSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        this.Gvwpartlist.DataSource = CommonDB.GetInfoFromHistoryParts(this.txtpartno.Text.Trim());
        this.Gvwpartlist.DataBind();
    }
}
