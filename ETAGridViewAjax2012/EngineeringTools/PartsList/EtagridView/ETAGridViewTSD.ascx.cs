﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class ETAGridViewTSD : System.Web.UI.UserControl
{
    #region Property For State Info

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

    public DropDownList Ecilist;
    /// <summary>
    /// Gets or sets the module.
    /// </summary>
    /// <value>The module.</value>
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


    /// <summary>
    /// Gets or sets the header ID.
    /// </summary>
    /// <value>The header ID.</value>  

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


    /// <summary>
    /// ECi Number
    /// </summary> 
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
    /// <summary>
    /// Gets or sets the eci mode.
    /// </summary>
    /// <value>The eci mode.</value>    
    public string EciMode
    {
        get
        {
            return (string)ViewState["EciMode"];
        }
        set
        {
            ViewState["EciMode"] = value;
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

    public string KeyA
    {
        get
        {
            return (string)ViewState["KeyA"];
        }
        set
        {
            ViewState["KeyA"] = value;
        }
    }

    public string CurrentRev
    {
        get
        {
            return (string)ViewState["CurrentRev"];
        }
        set
        {
            ViewState["CurrentRev"] = value;
        }
    }
    public string ParentPart
    {
        get
        {
            return (string)ViewState["ParentPart"];
        }
        set
        {
            ViewState["ParentPart"] = value;
        }
    }

    /// <summary>
    /// Gets or sets the source DT.
    /// </summary>
    /// <value>The source DT.</value>
    private DataTable _sourcedt = null;
    public DataTable SourceDT
    {
        get { return _sourcedt; }
        set { _sourcedt = value; }
    }


    private bool partlistvalid = true;

    // private string currentrev = null;

    #endregion

    #region Page Methods

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            BindGridView();

            //Assign values
            AssignHeaderValues();


        }
        else
        {
            //postback





        }

        //
        SettingHeadControlsByMode();
    }

    /// <summary>
    /// Assigns the header values.
    /// </summary>
    public void AssignHeaderValues()
    {
        // assign left header grid values 
        DataTable leftheaddt = CommonDB.GetPartListHeaderInfoByPackageNameAndHeaderId(PackageName, HeaderID).Tables[0];
        HeaderGridview.DataSource = leftheaddt;
        HeaderGridview.DataBind();
        if (leftheaddt.Rows.Count > 0)
        {

            //  CurrentRev = leftheaddt.Rows[0][0].ToString().Trim();
            // bind the  grid to headergridview

            //hpledit.NavigateUrl = "";

            //imgecimark.ImageUrl = "";

            //lblrevisedby.Text = leftheaddt.Rows[0]["RevInitials"].ToString();
            // ../../Eci/EciHeaderEdit.asp?Form=Parts&Mark=RevA.gif&Eci=N9JUE0001&Rev=&RevDate=&RevBy=Dayang
        }




        if (_sourcedt.Rows.Count > 0)
        {
            //lblmodule.Text = _sourcedt.Rows[0]["TFC"].ToString();
            //lbloriginal.Text = _sourcedt.Rows[0]["Original"].ToString();
            //lblCODE5.Text = _sourcedt.Rows[0]["CODE5"].ToString();

            //txtNAME1.Text = _sourcedt.Rows[0]["NAME1"].ToString();
            //txtNAME2.Text = _sourcedt.Rows[0]["NAME2"].ToString();
            //txtNAME3.Text = _sourcedt.Rows[0]["NAME3"].ToString();
            //txtNAME4.Text = _sourcedt.Rows[0]["NAME4"].ToString();
            //txtCODE1.Text = _sourcedt.Rows[0]["CODE1"].ToString();
            //txtCODE2.Text = _sourcedt.Rows[0]["CODE2"].ToString();
            //txtCODE3.Text = _sourcedt.Rows[0]["CODE3"].ToString();
            //txtCODE4.Text = _sourcedt.Rows[0]["CODE4"].ToString();


            //// Assign Header property values
            //TxtName1 = txtNAME1.Text;
            //TxtName2 = txtNAME2.Text;
            //TxtName3 = txtNAME3.Text;
            //TxtName4 = txtNAME4.Text;

            //TxtCode1 = txtCODE1.Text;
            //TxtCode2 = txtCODE2.Text;
            //TxtCode3 = txtCODE3.Text;
            //TxtCode4 = txtCODE4.Text;


        }
    }
    public void BindGridView()
    {
        //
        bool moduleflag = false;
        if (Module.Length > 4)
        {
            if (Module.Substring(4, 1) == "-")
            {
                moduleflag = true;
            }
        }
        //if (moduleflag)
        //{
        //    _sourcedt = CommonDB.GetPartListInfoByPackageNameAndHeaderId(Module, HeaderID, moduleflag).Tables[0];
        //}
        //else
        //{
        //   _sourcedt = CommonDB.GetPartListInfoByPackageNameAndHeaderId(PackageName, HeaderID, moduleflag).Tables[0];
        //}
        _sourcedt = CommonDB.GetPartListInfoTSDByPackageNameAndParentNumber(PackageName, ParentPart,true).Tables[0];


        
        // decide select index by revision value

        //ddlrevision.SelectedIndex = 0;

        Gvwpartlist.DataSource = _sourcedt;
        this.DataBind();


    }
    private void SettingHeadControlsByMode()
    {

        EnableHeadControls(false, txtNAME1);
        EnableHeadControls(false, txtNAME2);
        EnableHeadControls(false, txtNAME3);
        EnableHeadControls(false, txtNAME4);
        EnableHeadControls(false, txtCODE1);
        EnableHeadControls(false, txtCODE2);
        EnableHeadControls(false, txtCODE3);
        EnableHeadControls(false, txtCODE4);


    }

    /// <summary>
    /// Enables the head controls.
    /// </summary>
    /// <param name="flag">if set to <c>true</c> [flag].</param>
    /// <param name="txtcontrol">The txtcontrol.</param>
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

    /// <summary>
    /// Handles the Click event of the btnsubmit control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        //validate input 


        DataTable dt = CommonDB.GetPartListHeaderValidateResult(PackageName, txtCODE1.Text, txtCODE2.Text, txtCODE3.Text, txtCODE4.Text).Tables[0];
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
                    //  this.chkeditheader.Checked = false;
                    SettingHeadControlsByMode();


                    Response.Redirect("done.aspx");
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
    /// Handles the CheckedChanged event of the chkeditheader control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void chkeditheader_CheckedChanged(object sender, EventArgs e)
    {
        //SettingEditMode();
        this.lblerror.Visible = false;
        SettingHeadControlsByMode();
        SetHeaderTxtValuesFromProperties();

    }

    /// <summary>
    /// Sets the header TXT value from properties.
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

    #endregion

    #region Grid Methods

    #region Gvwpartlist Methods



    protected void Gvwpartlist_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Gvwpartlist.EditIndex = e.NewEditIndex;
        BindGridView();
    }

    #endregion

    #region HeaderGridview Methods
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
    /// Handles the RowCancelingEdit event of the Gvwpartlist control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCancelEditEventArgs"/> instance containing the event data.</param>
    protected void Gvwpartlist_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

        this.lblpartlisterror.Visible = false;
        Gvwpartlist.EditIndex = -1;
        BindGridView();
    }



    protected void Gvwpartlist_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        //hiddenfield.Value = "packagedata";



        // only update when inputs are valid 
        if (partlistvalid)
        {
            // TextBox1.Text = "A:119933";
            bool vLog = ((CheckBox)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("chkecilog") as CheckBox).Checked;
            if (vLog)
            {

                if (string.IsNullOrEmpty(hiddenfield.Value) || hiddenfield.Value.ToUpper().Trim() == "undefined".ToUpper())
                {
                    // not check radio button or  cancel buttion clicked
                    // do nothing
                    //  return;

                    EciAcid = "";
                }

                else
                {
                    EciAcid = hiddenfield.Value;


                }

            }


            // save to database

            string partlistitemid = ((Label)Gvwpartlist.Rows[e.RowIndex].FindControl("lblitemid")).Text;

            string partno = (Gvwpartlist.Rows[e.RowIndex].FindControl("txtpartno") as TextBox).Text;
            string LevelValue = (Gvwpartlist.Rows[e.RowIndex].FindControl("txtleve1") as TextBox).Text;
            string PartNameValue = (Gvwpartlist.Rows[e.RowIndex].FindControl("txtpartname") as TextBox).Text;

            string QtyValue = (Gvwpartlist.Rows[e.RowIndex].FindControl("txtQty") as TextBox).Text;
            string MaterialValue = (Gvwpartlist.Rows[e.RowIndex].FindControl("txtMaterial") as TextBox).Text;
            string SizeValue = (Gvwpartlist.Rows[e.RowIndex].FindControl("txtSize") as TextBox).Text;

            string DwgValue = (Gvwpartlist.Rows[e.RowIndex].FindControl("ddldwg") as DropDownList).SelectedItem.ToString();
            string CommentValue = (Gvwpartlist.Rows[e.RowIndex].FindControl("txtaComment") as TextBox).Text;
            //    bool vLog = false;//(Gvwpartlist.Rows[e.RowIndex].FindControl("chkecilog") as CheckBox).Checked;
            string[] arrUser = Request.ServerVariables["Auth_User"].ToString().Split(@"\".ToCharArray());
            string[] arrName = arrUser[arrUser.Length - 1].Split(@".".ToCharArray());
            string sInitials = null;

            if (arrName.Length > 1)
            {
                sInitials = arrName[0].Substring(0, 1) + arrName[1].Substring(0, 1);
            }

            // If ECI Mode, Log data


            string result = CommonDB.InsertPartListWithTransactionForEdit(partlistitemid, sInitials, EciNumber, CurrentRev, EciAcid, PackageName, KeyA, LevelValue, partno, QtyValue, PartNameValue, DwgValue, MaterialValue, SizeValue, CommentValue, vLog, EciMode);

            if (result == "Success")
            {
                Response.Redirect("PartListEditDone.aspx");
            }
            else
            {
                //error
                Response.Redirect("http://colweb01/eta/NoTrans.asp?Message=" + result);

            }








            //   Gvwpartlist.EditIndex = -1;
            //   BindGridView();

        }
    }



    /// <summary>
    /// Handles the TextChanged event of the txtpartno control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void txtpartno_TextChanged(object sender, EventArgs e)
    {



    }
    //protected void txtpartno_TextChanged_old(object sender, EventArgs e)
    //{




    //    // If user enable overwrite, the partno will be saved to database anyway
    //    CheckBox chkoverwrite = (CheckBox)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("chkoverwrite") as CheckBox;
    //    ScriptManager sm = (ScriptManager)this.Page.FindControl("ScriptManager1");


    //    if (chkoverwrite != null)
    //    {
    //        if (chkoverwrite.Checked)
    //        {
    //            this.lblpartlisterror.Visible = false;
    //            lblpartlisterror.Text = "";
    //            partlistvalid = true;
    //            sm.SetFocus(Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("txtpartname") as TextBox);
    //            return;
    //        }

    //    }



    //    //Regex re = new Regex("(^\\w{5}[-]\\w{5}[\\w-]*$)|(^\\w{4}[(]\\w{3}[)][\\w-]*$)", RegexOptions.None);
    //    //MatchCollection mc = re.Matches("text");
    //    //foreach (Match ma in mc)
    //    //{
    //    //}


    //    int validateresult = CommonTool.IsvalidPartno(((TextBox)sender).Text.Trim(), PackageName);
    //    // Set focus to next partname control
    //    if (validateresult > 0)
    //    {
    //        this.lblpartlisterror.Visible = false;
    //        lblpartlisterror.Text = "";
    //        partlistvalid = true;
    //        sm.SetFocus(Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("txtpartname") as TextBox);
    //    } // end valid partno

    //    else
    //    {
    //        // if 0:warning
    //        if (validateresult == 0)
    //        {
    //            //warning message
    //            this.lblpartlisterror.Visible = true;
    //            lblpartlisterror.Text = "Warning : The partno does not match the package";
    //            partlistvalid = true;
    //            sm.SetFocus(Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("txtpartname") as TextBox);
    //        }
    //        else
    //        {
    //            // error message
    //            this.lblpartlisterror.Visible = true;
    //            lblpartlisterror.Text = "Error : Invalid Partno";
    //            partlistvalid = false;
    //            sm.SetFocus(Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("txtpartno") as TextBox);
    //        }


    //    }


    //    // ((TextBox)sender).Focus();
    //    //this.Focus();



    //}

    #endregion



    #endregion

    #region Validation Method
    /// <summary>
    /// Customizeds the quantity validation handler.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="args">The <see cref="System.Web.UI.WebControls.ServerValidateEventArgs"/> instance containing the event data.</param>
    protected void CustomizedQuantityValidationHandlerForQuantity(object source, ServerValidateEventArgs args)
    {
        bool result = false;
        CustomValidator cv = (CustomValidator)source as CustomValidator;
        TextBox txtQty = (TextBox)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("txtQty") as TextBox;

        if (txtQty != null)
        {
            if (CommonTool.IsValidQuantity(txtQty.Text.Trim()))
            {

                result = true;
            }
            else
            {
                this.lblpartlisterror.Visible = true;
                lblpartlisterror.Text = " Error : Invalid Input(s)";
                result = false;
            }


            //int quantity = 0;

            //if (txtQty.Text.Trim().ToUpper() == "XXX")
            //{
            //    result = true;
            //}
            //else
            //{


            //    if (Int32.TryParse(txtQty.Text.Trim(), out quantity))
            //    {

            //        result = true;

            //    }

            //    else
            //    {
            //        this.lblpartlisterror.Visible = true;
            //        lblpartlisterror.Text = " Error : Invalid Input(s)";
            //        result = false;

            //    }
            //}


        }



        args.IsValid = result;
    }

    protected void CustomizedQuantityValidationHandlerForPartno(object source, ServerValidateEventArgs args)
    {
        bool result = false;
        CustomValidator cv = (CustomValidator)source as CustomValidator;



        // If user enable overwrite, the partno will be saved to database anyway
        CheckBox chkoverwrite = (CheckBox)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("chkoverwrite") as CheckBox;
        // ScriptManager sm = (ScriptManager)this.Page.FindControl("ScriptManager1");


        if (chkoverwrite != null)
        {
            if (chkoverwrite.Checked)
            {
                this.lblpartlisterror.Visible = false;

                partlistvalid = true;
                //sm.SetFocus(Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("txtpartname") as TextBox);
                result = true;

            }

            else
            {
                TextBox txtpartno = (TextBox)Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("txtpartno") as TextBox;
                int validateresult = (int)CommonTool.IsvalidPartnoWithEnu(txtpartno.Text.Trim(), PackageName, 0);

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
                        partlistvalid = true;
                        result = true;
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


                //if (validateresult > 0)
                //{
                //    this.lblpartlisterror.Visible = false;
                //    lblpartlisterror.Text = "";
                //    partlistvalid = true;
                //    result = true;
                //} // end valid partno
                //else
                //{
                //    // if 0:warning
                //    if (validateresult == 0)
                //    {
                //        //warning message
                //        this.lblpartlisterror.Visible = true;
                //        lblpartlisterror.Text = "Warning : The partno does not match the package";
                //        partlistvalid = true;
                //      //  sm.SetFocus(Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("txtpartname") as TextBox);
                //        result = true;
                //    }
                //    else
                //    {
                //        // error message
                //        this.lblpartlisterror.Visible = true;
                //        lblpartlisterror.Text = "Error : Invalid Input(s)";
                //        partlistvalid = false;
                //      //  sm.SetFocus(Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("txtpartno") as TextBox);
                //        result = false;
                //    }

                //}


            }

        }



        args.IsValid = result;
    }


    #endregion
    //protected void Gvwpartlist_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    //if (e.Row.RowType == DataControlRowType.Header)
    //    //{
    //    //    if (this.EciMode == "on")
    //    //    {
    //    //        e.Row.Cells[1].Visible = true;
    //    //    }
    //    //    else
    //    //    {
    //    //        e.Row.Cells[1].Visible = false;
    //    //    }

    //    //}

    //    return;

    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        TextBox txtpartno = e.Row.FindControl("txtpartno") as TextBox;
    //        if (txtpartno != null)
    //        {
    //            txtpartno.Attributes.Add("onfocus", "try{document.getElementById('__LASTFOCUS').value=this.id} catch(e) {};");
    //        }


    //        CheckBox chkecilog = e.Row.FindControl("chkecilog") as CheckBox;

    //        if (chkecilog != null)
    //        {
    //            if (this.EciMode == "on")
    //            {
    //                chkecilog.Visible = true;

    //            }
    //            else
    //            {
    //                chkecilog.Visible = false;
    //            }

    //        }

    //        foreach (Control c in e.Row.Cells[0].Controls)
    //        {
    //            try
    //            {
    //                LinkButton lb = (LinkButton)c;
    //                if (lb.Text == "Update")



    //                    lb.Attributes.Add("onClick", "javascript:InvokePop('" + this.hiddenfield.ClientID + "','" + chkecilog.ClientID + "','" + PackageName + "','" + EciNumber + "');");
    //            }
    //            catch (Exception e1)
    //            {


    //            }

    //        }
    //    }


    //}
    protected void ddlpartlist_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}