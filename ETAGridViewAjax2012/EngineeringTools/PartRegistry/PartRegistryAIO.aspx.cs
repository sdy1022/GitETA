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

public partial class EngineeringTools_PartRegistry_PartRegistryAIO : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["uid"] == null)
            {
                userinfo.Value = System.Web.HttpContext.Current.User.Identity.Name;
            }
            else
            {

                userinfo.Value = Session["uid"].ToString();
            }

            //if(string.IsNullOrEmpty(Request.QueryString["Package"].ToString()))
            //{

            //    packagevalue.Value = Request.QueryString["Package"].ToString();
            //}
            //else
            //{

            //    packagevalue.Value = "XXX0";
            //}
        }
    }
}
