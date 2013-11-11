using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JqueryTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // CommonTool.IsvalidPartnoNew ("MX36(510)0401", "",0);



    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //jhjhjh
        //http://colweb01/eta/EngineeringTools/PartsList/EtagridView/PartListNew.aspx?Module=XXX0-20
        Response.Redirect(
            "http://localhost/ETAGridViewAjax/StdPartsBrowser/BrowseBottom.aspx?Model=G851&Assy=51U&PageCode1=01&PageCode2=02&PageCode3=");

        //asdfasdf
        //   "http://localhost/ETAGridViewAjax/EngineeringTools/PartsList/EtagridView/PartListNew.aspx?Module=XXX0-19");
        //"http://localhost/ETAGridViewAjax/PackageMaintenance/SaveAsSelection.aspx?Package=xxx0");
        ////"http://localhost/ETAGridViewAjax/EngineeringTools/PartsList/EtagridView/PartListEdit.aspx?Package=XXX0&Module=&Mode=XXX0E0003");

        // "http://localhost/ETAGridViewAjax/EngineeringTools/PartsList/EtagridView/PartListEdit.aspx?Package=XXX0&Module=XXX0-19&Mode=XXX0E0003");

        //int sdy=    CommonTool.IsvalidPartno("53786-u2100-71", "xxx0");

    }
}
