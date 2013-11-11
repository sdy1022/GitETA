using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using System.Configuration;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.VisualBasic;

public partial class EngineeringTools_PartsList_PartListNewTSD : System.Web.UI.Page
{
    #region Property

    public bool _quantyvalidateflag = true;
    public bool _quantyvalidateresult = true;
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
    public string GridViewSelectRowOrderValue
    {
        get
        {
            return (string)ViewState["GridViewSelectRowOrderValue"];
        }
        set
        {
            ViewState["GridViewSelectRowOrderValue"] = value;
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

    public DataTable ECIFilteredGridViewDataTable
    {
        get
        {
            return (DataTable)ViewState["ECIFilteredGridViewDataTable"];
        }
        set
        {
            ViewState["ECIFilteredGridViewDataTable"] = value;
        }
    }

    public string PartList
    {
        get
        {
            return (string)ViewState["PartList"];
        }
        set
        {
            ViewState["PartList"] = value;
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
    /// Gets or sets the eci number.
    /// </summary>
    /// <value>The eci number.</value>
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


            //  DataTable CategoryDataTable = CommonTool.GetCategory();//CommonDB.GetCategory().Tables[0];

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

    public string ErrorMessage
    {
        get
        {
            return (string)ViewState["ErrorMessage"];
        }
        set
        {
            ViewState["ErrorMessage"] = value;
            if (string.IsNullOrEmpty(value))
            {
                this.lblpartlisterror.Visible = false;

            }
            else
            {

                this.lblpartlisterror.Visible = true;
                this.lblpartlisterror.Text = value;

            }

        }
    }
    #endregion

    #region Page Methods


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {


            // check package status
            PartList = Request.QueryString["PartList"];

            if (PartList == null)
            {

                return;
            }
            PartList = PartList.Replace(" ", "");


            PackageName = Request.QueryString["Package"];


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

#if (Release)

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
                            this.lblecimode.Text = "EciMode=on";
                        }

                    }

                }
            }
#endif


            this.lblmodule.Text = PartList;

            // Bind Header View



            // Bind  Header Name2 Name2 Name4 with current CurrentParentPart 
            AssignTxtNameValues();


            dt = CommonDB.GetPartListInfoTSDByPackageName(PackageName, PartList).Tables[0];


            // Assign HeaderID

            if (dt.Rows.Count == 0)
            {
                return;
            }
            HeaderID = dt.Rows[0]["HeaderID"].ToString();

            DataTable leftheaddt = CommonDB.GetPartListHeaderInfoByPackageNameAndHeaderId(PackageName, dt.Rows[0]["HeaderID"].ToString()).Tables[0];
            HeaderGridview.DataSource = leftheaddt;

            //  List<string> finalrows= rows.ToList();
            DataTable dteci = CommonDB.GetPartListInfoTSDByPackageNameAndParentNumber(PackageName, PartList, false).Tables[0];
            // string currenteci = null;
            if (dteci.Rows.Count > 0)
            {

                CurrentECI = dteci.Rows[0]["FinalResult"].ToString();
                //this.ddleci.DataSource = dteci;

                //ddleci.DataTextField = "FinalResult";
                //ddleci.DataValueField = "FinalResult";
            }




            BindGridData();


            // underneath insert table
            SetControlsStatus();
        }


