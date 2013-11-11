using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class EngineeringTools_FormC_FormCDataAdd : System.Web.UI.Page
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

    public bool _validateflag = true;

    public bool _validateresult = true;

    public string _btnsubmitclientid = null;

    #endregion

    #region Page Methods
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {


        //  string sss = CommonTool.GetFormattedPageCodeUpdate("12    345");       
        if (!Page.IsPostBack)
        {
            PackageName = Request.QueryString["Package"];

            Module = Request.QueryString["Module"];
            if (string.IsNullOrEmpty(PackageName))
            {
                //  PackageName = "N9ju";
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

            BindNewGridView();


            //create datatable for newGvwpartlist


            //bind newGvwpartlist

            //bind two dropdownlist when editing newGvwpartlist

            //edit,cancel, update


            this.DataBind();





        }





    }
    /// <summary>
    /// Binds the new grid view.
    /// </summary>
    private void BindNewGridView()
    {
        if (NewDataTable == null)
        {

            NewDataTable = MakeDataTable(PackageName);
        }

        NewGvwpartlist.DataSource = NewDataTable;
    }
    /// <summary>
    /// Makes the data table.
    /// </summary>
    /// <returns></returns>
    private DataTable MakeDataTable(string package)
    {

        DataTable table = new DataTable();

        table.Columns.Add("rowid", typeof(int));
        table.Columns.Add("ecilog", typeof(string));
        table.Columns.Add("rev", typeof(string));
       
        table.Columns.Add("assyname", typeof(string));
        table.Columns.Add("key", typeof(string));
        table.Columns.Add("Aitemid", typeof(string));
        table.Columns.Add("assycode", typeof(string));
        table.Columns.Add("treatment", typeof(string));
        table.Columns.Add("partcode", typeof(string));
        table.Columns.Add("pagecode", typeof(string));
        table.Columns.Add("description", typeof(string));
        table.Columns.Add("partlistlocation", typeof(string));
        table.Columns.Add("citemid", typeof(string));
        table.Columns.Add("pcitemid", typeof(string));

        string currentrev = string.Empty;

        DataTable dt = CommonDB.GetCurrentRevByPackage(package).Tables[0];
        if (dt.Rows.Count > 0)
        {
            currentrev = dt.Rows[0][0].ToString();
        }

        else
        {
            currentrev = "A";
        }

        table.Rows.Add(0, "False", currentrev, null, null, null, null, null, null, null, null, null, null, null);
        table.Rows.Add(1, "False", currentrev, null, null, null, null, null, null, null, null, null, null, null);
        table.Rows.Add(2, "False", currentrev, null, null, null, null, null, null, null, null, null, null, null);
        table.Rows.Add(3, "False", currentrev, null, null, null, null, null, null, null, null, null, null, null);
        table.Rows.Add(4, "False", currentrev, null, null, null, null, null, null, null, null, null, null, null);
        table.Rows.Add(5, "False", currentrev, null, null, null, null, null, null, null, null, null, null, null);
        table.Rows.Add(6, "False", currentrev, null, null, null, null, null, null, null, null, null, null, null);
        table.Rows.Add(7, "False", currentrev, null, null, null, null, null, null, null, null, null, null, null);
        table.Rows.Add(8, "False", currentrev, null, null, null, null, null, null, null, null, null, null, null);
        table.Rows.Add(9, "False", currentrev, null, null, null, null, null, null, null, null, null, null, null);
        //table.Rows.Add(null, "A", null, null, null, null, null, null, null, null);
        //table.Rows.Add(null, "A", null, null, null, null, null, null, null, null);
        //table.Rows.Add(null, "A", null, null, null, null, null, null, null, null);
        //table.Rows.Add(null, "A", null, null, null, null, null, null, null, null);

        return table;


    }
    /// <summary>
    /// Binds the first gridview.
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

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DataRowView dview = (DataRowView)e.Row.DataItem as DataRowView;
            // May Change Later
            if (!string.IsNullOrEmpty(dview.Row["Rev"].ToString().Trim()))
            {

                string[] imagearray = dview.Row["Rev"].ToString().Trim().Split(' ');
                foreach (string s in imagearray)
                {
                    Image dimage = new Image();
                    dimage.ImageUrl = string.Format("http://colweb01/eta/images/Rev{0}.gif", s);
                    e.Row.Cells[1].Controls.AddAt(0, dimage);
                }
            }

        }

    }

    #endregion

    #region NewGvwpartlist Methods
    /// <summary>
    /// Handles the RowDataBound event of the NewGvwpartlist control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void NewGvwpartlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // this.btnsubmit.Attributes.Add("onClick", "javascript:InvokePop('" + this.TextBox1.ClientID + "','" + PackageName + "','" + EciNumber + "');");

            //foreach (Control c in e.Row.Cells[0].Controls)
            //{
            //    try
            //    {
            //        Button  lb = (Button)c;
            //      Button sdy=  e.Row.Cells[0].FindControl("btnsubmit") as Button;
            //        if (lb.Text == "Submit")
            //        {
            //            lb.Attributes.Add("onClick", "javascript:InvokePop('" + this.hiddenfield.ClientID + "','" + PackageName + "','" + EciNumber + "');");


            //        }
            //    }
            //    catch (Exception e1)
            //    {

            //    }

            //}


            Button btnsubmit = e.Row.Cells[0].FindControl("btnsubmit") as Button;


            if (btnsubmit != null)
            {
                if (!string.IsNullOrEmpty(NewDataTable.Rows[0][3].ToString()) || !string.IsNullOrEmpty(NewDataTable.Rows[1][3].ToString()))
                {

                    btnsubmit.Attributes.Add("onClick", "javascript:InvokePop('" + this.hiddenfield.ClientID + "','" + PackageName + "','" + EciNumber + "');");
                    btnsubmit.Enabled = true;
                }

                else
                {
                    btnsubmit.Enabled = false;
                }


            }

        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkecilog = (CheckBox)e.Row.FindControl("chkecilog") as CheckBox;
            if (chkecilog != null)
            {
                Label lblecilog = e.Row.FindControl("lblecilog") as Label;


                if (lblecilog != null)
                {
                    // chkecilog.Enabled = Convert.ToBoolean(lblecilog.Text);

                    chkecilog.Checked = Convert.ToBoolean(lblecilog.Text);
                }

            }


            Image imgrev = (Image)e.Row.FindControl("imgrev") as Image;
            if (imgrev != null)
            {

                DataRowView dview = (DataRowView)e.Row.DataItem as DataRowView;


                if (!string.IsNullOrEmpty(dview.Row["Rev"].ToString().Trim()))
                {
                    imgrev.Visible = true;
                    imgrev.ImageUrl = string.Format("http://colweb01/eta/images/Rev{0}.gif", dview.Row["Rev"].ToString().Trim());
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
                ddlkey.DataValueField = "Aitemid";
                ddlkey.DataBind();
                Label lblkey = e.Row.FindControl("lblkey") as Label;
                if (lblkey != null)
                {
                    ddlkey.SelectedIndex = CommonTool.SelectDropDownListIndexByValue(ddlkey, lblkey.Text);

                    //   ddlkey.SelectedIndex = ddlkey.Items.IndexOf(ddlkey.Items.FindByValue(lblkey.Text));
                }


            }

            DropDownList ddltreatment = (DropDownList)e.Row.FindControl("ddltreatment") as DropDownList;
            if (ddltreatment != null)
            {
                Label lbltreatment = e.Row.FindControl("lbltreatment") as Label;
                if (lbltreatment != null)
                {
                    ddltreatment.SelectedIndex = CommonTool.SelectDropDownListIndexByText(ddltreatment, lbltreatment.Text.Trim());
                }
            }

        }


    }
    /// <summary>
    /// Handles the RowEditing event of the NewGvwpartlist control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
    protected void NewGvwpartlist_RowEditing(object sender, GridViewEditEventArgs e)
    {
        NewGvwpartlist.EditIndex = e.NewEditIndex;
        // NewDataTable.Rows[NewGvwpartlist.EditIndex]["ecilog"] = "True";
        BindNewGridView();
        NewGvwpartlist.DataBind();



    }
    /// <summary>
    /// Handles the RowCancelingEdit event of the NewGvwpartlist control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCancelEditEventArgs"/> instance containing the event data.</param>
    protected void NewGvwpartlist_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //  this.lblpartlisterror.Visible = false;
        this.hiddenfield.Value = "";

        NewGvwpartlist.EditIndex = -1;
        BindNewGridView();
        NewGvwpartlist.DataBind();
    }
    /// <summary>
    /// Handles the RowUpdating event of the NewGvwpartlist control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewUpdateEventArgs"/> instance containing the event data.</param>
    protected void NewGvwpartlist_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        // update datatable


        NewDataTable.Rows[NewGvwpartlist.EditIndex]["ecilog"] = ((CheckBox)NewGvwpartlist.Rows[NewGvwpartlist.EditIndex].FindControl("chkecilog") as CheckBox).Checked.ToString();



        NewDataTable.Rows[NewGvwpartlist.EditIndex]["assyname"] = ((DropDownList)NewGvwpartlist.Rows[NewGvwpartlist.EditIndex].FindControl("ddlassyname") as DropDownList).SelectedItem.ToString();

        NewDataTable.Rows[NewGvwpartlist.EditIndex]["key"] = ((DropDownList)NewGvwpartlist.Rows[NewGvwpartlist.EditIndex].FindControl("ddlkey") as DropDownList).SelectedItem.Text;//

        NewDataTable.Rows[NewGvwpartlist.EditIndex]["Aitemid"] = ((DropDownList)NewGvwpartlist.Rows[NewGvwpartlist.EditIndex].FindControl("ddlkey") as DropDownList).SelectedValue;//

        NewDataTable.Rows[NewGvwpartlist.EditIndex]["assycode"] = GetTextBoxValue(NewGvwpartlist.EditIndex, "txtcode");
        NewDataTable.Rows[NewGvwpartlist.EditIndex]["treatment"] = ((DropDownList)NewGvwpartlist.Rows[NewGvwpartlist.EditIndex].FindControl("ddltreatment") as DropDownList).SelectedItem.ToString().Trim();

        NewDataTable.Rows[NewGvwpartlist.EditIndex]["partcode"] = GetTextBoxValue(NewGvwpartlist.EditIndex, "txtpartcode");
        // add by dayang on 03/12/2013
        NewDataTable.Rows[NewGvwpartlist.EditIndex]["pagecode"] = GetTextBoxValue(NewGvwpartlist.EditIndex, "txtpagecode").Replace(" ","");
        NewDataTable.Rows[NewGvwpartlist.EditIndex]["description"] = GetTextBoxValue(NewGvwpartlist.EditIndex, "txtdesc");
        // add by dayang on 01/09/2013
       // NewDataTable.Rows[NewGvwpartlist.EditIndex]["pcitemid"] = GetTextBoxValue(NewGvwpartlist.EditIndex, "txtpair");







        // Gridview DataBind


        NewGvwpartlist.EditIndex = -1;
        NewGvwpartlist.DataSource = NewDataTable;
        NewGvwpartlist.DataBind();




        return;


        //  hiddenfield.Value = "A:119933";
        bool vLog = ((CheckBox)NewGvwpartlist.Rows[NewGvwpartlist.EditIndex].FindControl("chkecilog") as CheckBox).Checked;

        if (vLog)
        {
            //logging

            if (string.IsNullOrEmpty(hiddenfield.Value))// || hiddenfield.Value.ToUpper().Trim() == "undefined".ToUpper())
            {
                // not check radio button or  cancel buttion clicked
                // do nothing
                hiddenfield.Visible = true;
                return;

            }

            else
            {
                hiddenfield.Visible = false;
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

        string[] arrUser = Request.ServerVariables["Auth_User"].ToString().Split(@"\".ToCharArray());
        string[] arrName = arrUser[arrUser.Length - 1].Split(@".".ToCharArray());
        string sInitials = null;
        if (arrName.Length > 1)
        {
            sInitials = arrName[0].Substring(0, 1) + arrName[1].Substring(0, 1);
        }

        string vRev = "A";
        string vCategory = ((DropDownList)NewGvwpartlist.Rows[NewGvwpartlist.EditIndex].FindControl("ddlassyname") as DropDownList).SelectedItem.ToString().Trim();
        string vKey = ((DropDownList)NewGvwpartlist.Rows[NewGvwpartlist.EditIndex].FindControl("ddlkey") as DropDownList).SelectedValue;//ddlassyname
        //txtcode
        string vAssyCode = GetTextBoxValue(NewGvwpartlist.EditIndex, "txtcode");

        string vTreatment = ((DropDownList)NewGvwpartlist.Rows[NewGvwpartlist.EditIndex].FindControl("ddltreatment") as DropDownList).SelectedItem.ToString().Trim();

        string vPartCode = GetTextBoxValue(NewGvwpartlist.EditIndex, "txtpartcode");

        string vPageCode = CommonTool.GetFormattedPageCodeUpdate(GetTextBoxValue(NewGvwpartlist.EditIndex, "txtpagecode"));

        string vDescription = GetTextBoxValue(NewGvwpartlist.EditIndex, "txtdesc");




        string result = CommonDB.InsertNewFormCWithTransactionForInsert(EciNumber, sInitials, PackageName, vLog, vRev, vCategory, vKey, vAssyCode, vTreatment, vPartCode, vPageCode, vDescription, EciAcid, AitemId);

        if (result == "Success")
        {
            this.hiddenfield.Value = "";
            Response.Redirect("FormcDataAddDone.aspx");
        }
        else
        {
            //error
            this.hiddenfield.Value = "";
            Response.Redirect("http://colweb01/eta/NoTrans.asp?Message=" + result);

        }




    }
    /// <summary>
    /// Enables the submit button.
    /// </summary>
    /// <param name="flag">if set to <c>true</c> [flag].</param>
    private void EnableSubmitButton(bool flag)
    {
        Button btnsubmit = this.FindControl("btnsubmit") as Button;

        if (btnsubmit != null)
        {
            btnsubmit.Enabled = flag;
        }
    }
    /// <summary>
    /// Handles the Click event of the btnsubmit control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        //  hiddenfield.Value = "A:119933";      

        // Get Vlog value from NewTable
        bool vLog = false;
        foreach (DataRow row in NewDataTable.Rows)
        {

            if (row["ecilog"].ToString().Equals("True"))
            {

                vLog = true;
                break;
            }

        }


        if (vLog)
        {
            //logging

            if (string.IsNullOrEmpty(hiddenfield.Value) || hiddenfield.Value.ToUpper().Trim() == "undefined".ToUpper())
            {
                // not check radio button or  cancel buttion clicked
                // do nothing
                hiddenfield.Visible = true;
                return;

            }

            else
            {
                hiddenfield.Visible = false;
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
        string[] arrUser = Request.ServerVariables["Auth_User"].ToString().Split(@"\".ToCharArray());
        string[] arrName = arrUser[arrUser.Length - 1].Split(@".".ToCharArray());
        string sInitials = null;
        if (arrName.Length > 1)
        {
            sInitials = arrName[0].Substring(0, 1) + arrName[1].Substring(0, 1);
        }

        string vRev = "A";

        //string result = CommonDB.InsertNewFormCWithTransactionForInsert(EciNumber, sInitials, PackageName, vLog, vRev, vCategory, vKey, vAssyCode, vTreatment, vPartCode, vPageCode, vDescription, EciAcid, AitemId);

        string result = CommonDB.InsertNewFormCWithTransactionForMutipleInserts(EciNumber, sInitials, PackageName, vLog, vRev, NewDataTable, EciAcid, AitemId);

        if (result == "Success")
        {
            this.hiddenfield.Value = "";
            Response.Redirect("FormcDataAddDone.aspx");
        }
        else
        {
            //error
            this.hiddenfield.Value = "";
            Response.Redirect("http://colweb01/eta/NoTrans.asp?Message=" + result);

        }


    }
    /// <summary>
    /// Gets the text box value.
    /// </summary>
    /// <param name="editindex">The editindex.</param>
    /// <param name="controlname">The controlname.</param>
    /// <returns></returns>
    private string GetTextBoxValue(int editindex, string controlname)
    {
        TextBox txtbox = (TextBox)NewGvwpartlist.Rows[editindex].FindControl(controlname) as TextBox;
        string result = "";

        if (!string.IsNullOrEmpty(txtbox.Text))
        {
            result = txtbox.Text.Trim();
        }

        return result;
    }
    #endregion

    #region Validation Method
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pairid"></param>
    /// <returns></returns>
    protected bool IsValidPairId(string pairid)
    {
        bool result = false;

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

    /// <summary>
    /// Handles the ServerValidate event of the CustomValidator1 control.
    /// </summary>
    /// <param name="source">The source of the event.</param>
    /// <param name="args">The <see cref="System.Web.UI.WebControls.ServerValidateEventArgs"/> instance containing the event data.</param>

    protected void PageCodeValidatingHandler(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source as CustomValidator;
        string stringresult = string.Empty;
        if (_validateflag)
        {
            // Validate Pair
            // add by dayang on 01/08/2013 for configurator validation
            //string forcpcitemid = GetTextBoxValue(this.NewGvwpartlist.EditIndex, "txtpair");
            ////DataTable dt = this.Gvwpartlist.DataSource as DataTable;
            ////var rows = dt.Select(string.Format("CItemId='{0}'", forcpcitemid));
            //// Label ttt = Gvwpartlist.Rows[0].FindControl("lblid") as Label;
            //// string id = ttt.Text;
            ////  bool isvalidpair = IsValidPairId(forcpcitemid);

            //// validate this formcitemid
            //// go through current gvwpartlist to find this item
            //// this.Gvwpartlist.Rows.Count
            //if (!IsValidPairId(forcpcitemid))
            //{
            //    args.IsValid = false;
            //    cv.ErrorMessage = CommonTool.FormCPairIDErrorMessage;
            //    _validateflag = false;
            //    _validateresult = args.IsValid;
            //    return;
            //}
            // Validate Page Code

            if (StandardPartcodeDataTable == null)
            {
                StandardPartcodeDataTable = CommonDB.GetStandardPartcodeDataTableFromStandPartsList().Tables[0];
            }
            DropDownList ddlassyname = (DropDownList)NewGvwpartlist.Rows[NewGvwpartlist.EditIndex].FindControl("ddlassyname") as DropDownList;

            TextBox txtpagecode = (TextBox)NewGvwpartlist.Rows[NewGvwpartlist.EditIndex].FindControl("txtpagecode") as TextBox;
            string pagecode = CommonTool.GetFormattedPageCodeUpdate(txtpagecode.Text.Trim())?? "";

            TextBox txtcode = (TextBox)NewGvwpartlist.Rows[NewGvwpartlist.EditIndex].FindControl("txtcode") as TextBox;

            // get partcode 
            TextBox txtpartcode = (TextBox)NewGvwpartlist.Rows[NewGvwpartlist.EditIndex].FindControl("txtpartcode") as TextBox;
            string partcode = txtpartcode.Text.Trim();

            if ((!string.IsNullOrEmpty(txtcode.Text.Trim())) || CommonTool.IsFormCPageCode99(pagecode) )
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
                     //   string[] codearray = pagecode.Split("".ToCharArray());

                        if (CommonTool.IsvalidRefModulePartnumbers(partcode, pagecode.Substring(0,2), pagecode.Substring(2,2), assyname, ref stringresult))
                        {
                            args.IsValid = true;
                        }
                        else
                        {
                            cv.ErrorMessage = CommonTool.FormCRefErrorMessage + " Invalid PN: " + stringresult;
                            args.IsValid = false;
                        }
                    }
                }
            }
            
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

        //    Button btnsubmit = Page.FindControl("btnsubmit") as Button;

        //}
        //else
        //{
        //    Button btnsubmit = (Button)NewGvwpartlist.Rows[0].Cells[0].FindControl("btnsubmit") as Button;

        //}
        //return;



    }


    #endregion




    //protected void NewGvwpartlist_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    //protected void NewGvwpartlist_SelectedIndexChanged1(object sender, EventArgs e)
    //{

    //}
}