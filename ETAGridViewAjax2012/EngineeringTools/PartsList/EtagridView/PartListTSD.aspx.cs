using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Diagnostics;

public partial class EngineeringTools_PartsList_EtagridView_PartListTSD : System.Web.UI.Page
{
    #region Prop
    // protected string Package = null;
    private bool isgridviewchange = false;

    public string Package
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

    //public DataTable CategoryDataTable
    //{
    //    get
    //    {
    //        return (DataTable)ViewState["CategoryDataTable"];
    //    }
    //    set
    //    {
    //        ViewState["CategoryDataTable"] = value;
    //    }
    //}



    //protected string Ecinumber = null;
    public string Ecinumber
    {
        get
        {
            return (string)ViewState["Ecinumber"];
        }
        set
        {
            ViewState["Ecinumber"] = value;
        }
    }

    //  protected string Ecimode = null;
    public string Ecimode
    {
        get
        {
            return (string)ViewState["Ecimode"];
        }
        set
        {
            ViewState["Ecimode"] = value;
        }
    }

    // protected string Module = null;
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
    //  protected string Eciacid = null;

    public string Eciacid
    {
        get
        {
            return (string)ViewState["Eciacid"];
        }
        set
        {
            ViewState["Eciacid"] = value;
        }
    }
    //   protected string Keya = null;
    public string Keya
    {
        get
        {
            return (string)ViewState["Keya"];
        }
        set
        {
            ViewState["Keya"] = value;
        }
    }

    protected string Currentrev = null;
    //   protected string CurrentRevision = null;

    public string CurrentParentPart
    {
        get
        {
            return (string)ViewState["CurrentParentPart"];
        }
        set
        {

            ViewState["CurrentParentPart"] = value;
            if (value.Length == 7 || value.Length == 9)
            {
                TxtCode1 = value.Substring(0, 3);
                TxtCode2 = value.Substring(3, 2);
                TxtCode3 = value.Substring(5, 2);
                if (value.Length == 9)
                {
                    TxtCode4 = value.Substring(7, 2);
                }
            }
        }
    }


    //public string CurrentECI
    //{
    //    get
    //    {
    //        return (string)ViewState["CurrentECI"];
    //    }
    //    set
    //    {

    //        ViewState["CurrentECI"] = value;

    //    }
    //}

    public DataTable GridViewDataTable
    {
        get
        {
            return (DataTable)ViewState["GridViewDataTable"];
        }
        set
        {
            ViewState["GridViewDataTable"] = value;
        }
    }

