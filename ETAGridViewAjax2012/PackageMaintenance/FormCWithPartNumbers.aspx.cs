using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text;

public partial class EngineeringTools_FormC_FormCWithPartNumbers : System.Web.UI.Page
{


    public string module
    {
        get
        {
            return (string)ViewState["module"];
        }
        set
        {
            ViewState["module"] = value;
        }
    }

    public string FromLastPage
    {
        get
        {
            return (string)ViewState["FromLastPage"];
        }
        set
        {
            ViewState["FromLastPage"] = value;
        }
    }

    private StringBuilder selectedcitemsids = new StringBuilder(500);
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            module = Request.QueryString["Module"];
            FromLastPage= Request.QueryString["FromPage"];
            this.GRVPartNumber.GroupingDataField = "CItemid";
            this.GRVPartNumber.DataSource = CommonDB.GetAllPartsByModule(module.Substring(0, 4), module).Tables[0];
            this.DataBind();

        }

    }
    protected void GRVPartNumber_ItemDataBound(object o, UNLV.IAP.WebControls.GroupingViewEventArgs e)
    {

        Label lbl = e.Item.Controls[1] as Label;
        if (lbl != null)
        {
            string partno = lbl.Text;


            string statusresult = string.Empty;

            if (CommonTool.IsvalidPartnoNew(partno, module.Substring(0, 4), 0, ref statusresult) < 0)
            {
                lbl.BackColor = Color.Red;

            }
            else
            {
              //  lbl.Visible = false;
            }

        }



    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        //this.GRVPartNumber.Groups[0].Controls[1]
        selectedcitemsids.Length = 0;
        for (int i = 0; i < GRVPartNumber.Groups.Count; i++)
        {

            CheckBox chkbox = GRVPartNumber.Groups[i].Controls[1] as CheckBox;
            if (chkbox != null)
            {
                if (chkbox.Checked == true)
                {
                    Label lblcitemid = this.GRVPartNumber.Groups[i].Controls[3] as Label;
                    if (lblcitemid != null)
                    {
                        selectedcitemsids.Append(string.Format("{0};", lblcitemid.Text));

                    }
                }
            }

        }
        this.lblSelectedCitemids.Text = selectedcitemsids.ToString();

        // window.location.href = "LinkModuleEcrCheck1.asp?Package=" + RequestQuerytring("Package") + "&Module=" + $("#txtModule").val();
        string url = string.Empty;

        if (FromLastPage == "Link")
        {
            url = string.Format("LinkModuleEcrCheck2.asp?Package={0}&Module={1}&Cids={2}", module.Substring(0, 4), module, selectedcitemsids.ToString());

        }
        else
        {// window.location.href = "SaModuleEcrCheckByquerystring.asp?Package=" + RequestQuerytring("Package") + "&Module=" + $("#txtModule").val();

            url = string.Format("SaModuleEcrCheckByquerystring1.asp?Package={0}&Module={1}&Cids={2}", module.Substring(0, 4), module, selectedcitemsids.ToString());

        }
        Response.Redirect(url);


    }
}
