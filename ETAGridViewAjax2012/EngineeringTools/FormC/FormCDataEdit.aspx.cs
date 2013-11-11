using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class EngineeringTools_FormCDataEdit : System.Web.UI.Page
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
        string ss = "";
        ss = ss ?? "sdy";



        if (e.Row.RowType == DataControlRowType.DataRow)
        {


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


            Label lblpartlistlocation = (Label)e.Row.FindControl("lblTireID") as Label;
            // hide edit button if neccessary
            if (!string.IsNullOrEmpty(lblpartlistlocation.Text))
            {
                if (lblpartlistlocation.Text.Substring(0, 4).ToUpper() != PackageName.ToUpper())
                {

                    foreach (Control c in e.Row.Cells[0].Controls)
                    {
                        try
                        {
                            LinkButton lb = (LinkButton)c;
                            if (lb.Text == "Edit")
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


            DropDownList ddlassyname = (DropDownList)e.Row.FindControl("ddlassyname") as DropDownList;
            if (ddlassyname != null)
            {
                // DropDownList ddltiretype = (DropDownList)e.Row.FindControl("ddltiretype");
                //ddlassyname.Items.Clear();
                if (CategoryDataTable == null)
                    CategoryDataTable = CommonTool.GetCategory();
                        
                        //CommonDB.GetCategory().Tables[0];

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
                    KeyDataTable = CommonDB.GetKey(PackageName).Tables[0];
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

            DropDownList ddltreatment = (DropDownList)e.Row.FindControl("ddltreatment") as DropDownList;
            if (ddltreatment != null)
            {
                Label lbltreatment = e.Row.FindControl("lbltreatment") as Label;
                if (lbltreatment != null)
                {
                    ddltreatment.SelectedIndex = CommonTool.SelectDropDownListIndexByText(ddltreatment, lbltreatment.Text);
                }
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

        // add by dayang on 01/08/2013 for configurator validation
        string oriforcpcitemid = ((Label)Gvwpartlist.Rows[e.RowIndex].FindControl("lblpair2")).Text;
        string forcpcitemid = GetTextBoxValue(Gvwpartlist.EditIndex, "txtpair");
        // will create a string to update pairds
        // if newinput pairid is null
        StringBuilder sb = new StringBuilder();
        if(string.IsNullOrEmpty(forcpcitemid))
        {
            // need to delete two formc.
            if(string.IsNullOrEmpty(oriforcpcitemid))
            {
                // no change
               // sb.Append(string.Format("{0},'';", forcpcitemid, oriforcpcitemid));
                
            }
            else
            {

                
                sb.Append(string.Format("{0},empty;{1},empty;", formcitemid, oriforcpcitemid));
                
            }
            
        }
        else
        {
            // need to get the pair id of forcpcitemid
            sb.Append(string.Format("{2},empty;{3},empty;{0},{1};{1},{0};", formcitemid, forcpcitemid, oriforcpcitemid, GetPairId(forcpcitemid)));

        }

        string pairidlist = sb.ToString();




        string[] arrUser = Request.ServerVariables["Auth_User"].ToString().Split(@"\".ToCharArray());
        string[] arrName = arrUser[arrUser.Length - 1].Split(@".".ToCharArray());
        string sInitials = null;
        if (arrName.Length > 1)
        {
            sInitials = arrName[0].Substring(0, 1) + arrName[1].Substring(0, 1);
        }

        string vRev = "A";
        string vCategory = ((DropDownList)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("ddlassyname") as DropDownList).SelectedItem.ToString().Trim();
        string vKey = ((DropDownList)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("ddlkey") as DropDownList).SelectedValue;//ddlassyname
        //txtcode
        string vAssyCode = GetTextBoxValue(Gvwpartlist.EditIndex, "txtcode");
        string vTreatment = ((DropDownList)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("ddltreatment") as DropDownList).SelectedItem.ToString().Trim();
        string vPartCode = GetTextBoxValue(Gvwpartlist.EditIndex, "txtpartcode");
        string vPageCode = CommonTool.GetFormattedPageCodeUpdate(GetTextBoxValue(Gvwpartlist.EditIndex, "txtpagecode"));

        string vDescription = GetTextBoxValue(Gvwpartlist.EditIndex, "txtdesc");
        string Modulevalue = GetTextBoxValue(Gvwpartlist.EditIndex, "txtpartlistlocation");

        string result = CommonDB.InsertFormCWithTransactionForEdit(formcitemid, forcpcitemid, EciMode, EciNumber, sInitials, PackageName, vLog, vRev, vCategory, vKey, vAssyCode, vTreatment, vPartCode, vPageCode, vDescription, Modulevalue, EciAcid, AitemId,pairidlist);



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
            //if (string.IsNullOrEmpty(PackageName))
            //{
            //    PackageName = "N9ju";
            //}

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
        //'**Get current Form C items
        //'**Apply filter from TabStrip if required

        if (string.IsNullOrEmpty(Module))
        {

            BottomGvNewDataTable = CommonDB.GetFormCInfoByPackageNameAndModule(PackageName, Module, false).Tables[0];
        }

        else
        {
            BottomGvNewDataTable = CommonDB.GetFormCInfoByPackageNameAndModule(PackageName, Module, true).Tables[0];
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

        //Check ECI Status and set ecinumber
        DataTable dtecistatus = CommonDB.GetECIStatusInfoByPackageName(PackageName).Tables[0];
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

            EciNumber = dtecistatus.Rows[0][1].ToString();
        }

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

            // Validate Pair
            // add by dayang on 01/08/2013 for configurator validation
            string forcpcitemid = GetTextBoxValue(Gvwpartlist.EditIndex, "txtpair");
            // only trigger pairid validation if it has value
            if (!string.IsNullOrEmpty(forcpcitemid))
            {
                //DataTable dt = this.Gvwpartlist.DataSource as DataTable;
                // DataTable dt = BottomGvNewDataTable.Copy();
                //var rows = dt.Select(string.Format("CItemId='{0}'", forcpcitemid));
                Label ttt = Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("lblid") as Label;
                string id = ttt.Text;
                //bool isvalidpair = IsValidPairId(forcpcitemid);

                // validate this formcitemid
                // go through current gvwpartlist to find this item
                // this.Gvwpartlist.Rows.Count
                if (!IsValidPairId(forcpcitemid))
                // Using same rule as that of auto pair
                //  if (forcpcitemid != CommonTool.GetPairID(dt, id))
                {
                    args.IsValid = false;
                    cv.ErrorMessage = CommonTool.FormCPairIDErrorMessage;
                    _validateflag = false;
                    _validateresult = args.IsValid;
                    return;
                }
            }
            // Validate Page Code
            if (StandardPartcodeDataTable == null)
            {
                StandardPartcodeDataTable = CommonDB.GetStandardPartcodeDataTableFromStandPartsList().Tables[0];
            }
            DropDownList ddlassyname = (DropDownList)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("ddlassyname") as DropDownList;

            TextBox txtpagecode = (TextBox)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("txtpagecode") as TextBox;
            string pagecode = CommonTool.GetFormattedPageCodeUpdate(txtpagecode.Text.Trim()) ?? "";




            TextBox txtcode = (TextBox)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("txtcode") as TextBox;

            // get partcode 
            TextBox txtpartcode = (TextBox)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("txtpartcode") as TextBox;
            string partcode = txtpartcode.Text.Trim();

            // If assy  code is not empty , skip the validation
            // on 09/15/2009 
            // If assy  code is not empty , skip the validation
            // on 09/15/2009 
            // revise rule on 02/27/2013
            // if txtcode is not empty ; 
            /**********************************************************
             
             --validate pairid first ; if invalid return;
             
           
              
              
              
             
             //**********************************************************/






            if (!string.IsNullOrEmpty(txtcode.Text.Trim()) || CommonTool.IsFormCPageCode99(pagecode))
            {
                args.IsValid = true;
            }
            else
            {

                int result = CommonTool.IsvalidPagecode(pagecode, partcode, ddlassyname.SelectedItem.Text.Trim(), txtcode.Text.Trim(), PackageName, StandardPartcodeDataTable);
                if (result < 1)
                {
                    cv.ErrorMessage = CommonTool.FormCPageCodeErrorMessage;
                    args.IsValid = false;

                }

                else
                {
                    if (string.IsNullOrEmpty(pagecode))
                    {
                        cv.ErrorMessage = CommonTool.FormCRefErrorMessage;
                        args.IsValid = false;
                    }

                    else
                    {
                        // add a new validation
                        string assyname = ddlassyname.SelectedItem.Text.Trim().Split("".ToCharArray())[0];
                       // string[] codearray = pagecode.Split("".ToCharArray());

                        if (CommonTool.IsvalidRefModulePartnumbers(partcode, pagecode.Substring(0, 2), pagecode.Substring(2, 2), assyname, ref stringresult))
                        {

                            args.IsValid = true;
                        }
                        else
                        {
                            cv.ErrorMessage = CommonTool.FormCRefErrorMessage + "  :: " + stringresult;
                            args.IsValid = false;
                        }
                    }
                }
            }
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
    #endregion


    protected void btnpair_Click(object sender, EventArgs e)
    {
        //JavascriptHelper.Alerts(this, "will start autopair ");
        //// Get current formc item list
        //DataTable dt1 = BottomGvNewDataTable.Copy();
        ////dt.Rows[0].AcceptChanges();
        ////dt.Rows[0].SetModified();
        //////var rows = (from p in dt.AsEnumerable()
        //////            where p.Field<string>("") == "" && p.Field<string>("ADD_INDEX") == ""
        //////            select ).ToList();

        //var row = (from p in dt1.AsEnumerable()
        //           select p).Take(3);
        ////var test = dt.Rows[2];


        //   int k = dt1.Rows.IndexOf(row.ToList()[2]);

        // Get current itemlist copy
        DataTable dt = BottomGvNewDataTable.Copy();


        var dbinputs = CommonTool.GetFormCAutoPairStringByDataTable(dt);
        if (dbinputs.Length > 0)
        {
            if (dbinputs.Length >= 4000)
            {
                // this.btnpair.Enabled = false;
                // JavascriptHelper.Alerts(this, "Error: Too many items need to be paired.Pleaese contact system administrator");
                List<string> list2 = dbinputs.Split(';').ToList();
                int times = Convert.ToInt16(Math.Ceiling(list2.Count / Convert.ToDouble(MAXPAIRSIZE)));
                string[] subarray = null;
                for (int i = 0; i < times; i++)
                {
                    if (i == times - 1)
                    {
                        subarray = list2.GetRange(i * MAXPAIRSIZE, list2.Count - i * MAXPAIRSIZE).ToArray();

                    }
                    else
                    {
                        subarray = list2.GetRange(i * MAXPAIRSIZE, MAXPAIRSIZE).ToArray();

                    }

                    string subinputs = String.Join(";", subarray);
                    CommonDB.BulkUpdateFormCItems(subinputs);

                }

            }
            else
            {
                CommonDB.BulkUpdateFormCItems(dbinputs);
                //JavascriptHelper.Alerts(this,dbinputs);


            }
            BindFirstGridview();
            Gvwpartlist.DataBind();
        }
    }


}