    /// <summary>
    /// Gets or sets the TXT code1.
    /// </summary>
    /// <value>The TXT code1.</value>
    public string TxtCode1
    {
        get
        {
            return (string)ViewState["TxtCode1"];
        }
        set
        {
            ViewState["TxtCode1"] = value;
            txtCODE1.Text = value;


            //if (CategoryDataTable == null)
            //    CategoryDataTable = CommonTool.GetCategory();//CommonDB.GetCategory().Tables[0];

            string result = (from t in CommonTool.GetCategory().AsEnumerable()
                             where t.Field<string>("Category").Substring(0, 3) == value
                             select t.Field<string>("Category")).ToList().FirstOrDefault();


            txtNAME1.Text = result;



        }
    }
    /// <summary>
    /// Gets or sets the TXT code2.
    /// </summary>
    /// <value>The TXT code2.</value>
    public string TxtCode2
    {
        get
        {
            return (string)ViewState["TxtCode2"];
        }
        set
        {
            ViewState["TxtCode2"] = value;
            txtCODE2.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the TXT code3.
    /// </summary>
    /// <value>The TXT code3.</value>
    public string TxtCode3
    {
        get
        {
            return (string)ViewState["TxtCode3"];
        }
        set
        {
            ViewState["TxtCode3"] = value;
            txtCODE3.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the TXT name3.
    /// </summary>
    /// <value>The TXT name3.</value>
    public string TxtName3
    {
        get
        {
            return (string)ViewState["TxtName3"];
        }
        set
        {
            ViewState["TxtName3"] = value;
        }
    }

    /// <summary>
    /// Gets or sets the TXT name4.
    /// </summary>
    /// <value>The TXT name4.</value>
    public string TxtName4
    {
        get
        {
            return (string)ViewState["TxtName4"];
        }
        set
        {
            ViewState["TxtName4"] = value;
        }
    }

    /// <summary>
    /// Gets or sets the TXT code4.
    /// </summary>
    /// <value>The TXT code4.</value>
    public string TxtCode4
    {
        get
        {
            return (string)ViewState["TxtCode4"];
        }
        set
        {
            ViewState["TxtCode4"] = value;
            txtCODE4.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the TXT name1.
    /// </summary>
    /// <value>The TXT name1.</value>
    public string TxtName1
    {
        get
        {
            return (string)ViewState["TxtName1"];
        }
        set
        {
            ViewState["TxtName1"] = value;
        }
    }

    /// <summary>
    /// Gets or sets the TXT name2.
    /// </summary>
    /// <value>The TXT name2.</value>
    public string TxtName2
    {
        get
        {
            return (string)ViewState["TxtName2"];
        }
        set
        {
            ViewState["TxtName2"] = value;
        }
    }

    #endregion
    #region Page Methods
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
                       


            // get Package
            Package = Request.QueryString["Package"];
            if (string.IsNullOrEmpty(Package))
            {
                Package = "N121";
            }

            Module = Request.QueryString["Module"];
            if (string.IsNullOrEmpty(Module))
            {
                Module = "";
            }

            Eciacid = Request.QueryString["EciAcId"];

            //  // get current rev

            //if (CommonDB.GetCurrentRevByPackage(Package).Tables[0].Rows.Count > 0)
            //{
            //    Currentrev = CommonDB.GetCurrentRevByPackage(Package).Tables[0].Rows[0][0].ToString();
            //}

            //else
            //{
            //    Currentrev = "";
            //}

            // Package = "N1A3";
            if (string.IsNullOrEmpty(Module))
            {
                //  Module = "None-1";
            }

            //http://localhost/ETAGridViewAjax/EngineeringTools/PartsList/EtagridView/PartListEdit.aspx?Package=N9JU&Module=&Mode=N9JUE0001
            //http://localhost/ETAGridView/EngineeringTools/PartsList/EtagridView/PartListEdit.aspx?Package=N9JU&Module=&Mode=N9JUE0001
            // get Keya


            // Check for released status
            DataTable dt = CommonDB.GetLockInfoByPackageName(Package).Tables[0];

            //Check ECI Status and set Ecinumber
            DataTable dtecistatus = CommonDB.GetECIStatusInfoByPackageName(Package).Tables[0];
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

                Ecinumber = dtecistatus.Rows[0][1].ToString();
            }


#if (Release)
          {                     
          
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dt.Rows[0][0]) == true)
                    {
                        //**If Package is locked, redirect
                        Response.Redirect("http://colweb01/eta/locked.asp?Package=" + Package);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(ecistart))
                        {
                            //**If Package is not locked but ECI is not started or NoEci, redirect
                            Response.Redirect("http://colweb01/eta/locked.asp?Package=" + Package);
                        }
                        else
                        {
                            if (!ecistart.Equals("NoEci"))
                            {
                                Ecimode = "on";
                                // this.lblecimode.Text = "ECI ON";
                            }
                            else
                            {
                                // this.lblecimode.Visible = false;
                            }


                        }

                    }
                }
            }
#endif

            DataTable dtkeya = CommonDB.GetKeyAByEciNumber(Ecinumber).Tables[0];

            if (dtkeya.Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(dtkeya.Rows[0][0].ToString().Trim()))
                {
                    Keya = "A";
                }
                else
                {

                    Keya = CommonTool.Chr(CommonTool.Asc(dtkeya.Rows[0][0].ToString().Trim()) + 1);
                }
            }

            else
            {
                Keya = "A";
            }