        // Hide Eci pop log 
        //this.btnsubmit.Attributes.Add("onClick", "javascript:InvokePop('" + this.hiddenfield.ClientID + "','" + PackageName + "','" + EciNumber + "');");




    }



    private void AssignTxtNameValues()
    {
        DataTable dt;
        dt = CommonDB.GetTSDHeaderNamesInfo(PackageName, TxtCode1, TxtCode2, TxtCode3, TxtCode4).Tables[0];

        if (dt.Rows.Count > 0)
        {
            TxtName2 = dt.Rows[0]["NAME2"].ToString();
            TxtName3 = dt.Rows[0]["NAME3"].ToString();
            TxtName4 = dt.Rows[0]["NAME4"].ToString();
        }
    }


    /// <summary>
    /// Binds the grid data.
    /// </summary>
    private void BindGridData()
    {

        //this.myGridView.DataSource = CommonDB.GetPartListInfoByModuleName(PartList).Tables[0];
        this.DataBind();
    }


    protected void chk1_CheckedChanged(object sender, EventArgs e)
    {
        GenergicCheckBoxChangedHandler(sender);
    }
    protected void txtpartno1_TextChanged(object sender, EventArgs e)
    {
        GenergicTextChangedHandler(sender);

    }
    /// <summary>
    /// Changed on 11/01/2013 to accommondate new rule of level
    /// </summary>
    /// <param name="sender"></param>
    private void GenergicTextChangedHandler(object sender)
    {
        TextBox txt = ((TextBox)sender);
        string id = txt.ClientID.Substring(txt.ClientID.Length - 1, 1);
        string partno = txt.Text.Trim();
        int result = (int)CommonTool.IsvalidTSDPartnoWithEnu(partno, PackageName, 0);

        Label lbl = Page.FindControl("lblpartlisterror" + id) as Label;
        //ScriptManager sm = (ScriptManager)this.Page.FindControl("ScriptManager1");
        //switch (result)
        //{
        //    case -1:
        //        lbl.Visible = true;
        //        lbl.Text = "Error";
        //        //btnsumbit.Enabled = false;
        //        sm.SetFocus(this.FindControl("txtpartno" + id) as TextBox);
        //        break;
        //    case 0:
        //        lbl.Visible = true;
        //        lbl.Text = "Warning:No Match";
        //        sm.SetFocus(this.FindControl("txtpartname" + id) as TextBox);
        //        break;
        //    case 1:
        //        lbl.Visible = false;
        //        lbl.Text = null;
        //        sm.SetFocus(this.FindControl("txtpartname" + id) as TextBox);
        //        break;
        //    default:
        //        break;
        //}

        switch (result)
        {
            case -4:
                lbl.Visible = true;
                lbl.Text = "Exception Error";
                //  sm.SetFocus(this.FindControl("txtpartno" + id) as TextBox);
                break;
            case -3:
                lbl.Visible = true;
                lbl.Text = "Invalid Value With Valid Format";
                //   sm.SetFocus(this.FindControl("txtpartno" + id) as TextBox);
                break;

            case -2:
                lbl.Visible = true;
                lbl.Text = "Resurve Error";
                ///      sm.SetFocus(this.FindControl("txtpartno" + id) as TextBox);
                break;
            case -1:

                lbl.Visible = true;
                lbl.Text = "Format Error";
                //btnsumbit.Enabled = false;
                //    sm.SetFocus(this.FindControl("txtpartno" + id) as TextBox);
                break;
            case 0:
                lbl.Visible = true;
                lbl.Text = "Warning:No Match";
                //     sm.SetFocus(this.FindControl("txtpartname" + id) as TextBox);
                break;
            case 1: // normal
                lbl.Visible = false;
                lbl.Text = null;


                // sm.SetFocus(this.FindControl("txtpartname" + id) as TextBox);
                // Assign other control value in same row
                DataTable dtpartno = CommonDB.GetTSDAll05ResultByPartno(partno).Tables[0];
                if (dtpartno.Rows.Count > 0)
                {
                    (this.FindControl("txtpartname" + id) as TextBox).Text = dtpartno.Rows[0]["PARTSNAME"].ToString();
                    (this.FindControl("txtminor" + id) as TextBox).Text = dtpartno.Rows[0]["MAINOR"].ToString();
                    (this.FindControl("txtmaterial" + id) as TextBox).Text = dtpartno.Rows[0]["MATERIAL1"].ToString();
                    (this.FindControl("txtsize" + id) as TextBox).Text = dtpartno.Rows[0]["MATERIAL2"].ToString();

                    //(this.FindControl("txtminor" + id) as TextBox).Text = dtpartno.Rows[0]["DRW"].ToString();
                    //ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(ddlvalue));
                    DropDownList ddl = (this.FindControl("ddldwg" + id) as DropDownList);
                    ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(dtpartno.Rows[0]["DRW"].ToString()));

                    (this.FindControl("txtfrompn" + id) as TextBox).Text = dtpartno.Rows[0]["FROM_ECI"].ToString();
                    (this.FindControl("txtopn" + id) as TextBox).Text = dtpartno.Rows[0]["TO_ECI"].ToString();
                }

                break;
            default:
                break;
        }

    }

    private void SetControlsStatus()
    {
        for (int i = 1; i <= 6; i++)
        {
            CheckBox chk = Page.FindControl("chk" + i) as CheckBox;
            chk.AutoPostBack = true;
            // chk.CheckedChanged += new EventHandler(chk_CheckedChanged);

            //CheckBox chkecilog = Page.FindControl("chkecilog" + i) as CheckBox;
            //chkecilog.Enabled = chk.Checked;

            DropDownList ddllevel = Page.FindControl("ddllevel" + i) as DropDownList;
            ddllevel.Enabled = chk.Checked;

            TextBox txtpartno = Page.FindControl("txtpartno" + i) as TextBox;
            txtpartno.Enabled = chk.Checked;
            txtpartno.AutoPostBack = true;

            //txtpartname2
            TextBox txtpartname = Page.FindControl("txtpartname" + i) as TextBox;
            txtpartname.Enabled = chk.Checked;

            //txtqty2
            TextBox txtqty = Page.FindControl("txtqty" + i) as TextBox;
            txtqty.Enabled = chk.Checked;

            //txtmaterial1
            TextBox txtmaterial = Page.FindControl("txtmaterial" + i) as TextBox;
            txtmaterial.Enabled = chk.Checked;

            //txtsize1
            TextBox txtsize = Page.FindControl("txtsize" + i) as TextBox;
            txtsize.Enabled = chk.Checked;

            //ddldwg1
            DropDownList ddldwg = Page.FindControl("ddldwg" + i) as DropDownList;
            ddldwg.Enabled = chk.Checked;

            //txtcomment1
            TextBox txtcomment = Page.FindControl("txtcomment" + i) as TextBox;
            txtcomment.Enabled = chk.Checked;
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        //if (string.IsNullOrEmpty(hiddenfield.Value) || hiddenfield.Value.ToUpper().Trim() == "undefined".ToUpper())
        //{
        //    return;
        //}
        // check all inputs valid or not
        if (!IsValid) return;

        bool isinputvalid = CheckInputsValid();
        if (!isinputvalid)
        {
            lblpartlisterror.Text = "Invalid Inputs. Please Make The Change";
            lblpartlisterror.Visible = true;
            return;
        }


        DataTable dtmaketable = MakeDataTable();

        if (dtmaketable == null)
        {
            lblpartlisterror.Text = "Cannot find Parent Parts for new items.  Ensure Level for each item is correct.";
            lblpartlisterror.Visible = true;

            return;
        }


        string result2 = null;

        // Get inputs of header three textbox inputs

        string txtname2 = this.txtNAME2.Text;
        string txtname3 = this.txtNAME3.Text;
        string txtname4 = this.txtNAME4.Text;
        //string txtcode1=



        string result = CommonDB.InsertTSDAttachPartListWithTransactionForInsert(ref result2, dtmaketable,PackageName,TxtCode1,TxtCode2,TxtCode3,TxtCode4,txtname2,txtname3,txtname4);


        if (result2 != null)
        {
            lblresult.Text = result2;
        }

        if (result == "Success")
        {
            Response.Redirect("PartListInsertDone.aspx");
        }
        else
        {
            //error
            Response.Redirect("http://colweb01/eta/NoTrans.asp?Message=" + result);

        }





    }

    private bool CheckInputsValid()
    {
        bool result = true;
        bool haschecked = false;
        for (int i = 1; i <= 6; i++)
        {
            CheckBox chk = Page.FindControl("chk" + i) as CheckBox;
            if (chk.Checked)
            {
                haschecked = true;
                // check error label 
                Label lbl = Page.FindControl("lblpartlisterror" + i) as Label;
                if (lbl.Text.Contains("Error"))
                {

                    result = false;
                    break;
                }
                else
                {
                    ;

                }


            }




        } // end for
        //return false if result=false or result=true, 

        return result & haschecked;
    }
    private DataTable MakeDataTable()
    {
        //[TFC],[INDEX_PARENTPART],[PARENT_CHILDPART],[QTY],[COMMENT1],[FROM_ECI],[FROM_DATE],[TO_DATE])
        // Create a new DataTable.
        System.Data.DataTable table = new DataTable();
        // Declare variables for DataColumn and DataRow objects.
        DataColumn column;


        // Create new DataColumn, set DataType, 
        // ColumnName and add to DataTable.  

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "tfc";
        table.Columns.Add(column);


        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "parentpartno";
        table.Columns.Add(column);


        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "partno";
        table.Columns.Add(column);




        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "qty";
        table.Columns.Add(column);



        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "comments";
        table.Columns.Add(column);


        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "fromeci";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "fromdate";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "todate";
        table.Columns.Add(column);

               
      
        for (int i = 1; i <= 6; i++)
        {
            CheckBox chk = Page.FindControl("chk" + i) as CheckBox;
            if (chk.Checked)
            {
                string partentpart = null;
                //       CheckBox chkecilog = Page.FindControl("chkecilog" + i) as CheckBox;


                DropDownList ddllevel = Page.FindControl("ddllevel" + i) as DropDownList;
                TextBox txtpartno = Page.FindControl("txtpartno" + i) as TextBox;
                int currentlevel = Convert.ToInt16(ddllevel.SelectedValue);
                if (currentlevel == 1)
                {

                    partentpart = string.Format("{0} {1}{2}", TxtCode1, TxtCode2, TxtCode3);
                }
                else
                {
                    // current level is more than 1 ; search six table first ; if not , go to  gridviewtable to find one
                    // bool isfound = false;
                    int leveltosearch = currentlevel - 1;
                    // while (!isfound)

                    // search first six row table
                    for (int k = i - 1; k >= 1; k--)
                    {
                        DropDownList ddlleveltosearach = Page.FindControl("ddllevel" + k) as DropDownList;
                        if (ddlleveltosearach.SelectedValue == leveltosearch.ToString())
                        {

                            partentpart = (Page.FindControl("txtpartno" + k) as TextBox).Text;

                        }
                    }

                    //selectedordervaluelbl
                    //  string selectordervalue = selectedordervaluelbl.Text;
                    // search gridview table

                    //DataTable filterdt = ECIFilteredGridViewDataTable.Select("ordervalue<='" + selectordervalue + "'", "ordervalue DESC").CopyToDataTable();
                    ////DataView dv=new DataView(filterdt);
                    ////dv.Sort = "";
                    //foreach (DataRow dr in filterdt.Rows)
                    //{

                    //    if (dr["TreeLevel"].ToString() == leveltosearch.ToString())
                    //    {

                    //        partentpart = dr["PARENT_CHILDPART"].ToString();
                    //        break;

                    //    }


                    //}
                    // Error Happens , return null
                    if (partentpart == null)
                    {
                        return null;
                    }

                    //leveltosearch--;

                    //if(leveltosearch==1)
                    //{
                    //    partentpart = string.Format("{0} {1}{2}", TxtCode1, TxtCode2, TxtCode3);
                    //    break;
                    //}




                }

                //[TFC],[INDEX_PARENTPART],[PARENT_CHILDPART],[QTY],[COMMENT1],[FROM_ECI],[FROM_DATE],[TO_DATE])

                //txtpartname2
                // TextBox txtpartname = Page.FindControl("txtpartname" + i) as TextBox;

                //txtqty2
                TextBox txtqty = Page.FindControl("txtqty" + i) as TextBox;

                //txtmaterial1
                //     TextBox txtfrompn = Page.FindControl("txtfrompn" + i) as TextBox;
                //txtsize1
                TextBox txtfromeci = Page.FindControl("txtfromv" + i) as TextBox;

                //   TextBox txtopn = Page.FindControl("txtopn" + i) as TextBox;

                //ddldwg1

                //txtcomment1
                TextBox txtcomment = Page.FindControl("txtcomment" + i) as TextBox;
                //table.Rows.Add(chkecilog.Checked, ddllevel.SelectedItem.Text, txtpartno.Text, txtqty.Text,  txtcomment.Text);
                table.Rows.Add(PackageName, partentpart, txtpartno.Text, txtqty.Text, txtcomment.Text, txtfromeci.Text, DateTime.Now.ToString(), '0');
            }

        }

        ////selectedordervaluelbl
        //DataRow[] drs = table.Select("ordervalue<='" + selectedordervaluelbl.Text + "'");
        ////if(drs.Length>1)
        ////{


        ////}

        ////else
        ////{


        ////}
        //return drs.CopyToDataTable();
        return table;

    }




    /// <summary>
    /// Gets the selected itemid.
    /// </summary>
    /// <returns></returns>
    private string GetSelectedItemid()
    {
        string itemid = null;
        bool continueloopflag = true;
        //foreach (GridViewRow gvrow in myGridView.Rows)
        //{
        //    if (continueloopflag)
        //    {

        //        ETAGridViewInsert etagridviewinsert = (ETAGridViewInsert)gvrow.FindControl("ETAGridView1");

        //        //  etagridviewinsert.


        //        foreach (GridViewRow gvrow2 in etagridviewinsert.GvdView.Rows)
        //        {

        //            RadioButton rdo = (RadioButton)gvrow2.FindControl("rdoselect");
        //            if (rdo.Checked)
        //            {
        //                Label lbl = (Label)gvrow2.FindControl("lblitemid");
        //                itemid = lbl.Text.Trim();
        //                continueloopflag = false;
        //                break;

        //            }

        //        }
        //    }

        //    else
        //    {
        //        break;
        //    }

        //}// end first for each

        return itemid;
    }

    /// <summary>
    /// Genergics the check box changed handler. Changed on 11/01/2013
    /// </summary>
    /// <param name="sender">The sender.</param>
    private void GenergicCheckBoxChangedHandler(object sender)
    {
        CheckBox chk = ((CheckBox)sender);



        string chkid = chk.ClientID;
        int i = Convert.ToInt16(chkid.Substring(chkid.Length - 1));

        // based on current id, decide if this box is eligible to check 
        for (int k = 1; k < Convert.ToInt16(i); k++)
        {

            CheckBox chkecilog = Page.FindControl("chk" + k) as CheckBox;

            if (chkecilog == null || chkecilog.Checked == false)
            {
                chk.Checked = false;
                return;
            }

        }
        // if it is valid , decide all valid level values of current dropdown list

        // if first row, get the selected radio value
        int maxrowlevel = 0;

        if (i == 1)
        {
            //Label gridselectlevellabel = selectedrow.FindControl("lbllevel") as Label;
            //string gslevel = gridselectlevellabel.Text;
            maxrowlevel = 1;
        }
        else
        {
            // find top last row 
            DropDownList ddllastlevel = this.FindControl("ddllevel" + (i - 1).ToString()) as DropDownList;
            maxrowlevel = Convert.ToInt16(ddllastlevel.SelectedItem.Text) + 1;

        }





        //CheckBox chkecilog = Page.FindControl("chkecilog" + i) as CheckBox;
        //chkecilog.Enabled = chk.Checked;

        DropDownList ddllevel = Page.FindControl("ddllevel" + i) as DropDownList;

        ddllevel.Items.Clear();

        for (int j = 1; j <= maxrowlevel; j++)
        {

            ddllevel.Items.Add(new ListItem(j.ToString(), j.ToString()));
        }


        ddllevel.Enabled = chk.Checked;

        TextBox txtpartno = Page.FindControl("txtpartno" + i) as TextBox;
        txtpartno.Enabled = chk.Checked;
        txtpartno.AutoPostBack = true;
        //txtpartname2

        //TextBox txtpartname = Page.FindControl("txtpartname" + i) as TextBox;
        //txtpartname.Enabled = chk.Checked;

        //txtqty2
        TextBox txtqty = Page.FindControl("txtqty" + i) as TextBox;
        txtqty.Enabled = chk.Checked;

        ////txtmaterial1
        //TextBox txtmaterial = Page.FindControl("txtmaterial" + i) as TextBox;
        //txtmaterial.Enabled = chk.Checked;
        ////txtsize1
        //TextBox txtsize = Page.FindControl("txtsize" + i) as TextBox;
        //txtsize.Enabled = chk.Checked;
        ////ddldwg1
        //DropDownList ddldwg = Page.FindControl("ddldwg" + i) as DropDownList;
        //ddldwg.Enabled = chk.Checked;
        //txtcomment1
        TextBox txtcomment = Page.FindControl("txtcomment" + i) as TextBox;
        txtcomment.Enabled = chk.Checked;
        //
        Label lbl = Page.FindControl("lblpartlisterror" + i) as Label;
        lbl.Visible = chk.Checked;


        //txtfrompn
        //TextBox txtfrompn = Page.FindControl("txtfrompn" + i) as TextBox;
        //txtfrompn.Enabled = chk.Checked;

        //txtfromv
        TextBox txtfromv = Page.FindControl("txtfromv" + i) as TextBox;
        txtfromv.Enabled = chk.Checked;

        //txtopn
        //TextBox txtopn = Page.FindControl("txtopn" + i) as TextBox;
        //txtopn.Enabled = chk.Checked;




    }




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


    #endregion

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
            // Get current row index

            string txtname = ((CustomValidator)source).ControlToValidate;
            //   int rowindex = Convert.ToInt16(txtname.Substring(txtname.Length - 2, 1));

            TextBox txtQty = (TextBox)this.FindControl(txtname) as TextBox;

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
}