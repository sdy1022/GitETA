using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data ;
using System.IO;



public partial class EngineeringTools_PartsList_EtagridView_PartListInsert : System.Web.UI.Page
{
   // protected string module = null;
   // protected string ecinumber = null;
   // protected string ecimode = null;
    protected string keya = null;
  //  protected string eciacid = null;

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
    
    protected void Page_Load(object sender, EventArgs e)
    {
        // add atrributes to buttonsubmit\


        //

        if (!Page.IsPostBack)
        {
           

            // check package status
            Module = Request.QueryString["Module"];
            if (string.IsNullOrEmpty(Module))
            {
               // Module = "N9ju-2";
            }

            PackageName = Module.Substring(0, 4);

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
                            this.lblecimode.Text = "EciMode=on";
                        }

                    }

                }
            }
            DataTable dtkeya = CommonDB.GetKeyAByEciNumber(EciNumber).Tables[0];


            if (dtkeya.Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(dtkeya.Rows[0][0].ToString().Trim()))
                {
                    keya = "A";
                }
                else
                {

                    keya = CommonTool.Chr(CommonTool.Asc(dtkeya.Rows[0][0].ToString().Trim()) + 1);
                }
            }

            else
            {
                keya = "A";
            }
           


            this.lblmodule.Text = Module;

          

            BindGridData();


            // underneath insert table
            SetControlsStatus();
        }

        //this.btnsubmit.Attributes.Add("OnClientClick", strScript);
     //   this.btnsubmit.Attributes.Add("onclick", strScript);
       // string str = "return confirm('Are you sure you want to delete this user');";

         //this.btnsubmit.Attributes.Add("onClick", "javascript:InvokePop('" + this.TextBox1.ClientID + "','" + PackageName + "','" + EciNumber + "');");

        this.btnsubmit.Attributes.Add("onClick", "javascript:InvokePop('" + this.hiddenfield.ClientID + "','" + PackageName + "','" + EciNumber + "');");
     
       


    }

    /// <summary>
    /// Binds the grid data.
    /// </summary>
    private void BindGridData()
    {

        this.myGridView.DataSource = CommonDB.GetPartListInfoByModuleName(Module).Tables[0];
        this.myGridView.DataBind();
    }
   
    protected void myGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
          

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strScript = "uncheckOthers(" + ((CheckBox)e.Row.Cells[0].FindControl("chkselect")).ClientID + ");";
                ((CheckBox)e.Row.Cells[0].FindControl("chkselect")).Attributes.Add("onclick", strScript);
            }
        }
        catch 
        {
            //report error
        }        

    }
  

    protected void myGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        


    }
    protected void chkselect_CheckedChanged(object sender, EventArgs e)
    {
       // myGridView.SelectedRow;
       //ETAGridViewInsert temp= this.myGridView.Rows[0].Cells[2].Controls[1] as ETAGridViewInsert;
        for (int i = 0; i < myGridView.Rows.Count; i++)
        {
            GridViewRow row = myGridView.Rows[i];
            CheckBox chk = row.FindControl("chkselect") as CheckBox;
            if (chk != null)
            {
                ETAGridViewInsert temp = this.myGridView.Rows[i].Cells[1].Controls[1] as ETAGridViewInsert;
                if (chk.Checked)
                {
                  
                    temp.EnableGridView = true;
                    // uncheck all radio buttion

                }
                else
                {
                    temp.EnableGridView = false;
                    //uncheck all radio button
                   
                }
            }

        }
    }
    protected void chk1_CheckedChanged(object sender, EventArgs e)
    {
        GenergicCheckBoxChangedHandler(sender);
    }
    protected void txtpartno1_TextChanged(object sender, EventArgs e)
    {
        GenergicTextChangedHandler(sender);

    }
    private void GenergicTextChangedHandler(object sender)
    {
        TextBox txt = ((TextBox)sender);
        string id = txt.ClientID.Substring(txt.ClientID.Length - 1, 1);

        int result = (int) CommonTool.IsvalidPartnoWithEnu(txt.Text.Trim(), PackageName,0);

        Label lbl = Page.FindControl("lblpartlisterror" + id) as Label;
        ScriptManager sm = (ScriptManager)this.Page.FindControl("ScriptManager1");
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
                sm.SetFocus(this.FindControl("txtpartno" + id) as TextBox);
                break;
            case -3:
                lbl.Visible = true;
                lbl.Text = "Invalid Value With Valid Format";
                sm.SetFocus(this.FindControl("txtpartno" + id) as TextBox);
                break;

            case -2:
                lbl.Visible = true;
                lbl.Text = "Resurve Error";
                sm.SetFocus(this.FindControl("txtpartno" + id) as TextBox);
                break;
            case -1:

                lbl.Visible = true;
                lbl.Text = "Format Error";
                //btnsumbit.Enabled = false;
                sm.SetFocus(this.FindControl("txtpartno" + id) as TextBox);
                break;
            case 0:
                lbl.Visible = true;
                lbl.Text = "Warning:No Match";
                sm.SetFocus(this.FindControl("txtpartname" + id) as TextBox);
                break;
            case 1:
                lbl.Visible = false;
                lbl.Text = null;
                sm.SetFocus(this.FindControl("txtpartname" + id) as TextBox);
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

            CheckBox chkecilog = Page.FindControl("chkecilog" + i) as CheckBox;
            chkecilog.Enabled = chk.Checked;

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
        bool isinputvalid = CheckInputsValid();
        if (!isinputvalid)
        {
            return;
        }


    //   TextBox1.Text = "8053";
        if (string.IsNullOrEmpty(hiddenfield.Value) || hiddenfield.Value.ToUpper().Trim() == "undefined".ToUpper())
        //if ( string.IsNullOrEmpty(TextBox1.Text)|| TextBox1.Text.ToUpper().Trim() == "undefined".ToUpper())
        {
            // not check radio button or  cancel buttion clicked
            // do nothing
            EciAcid = "";
        }
        else
        {
            // select successfully
            //  EciAcid = TextBox1.Text;
            EciAcid = hiddenfield.Value;
        }
            // get selected itemid of radio button
            //loop through all grids
            string itemid=GetSelectedItemid();

            DataTable dt=CommonDB.GetInfoFromPartsListItems(itemid).Tables[0];
            //'**Get Line number of item to insert around
            if (dt.Rows.Count > 0)
            {
                int insertline = Int32.Parse(dt.Rows[0][0].ToString());
                string headerid = dt.Rows[0][1].ToString();
                string partlistitemmodule = dt.Rows[0][2].ToString();



                int insertstart = insertline;
                bool isbefore = false;
                if (rdolist.SelectedIndex == 0)
                {
                    //before
                    // change by dayang on 12/16/09 for ITG

                    //insertstart = insertline - 9; 
                   // insertstart = insertline - 10; 

                    isbefore = true;
                }
                else
                {
                    //after
                   // insertstart = insertline + 1;
                    isbefore = false;
                }
                //C:\Inetpub\wwwroot\TIEM\test\eta\EngineeringTools

                string TrapFile = null;//string.Format("{0}{1}.txt",@"C:\Inetpub\wwwroot\TIEM\ETA\EngineeringTools\PartsList\",PackageName);

                //TrapFile = string.Format("{0}{1}.txt", @"C:\deploy4\", PackageName);

                string TrappedError = null;//string.Format("{0}{1}.txt", @"C:\Inetpub\wwwroot\TIEM\ETA\EngineeringTools\PartsList\asperror\", PackageName);

                //try
                //{

                //    //'**********   Start Trap   **********
                //    CommonTool.WriteToFile(TrapFile, string.Format("{0}TrapFile={1}", System.Environment.NewLine, TrapFile), true);

                //    CommonTool.WriteToFile(TrapFile, string.Format("{0}TrappedError={1}", System.Environment.NewLine, TrappedError), true);

                //    CommonTool.WriteToFile(TrapFile, string.Format("{0}ItemId={1}", System.Environment.NewLine, itemid), true);

                //    CommonTool.WriteToFile(TrapFile, string.Format("{0}EciMode={1}", System.Environment.NewLine, EciMode), true);

                //    CommonTool.WriteToFile(TrapFile, string.Format("{0}EciAcId={1}", System.Environment.NewLine, EciAcid), true);

                //    CommonTool.WriteToFile(TrapFile, string.Format("{0}Package={1}", System.Environment.NewLine, PackageName), true);

                //    CommonTool.WriteToFile(TrapFile, string.Format("{0}sSql=SELECT LineNumber,HeaderId,DesignNumber FROM ETA.dbo.PartsListItems WHERE ItemId={1}", System.Environment.NewLine, itemid), true);


                //    CommonTool.WriteToFile(TrapFile, string.Format("{0}HeaderId={1}", System.Environment.NewLine, headerid), true);

                //    CommonTool.WriteToFile(TrapFile, string.Format("{0}Module={1}", System.Environment.NewLine, partlistitemmodule), true);

                //    CommonTool.WriteToFile(TrapFile, string.Format("{0}InsertStart={1}", System.Environment.NewLine, insertstart), true);

                //}
                //catch (Exception err)
                //{
                //    Response.Redirect("http://colweb01/eta/NoTrans.asp?Message=" + err.Message);
                    
                //}
                string[] arrUser = Request.ServerVariables["Auth_User"].ToString().Split(@"\".ToCharArray());
                string[] arrName = arrUser[arrUser.Length - 1].Split(@".".ToCharArray());
                string sInitials = null;
                if (arrName.Length > 1)
                {
                    sInitials = arrName[0].Substring(0, 1) + arrName[1].Substring(0, 1);
                }


                string result2 = null;
                string result = CommonDB.InsertNewPartListWithTransactionForInsert(ref result2, PackageName, EciNumber, headerid, sInitials, insertstart, MakeDataTable(), TrapFile, partlistitemmodule, EciMode, EciAcid, TrappedError,isbefore );


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
                     Response.Redirect("http://colweb01/eta/NoTrans.asp?Message=" + result );
                    
                }


            }// end dt.rows.count>0
            //BEFORE : rbl1.selectindex=0
            //After : rbl.selectindex=1
        
        

        
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
        // Create a new DataTable.
        System.Data.DataTable table = new DataTable();
        // Declare variables for DataColumn and DataRow objects.
        DataColumn column;
        

        // Create new DataColumn, set DataType, 
        // ColumnName and add to DataTable.  

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Boolean");
        column.ColumnName = "ecilog";
        table.Columns.Add(column);

        

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "level";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "partno";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "partname";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "qty";
        table.Columns.Add(column);


        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "material";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "size";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "dwg";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "comments";
        table.Columns.Add(column);

        for (int i = 1; i <= 6; i++)
        {
            CheckBox chk = Page.FindControl("chk" + i) as CheckBox;
            if (chk.Checked)
            {

                CheckBox chkecilog = Page.FindControl("chkecilog" + i) as CheckBox;
            

                DropDownList ddllevel = Page.FindControl("ddllevel" + i) as DropDownList;

                TextBox txtpartno = Page.FindControl("txtpartno" + i) as TextBox;
                //txtpartname2
                TextBox txtpartname = Page.FindControl("txtpartname" + i) as TextBox;

                //txtqty2
                TextBox txtqty = Page.FindControl("txtqty" + i) as TextBox;

                //txtmaterial1
                TextBox txtmaterial = Page.FindControl("txtmaterial" + i) as TextBox;
                //txtsize1
                TextBox txtsize = Page.FindControl("txtsize" + i) as TextBox;
                //ddldwg1
                DropDownList ddldwg = Page.FindControl("ddldwg" + i) as DropDownList;
                //txtcomment1
                TextBox txtcomment = Page.FindControl("txtcomment" + i) as TextBox;
                table.Rows.Add(chkecilog.Checked , ddllevel.SelectedItem.Text, txtpartno.Text, txtpartname.Text, txtqty.Text, txtmaterial.Text, txtsize.Text, ddldwg.SelectedItem.Text, txtcomment.Text);
            }

        }

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
        foreach (GridViewRow gvrow in myGridView.Rows)
        {
            if (continueloopflag)
            {

                ETAGridViewInsert etagridviewinsert = (ETAGridViewInsert)gvrow.FindControl("ETAGridView1");

                //  etagridviewinsert.


                foreach (GridViewRow gvrow2 in etagridviewinsert.GvdView.Rows)
                {

                    RadioButton rdo = (RadioButton)gvrow2.FindControl("rdoselect");
                    if (rdo.Checked)
                    {
                        Label lbl = (Label)gvrow2.FindControl("lblitemid");
                        itemid = lbl.Text.Trim();
                        continueloopflag = false;
                        break;

                    }

                }
            }

            else
            {
                break;
            }

        }// end first for each

        return itemid;
    }

    /// <summary>
    /// Genergics the check box changed handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    private void GenergicCheckBoxChangedHandler(object sender)
    {
        CheckBox chk = ((CheckBox)sender);
        string chkid = chk.ClientID;
        string i = chkid.Substring(chkid.Length - 1);


        CheckBox chkecilog = Page.FindControl("chkecilog" + i) as CheckBox;
        chkecilog.Enabled = chk.Checked;

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
        //
        Label lbl = Page.FindControl("lblpartlisterror" + i) as Label;
        lbl.Visible = chk.Checked;

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //Server.Transfer("default2.asp");
        string TrapFile = string.Format("{0}{1}.txt", System.IO.Directory.CreateDirectory(Server.MapPath("log") ), PackageName);

        StreamWriter sw = new StreamWriter("c:\\N9JU.txt");

        CommonTool.WriteToFile(TrapFile, System.Environment.NewLine+ "aaa", false);
        //
        CommonTool.WriteToFile(TrapFile, System.Environment.NewLine+"bbb", true);
      

        Response.Redirect("default2.asp");
      
    }



    protected void Button1_Click1(object sender, EventArgs e)
    {
        //Server.Transfer("default2.asp");
        string TrapFile = string.Format("{0}{1}.txt", @"c:\sdy\", PackageName);
//
  //      StreamWriter sw = new StreamWriter("c:\\sdy\\N9JU.txt");

        CommonTool.WriteToFile(TrapFile, System.Environment.NewLine + "aaa", false);
        //
        CommonTool.WriteToFile(TrapFile, System.Environment.NewLine + "bbb", true);
    }
    //protected void CustomizedQuantityValidationHandlerForQuantity(object source, ServerValidateEventArgs args)
    //{
    //    CustomValidator cv = (CustomValidator)source as CustomValidator;

    //    TextBox txtQty = (TextBox)this.FindControl(cv.ControlToValidate) as TextBox;

    //    if (txtQty != null)
    //    {

    //        args.IsValid = CommonTool.IsValidQuantity(txtQty.Text.Trim());

    //    }
    //    else
    //    {
    //        args.IsValid = true;
    //    }
           
       

    //}
}