            // Changed on 10/08/2013 for TSD Dropdownlist Binding
            BindGridAndPartListData();


        }






    }
    protected void ddlpartlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        CurrentParentPart = ddlpartlist.SelectedValue;

        BindTSDGrid();


    }
    //protected void myGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        ETAGridViewTSD control = (ETAGridViewTSD)e.Row.FindControl("ETAGridView1") as ETAGridViewTSD;
    //        /*CurrentRev='<%# Currentrev %>'
    //                        HeaderID='<%# Bind("Headerid") %>' PackageName='<%#Package %>' EciAcid='<%# Eciacid%>'
    //                        EciNumber='<%# Ecinumber%>' KeyA='<%# Keya%>' EciMode='<%# Ecimode%>' Module='<%# Module%>'

    //        */
    //        Label lblheaderid = e.Row.FindControl("lblheaderid") as Label;
    //        control.CurrentRev = Currentrev;
    //        control.HeaderID = lblheaderid.Text;//"1098349";
    //        control.PackageName = Package;
    //        control.EciAcid = Eciacid;
    //        control.EciNumber = Ecinumber;
    //        control.KeyA = Keya;
    //        control.EciMode = Ecimode;
    //        control.Module = Module;
    //        control.Ecilist = this.ddleci;
    //        control.ParentPart = CurrentParentPart;


    //        control.BindGridView();

    //        DataTable dt = control.SourceDT;

    //        //Assign values
    //        control.AssignHeaderValues();


    //        Debug.WriteLine("gridrow databind");
    //    }
    //}


    protected void HeaderGridview_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {



            HyperLink hpledit = (HyperLink)e.Row.FindControl("hpledit") as HyperLink;
            Image imgrev = (Image)e.Row.FindControl("imgrev") as Image;
            HyperLink hpleci = (HyperLink)e.Row.FindControl("hpleci") as HyperLink;
            if (hpledit != null && imgrev != null && hpleci != null)
            {
                DataRowView dview = (DataRowView)e.Row.DataItem as DataRowView;
                if (Ecinumber == dview.Row[2].ToString().Trim())
                {// DateTime.Parse((((DataRowView)Container.DataItem)["CommentDate"]).ToString()).ToString("yyyy-mm-dd")
                    // string date = ;
                    if (!string.IsNullOrEmpty(dview.Row[2].ToString()))
                    {
                        if (string.IsNullOrEmpty(dview.Row[3].ToString())) // empty datestring
                        {
                            hpledit.NavigateUrl = string.Format("http://colweb01/eta/Eci/EciHeaderEdit.asp?Form=Parts&Mark=Rev{0}.gif&Eci={1}&Rev={2}&RevDate={3}&RevBy={4}", dview.Row[0].ToString().Trim(), dview.Row[2].ToString().Trim(), dview.Row[1].ToString().Trim(), "", dview.Row[4].ToString().Trim());
                        }
                        else
                        {
                            hpledit.NavigateUrl = string.Format("http://colweb01/eta/Eci/EciHeaderEdit.asp?Form=Parts&Mark=Rev{0}.gif&Eci={1}&Rev={2}&RevDate={3}&RevBy={4}", dview.Row[0].ToString().Trim(), dview.Row[2].ToString().Trim(), dview.Row[1].ToString().Trim(), Convert.ToDateTime(dview.Row[3].ToString().Trim()).ToShortDateString(), dview.Row[4].ToString().Trim());

                        }

                        hpledit.Text = "Edit";

                    }
                }
                imgrev.ImageUrl = string.Format("http://colweb01/eta/images/Rev{0}.gif", dview.Row[0].ToString().Trim());
                hpleci.NavigateUrl = string.Format("http://colweb01/eta/ECI/eci.asp?Eci={0}", Ecinumber);
                hpleci.Text = Ecinumber;
            }



        }
    }
    protected void ddleci_SelectedIndexChanged(object sender, EventArgs e)
    {
        isgridviewchange = true;
    }
    protected void btnfiler_Click(object sender, EventArgs e)
    {
        if (isgridviewchange)
        {
            //// get current selection of eci drop down
            //string orieci = ddleci.SelectedValue;
            //// get datasource of 
            //var list = orieci.Split('_');
            //string eci = string.Empty;
            //string date = string.Empty;

            //if (list.Length == 2)
            //{
            //    eci = list[0];
            //    date = list[1];
            //    var rows =
            //        GridViewDataTable.Select(string.Format("FROMDATE<='{0}' And FROM_DATE<='{0}' And (TODATE='0' or  TODATE>'{0}' )  And (TO_DATE='0' or  TO_DATE>'{0}')",
            //                                               date));
            //    //GridViewDataTable.Select(string.Format("FROMDATE<='{0}' AND TO_DATE>='{0}'", date));
            //    if (rows.Length != 0)
            //    {
            //        var dt = rows.CopyToDataTable();
            //        dt.DefaultView.Sort = "ordervalue";

            //        Gvwpartlist.DataSource = dt;
            //    }
            //}
            //else // Means All
            //{
            //    Gvwpartlist.DataSource = GridViewDataTable;
            //}
            FilterGridDTByECI(ddleci.SelectedValue);
            //  DataTable dt=NewDataTable.Select()
            // Get current datasource of gridview
            Gvwpartlist.DataBind();
            isgridviewchange = false;
        }

    }

    #endregion
    #region Customized Methods
    /// <summary>
    /// Binds the grid data.
    /// </summary>
    private void BindGridAndPartListData()
    {
        // Get data source of ddlpartlist 
        // 
        DataTable dt = CommonDB.GetPartListTSDDropDownByPackageName(Package).Tables[0]; ;
        this.ddlpartlist.DataSource = dt;
        ddlpartlist.DataTextField = "ADD_INDEX";
        ddlpartlist.DataValueField = "ADD_INDEX";
        // Get first item
        // string CurrentRevision = string.Empty;
        if (dt.Rows.Count > 0)
        {
            CurrentParentPart = dt.Rows[0]["ADD_INDEX"].ToString();
        }
        BindTSDGrid();
    }
    /// <summary>
    /// 
    /// </summary>
    private void BindTSDGrid()
    {
        DataTable dt = null;
        // Bind  Header Name2 Name2 Name4 with current CurrentParentPart 
        dt = CommonDB.GetTSDHeaderNamesInfo(Package, TxtCode1, TxtCode2, TxtCode3, TxtCode4).Tables[0];

        if (dt.Rows.Count > 0)
        {
            txtNAME2.Text = dt.Rows[0]["NAME2"].ToString();
            txtNAME3.Text = dt.Rows[0]["NAME3"].ToString();
            txtNAME4.Text = dt.Rows[0]["NAME4"].ToString();
        }

        if (Module.IndexOf('-') == 4)
        {
            dt = CommonDB.GetPartListInfoTSDByPackageName(Module, CurrentParentPart).Tables[0];
        }

        else
        {
            dt = CommonDB.GetPartListInfoTSDByPackageName(Package, CurrentParentPart).Tables[0];
        }

        DataTable leftheaddt = CommonDB.GetPartListHeaderInfoByPackageNameAndHeaderId(Package, dt.Rows[0]["HeaderID"].ToString()).Tables[0];
        HeaderGridview.DataSource = leftheaddt;

        //  List<string> finalrows= rows.ToList();
        DataTable dteci = CommonDB.GetPartListInfoTSDByPackageNameAndParentNumber(Package, CurrentParentPart, false).Tables[0];
        string currenteci = null;
        if (dteci.Rows.Count > 0)
        {

            currenteci = dteci.Rows[0]["FinalResult"].ToString();
            this.ddleci.DataSource = dteci;

            ddleci.DataTextField = "FinalResult";
            ddleci.DataValueField = "FinalResult";
        }



        GridViewDataTable = CommonDB.GetPartListInfoTSDByPackageNameAndParentNumber(Package, CurrentParentPart, true).Tables[0];

        // get current selection of eci drop down
        FilterGridDTByECI(currenteci);

        //   this.Gvwpartlist.DataSource = GridViewDataTable;


        this.DataBind();
    }

    private void FilterGridDTByECI(string orieci)
    {
        //  string orieci = CurrentECI;
        // get datasource of 
        var list = orieci.Split('_');
        string eci = string.Empty;
        string date = string.Empty;

        if (list.Length == 2)
        {
            eci = list[0];
            date = list[1];
            var rows =
                GridViewDataTable.Select(
                    string.Format(
                        "FROMDATE<='{0}' And FROM_DATE<='{0}' And (TODATE='0' or  TODATE>'{0}' )  And (TO_DATE='0' or  TO_DATE>'{0}')",
                        date));
            //GridViewDataTable.Select(string.Format("FROMDATE<='{0}' AND TO_DATE>='{0}'", date));
            if (rows.Length != 0)
            {
                var dt1 = rows.CopyToDataTable();
                dt1.DefaultView.Sort = "ordervalue";

                Gvwpartlist.DataSource = dt1;
            }
        }
        else // Means All
        {
            Gvwpartlist.DataSource = GridViewDataTable;
        }
    }

    #endregion

}

