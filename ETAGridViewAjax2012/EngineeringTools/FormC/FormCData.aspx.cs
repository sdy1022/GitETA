using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Linq;

public partial class EngineeringTools_FormC_FormCData : System.Web.UI.Page
{
    #region Prop

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
    //AitemId
    public string AitemId
    {
        get
        {
            return (string)ViewState["AitemId"];
        }
        set
        {
            ViewState["AitemId"] = value;
        }
    }

    public string EciAcid
    {
        get
        {
            return (string)ViewState["EciAcid"];
        }
        set
        {
            ViewState["EciAcid"] = value;
        }
    }

    public string EciNumber
    {
        get
        {
            return (string)ViewState["EciNumber"];
        }
        set
        {
            ViewState["EciNumber"] = value;
        }
    }



    public DataTable NewDataTable
    {
        get
        {
            return (DataTable)ViewState["NewDataTable"];
        }
        set
        {
            ViewState["NewDataTable"] = value;
        }
    }
    //GetCategory
    public DataTable CategoryDataTable
    {
        get
        {
            return (DataTable)ViewState["CategoryDataTable"];
        }
        set
        {
            ViewState["CategoryDataTable"] = value;
        }
    }

    public DataTable KeyDataTable
    {
        get
        {
            return (DataTable)ViewState["KeyDataTable"];
        }
        set
        {
            ViewState["KeyDataTable"] = value;
        }
    }

    public DataTable StandardPartcodeDataTable
    {
        get
        {
            return (DataTable)ViewState["StandardPartcodeDataTable"];
        }
        set
        {
            ViewState["StandardPartcodeDataTable"] = value;
        }
    }

    //public bool _validateflag = true;

    //public bool _validateresult = true;

