using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GeneralError : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // this.lblerror.Text = string.Format("Unhandled System Error Happened At '{0} '. Please Contact System Administrator For Help.", Request.QueryString["aspxerrorpath"]);
            this.lblerror.Text=Request.QueryString["aspxerrorpath"];
        }
    }
    
}
