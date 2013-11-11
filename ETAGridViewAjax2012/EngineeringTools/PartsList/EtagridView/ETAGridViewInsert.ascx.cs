using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class ETAGridViewInsert : System.Web.UI.UserControl
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

    public GridView GvdView
    {
        get { return Gvwpartlist;}
    }

    public bool EnableGridView
    {
        
        set
        {
            ViewState["EnableGridView"] = value;
            //foreach (GridViewRow gvr in Gvwpartlist.Rows)
            //{
            //    RadioButton rdo = gvr.FindControl("rboselect") as RadioButton;
            //    rdo.Checked = true;
            //}
            this.Gvwpartlist.Enabled = value;
           


          
    
        }
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
    private void AssignHeaderValues()
    {
        // assign left header grid values 
        DataTable leftheaddt = CommonDB.GetPartListHeaderInfoByPackageNameAndHeaderId(PackageName, HeaderID).Tables[0];
        HeaderGridview.DataSource = leftheaddt;
        HeaderGridview.DataBind();
        if (leftheaddt.Rows.Count > 0)
        {

            CurrentRev = leftheaddt.Rows[0][0].ToString().Trim();
            // bind the  grid to headergridview

            //hpledit.NavigateUrl = "";

            //imgecimark.ImageUrl = "";

            //lblrevisedby.Text = leftheaddt.Rows[0]["RevInitials"].ToString();
            // ../../Eci/EciHeaderEdit.asp?Form=Parts&Mark=RevA.gif&Eci=N9JUE0001&Rev=&RevDate=&RevBy=Dayang
        }

       


        if (_sourcedt.Rows.Count > 0)
        {
            lblmodule.Text = _sourcedt.Rows[0]["Designnumber"].ToString();
            lbloriginal.Text = _sourcedt.Rows[0]["Original"].ToString();
            lblCODE5.Text = _sourcedt.Rows[0]["CODE5"].ToString();

            txtNAME1.Text = _sourcedt.Rows[0]["NAME1"].ToString();
            txtNAME2.Text = _sourcedt.Rows[0]["NAME2"].ToString();
            txtNAME3.Text = _sourcedt.Rows[0]["NAME3"].ToString();
            txtNAME4.Text = _sourcedt.Rows[0]["NAME4"].ToString();
            txtCODE1.Text = _sourcedt.Rows[0]["CODE1"].ToString();
            txtCODE2.Text = _sourcedt.Rows[0]["CODE2"].ToString();
            txtCODE3.Text = _sourcedt.Rows[0]["CODE3"].ToString();
            txtCODE4.Text = _sourcedt.Rows[0]["CODE4"].ToString();
           

            // Assign Header property values
            TxtName1 = txtNAME1.Text;
            TxtName2 = txtNAME2.Text;
            TxtName3 = txtNAME3.Text;
            TxtName4 = txtNAME4.Text;

            TxtCode1 = txtCODE1.Text;
            TxtCode2 = txtCODE2.Text;
            TxtCode3 = txtCODE3.Text;
            TxtCode4 = txtCODE4.Text;


        }
    }
    private void BindGridView()
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
        if (moduleflag)
        {
            _sourcedt = CommonDB.GetPartListInfoByPackageNameAndHeaderId(Module, HeaderID, moduleflag).Tables[0];
        }
        else
        {
            _sourcedt = CommonDB.GetPartListInfoByPackageNameAndHeaderId(PackageName, HeaderID, moduleflag).Tables[0];
        }
        Gvwpartlist.DataSource = _sourcedt;
        Gvwpartlist.DataBind();


    }
    private void SettingHeadControlsByMode()
    {
        if (chkeditheader.Checked)
        {
            EnableHeadControls(true, txtNAME1);
            EnableHeadControls(true, txtNAME2);
            EnableHeadControls(true, txtNAME3);
            EnableHeadControls(true, txtNAME4);
            EnableHeadControls(true, txtCODE1);
            EnableHeadControls(true, txtCODE2);
            EnableHeadControls(true, txtCODE3);
            EnableHeadControls(true, txtCODE4);
            this.btnsubmit.Visible = true;

        }
        else
        {
            EnableHeadControls(false, txtNAME1);
            EnableHeadControls(false, txtNAME2);
            EnableHeadControls(false, txtNAME3);
            EnableHeadControls(false, txtNAME4);
            EnableHeadControls(false, txtCODE1);
            EnableHeadControls(false, txtCODE2);
            EnableHeadControls(false, txtCODE3);
            EnableHeadControls(false, txtCODE4);
            this.btnsubmit.Visible = false;
          
        }
       
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
                    this.chkeditheader.Checked = false;
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

                        hpledit.NavigateUrl = string.Format("http://colweb01/eta/Eci/EciHeaderEdit.asp?Form=Parts&Mark=Rev{0}.gif&Eci={1}&Rev={2}&RevDate={3}&RevBy={4}", dview.Row[0].ToString().Trim(), dview.Row[2].ToString().Trim(), dview.Row[1].ToString().Trim(), dview.Row[3].ToString().Trim(), dview.Row[4].ToString().Trim());
                        hpledit.Text = "Edit";

                    }


                    imgrev.ImageUrl = string.Format("http://colweb01/eta/images/Rev{0}.gif", dview.Row[0].ToString().Trim());

                    hpleci.NavigateUrl = string.Format("http://colweb01/eta/ECI/eci.asp?Eci={0}",EciNumber);
                    hpleci.Text =EciNumber;
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
      


      

        /// <summary>
        /// Isvalids the partno.
        /// </summary>
        /// <param name="partno">The partno.</param>
        /// <returns></returns>
        //protected int IsvalidPartno(string partno)
        //{

        //    int result = 1;
        //   // partno = "UN107-21320-71";

        //    string[] partarray = partno.Split('-');
        //    //Step 1 : split the input to array , check length >0 means at least xxx-xxx

        //    // first step to make sure the number has xxx-xxx
        //    if (partarray.Length > 1)
        //    {
        //        // Step 2:: check the first and two items are 5
        //        if ((partarray[0].Length == 5) && (partarray[1].Length == 5))
        //        {
        //            //Step 3 : if second item start with "UN, UM , 9N, 9M", compare with package number and lead to warning
        //            if (partarray[0].Substring(0, 2).ToUpper() == "UN" || partarray[0].Substring(0, 2).ToUpper() == "UM" || partarray[0].Substring(0, 2).ToUpper() == "9N" || partarray[0].Substring(0, 2).ToUpper() == "9M")
        //            {
        //                //compare with package number . last three
        //                if (partarray[0].Substring(2, 3).ToUpper() == PackageName.Substring(PackageName.Length - 3, 3))
        //                {
        //                    result = 1;
        //                }
        //                else
        //                {
        //                    result = 0;
                            
        //                }

        //            }// not UN
        //            else
        //            {
        //                // ' standpard partno
        //                // Validate all the input header info
        //                //"SELECT  top 1 *  FROM ETA.dbo.viewStandardParts WHERE Partno like '" & Replace(partnumber,"-","") & "%'" 
        //                // check whether it is in database 
        //                //if yes
        //                DataTable dt = CommonDB.GetPartNumberCount(partno).Tables[0];

        //                if (dt.Rows.Count > 0)
        //                {
        //                    result = 1;
        //                }
        //                else
        //                {
        //                    result = -1;
        //                }

        //            }
        //        }
        //        else
        //        {
        //            result = -1;
                   
        //        }

        //    } // end length>2
        //    else
        //    {
        //        result = -1;
             

        //    }

        //    return result;

        //}

        /// <summary>
        /// Handles the TextChanged event of the txtpartno control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void txtpartno_TextChanged(object sender, EventArgs e)
        {
            int validateresult = (int) CommonTool.IsvalidPartnoWithEnu(((TextBox)sender).Text.Trim(),PackageName,0);
            ScriptManager sm = (ScriptManager)this.Page.FindControl("ScriptManager1");
            if (validateresult > 0)
            {
                               

                this.lblpartlisterror.Visible = false;
                lblpartlisterror.Text = "";
                partlistvalid = true;
                sm.SetFocus(Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("txtpartname") as TextBox);
           
            } // end valid partno

            else
            {
                // if 0:warning
                if (validateresult == 0)
                {
                    //warning message
                    this.lblpartlisterror.Visible = true;
                    lblpartlisterror.Text = "Warning : The partno does not match the package";
                    partlistvalid = true;
                    sm.SetFocus(Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("txtpartname") as TextBox);
                }
                else
                {
                    // error message
                    this.lblpartlisterror.Visible = true;
                    lblpartlisterror.Text = "Error : Invalid Partno";
                    partlistvalid = false;
                    sm.SetFocus(Gvwpartlist.Rows[Gvwpartlist.EditIndex].FindControl("txtpartno") as TextBox);
                }

               
            }

            
        }

        #endregion
    #endregion


     
        protected void Gvwpartlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gvwpartlist.SelectedRow;
     //       Message.Text = "You selected " + row.Cells[2].Text + ".";

        }
        protected void Gvwpartlist_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = Gvwpartlist.Rows[e.NewSelectedIndex];
            if (row.Cells[1].Text == "ANATR")
            {

                e.Cancel = true;
                //Message.Text = "You cannot select " + row.Cells[2].Text + ".";

            }
        }
        protected void Gvwpartlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

               

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                   // e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#99cc00'; this.style.color='buttontext';this.style.cursor='default';");

                   // e.Row.Attributes.Add("onclick", "ChangeRowColor(" + e.Row.ClientID + ");");


                   string rowID = Gvwpartlist.ClientID+"row" + e.Row.RowIndex;

                   e.Row.Attributes.Add("id", Gvwpartlist.ClientID + "row" + e.Row.RowIndex);

                    e.Row.Attributes.Add("onclick", "ChangeRowColor(" + "'" + rowID + "'" + ")");
                    string strScript = "uncheckOthersradio(" + ((CheckBox)e.Row.Cells[0].FindControl("rdoselect")).ClientID + ");";
                    ((CheckBox)e.Row.Cells[0].FindControl("rdoselect")).Attributes.Add("onclick", strScript);
                }
            }
            catch (Exception Ex)
            {
                //report error
            }        

        }
        protected void Gvwpartlist_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
}