using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using System.Configuration;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.VisualBasic; 

public partial class EngineeringTools_PartsList_PartListNew : System.Web.UI.Page
{
    

    protected string module = null;
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
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
            // check package status
           // module = "N9ju-2";
             module = Request.QueryString["Module"];
            
            if (string.IsNullOrEmpty(module))
            {
              //  module = "N9ju-2";
            }
        //    module = "N9ju-1";
            PackageName = module.Substring(0, 4);

            DataTable dt = CommonDB.GetPackageLockStatusInfo(PackageName).Tables[0];
            //if (dt.Rows.Count == 0)
            //{

            //    Response.Redirect("http://colweb01/eta/locked.asp?Package=" + PackageName);
            //}

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dt.Rows[0][0]) == true)
                {
                    //**If package is locked, redirect
                    Response.Redirect("http://colweb01/eta/locked.asp?Package=" + PackageName);
                }
            }
            // set package value
            this.lblpackage.Text = PackageName;

            // bind dropdownlist of Citem

            dt = CommonDB.GetCitemInfo(PackageName, module).Tables[0];

            if (dt.Rows.Count > 0)
            {

                ddlcitem.DataSource = dt;
                this.DataBind();              
               

            }

            SetControlsStatus();

        
        }
    }

    protected void btnsumbit_Click(object sender, EventArgs e)
    {
        // check all inputs valid or not
        bool isinputvalid= CheckInputsValid();


        if (isinputvalid)
        {
            // save to database
            //Check  Citemid 
            DataTable dt = CommonDB.GetFormCItemstatusInfo(this.ddlcitem.SelectedValue).Tables[0];
            if (dt.Rows.Count == 0)
            {
                lblerror.Visible = true;


            }
            else
            {
                //Insert Header and get ID
                string module=dt.Rows[0][0].ToString();
                string result = CommonDB.InsertNewPartListWithTransactionForNew(module, txtMajor.Text.Trim(), txtMinor.Text.Trim(),txtName3.Text.Trim(),txtAssyCode.Text.Trim(), txtPageCode1.Text.Trim(), txtPageCode2.Text.Trim(),  ddlcitem.SelectedValue,MakeDataTable());
                if (!result.Equals("Success"))
                {
                    //'Direct user to error notification
                    Response.Redirect("http://colweb01/eta/NoTrans.asp?Message=" + result);
                }
                else
                {
                    Response.Redirect("PartListAddDone.aspx");
                }
                



            }
        }



        



            

       
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

        for (int i = 1; i <= 13; i++)
        {
            CheckBox chk = Page.FindControl("chk" + i) as CheckBox;
            if (chk.Checked)
            {


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
                table.Rows.Add(ddllevel.SelectedItem.Text, txtpartno.Text, txtpartname.Text, txtqty.Text, txtmaterial.Text, txtsize.Text, ddldwg.SelectedItem.Text, txtcomment.Text);
            }
            
        }

        return table;
        
    }
 


    private  bool CheckInputsValid()
    {
        bool noerrorresult = true;
        bool checkresult = false;
        for (int i = 1; i <= 13; i++)
        {
            CheckBox chk = Page.FindControl("chk" + i) as CheckBox;
            if (chk.Checked)
            {
                checkresult = true;
                // check error label 
                Label lbl = Page.FindControl("lblpartlisterror" + i) as Label;
                if (lbl.Text.Contains("Error"))
                {
                    noerrorresult = false;
                    break;
                }

               


            }
        }

        if (!checkresult)
        {
            return checkresult;


        }
           
        
               
           

         // end for
        return noerrorresult;
    }
    protected void ddlcitem_SelectedIndexChanged(object sender, EventArgs e)
    {

        string text = ddlcitem.SelectedItem.Text;
        string[] array=text.Split(@"|".ToCharArray());
        this.txtMajor.Text = array[0].Substring(4);
        this.txtAssyCode.Text = array[0].Substring(0, 3);
        this.txtPageCode1.Text = array[2].Replace(" ", "");
        this.txtPageCode2.Text = array[4].Replace(" ", "");


    }

    protected void txtpartno1_TextChanged(object sender, EventArgs e)
    {
        GenergicTextChangedHandler(sender,1);
       
       
    }

    /// <summary>
    /// Genergics the text changed handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    private void GenergicTextChangedHandler(object sender,int idlength)
    {
        TextBox txt = ((TextBox)sender);
        string id = txt.ClientID.Substring(txt.ClientID.Length - idlength);

        int result =(int) CommonTool.IsvalidPartnoWithEnu(txt.Text.Trim(), PackageName,0);

        Label lbl = Page.FindControl("lblpartlisterror" + id) as Label;
        // Set focus to next partname control
        ScriptManager sm = (ScriptManager)this.Page.FindControl("ScriptManager1");
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


    /// <summary>
    /// Genergics the check box changed handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    private void GenergicCheckBoxChangedHandler(object sender, int idlength)
    {
        CheckBox chk = ((CheckBox)sender);
        string chkid = chk.ClientID;
        string i = chkid.Substring(chkid.Length - idlength);


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
        //TextBox txtcomment = Page.FindControl("txtcomment" + i) as TextBox;
        //txtcomment.Enabled = chk.Checked;
        //
        Label lbl = Page.FindControl("lblpartlisterror" + i) as Label;
        lbl.Visible = chk.Checked;
        
    }

    private void SetControlsStatus()
    {
        for (int i = 1; i <= 13; i++)
        {
            CheckBox chk = Page.FindControl("chk" + i) as CheckBox;
            chk.AutoPostBack = true;
           // chk.CheckedChanged += new EventHandler(chk_CheckedChanged);

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
            //TextBox txtcomment = Page.FindControl("txtcomment" + i) as TextBox;
            //txtcomment.Enabled = chk.Checked;
        }
    } 
   
    protected void chk1_CheckedChanged(object sender, EventArgs e)
    {
        GenergicCheckBoxChangedHandler(sender,1);
        //TextBox txtcomment = Page.FindControl("txtcomment" + 14) as TextBox;
        //txtcomment.Enabled = true;
    }
    protected void chk2_CheckedChanged1(object sender, EventArgs e)
    {
        GenergicCheckBoxChangedHandler(sender, 1);
    }
    protected void chk3_CheckedChanged1(object sender, EventArgs e)
    {
        GenergicCheckBoxChangedHandler(sender,1);
    }
    protected void chk4_CheckedChanged(object sender, EventArgs e)
    {
        GenergicCheckBoxChangedHandler(sender,1);
    }
    protected void chk5_CheckedChanged(object sender, EventArgs e)
    {
        GenergicCheckBoxChangedHandler(sender,1);
    }
    protected void chk6_CheckedChanged(object sender, EventArgs e)
    {
        GenergicCheckBoxChangedHandler(sender,1);
    }
    protected void chk7_CheckedChanged(object sender, EventArgs e)
    {
        GenergicCheckBoxChangedHandler(sender,1);
    }
    protected void chk8_CheckedChanged(object sender, EventArgs e)
    {
        GenergicCheckBoxChangedHandler(sender,1);
    }
    protected void chk9_CheckedChanged(object sender, EventArgs e)
    {
        GenergicCheckBoxChangedHandler(sender,1);
    }
    protected void chk10_CheckedChanged(object sender, EventArgs e)
    {
        GenergicCheckBoxChangedHandler(sender,2);
    }
    protected void chk11_CheckedChanged(object sender, EventArgs e)
    {
        GenergicCheckBoxChangedHandler(sender,2);
    }
    protected void chk12_CheckedChanged(object sender, EventArgs e)
    {
        GenergicCheckBoxChangedHandler(sender,2);
    }
    protected void chk13_CheckedChanged(object sender, EventArgs e)
    {
        GenergicCheckBoxChangedHandler(sender,2);
    }
    protected void txtpartno2_TextChanged(object sender, EventArgs e)
    {
        GenergicTextChangedHandler(sender,1);  
    }
    protected void txtpartno3_TextChanged(object sender, EventArgs e)
    {
        GenergicTextChangedHandler(sender,1);  
    }
    protected void txtpartno4_TextChanged(object sender, EventArgs e)
    {
        GenergicTextChangedHandler(sender,1);  
    }
    protected void txtpartno5_TextChanged(object sender, EventArgs e)
    {
        GenergicTextChangedHandler(sender,1);  
    }
    protected void txtpartno6_TextChanged(object sender, EventArgs e)
    {
        GenergicTextChangedHandler(sender,1);  
    }
    protected void txtpartno7_TextChanged(object sender, EventArgs e)
    {
        GenergicTextChangedHandler(sender,1);  
    }
    protected void txtpartno8_TextChanged(object sender, EventArgs e)
    {
        GenergicTextChangedHandler(sender,1);  
    }
    protected void txtpartno9_TextChanged(object sender, EventArgs e)
    {
        GenergicTextChangedHandler(sender,1);  

    }
    protected void txtpartno10_TextChanged(object sender, EventArgs e)
    {
        GenergicTextChangedHandler(sender,2);  
    }
    protected void txtpartno11_TextChanged(object sender, EventArgs e)
    {
        GenergicTextChangedHandler(sender,2);  
    }
    protected void txtpartno12_TextChanged(object sender, EventArgs e)
    {
        GenergicTextChangedHandler(sender,2);  
    }
    protected void txtpartno13_TextChanged(object sender, EventArgs e)
    {
        GenergicTextChangedHandler(sender,2);  
    }


    
    //protected void CustomValidator1_ServerValidate1(object source, ServerValidateEventArgs args)
    //{
    //    //args.IsValid = (args.Value > 8);

    //  int result=  CommonTool.IsvalidPartno(args.Value , PackageName);
    //  //if (result > 0)
    //  //{
    //  //    args.IsValid = true;
    //  //}
    //  //else
    //  //{
    //  //    args.IsValid = false;
    //  //}
    //  CustomValidator cv = source as CustomValidator;
    //  switch (result)
    //  {
    //      case -1:
    //          args.IsValid = false;
    //          cv.ErrorMessage = string.Format("Error : {0}",args.Value);
    //          //btnsumbit.Enabled = false;
    //       //   btnsumbit.CausesValidation = true;
    //          break;
    //      case 0:
    //          args.IsValid = false;
    //          cv.ErrorMessage = string.Format("Warning : {0}", args.Value);
    //       //   btnsumbit.CausesValidation = false ;
    //          break;
    //      case 1:
    //          args.IsValid = true;
    //          break;
    //      default:
    //          break;
    //  }

    //}
    protected void txtcomment7_TextChanged(object sender, EventArgs e)
    {

    }
}