    //public string _btnsubmitclientid = null;

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {


            // The code CItemId
            PackageName = Request.QueryString["Package"];
            //PackageName = "XXX0";
            Module = Request.QueryString["Module"];


            EciAcid = Request.QueryString["EciAcId"];
            AitemId = Request.QueryString["AitemId"];


            StandardPartcodeDataTable = CommonDB.GetStandardPartcodeDataTableFromStandPartsList().Tables[0];

            // Assign Model, Mast, Attachment
            DataSet ds = CommonDB.GetHeaderInfo(PackageName);
            if (ds.Tables[0].Rows.Count != 0)
            {

                lblmodel.Text = ds.Tables[0].Rows[0]["Model"].ToString();
                lblmast.Text = ds.Tables[0].Rows[0]["Mast"].ToString();
                lblatt.Text = ds.Tables[0].Rows[0]["Attachment"].ToString();
            }
            // Header Gridview
            AssignHeaderValues();
            // Bottom Gridview
            BindFirstGridview();


            this.DataBind();
        }

    }

    #region HeaderGridView
    private void AssignHeaderValues()
    {
        DataTable leftheaddt = CommonDB.GetPartListHeaderInfoByPackageName(PackageName).Tables[0];
        HeaderGridview.DataSource = leftheaddt;
        //HeaderGridview.DataBind();
    }

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

                if (EciNumber == dview.Row[2].ToString().Trim())
                {
                    // DateTime.Parse((((DataRowView)Container.DataItem)["CommentDate"]).ToString()).ToString("yyyy-mm-dd")

                    // string date = ;


                    if (!string.IsNullOrEmpty(dview.Row[2].ToString()))
                    {
                        if (string.IsNullOrEmpty(dview.Row[3].ToString())) // empty datestring
                        {
                            hpledit.NavigateUrl = string.Format("http://colweb01/eta/Eci/EciHeaderEdit.asp?Form=C&Mark=Rev{0}.gif&Eci={1}&Rev={2}&RevDate={3}&RevBy={4}", dview.Row[0].ToString().Trim(), dview.Row[2].ToString().Trim(), dview.Row[1].ToString().Trim(), "", dview.Row[4].ToString().Trim());
                        }
                        else
                        {
                            hpledit.NavigateUrl = string.Format("http://colweb01/eta/Eci/EciHeaderEdit.asp?Form=C&Mark=Rev{0}.gif&Eci={1}&Rev={2}&RevDate={3}&RevBy={4}", dview.Row[0].ToString().Trim(), dview.Row[2].ToString().Trim(), dview.Row[1].ToString().Trim(), Convert.ToDateTime(dview.Row[3].ToString().Trim()).ToShortDateString(), dview.Row[4].ToString().Trim());

                        }

                        hpledit.Text = "Edit";

                    }
                }



                imgrev.ImageUrl = string.Format("http://colweb01/eta/images/Rev{0}.gif", dview.Row[0].ToString().Trim());

                hpleci.NavigateUrl = string.Format("http://colweb01/eta/ECI/eci.asp?Eci={0}", dview.Row[2].ToString().Trim());
                hpleci.Text = dview.Row[2].ToString().Trim();
            }



        }
    }



    #endregion

    #region BottomGridView
    /// <summary>
    /// Binds the Bottom gridview.
    /// </summary>
    private void BindFirstGridview()
    {
        //'**Get current Form C items
        //'**Apply filter from TabStrip if required
        DataTable dt = null;
        if (string.IsNullOrEmpty(Module))
        {

            dt = CommonDB.GetFormCInfoByPackageNameAndModule(PackageName, Module, false).Tables[0];
        }

        else
        {
            dt = CommonDB.GetFormCInfoByPackageNameAndModule(PackageName, Module, true).Tables[0];
        }


        Gvwpartlist.DataSource = dt;

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Gvwpartlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Rev
            SetRevResult(e.Row);
            ////// configurator validation
            SetConfigResult(e.Row);
            SetPairResult(e);
        }
    }
    /// <summary>
    /// Display pair validation result 
    /// </summary>
    /// <param name="e"></param>
    private static void SetPairResult(GridViewRowEventArgs e)
    {
        //var dt = Gvwpartlist.DataSource as DataTable;
        // Pair validation
        // Label lblvalidation = e.Row.FindControl("lblvalidation") as Label;
        Label lblpair = e.Row.FindControl("lblpair") as Label;

        bool isvalid = false;
        // paired not empty
        // when add paired value with this citemid, if it is mutual based, then we can create/update value mutually , then no need for validation. the ending result is if the value is not empty, the value is always valid
        if (!string.IsNullOrEmpty(lblpair.Text))
        {
            // need to find paired row in dt 

            isvalid = true;
        }
        if (isvalid)
        {
            // lblvalidation.Text = "valid";

            //lblpair.BackColor = System.Drawing.Color.LawnGreen;
        }
        else
        {
            //lblvalidation.Text = "Invalid";
            lblpair.Text = "Invalid";
            lblpair.BackColor = System.Drawing.Color.LightCoral;
        }
    }

    /// <summary>
    /// for config label
    /// </summary>
    /// <param name="row"></param>
    private void SetConfigResult(GridViewRow row)
    {


        // configurator validation
        DataTable dt = CommonDB.GetConfiguratorValidationDataTableByPackage(PackageName);

        if (dt == null)
            return;
        // vaidatte result with dt
        if ( dt.Rows.Count > 0)
        {
            // get current input parameter
            // first three assy's name ; part code, page code
            //string GroupNo = assyname.Split("".ToCharArray())[0];
            //string[] pagecodearray = pagecode.Split("".ToCharArray());
            Label lblname = row.FindControl("lblname") as Label;
            string groupno = null;
            if (!string.IsNullOrEmpty(lblname.Text)) groupno = lblname.Text.Split("".ToCharArray())[0];

            Label lblpagecode = row.FindControl("lblpagecode") as Label;
            string pagecode = null;
            if (!string.IsNullOrEmpty(lblpagecode.Text)) pagecode = lblpagecode.Text.Replace(" ", "");
                //Split("".ToCharArray())[0];

            Label lblpartcode = row.FindControl("lblpartcode") as Label;
            string partcode = null;
            if (!string.IsNullOrEmpty(lblpartcode.Text)) partcode = lblpartcode.Text.Split("".ToCharArray())[0];

          //  Label lbltreatment = row.FindControl("lbltreatment ") as Label;
            string add_index = string.Empty;

            // the addindex value will be decided by Treadment value

            add_index = string.Format("{0} {1}", groupno, pagecode).Replace(" ", "");
            string add_tfc = partcode;

            //add_tfc = "G839";
            //add_index = "000 0201";
            Label lbltreatment = row.FindControl("lbltreatment") as Label;
            List<DataRow> drrows = null;
            if (lbltreatment.Text.Trim() == "S")
            {
                 //drrows = (from p in dt.AsEnumerable()
                 //             where p.Field<string>("Add_TFC") == add_tfc && p.Field<string>("ADD_INDEX") == add_index
                 //             select p).ToList();
            }
            else
            {
               
                // treatment is D
                drrows = (from p in dt.AsEnumerable()
                          where p.Field<string>("Add_TFC") == add_tfc && (p.Field<string>("ADD_INDEX") + p.Field<string>("PageCode3")).Replace(" ", "") == add_index
                              select p).ToList();
                Label lblconfig = row.FindControl("lblconfig") as Label;
                if (drrows.Count > 0)
                {

                    lblconfig.Text = "Valid";


                }
                else
                {
                    lblconfig.Text = "InValid";

                    List<DataRow> drrows1 = (from x in StandardPartcodeDataTable.AsEnumerable()
                               where x.Field<string>("Model") == partcode
                               select x).ToList();
                    if(drrows1.Count==0)
                    {
                        lblconfig.BackColor = System.Drawing.Color.LightBlue;

                    }
                    else
                    {
                        lblconfig.BackColor = System.Drawing.Color.LightCoral;
                        
                    }
                   
                }


              
            }
            
           

        }




    }
    /// <summary>
    /// for rev result
    /// </summary>
    /// <param name="row"></param>
    private void SetRevResult(GridViewRow row)
    {

        Image imgrev = (Image)row.FindControl("imgrev") as Image;
        if (imgrev != null)
        {

            DataRowView dview = (DataRowView)row.DataItem as DataRowView;


            if (!string.IsNullOrEmpty(dview.Row["Rev"].ToString().Trim()))
            {
                string finalvalue = dview.Row["Rev"].ToString().Trim().Replace(" ", "");
                if (finalvalue.Length > 1)
                {
                    //finalvalue.Substring(0,1)
                    imgrev.Visible = true;
                    imgrev.ImageUrl = string.Format("http://colweb01/eta/images/Rev{0}.gif", finalvalue.Substring(0, 1));

                    for (int i = 1; i < finalvalue.Length; i++)
                    {
                        Image temp = new Image();
                        temp.Visible = true;
                        temp.ImageUrl = string.Format("http://colweb01/eta/images/Rev{0}.gif", finalvalue.Substring(i, 1));
                        // need to make sure the row.Cells[0] is the cell for rev cell 
                        row.Cells[0].Controls.Add(temp);
                    }
                }

                else
                {
                    imgrev.Visible = true;
                    imgrev.ImageUrl = string.Format("http://colweb01/eta/images/Rev{0}.gif", dview.Row["Rev"].ToString().Trim());
                }
            }
            else
            {
                imgrev.Visible = false;
            }
        }


    }
    #endregion
}
