using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DAL.AS400;

public partial class EngineeringTools_Configurator_Default : System.Web.UI.Page
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
            this.lblpackage.Text = value;

        }
    }
  
    public string FirstOrderNo
    {
        get
        {
            return (string)ViewState["FirstOrderNo"];
        }
        set
        {
            ViewState["FirstOrderNo"] = value;

        }
    }
    public DataTable DateList
    {
        get
        {
            return (DataTable)ViewState["DateList"];
        }
        set
        {
            ViewState["DateList"] = value;

        }


    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PackageName = Request.QueryString["Package"];
            //PackageName = "NRDM";

            // Get firstorder of this package
            if (!string.IsNullOrEmpty(PackageName))
            {
                FirstOrderNo = CommonDB.GetFirstOrderNoByPackage(PackageName);
                //FirstOrderNo = "0531567";
                // load history list 
                DateList = CommonDB.GetDateList(FirstOrderNo);

                if (DateList.Rows.Count > 0)
                {
                    this.ddlhistory.DataSource = DateList;
                    ddlhistory.DataTextField = "Pdate";
                    ddlhistory.DataValueField = "Pdate";
                    BindGrid(DateList.Rows[0][0].ToString(), false);
                    this.DataBind();
                }
            }
        }

    }
    protected void ddlhistory_SelectedIndexChanged(object sender, EventArgs e)
    {
        // FirstOrderNo = ddlhistory.SelectedValue;
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (ddlhistory.SelectedIndex != -1)
        {
            string pdate = ddlhistory.SelectedValue;

            if (BindGrid(pdate, true))
            {
                this.DataBind();
            }

        }
    }

    private bool BindGrid(string pdate, bool isfromdw)
    {
        bool ishavingresult = false;

        string tableresultstatus = string.Empty;

        DataTable dt = CommonDB.GetConfiguratorSummaryByOrderResult(FirstOrderNo,ref tableresultstatus);

        // get process date 
        string processdate = string.Empty;

      

        if (dt.Rows.Count > 0)
        {

            this.GridView1.DataSource = dt;
            processdate = dt.Rows[0]["PROCESSED_DATE"].ToString();
            ishavingresult = true;
        }


        if (tableresultstatus == "0")
        {
            lblgridscstatus.Text = "The following result are from working table. Processed Time :"+ processdate ;

        }
        else
        {

            lblgridscstatus.Text = "The following result are from history table.Processed Time :" + processdate;
        }



        return ishavingresult;
    }
    protected void btnautopair_Click(object sender, EventArgs e)
    {

        // not autopair, but scehdule configuraotr
        // see this ordernumber exist in job list with type 2 or not
        if(string.IsNullOrEmpty(FirstOrderNo))
        {

            return;
        }
        DataTable dt = CommonDB.IsItemExistInJobQueue(JobType.Configuraotr, FirstOrderNo);
        if (dt.Rows.Count > 0)
        {
            this.lblstatus.Text = "Package In Queue Already. Configurator could take up to 1 hour to process ";
        }

        else
        {
            //FirstOrderNo
            // Get orderdetail from AS400
            AS400A004PEntity result = DAL.AS400.A004PFunction.GetOrderData(FirstOrderNo);
            if (result != null)
            {
                if (string.IsNullOrEmpty(result.ADAON) || result.ADAON == "0")
                {
                    if (result.ADEON == "0" || string.IsNullOrEmpty(result.ADEON))
                    {
                        result = null;
                    }
                    else
                    {

                        result.ADAON = result.ADEON;
                    }
                }
            }
            // insert to job queue ,
            string sql = string.Format(" EXEC [ST_SUPPORT].[dbo].[InsertJobAS400OrderInfo] '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}'", PackageName, FirstOrderNo, result.TFC, result.ADMDN, result.ADMST, result.ADAON, result.ADATT, result.WholeSalesCode, result.WholeOptions);
            //          EXECUTE @RC = [ST_SUPPORT].[dbo].[InsertJobAS400OrderInfo] 
            // @jobvalue
            //,@orderno
            //,@tfc
            //,@modelcode
            //,@masttype
            //,@adate
            //,@attach
            //,@salescodelist
            //,@inputoptions

            CommonDB.InsertJobAS400OrderInfo(sql);


            this.lblstatus.Text = "Configurator could take up to 1 hour to process ";

        }

        // CommonDB.ProcessETAJobQueue(PackageName,Convert.ToInt16(JobType.Configuraotr),0);
    }
}
