using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class EngineeringTools_FormC_FormCDataEditTSD : System.Web.UI.Page
{
    public const int MAXPAIRSIZE = 200;

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

    public string EciMode
    {
        get
        {
            return (string)ViewState["EciMode"];
        }
        set
        {
            ViewState["EciMode"] = value;

            if (value == "on")
            {
                this.lbleci.Visible = true;
            }
        }
    }

    public DataTable BottomGvNewDataTable
    {
        get
        {
            return (DataTable)ViewState["BottomGvNewDataTable"];
        }
        set
        {
            ViewState["BottomGvNewDataTable"] = value;
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

    public string ReleaseDate
    {
        get
        {
            return (string)ViewState["ReleaseDate"];
        }
        set
        {
            ViewState["ReleaseDate"] = value;
        }
    }
    public bool _validateflag = true;

    public bool _validateresult = true;

    #endregion

    #region HeaderGridview Methods
    /// <summary>
    /// Handles the RowDataBound event of the HeaderGridview control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>

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
    protected void HeaderGridview_RowDataBound_old(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hpledit = (HyperLink)e.Row.FindControl("hpledit") as HyperLink;




            Image imgrev = (Image)e.Row.FindControl("imgrev") as Image;
            HyperLink hpleci = (HyperLink)e.Row.FindControl("hpleci") as HyperLink;



            if (imgrev != null && hpleci != null)
            {

                DataRowView dview = (DataRowView)e.Row.DataItem as DataRowView;




                imgrev.ImageUrl = string.Format("http://colweb01/eta/images/Rev{0}.gif", dview.Row[0].ToString().Trim());

                hpleci.NavigateUrl = string.Format("http://colweb01/eta/ECI/eci.asp?Eci={0}", dview.Row[2].ToString().Trim());
                hpleci.Text = dview.Row[2].ToString().Trim();
            }



        }
    }

    #endregion

    #region  Gvwpartlist Methods

    /// <summary>
    /// Handles the RowDataBound event of the Gvwpartlist control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void Gvwpartlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //string ss = "";
        //ss = ss ?? "sdy";

        bool isdisable = false;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtdelindex = e.Row.FindControl("txtdelindex") as TextBox;
            TextBox txtaddindex = e.Row.FindControl("txtaddindex") as TextBox;
            // check FromEci is equal to active or not 

            TextBox txtfromeci = e.Row.FindControl("txtfromeci") as TextBox;
            if (txtfromeci != null)
            {
                if (!string.IsNullOrEmpty(EciNumber) && txtfromeci.Text != EciNumber)
                {

                    isdisable = true;
                }

            }


            foreach (Control c in e.Row.Cells[0].Controls)
            {
                try
                {
                    LinkButton lb = (LinkButton)c;
                    if (lb.Text == "Update")

                        lb.Attributes.Add("onClick", "javascript:InvokePop('" + this.hiddenfield.ClientID + "','" + PackageName + "','" + EciNumber + "');");
                }
                catch (Exception e1)
                {


                }

            }

            if (!string.IsNullOrEmpty(ReleaseDate))
            {


                Label label = (Label)e.Row.FindControl("lblfromeci") as Label;

                // hide edit button if neccessary or delete button
                // if fromeci not equal Ecinumber
                if (label.Text != EciNumber)
                {

                    foreach (Control c in e.Row.Cells[0].Controls)
                    {
                        try
                        {
                            LinkButton lb = (LinkButton)c;
                            if (lb.Text == "Delete")
                            {
                                lb.Visible = false;
                                break;

                            }
                        }
                        catch (Exception e1)
                        {

                        }

                    }
                }


            }

            //Label lblpartlistlocation = (Label)e.Row.FindControl("lblkey") as Label;
            //// hide edit button if neccessary or delete button
            //if (!string.IsNullOrEmpty(lblpartlistlocation.Text))
            //{
            //    if (lblpartlistlocation.Text.Substring(0, 4).ToUpper() != PackageName.ToUpper())
            //    {

            //        foreach (Control c in e.Row.Cells[0].Controls)
            //        {
            //            try
            //            {
            //                LinkButton lb = (LinkButton)c;
            //                if (lb.Text == "Edit")
            //                {
            //                    lb.Visible = false;
            //                    break;

            //                }
            //            }
            //            catch (Exception e1)
            //            {

            //            }

            //        }
            //    }

            //}
            /*
           // Rev

           Image imgrev = (Image)e.Row.FindControl("imgrev") as Image;
           if (imgrev != null)
           {

               DataRowView dview = (DataRowView)e.Row.DataItem as DataRowView;


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
                           e.Row.Cells[2].Controls.Add(temp);
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

           */
            SetAssyNameResult(e.Row);
            DropDownList ddlassyname = (DropDownList)e.Row.FindControl("ddlassyname") as DropDownList;
            if (ddlassyname != null)
            {
                // DropDownList ddltiretype = (DropDownList)e.Row.FindControl("ddltiretype");
                //ddlassyname.Items.Clear();
                if (CategoryDataTable == null)
                    CategoryDataTable = CommonTool.GetCategory();// CommonDB.GetCategory().Tables[0];

                ddlassyname.DataSource = CategoryDataTable;
                ddlassyname.DataTextField = "Category";

                ddlassyname.DataBind();
                //   ddlassyname.SelectedIndex = 0;
                Label lblassyname = e.Row.FindControl("lblassyname") as Label;
                if (lblassyname != null)
                {
                    ddlassyname.SelectedIndex = CommonTool.SelectDropDownListIndexByText(ddlassyname, lblassyname.Text);
                }

            }



            DropDownList ddlkey = (DropDownList)e.Row.FindControl("ddlkey") as DropDownList;
            if (ddlkey != null)
            {
                // DropDownList ddltiretype = (DropDownList)e.Row.FindControl("ddltiretype");
                //ddlassyname.Items.Clear();
                if (KeyDataTable == null)
                    KeyDataTable = CommonDB.GetTSDFormAKey(PackageName).Tables[0];
                ddlkey.DataSource = KeyDataTable;
                ddlkey.DataTextField = "key";
                ddlkey.DataValueField = "AItemId";
                ddlkey.DataBind();
                //    ddlkey.SelectedIndex = 0;

                Label lblkey = e.Row.FindControl("lblkey") as Label;
                if (lblkey != null)
                {
                    ddlkey.SelectedIndex = CommonTool.SelectDropDownListIndexByText(ddlkey, lblkey.Text);
                }


            }



            /*
                        DropDownList ddltreatment = (DropDownList)e.Row.FindControl("ddltreatment") as DropDownList;
                        if (ddltreatment != null)
                        {
                            Label lbltreatment = e.Row.FindControl("lbltreatment") as Label;
                            if (lbltreatment != null)
                            {
                                ddltreatment.SelectedIndex = CommonTool.SelectDropDownListIndexByText(ddltreatment, lbltreatment.Text);
                            }
                        }


            */
            if (isdisable)
            {
                CheckBox ecicheck = e.Row.FindControl("chkecilog") as CheckBox;
                CommonTool.DisableControl(ecicheck);

                DropDownList ddlassyname1 = (DropDownList)e.Row.FindControl("ddlassyname") as DropDownList;
                CommonTool.DisableControl(ddlassyname1);

                DropDownList ddlkey1 = (DropDownList)e.Row.FindControl("ddlkey") as DropDownList;
                CommonTool.DisableControl(ddlassyname1);

                TextBox txtaddtfc = e.Row.FindControl("txtaddtfc") as TextBox;
                CommonTool.DisableControl(txtaddtfc);


                CommonTool.DisableControl(txtaddindex);

                TextBox txtdeltfc = e.Row.FindControl("txtdeltfc") as TextBox;
                CommonTool.DisableControl(txtdeltfc);


                CommonTool.DisableControl(txtdelindex);

                TextBox txtdesc = e.Row.FindControl("txtdesc") as TextBox;
                CommonTool.DisableControl(txtdesc);

                CommonTool.DisableControl(txtfromeci);
            }

        }

    }
    /// <summary>
    /// Handles the RowEditing event of the Gvwpartlist control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
    protected void Gvwpartlist_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Gvwpartlist.EditIndex = e.NewEditIndex;
        BindFirstGridview();
        Gvwpartlist.DataBind();

    }
    /// <summary>
    /// Handles the RowCancelingEdit event of the Gvwpartlist control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCancelEditEventArgs"/> instance containing the event data.</param>
    protected void Gvwpartlist_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Gvwpartlist.EditIndex = -1;
        BindFirstGridview();
        Gvwpartlist.DataBind();
    }

    protected void Gvwpartlist_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        //   hiddenfield.Value = "A:201110";
        bool vLog = ((CheckBox)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("chkecilog") as CheckBox).Checked;

        if (vLog)
        {
            //logging

            if (string.IsNullOrEmpty(hiddenfield.Value) || hiddenfield.Value.ToUpper().Trim() == "undefined".ToUpper())
            {
                // not check radio button or  cancel buttion clicked
                // do nothing
                return;
            }
            else
            {
                string[] resultarray = hiddenfield.Value.Split(':');
                if (resultarray[0] == "E")
                {
                    EciAcid = resultarray[1];
                }
                else
                {
                    AitemId = resultarray[1];

                }

            }

        }




        // save to database
        string formcitemid = ((Label)Gvwpartlist.Rows[e.RowIndex].FindControl("lblitemid")).Text;
        string formcaitemid = ((Label)Gvwpartlist.Rows[e.RowIndex].FindControl("lblaitemid")).Text;
        string assycode =
            ((DropDownList)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("ddlassyname") as DropDownList).
                SelectedItem.ToString().Trim().Substring(0, 3);
        string vKey = ((DropDownList)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("ddlkey") as DropDownList).SelectedValue;//ddlassyname
        string addtfc = GetTextBoxValue(Gvwpartlist.EditIndex, "txtaddtfc");
        string addindex = GetTextBoxValue(Gvwpartlist.EditIndex, "txtaddindex");
        string finaladdindex = assycode + addindex;
        string deltfc = GetTextBoxValue(Gvwpartlist.EditIndex, "txtdeltfc");
        string delindex = assycode + GetTextBoxValue(Gvwpartlist.EditIndex, "txtdelindex");
        string vDescription = GetTextBoxValue(Gvwpartlist.EditIndex, "txtdesc");
        string fromeci = GetTextBoxValue(Gvwpartlist.EditIndex, "txtfromeci");
        string toeci = GetTextBoxValue(Gvwpartlist.EditIndex, "txttoeci");



        string result = CommonDB.UpdateTSDFormCWithTransactionForEdit(formcitemid, formcaitemid, addtfc, finaladdindex, deltfc, delindex, vDescription, fromeci, toeci, vKey);
              


        if (result == "Success")
        {
            //this.TextBox1.Text = "";
            Response.Redirect("FormCDataEditDone.aspx");
        }
        else
        {
            //error
            //this.TextBox1.Text = "";
            Response.Redirect("http://colweb01/eta/NoTrans.asp?Message=" + result);

        }


    }

    private string GetTextBoxValue(int editindex, string controlname)
    {
        TextBox txtbox = (TextBox)Gvwpartlist.Rows[editindex].FindControl(controlname) as TextBox;
        string result = null;

        if (!string.IsNullOrEmpty(txtbox.Text))
        {
            result = txtbox.Text.Trim();
        }

        return result;
    }

    #endregion

    #region Page Methods
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PackageName = Request.QueryString["Package"];

            Module = Request.QueryString["Module"];
            if (string.IsNullOrEmpty(PackageName))
            {
                PackageName = "XXX1";
            }

            EciAcid = Request.QueryString["EciAcId"];
            AitemId = Request.QueryString["AitemId"];


            // Assign Model, Mast, Attachment
            DataSet ds = CommonDB.GetHeaderInfo(PackageName);
            if (ds.Tables[0].Rows.Count != 0)
            {

                lblmodel.Text = ds.Tables[0].Rows[0]["Model"].ToString();
                lblmast.Text = ds.Tables[0].Rows[0]["Mast"].ToString();
                lblatt.Text = ds.Tables[0].Rows[0]["Attachment"].ToString();
            }
            CheckStatusAndGetParameterValues();


            AssignHeaderValues();


            //bind Gvwpartlist 


            BindFirstGridview();

            this.DataBind();



        }

    }

    /// <summary>
    /// Binds the first gridview.
    /// </summary>
    private void BindFirstGridview()
    {

        if (BottomGvNewDataTable == null)
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
                BottomGvNewDataTable = CommonDB.GetTSDFormCInfoByPackageNameAndModule(PackageName, Module, false).Tables[0];
            }

            else
            {
                BottomGvNewDataTable = CommonDB.GetTSDFormCInfoByPackageNameAndModule(PackageName, Module, true).Tables[0];
            }


        }



        Gvwpartlist.DataSource = BottomGvNewDataTable;


    }


    /// <summary>
    /// Checks the status and get parameter values.
    /// </summary>
    private void CheckStatusAndGetParameterValues()
    {
        //'**Check ECI Status

        // Check for released status
        DataTable dt = CommonDB.GetLockInfoByPackageName(PackageName).Tables[0];

        string ecistart = string.Empty;
        string releasedate = string.Empty;
        string ecinumber = string.Empty;
        CommonDB.AssignReleaseDateByPackage(PackageName, out releasedate, out ecistart, out ecinumber);
        ReleaseDate = releasedate;
        EciNumber = ecinumber;


        if (dt.Rows.Count > 0)
        {
            if (Convert.ToBoolean(dt.Rows[0][0]) == true)
            {
                //**If package is locked, redirect
                Response.Redirect("http://colweb01/eta/locked.asp?Package=" + PackageName);
            }
            else
            {
                if (string.IsNullOrEmpty(ecistart))
                {
                    //**If package is not locked but ECI is not started or NoEci, redirect
                    Response.Redirect("http://colweb01/eta/locked.asp?Package=" + PackageName);
                }
                else
                {
                    if (!ecistart.Equals("NoEci"))
                    {
                        EciMode = "on";

                    }

                }

            }
        }
    }

    //private string AssignReleaseDateByPackage()
    //{
    //    //Check ECI Status and set ecinumber
    //    DataTable dtecistatus = CommonDB.GetECIStatusInfoByPackageName(PackageName).Tables[0];
    //    //  var ecistart = AssignReleaseDateByPackage(dtecistatus);

    //    string ecistart = null;

    //    if (dtecistatus.Rows.Count == 0)
    //    {
    //        ecistart = "NoEci";
    //        ReleaseDate = null;
    //    }
    //    else
    //    {
    //        if (dtecistatus.Rows[0][0] == null)
    //        {
    //            ecistart = null;
    //        }
    //        else
    //        {
    //            ecistart = dtecistatus.Rows[0][0].ToString();
    //        }

    //        if (dtecistatus.Rows[0][1] == null)
    //        {
    //            ReleaseDate = null;
    //        }
    //        else
    //        {
    //            ReleaseDate = dtecistatus.Rows[0][1].ToString();
    //        }


    //        EciNumber = dtecistatus.Rows[0][1].ToString();
    //    }
    //    return ecistart;
    //}

    /// <summary>
    /// Assigns the header values.
    /// </summary>
    private void AssignHeaderValues()
    {
        DataTable leftheaddt = CommonDB.GetPartListHeaderInfoByPackageName(PackageName).Tables[0];
        HeaderGridview.DataSource = leftheaddt;
        //HeaderGridview.DataBind();
    }
    #endregion

    #region Validation Method
    protected bool IsValidPairId(string pairid)
    {
        bool result = false;
        // Add on 03/08/2013
        if (String.IsNullOrEmpty(pairid))

            return true;

        for (int i = 0; i < this.Gvwpartlist.Rows.Count; i++)
        {
            Label label = Gvwpartlist.Rows[i].FindControl("lblid") as Label;
            if (label != null & label.Text == pairid)
            {

                result = true;
                break;
            }

        }

        return result;
    }

    protected string GetPairId(string id)
    {
        string result = string.Empty;

        for (int i = 0; i < this.Gvwpartlist.Rows.Count; i++)
        {
            Label label = Gvwpartlist.Rows[i].FindControl("lblid") as Label;
            if (label != null & label.Text == id)
            {

                Label label2 = Gvwpartlist.Rows[i].FindControl("lblpair") as Label;
                result = label2.Text;
                break;
            }

        }

        return result;
    }


    protected void PageCodePairValidatingHandler(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source as CustomValidator;
        string stringresult = string.Empty;
        if (_validateflag)
        {

            TextBox txtaddtfc = (TextBox)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("txtaddtfc") as TextBox;
            string addtfc = CommonTool.GetFormattedPageCodeUpdate(txtaddtfc.Text.Trim()) ?? "";

            TextBox txtaddindex = (TextBox)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("txtaddindex") as TextBox;
            string addindex = CommonTool.GetFormattedPageCodeUpdate(txtaddindex.Text.Trim()) ?? "";


            DropDownList ddlassyname = (DropDownList)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("ddlassyname") as DropDownList;
            string assyname1 = ddlassyname.SelectedItem.Text.Trim().Split("".ToCharArray())[0];

            string finaladdindex = assyname1 + addindex;

            // Validation When addtfc==Package
            //CommonTool.IsValidQuantity()
            if (addtfc == PackageName)
            {
                CommonTool.IsvalidPKGTFC(addtfc, finaladdindex);
                if (CommonTool.IsvalidPKGTFC(addtfc, finaladdindex) == -1)
                {
                    cv.ErrorMessage = "ADD TFC Is Package But Values Not Correct";
                    args.IsValid = false;

                    _validateflag = false;
                    _validateresult = args.IsValid;
                    return;
                }
            }

            // Validation When ADD is STD 


            if (CommonTool.IsvalidSTDTFC(addtfc, addindex, assyname1) == -1)
            {
                cv.ErrorMessage = "ADD TFC ; ADD INDEX Values Not Correct";
                args.IsValid = false;



            }

            else
            {
                args.IsValid = true;
            }



            //**********************************************************/







            /*
                        // add on 12/10/2012 for configurator outout validation
                        //1. if treatmentcode is "D", 
                        // with parameter: ordernumber, combination value of 
                        DropDownList ddltreatment = (DropDownList)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("ddltreatment") as DropDownList;
                        if (ddltreatment.SelectedValue.Trim() == "D")
                        {
                            if (!String.IsNullOrEmpty(ddlassyname.SelectedValue))
                            {
                                string para = string.Format("{0}{1}{2}",
                                                            partcode, ddlassyname.SelectedValue.Trim().Split("".ToCharArray())[0], pagecode.Replace(" ", ""));
                                //G839000  0101
                                // get datable by orderno ;
                                // find if table including this combination
                                // if yes,
                                string orderno = CommonDB.GetFirstOrderNoByPackage(PackageName);
                                if (CommonTool.IsValidByConfigurator(orderno, para))
                                {
                                    args.IsValid = true;

                                }
                                else
                                {
                                    cv.ErrorMessage = "Error!Not Pass Configurator Validation!";
                                    args.IsValid = false;
                                }



                            }
                        }
                        // end add 12/120/2012
             */
            _validateflag = false;
            _validateresult = args.IsValid;

        }
        else
        {
            args.IsValid = _validateresult;

        }
        //if (!args.IsValid)
        //{
        //    // cv.ErrorMessage = " Page Code is not assoicated with assycode and partcode.Please make the change !";


        //}
        //else
        //{

        //}
        //return;



    }

    protected void PageCodeValidatingHandlerold(object source, ServerValidateEventArgs args)
    {
        if (_validateflag)
        {
            //get treatment value if it is D , validate ,return
            DropDownList ddltreatment = (DropDownList)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("ddltreatment") as DropDownList;


            //get assyname value

            DropDownList ddlassyname = (DropDownList)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("ddlassyname") as DropDownList;

            if (ddlassyname.SelectedIndex == 0)
            {
                CustomValidator cv = (CustomValidator)source as CustomValidator;
                cv.ErrorMessage = "Please select category first ";
                args.IsValid = false;
                _validateflag = false;
                return;


            }


            string assyname = ddlassyname.SelectedItem.Text.Substring(0, 3);
            //SELECT Level_,PartNo,Minor,PartName,Qty,Dwg,Material1,Material2,PartNoCode1,PartNoComment1, PartNoCode2,PartNoComment2 FROM ETA.dbo.viewStandardParts WHERE Model='M100' AND GroupNo='631' AND CompCode='01' AND Vari='01' AND Ser='01'

            TextBox txtpartcode = (TextBox)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("txtpartcode") as TextBox;
            string partcode = txtpartcode.Text.Trim();

            TextBox txtpagecode = (TextBox)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("txtpagecode") as TextBox;
            string pagecode = CommonTool.GetFormattedPageCodeUpdate(txtpagecode.Text.Trim());

            string[] pagecodearray = pagecode.Split(" ".ToCharArray());
            string compcode = null;
            string vari = null;
            string ser = null;

            switch (pagecodearray.Length)
            {
                case 1:
                    compcode = pagecodearray[0];
                    vari = "";
                    ser = "";
                    break;
                case 2:
                    compcode = pagecodearray[0];
                    vari = pagecodearray[1];
                    ser = "";
                    break;
                default:

                    compcode = pagecodearray[0];
                    vari = pagecodearray[1];
                    ser = pagecodearray[2];

                    break;

            }

            //step 1 :check if partcode is from standardpartlist by check assyname, partcode, page code existing in viewstandpartlist

            DataTable dt = CommonDB.GetStandardPartListInfo(partcode, assyname, compcode, vari, ser).Tables[0];
            if (dt.Rows.Count != 0)
            {
                args.IsValid = true;

                return;

            }


            //step 2: check assycode is blank or not
            TextBox txtcode = (TextBox)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("txtcode") as TextBox;
            if (string.IsNullOrEmpty(txtcode.Text))
            {
                // blank assy code
                //get modulelocation from viewformcintems

                dt = CommonDB.GetNonStanardPartListInfoWithoutAssyCode(partcode, assyname, compcode, vari).Tables[0];


            }

            else
            {
                // not blank assy code
                // using current package and do query in viewpartlist
                dt = CommonDB.GetNonStanardPartListInfoWithAssyCode(PackageName, assyname, compcode, vari).Tables[0];

            }


            if (dt.Rows.Count != 0)
            {
                args.IsValid = true;//true; 



            }
            else
            {
                args.IsValid = true;//false;
            }

            _validateflag = false;

        }


    }

    private Label GetInputLabel(GridViewRow row)
    {

        Label input = row.FindControl("lbldelindex") as Label;
        if (input != null)
        {
            if (!(input.Text.Length == 7 || input.Text.Length == 9))
            {

                //input = row.FindControl("lbldelindex") as Label;
                //if (!(input.Text.Length == 7 || input.Text.Length == 9))
                //{
                input = null;
                // }
            }
        }
        return input;
    }




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

            Label lblname = row.FindControl("lblassyname") as Label;
            // set up value
            lblname.Text = (from p in dt.AsEnumerable()
                            where p.Field<string>("Category").Substring(0, 3) == input.Text.Trim().Substring(0, 3)
                            select p.Field<string>("Category")).FirstOrDefault();


        }

    }
    #endregion





    protected void Gvwpartlist_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // Get current rowid 

        string formcitemid = ((Label)Gvwpartlist.Rows[e.RowIndex].FindControl("lblitemid")).Text;

        //   Label label = (Label)e.Row.FindControl("lblitemid") as Label;


    }
}

