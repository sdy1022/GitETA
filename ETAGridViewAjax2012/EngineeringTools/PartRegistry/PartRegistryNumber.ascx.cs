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


public partial class EngineeringTools_PartRegistry_PartRegistryNumber : System.Web.UI.UserControl
{
    public string FullValue
    {
        get
        {
            return (string)ViewState["FullValue"];
        }
        set
        {
            ViewState["FullValue"] = value;
        }
    }

    /// <summary>
    /// PackageName
    /// </summary>
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {

            this.txtmiddle.Text = string.Format("U{0}", PackageName);
            FullValue = string.Format("{0}-{1}-{2}", this.txtfront.Text, this.txtmiddle.Text, this.Txtend.Text);

        }
    }



    protected void txtfront_TextChanged(object sender, EventArgs e)
    {
        FullValue = string.Format("{0}-{1}-{2}", this.txtfront.Text, this.txtmiddle.Text, this.Txtend.Text);
    }
    protected void Txtend_TextChanged(object sender, EventArgs e)
    {
        FullValue = string.Format("{0}-{1}-{2}", this.txtfront.Text, this.txtmiddle.Text, this.Txtend.Text);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    public string GetClientID()
    {

        return this.ClientID;
    }
}
