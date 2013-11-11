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

public partial class EngineeringTools_FormC_FormCDataTSD : System.Web.UI.Page
{
    #region Prop

    private bool isgridviewchange = false;

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
    /// <summary>
    /// For ECI Revison
    /// Added on 09/25
    /// </summary>
    public string Revision
    {
        get
        {
            return (string)ViewState["Revision"];
        }
        set
        {
            ViewState["Revision"] = value;
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
            PackageName = "N121";
                //Request.QueryString["Package"];
            //PackageName = "XXX0";
            Module = Request.QueryString["Module"];


            EciAcid = Request.QueryString["EciAcId"];
            AitemId = Request.QueryString["AitemId"];


            Revision = Request.QueryString["Revision"];
            if (string.IsNullOrEmpty(Revision))
            {

                Revision = "ALL";
            }

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
            BindFirstGridviewAndRevisionList();

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
    private void BindFirstGridviewAndRevisionList()
    {
        //'**Get current Form C items
        //'**Apply filter from TabStrip if required
     //   DataTable dt = null;
        if (string.IsNullOrEmpty(Module))
        {
            /*
             * SELECT   FormAItemsTSD.[key] as KeyCode ,[03_TSD].ADD_TFC ,  [03_TSD].ADD_INDEX, [03_TSD].DEL_TFC, [03_TSD].DEL_INDEX,
[03_TSD].FROM_ECI, [03_TSD].FROM_DATE, FormAItemsTSD.ModuleNumber
FROM         [03_TSD] INNER JOIN
                      FormAItemsTSD  ON [03_TSD].AItemId = FormAItemsTSD.AItemId
WHERE     ([03_TSD].TFC = 'N121')
            */
            NewDataTable = CommonDB.GetTSDFormCInfoByPackageNameAndModule(PackageName, Module, false).Tables[0];
        }

        else
        {
            NewDataTable = CommonDB.GetTSDFormCInfoByPackageNameAndModule(PackageName, Module, true).Tables[0];
        }


        Gvwpartlist.DataSource = NewDataTable;

        // Add on 09/25 for revision list

        //// get list of selected eci
        //var drrows = (from p in dt.AsEnumerable()
        //              //where p.Field<string>("Add_TFC") == ""
        //              orderby p.Field<string>("FROM_DATE") descending
        //              select p.Field<string>("FROM_ECI")).Distinct().ToList();

        //IEnumerable<DataRow> rows = dt.AsEnumerable().Distinct();
        //rows.OrderByDescending(a => a.Field<string>("FROM_ECI")).ThenByDescending(a => a.Field<string>("FROM_DATE"));

        // Get group by FromECI, top 1 FromDate decending
        /*
         * UPDATE    [03_TSD]
SET               
                      FROM_ECI = N'N1E10003',FROM_DATE = N'20130301'
WHERE     (ID = 20836)
 
        */

        IEnumerable<string> rows1 = (from t in NewDataTable.AsEnumerable()
                                     group t by t.Field<string>("FROM_ECI")
                                     into groupedT

                                     let topdate =
                                         groupedT.OrderByDescending(gt => gt.Field<string>("FROM_DATE"))
                                         .First().Field<string>("FROM_DATE")
                                  
                                        //select new
                                        //           {

                                        //               FinalValue = groupedT.Key + "_" + topdate,
                                        //               // FinalVA groupedT.First(gt2=>gt2.Field<string>("FROM_DATE")==topdate).Field<string>("From_ECI")
                                        //           };
                                    select groupedT.Key + "_" + topdate).OrderByDescending(p=>(p.Split('_')[1]));
                    
        //var rows2 = rows1.OrderBy(p=>p);
      //  var rows = (new[] {"ALL"}).Concat(rows1);
       
                   
        var rows = rows1.Concat((new[] { "ALL" }));

        // Get filterd datatable by Revision 
     //   ddlrevision.DataValueField = "FinalValue";
       // ddlrevision.DataTextField = "FinalValue";
        ddlrevision.DataSource = rows;
        // decide select index by revision value

        ddlrevision.SelectedIndex = 0;
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
            // SetRevResult(e.Row);
            ////// configurator validation
            SetConfigResult(e.Row);
            SetAssyNameResult(e.Row);
            // SetPairResult(e);

            // Set color of TFC
            // Get addtfc value

            Label lbladdtfc = e.Row.FindControl("lbladdtfc") as Label;
            if(lbladdtfc!=null )
            {
                if(lbladdtfc.Text==PackageName)
                {

                    lbladdtfc.BackColor = System.Drawing.Color.GreenYellow;
                }

            }

        }
    }
    ///// <summary>
    ///// Display pair validation result 
    ///// </summary>
    ///// <param name="e"></param>
    //private static void SetPairResult(GridViewRowEventArgs e)
    //{
    //    //var dt = Gvwpartlist.DataSource as DataTable;
    //    // Pair validation
    //    // Label lblvalidation = e.Row.FindControl("lblvalidation") as Label;
    //    Label lblpair = e.Row.FindControl("lblpair") as Label;

    //    bool isvalid = false;
    //    // paired not empty
    //    // when add paired value with this citemid, if it is mutual based, then we can create/update value mutually , then no need for validation. the ending result is if the value is not empty, the value is always valid
    //    if (!string.IsNullOrEmpty(lblpair.Text))
    //    {
    //        // need to find paired row in dt 

    //        isvalid = true;
    //    }
    //    if (isvalid)
    //    {
    //        // lblvalidation.Text = "valid";

    //        //lblpair.BackColor = System.Drawing.Color.LawnGreen;
    //    }
    //    else
    //    {
    //        //lblvalidation.Text = "Invalid";
    //        lblpair.Text = "Invalid";
    //        lblpair.BackColor = System.Drawing.Color.LightCoral;
    //    }
    //}

    private Label GetInputLabel(GridViewRow row)
    {

        Label input = row.FindControl("lbldelindex") as Label;

        if (!(input.Text.Length == 7 || input.Text.Length == 9))
        {

            //input = row.FindControl("lbldelindex") as Label;
            //if (!(input.Text.Length == 7 || input.Text.Length == 9))
            //{
                input = null;
           // }
        }

        return input;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="row"></param>
    private void SetAssyNameResult(GridViewRow row)
    {

        Label input = GetInputLabel(row);

        if (input == null)
        {
            return;
        }
        DataTable dt = CommonTool.GetCategory();
            //CommonDB.GetCachedCategorySet().Tables[0];
        // Get first three 


        if (dt.Rows.Count > 0)
        {

            Label lblname = row.FindControl("lblname") as Label;
            // set up value
            lblname.Text = (from p in dt.AsEnumerable()
                            where p.Field<string>("Category").Substring(0, 3) == input.Text.Trim().Substring(0, 3)
                            select p.Field<string>("Category")).FirstOrDefault();


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
        string del_index = string.Empty;
        Label lblconfig = row.FindControl("lblconfig") as Label;
        if (dt == null)
        {
            lblconfig.BackColor = System.Drawing.Color.Red;
            lblconfig.Text = "InValid";
            return;
        }
        // vaidatte result with dt
        // get correct add_index value

        if (dt.Rows.Count > 0)
        {
            // get current input parameter
            // first three assy's name ; part code, page code
            //string GroupNo = assyname.Split("".ToCharArray())[0];
            //string[] pagecodearray = pagecode.Split("".ToCharArray());
            Label lblname = GetInputLabel(row);
            if (lblname == null)
            {
                lblconfig.Text = "InValid";
                lblconfig.BackColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                del_index = lblname.Text;
            }

            string groupno = null;
            string pagecode = null;
            string partcode = null;


            //  Label lbltreatment = row.FindControl("lbltreatment ") as Label;


            // the addindex value will be decided by Treadment value




            groupno = del_index.Substring(0, 3);

            //
            pagecode = del_index.Substring(3, 2);

            //?
            partcode = del_index.Substring(5,del_index.Length-5);



            string del_tfc = (row.FindControl("lbldeltfc") as Label).Text;

            //add_tfc = "G839";
            //add_index = "000 0201";
            List<DataRow> drrows = null;


            // treatment is D
            drrows = (from p in dt.AsEnumerable()
                      where p.Field<string>("Add_TFC") == del_tfc && (p.Field<string>("ADD_INDEX") + p.Field<string>("PageCode3")).Replace(" ", "") == del_index
                      select p).ToList();

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
                if (drrows1.Count == 0)
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
    protected void Gvwpartlist_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnfilter_Click(object sender, EventArgs e)
    {
        // Get currenet selected value
        if (isgridviewchange)
        {
            var list = Revision.Split('_');
            string eci = string.Empty;
            string date = string.Empty;

            if (list.Length == 2)
            {
                eci = list[0];
                date = list[1];

                var rows = NewDataTable.Select(string.Format("FROM_DATE<='{0}' And (TO_DATE='0' or  TO_DATE>='{0}' ) ",
                                                           date));
                if (rows.Length != 0)
                {

                    Gvwpartlist.DataSource = rows.CopyToDataTable();
                }
            }
            else // Means All
            {
                Gvwpartlist.DataSource = NewDataTable;
            }
            //  DataTable dt=NewDataTable.Select()
            // Get current datasource of gridview
            Gvwpartlist.DataBind();
            isgridviewchange = false;
        }
        
    }
    protected void ddlrevision_SelectedIndexChanged(object sender, EventArgs e)
    {
        isgridviewchange = true;
        Revision = ddlrevision.SelectedValue;
    }
}
