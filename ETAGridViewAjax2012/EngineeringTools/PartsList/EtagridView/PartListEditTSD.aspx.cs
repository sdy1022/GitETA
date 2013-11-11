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

public partial class EngineeringTools_PartsList_EtagridView_PartListEditTSD : System.Web.UI.Page
{

    #region Prop
    // protected string Package = null;
    private bool isgridviewchange = false;
    private bool partlistvalid = true;
    public bool _levelvalidateresult = true;
    public bool _partnovalidateresult = true;
    public bool _quantyvalidateresult = true;
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
    public bool _levelvalidateflag = true;
    public bool _partnovalidateflag = true;
    public bool _quantyvalidateflag = true;
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



    //protected string EciNumber = null;
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
    public string CurrentECI
    {
        get
        {
            return (string)ViewState["CurrentECI"];
        }
        set
        {

            ViewState["CurrentECI"] = value;

        }
    }

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

    public string HeaderID
    {
        get
        {
            return (string)ViewState["HeaderID"];
        }
        set
        {
            ViewState["HeaderID"] = value;
        }
    }




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


            if (CategoryDataTable == null)
                CategoryDataTable = CommonTool.GetCategory();//CommonDB.GetCategory().Tables[0];

            string result = (from t in CategoryDataTable.AsEnumerable()
                             where t.Field<string>("Category").Substring(0, 3) == value
                             select t.Field<string>("Category")).ToList().FirstOrDefault();

            TxtName1 = result;



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
            txtNAME3.Text = value;
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
            txtNAME4.Text = value;
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
            txtNAME1.Text = value;
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
            txtNAME2.Text = value;
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

    #endregion
    #region Page Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {



