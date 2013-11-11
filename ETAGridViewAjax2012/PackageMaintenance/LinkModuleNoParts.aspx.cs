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


public partial class PackageMaintenance_LinkModuleNoParts : System.Web.UI.Page
{
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


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
                       

            // The code CItemId
            PackageName = Request.QueryString["Package"];
            //PackageName = "XXX0";
            Module = Request.QueryString["Module"];

            if (string.IsNullOrEmpty(PackageName) || string.IsNullOrEmpty(Module))
            {

            }
            else
            {




                string result = CommonDB.LinkModuleByPackage(PackageName, Module);
                if (!result.Equals("Success"))
                {
                    //'Direct user to error notification
                    Response.Redirect("http://colweb01/eta/NoTrans.asp?Message=" + result);
                }
                else
                {
                    Response.Redirect("http://colweb01/eta/EngineeringTools/FormC/FormcDataAddDone.aspx");
                }
            }

        }




        }

    
}
