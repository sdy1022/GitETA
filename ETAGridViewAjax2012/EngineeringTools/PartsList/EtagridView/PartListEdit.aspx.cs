using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    protected string package = null;
    protected string ecinumber = null;
    protected string ecimode = null;
    protected string module = null;
    protected string eciacid = null;
    protected string keya = null;

    protected string currentrev = null;

   
 


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
                       
            
          
            // get package
            package = Request.QueryString["Package"];
            if (string.IsNullOrEmpty(package))
            {
               package = "N9JU";
            }

            module = Request.QueryString["Module"];
            if (string.IsNullOrEmpty(module))
            {
                module = "N9JUE0001";
            }

            eciacid = Request.QueryString["EciAcId"];

            //  // get current rev

            if (CommonDB.GetCurrentRevByPackage(package).Tables[0].Rows.Count > 0)
            {
                currentrev = CommonDB.GetCurrentRevByPackage(package).Tables[0].Rows[0][0].ToString();
            }

            else
            {
                currentrev = "";
            }
            
           // package = "N1A3";
            if (string.IsNullOrEmpty(module))
            {
              //  module = "None-1";
            }

            //http://localhost/ETAGridViewAjax/EngineeringTools/PartsList/EtagridView/PartListEdit.aspx?Package=N9JU&Module=&Mode=N9JUE0001
            //http://localhost/ETAGridView/EngineeringTools/PartsList/EtagridView/PartListEdit.aspx?Package=N9JU&Module=&Mode=N9JUE0001
            // get keya

            
            // Check for released status
            DataTable dt = CommonDB.GetLockInfoByPackageName(package).Tables[0];

            //Check ECI Status and set ecinumber
            DataTable dtecistatus = CommonDB.GetECIStatusInfoByPackageName(package).Tables[0];
            string ecistart = null;

            if (dtecistatus.Rows.Count == 0)
            {
                ecistart = "NoEci";
            }
            else
            {
                if (dtecistatus.Rows[0][0] == null)
                {
                    ecistart = null;
                }
                else
                {
                    ecistart = dtecistatus.Rows[0][0].ToString();
                }

                ecinumber = dtecistatus.Rows[0][1].ToString();
            }

            

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dt.Rows[0][0]) == true)
                {
                    //**If package is locked, redirect
                    Response.Redirect("http://colweb01/eta/locked.asp?Package=" + package);
                }
                else
                {
                    if (string.IsNullOrEmpty(ecistart))
                    {
                        //**If package is not locked but ECI is not started or NoEci, redirect
                        Response.Redirect("http://colweb01/eta/locked.asp?Package=" + package);
                    }
                    else
                    {
                        if (!ecistart.Equals("NoEci"))
                        {
                            ecimode = "on";
                           // this.lblecimode.Text = "ECI ON";
                        }
                        else
                        {
                           // this.lblecimode.Visible = false;
                        }

                        
                    }

                }
            }

            DataTable dtkeya = CommonDB.GetKeyAByEciNumber(ecinumber).Tables[0];

            if (dtkeya.Rows.Count > 0)
            {
                if(string.IsNullOrEmpty(dtkeya.Rows[0][0].ToString().Trim()))
                {
                    keya = "A";
                }
                else
                {

                     keya = CommonTool.Chr(CommonTool.Asc(dtkeya.Rows[0][0].ToString().Trim())+1);
                }
            }

            else
            {
                keya = "A";
            }

            BindGridData();
        }

        else
        {
            //int sdy = 4;
        }

      


    }

  

    /// <summary>
    /// Binds the grid data.
    /// </summary>
    private void BindGridData()
    {
        
        if (module.IndexOf('-') == 4)
        {
            this.myGridView.DataSource = CommonDB.GetPartListInfoByModuleName(module).Tables[0];
        }

        else
        {
            this.myGridView.DataSource = CommonDB.GetPartListInfoByPackageName(package).Tables[0];
        }
        
        this.myGridView.DataBind();
    }

   
   
}