            // get Package
            Package = Request.QueryString["Package"];
            if (string.IsNullOrEmpty(Package))
            {
                Package = "NE69";
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




            // Check for released status
            DataTable dt = CommonDB.GetLockInfoByPackageName(Package).Tables[0];

            //Check ECI Status and set EciNumber
            //DataTable dtecistatus = CommonDB.GetECIStatusInfoByPackageName(Package).Tables[0];



            string ecistart = string.Empty;
            string releasedate = string.Empty;
            string ecinumber = string.Empty;
            CommonDB.AssignReleaseDateByPackage(Package, out releasedate, out ecistart, out ecinumber);
            ReleaseDate = releasedate;
            EciNumber = ecinumber;

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

            DataTable dtkeya = CommonDB.GetKeyAByEciNumber(EciNumber).Tables[0];

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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlpartlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        CurrentParentPart = ddlpartlist.SelectedValue;
        BindFormControls();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
                hpleci.NavigateUrl = string.Format("http://colweb01/eta/ECI/eci.asp?Eci={0}", EciNumber);
                hpleci.Text = EciNumber;
            }



        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddleci_SelectedIndexChanged(object sender, EventArgs e)
    {
        isgridviewchange = true;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnfiler_Click(object sender, EventArgs e)
    {
        if (isgridviewchange)
        {

            FilterGridDTByECI(ddleci.SelectedValue);
            //  DataTable dt=NewDataTable.Select()
            // Get current datasource of gridview
            Gvwpartlist.DataBind();
            isgridviewchange = false;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkeditheader_CheckedChanged(object sender, EventArgs e)
    {
        this.lblerror.Visible = false;
        SettingHeadControlsByMode();
        SetHeaderTxtValuesFromProperties();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        //validate input 



        DataTable dt = CommonDB.GetPartListHeaderValidateResult(Package, txtCODE1.Text, txtCODE2.Text, txtCODE3.Text, txtCODE4.Text).Tables[0];
        if (dt.Rows.Count > 0)
        {

            if (Int16.Parse(dt.Rows[0][0].ToString()) > 0)
            {



                //Save to database

                int result = CommonDB.UpdatePartListHeader(Int32.Parse(HeaderID), txtNAME1.Text, txtNAME2.Text, txtNAME3.Text, txtNAME4.Text, txtCODE1.Text, txtCODE2.Text, txtCODE3.Text, txtCODE4.Text);
                //UPDATE ETA.dbo.PartsListHeaders SET Name1='WIRE HARNESS',Name2='MAIN HARNESSdaaa',Name3='1TON ',Name4='',Code1='561',Code2='01',Code3='03',Code4='' WHERE HeaderId=45996

                if (result == -1)
                {
                    this.lblerror.Visible = true;
                    lblerror.Text = "Error Happens: Transaction Error";
                }
                else
                {
                    // back to view state
                    this.lblerror.Visible = false;
                    this.chkeditheader.Checked = false;
                    SettingHeadControlsByMode();
                    //  Response.Redirect("PartListEditDone.aspx");
                }


            }
            else
            {

                this.lblerror.Visible = true;
                lblerror.Text = "Error Happens: Invalid Inputs";
            }

        }

        // save the change to database

        // if successed, uncheck header and back to readonly mode for the header

        // if failed, 






    }
    /// <summary>
    /// /
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Gvwpartlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // if eci number is different 
        // all four controls is readonly
        //delete button is disabled




        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //if (!string.IsNullOrEmpty(ReleaseDate))
            //{
            // Editbutton



            Label label = (Label)e.Row.FindControl("lblfromeci1") as Label;

            // hide edit button if neccessary or delete button
            // if fromeci not equal EciNumber
            if (label.Text != EciNumber)
            {
                ChangeTextBoxControl(e, "txtlevel", false);
                ChangeTextBoxControl(e, "txtpartno", false);
                ChangeTextBoxControl(e, "txtqty", false);
                ChangeTextBoxControl(e, "txtfromeci1", false);
                // Hide Delete Button
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

            else
            {
                // text toeci is readyonly 
                ChangeTextBoxControl(e, "txttoeci", false);
            }
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    /// <param name="controlname"></param>
    /// <param name="isenable"></param>
    private static void ChangeTextBoxControl(GridViewRowEventArgs e, string controlname, bool isenable)
    {
        TextBox txt = (TextBox)e.Row.FindControl(controlname) as TextBox;
        if (txt != null)
        {
            txt.Enabled = isenable;
        }
    }

    /// <summary>
    /// /
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Gvwpartlist_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Gvwpartlist.EditIndex = e.NewEditIndex;
        //  BindFirstGridview();
        BindGridView();
        Gvwpartlist.DataBind();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Gvwpartlist_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Gvwpartlist.EditIndex = -1;
        BindGridView();
        Gvwpartlist.DataBind();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Gvwpartlist_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string partlistitemid = ((Label)Gvwpartlist.Rows[e.RowIndex].FindControl("lblid")).Text;
        string res = CommonDB.DeleteTSDPartListWithTransaction(partlistitemid);

        if (res == "Success")
        {
            Response.Redirect("PartListEditDone.aspx");
        }
        else
        {
            //error
            Response.Redirect("http://colweb01/eta/NoTrans.asp?Message=" + res);

        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Gvwpartlist_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {


        string partlistitemid = ((Label)Gvwpartlist.Rows[e.RowIndex].FindControl("lblid")).Text;
        int originaltreevalue = int.Parse(GridViewDataTable.Rows[Gvwpartlist.EditIndex]["TReeLevel"].ToString());
        int newtreevalue = int.Parse((Gvwpartlist.Rows[e.RowIndex].FindControl("txtlevel") as TextBox).Text);
        string currenetordervalue = ((Label)Gvwpartlist.Rows[e.RowIndex].FindControl("lblordervalue")).Text;
        // index_partent
        string newpartentvalue = null;

        if (newtreevalue == originaltreevalue)
        {

            newpartentvalue = (Gvwpartlist.Rows[e.RowIndex].FindControl("txtpartno") as TextBox).Text;
        }

        else
        {
            // look for the list of newlevel-1

            if (newtreevalue == 1)
            {
                newpartentvalue = string.Format("{0} {1}{2}{3}", TxtCode1, TxtCode2, TxtCode3, TxtCode4);
            }
            else
            {

                newpartentvalue = (from p in GridViewDataTable.AsEnumerable()
                                   where string.Compare(p.Field<string>("ordervalue"), currenetordervalue) < 0
                                         && p.Field<int>("TreeLevel") == (newtreevalue - 1)
                                   orderby p.Field<string>("ordervalue") descending
                                   select p.Field<string>("PARENT_CHILDPART")).FirstOrDefault();
            }
        }

        // parent_childpart
        string partno = (Gvwpartlist.Rows[e.RowIndex].FindControl("txtpartno") as TextBox).Text;

        string QtyValue = (Gvwpartlist.Rows[e.RowIndex].FindControl("txtqty") as TextBox).Text;
        string fromeci = (Gvwpartlist.Rows[e.RowIndex].FindControl("txtfromeci1") as TextBox).Text;
        string toeci = (Gvwpartlist.Rows[e.RowIndex].FindControl("txttoeci") as TextBox).Text;
        string res = CommonDB.UpdateTSDPartListWithTransactionForEdit(partlistitemid, newpartentvalue, partno, QtyValue, fromeci, toeci);

        if (res == "Success")
        {
            Response.Redirect("PartListEditDone.aspx");
        }
        else
        {
            //error
            Response.Redirect("http://colweb01/eta/NoTrans.asp?Message=" + res);

        }

        return;

        //string PartNameValue = (Gvwpartlist.Rows[e.RowIndex].FindControl("txtpartname") as TextBox).Text;


        //string MaterialValue = (Gvwpartlist.Rows[e.RowIndex].FindControl("txtMaterial") as TextBox).Text;
        //string SizeValue = (Gvwpartlist.Rows[e.RowIndex].FindControl("txtSize") as TextBox).Text;

        //string DwgValue = (Gvwpartlist.Rows[e.RowIndex].FindControl("ddldwg") as DropDownList).SelectedItem.ToString();
        //string CommentValue = (Gvwpartlist.Rows[e.RowIndex].FindControl("txtaComment") as TextBox).Text;
        ////    bool vLog = false;//(Gvwpartlist.Rows[e.RowIndex].FindControl("chkecilog") as CheckBox).Checked;


        //string[] arrUser = Request.ServerVariables["Auth_User"].ToString().Split(@"\".ToCharArray());
        //string[] arrName = arrUser[arrUser.Length - 1].Split(@".".ToCharArray());
        //string sInitials = null;

        //if (arrName.Length > 1)
        //{
        //    sInitials = arrName[0].Substring(0, 1) + arrName[1].Substring(0, 1);
        //}

        //// If ECI Mode, Log data

        //string itemid = null;
        //string result = CommonDB.UpdateTSDPartListWithTransactionForEdit(itemid, newtreevalue.ToString(), partno, QtyValue, PartNameValue, DwgValue, MaterialValue, SizeValue, CommentValue);

        //if (result == "Success")
        //{
        //    Response.Redirect("PartListEditDone.aspx");
        //}
        //else
        //{
        //    //error
        //    Response.Redirect("http://colweb01/eta/NoTrans.asp?Message=" + result);

        //}

    }

    protected void CustomizedQuantityValidationHandlerForLevel(object source, ServerValidateEventArgs args)
    {
        bool result = false;
        if (_levelvalidateflag)
        {
            string currenetordervalue = ((Label)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("lblordervalue")).Text;
            CustomValidator cv = (CustomValidator)source as CustomValidator;


            int originaltreevalue = int.Parse(GridViewDataTable.Rows[Gvwpartlist.EditIndex]["TReeLevel"].ToString());
            int newtreevalue = int.Parse(args.Value);



            if (newtreevalue == originaltreevalue)
            {

                result = true;


            }

            else
            {
                // look for the list of newlevel-1
                // from p in dt.AsEnumerable()
                //where p.Field<string>("Category").Substring(0, 3) == input.Text.Trim().Substring(0, 3)
                //select p.Field<string>("Category")).FirstOrDefault();
                string newpartentvalue = null;

                if (newtreevalue == 1)
                {
                    newpartentvalue = string.Format("{0} {1}{2}{3}", TxtCode1, TxtCode2, TxtCode3, TxtCode4);
                }
                else
                {

                    newpartentvalue = (from p in GridViewDataTable.AsEnumerable()
                                       where string.Compare(p.Field<string>("ordervalue"), currenetordervalue) < 0
                                             && p.Field<int>("TreeLevel") == (newtreevalue - 1)
                                       orderby p.Field<string>("ordervalue") descending
                                       select p.Field<string>("PARENT_CHILDPART")).FirstOrDefault();
                }
                if (string.IsNullOrEmpty(newpartentvalue))
                {
                    cv.ErrorMessage = "Level Value Is Invalid";
                    result = false;

                }
                else
                {

                    result = true;
                }

            }

            _levelvalidateflag = false;
            args.IsValid = result;


            _levelvalidateresult = args.IsValid;

        }
        else
        {
            args.IsValid = _levelvalidateresult;

        }


    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void CustomizedQuantityValidationHandlerForQuantity(object source, ServerValidateEventArgs args)
    {
        bool result = false;
        //   CustomValidator cv = (CustomValidator)source as CustomValidator;
        if (_quantyvalidateflag)
        {

            TextBox txtQty = (TextBox)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("txtqty") as TextBox;

            if (txtQty != null)
            {
                  if (CommonTool.IsValidQuantity(txtQty.Text.Trim()))
                //int quantity = 0;
                //if (Int32.TryParse(txtQty.Text.Trim(), out quantity))
                //{

                //    result = true;
                //}
                  {
                      result = true;
                  }
                else
                {
                    this.lblpartlisterror.Visible = true;
                    lblpartlisterror.Text = " Error : Invalid Input(s)";
                    result = false;
                }




            }

            _quantyvalidateflag = false;

            args.IsValid = result;
            _quantyvalidateresult = args.IsValid;


        }
        else
        {
            args.IsValid = _quantyvalidateresult;

        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void CustomizedQuantityValidationHandlerForPartno(object source, ServerValidateEventArgs args)
    {


        bool result = false;
        //  CustomValidator cv = (CustomValidator)source as CustomValidator;

        if (_partnovalidateflag)
        {

            TextBox txtpartno = (TextBox)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("txtpartno") as TextBox;
            int validateresult = (int)CommonTool.IsvalidTSDPartnoWithEnu(txtpartno.Text.Trim(), Package, 0);

            switch (validateresult)
            {
                case -4:
                    this.lblpartlisterror.Visible = true;
                    lblpartlisterror.Text = "Exception Error";
                    partlistvalid = false;
                    result = false;
                    break;

                case -3:
                    this.lblpartlisterror.Visible = true;
                    lblpartlisterror.Text = "Invalid Value With Valid Format";
                    partlistvalid = false;
                    result = false;
                    break;
                case -2:
                    this.lblpartlisterror.Visible = true;
                    lblpartlisterror.Text = "Recursive Error";
                    partlistvalid = false;
                    result = false;
                    break;
                case -1:
                    this.lblpartlisterror.Visible = true;
                    lblpartlisterror.Text = "Format Error ";
                    partlistvalid = false;
                    result = false;
                    break;


                case 0:
                    this.lblpartlisterror.Visible = true;
                    lblpartlisterror.Text = "Warning : The partno does not match the package";
                    partlistvalid = true;
                    result = true;
                    break;
                case 1:
                    this.lblpartlisterror.Visible = false;
                    lblpartlisterror.Text = "";
                    partlistvalid = true;
                    result = true;
                    break;
                default:
                    break;
            }

            _partnovalidateflag = false;
            args.IsValid = result;

            _partnovalidateresult = args.IsValid;


        }
        else
        {
            args.IsValid = _partnovalidateresult;

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
        BindFormControls();
    }
    /// <summary>
    /// 
    /// </summary>
    private void BindGridView()
    {

        GridViewDataTable = CommonDB.GetPartListInfoTSDByPackageNameAndParentNumber(Package, CurrentParentPart, true).Tables[0];

        // get current selection of eci drop down
        FilterGridDTByECI(CurrentECI);
    }


    /// <summary>
    /// 
    /// </summary>
    /// 
    /// 
    private void BindFormControls()
    {
        DataTable dt = null;
        // Bind  Header Name2 Name2 Name4 with current CurrentParentPart 
        AssignTxtNameValues();

        if (Module.IndexOf('-') == 4)
        {
            dt = CommonDB.GetPartListInfoTSDByPackageName(Module, CurrentParentPart).Tables[0];
        }

        else
        {
            dt = CommonDB.GetPartListInfoTSDByPackageName(Package, CurrentParentPart).Tables[0];
        }

        // Assign HeaderID

        if (dt.Rows.Count == 0)
        {
            return;
        }
        HeaderID = dt.Rows[0]["HeaderID"].ToString();

        DataTable leftheaddt = CommonDB.GetPartListHeaderInfoByPackageNameAndHeaderId(Package, dt.Rows[0]["HeaderID"].ToString()).Tables[0];
        HeaderGridview.DataSource = leftheaddt;

        //  List<string> finalrows= rows.ToList();
        DataTable dteci = CommonDB.GetPartListInfoTSDByPackageNameAndParentNumber(Package, CurrentParentPart, false).Tables[0];
        // string currenteci = null;
        if (dteci.Rows.Count > 0)
        {

            CurrentECI = dteci.Rows[0]["FinalResult"].ToString();
            this.ddleci.DataSource = dteci;

            ddleci.DataTextField = "FinalResult";
            ddleci.DataValueField = "FinalResult";
        }

        BindGridView();
        //   this.Gvwpartlist.DataSource = GridViewDataTable;
        this.DataBind();
    }
    /// <summary>
    /// 
    /// </summary>
    private void AssignTxtNameValues()
    {
        DataTable dt;
        dt = CommonDB.GetTSDHeaderNamesInfo(Package, TxtCode1, TxtCode2, TxtCode3, TxtCode4).Tables[0];

        if (dt.Rows.Count > 0)
        {
            TxtName2 = dt.Rows[0]["NAME2"].ToString();
            TxtName3 = dt.Rows[0]["NAME3"].ToString();
            TxtName4 = dt.Rows[0]["NAME4"].ToString();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="orieci"></param>
    private void FilterGridDTByECI(string orieci)
    {
        //  string orieci = CurrentECI;
        // get datasource of 
        if (string.IsNullOrEmpty(orieci))
        {
            return;
        }
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
    /// <summary>
    /// 
    /// </summary>
    private void SetHeaderTxtValuesFromProperties()
    {
        txtNAME1.Text = TxtName1;
        txtNAME2.Text = TxtName2;
        txtNAME3.Text = TxtName3;
        txtNAME4.Text = TxtName4;

        txtCODE1.Text = TxtCode1;
        txtCODE2.Text = TxtCode2;
        txtCODE3.Text = TxtCode3;
        txtCODE4.Text = TxtCode4;
    }
    /// <summary>
    /// 
    /// </summary>
    private void SettingHeadControlsByMode()
    {
        if (chkeditheader.Checked)
        {

            EnableHeadControls(true, txtNAME2);
            EnableHeadControls(true, txtNAME3);
            EnableHeadControls(true, txtNAME4);
            //EnableHeadControls(true, txtNAME1);
            //EnableHeadControls(true, txtCODE1);
            //EnableHeadControls(true, txtCODE2);
            //EnableHeadControls(true, txtCODE3);
            //EnableHeadControls(true, txtCODE4);
            this.btnsubmit.Visible = true;

        }
        else
        {

            EnableHeadControls(false, txtNAME2);
            EnableHeadControls(false, txtNAME3);
            EnableHeadControls(false, txtNAME4);
            //EnableHeadControls(false, txtNAME1);
            //EnableHeadControls(false, txtCODE1);
            //EnableHeadControls(false, txtCODE2);
            //EnableHeadControls(false, txtCODE3);
            //EnableHeadControls(false, txtCODE4);
            this.btnsubmit.Visible = false;

        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="flag"></param>
    /// <param name="txtcontrol"></param>
    private void EnableHeadControls(bool flag, TextBox txtcontrol)
    {
        if (flag)
        {
            txtcontrol.Enabled = true;
            txtcontrol.BackColor = System.Drawing.Color.Yellow;
        }
        else
        {
            txtcontrol.Enabled = false;
            txtcontrol.BackColor = System.Drawing.Color.White;

        }
    }



    #endregion

}


