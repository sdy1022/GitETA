using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class PackageMaintenance_SaveAsSelection : System.Web.UI.Page
{
    #region Page Methods


    private string PackageName = null;

    protected void Page_Load(object sender, EventArgs e)
    {

      //  Predicate<string> p = item => item.Length > 3;
        

        if (!Page.IsPostBack)
        {
           
            PackageName=Request.QueryString["Package"].Trim();

        //    PackageName = "NG00";
            #if   DEBUG
         
            //Linnemann, Josh
            //PackageName = "N4N5";
            

         //    PackageName = "XXX0";

#else

#endif


            if (!string.IsNullOrEmpty(PackageName))
            {

               int EditLock=0 ;
               int Released = 0;

               DataTable dt = CommonDB.GetPackageLogInfoByPackage(PackageName).Tables[0];
               if (dt.Rows.Count > 0)
               {
                   if (Convert.ToBoolean(dt.Rows[0][0].ToString()) == true)
                   {
                       EditLock = 1;
                   }

                   if (CommonTool.IsDate(dt.Rows[0][1].ToString()) == true)
                   {
                       Released = 1;
                   }


                   //
                   //DataTable dt2=CommonDB.GetActiveStatusByPackage(PackageName).Tables[0];
                   //if (dt2.Rows.Count > 0)
                   //{
                   //    if (dt2.Rows[0][0].ToString().Trim() == "Accepted")
                   //    {
                   //        EditLock = 0;

                   //        Released = 0;
                   //    }
                       

                   //}


                   // Changed on 09/09/2010 By Dayang
                   //if (EditLock == 1 || Released == 1)
                   if(EditLock ==1)
                   {
                       moduleregular.Visible = false;

                       modulelocked.Visible = true;
                   }
                   else
                   {
                       moduleregular.Visible = true;

                       modulelocked.Visible = false;

                   }

               }





            }

            else
            {


                modulelocked.Visible = false;
                moduleregular.Visible = false;
            }
            

            //Checkmodule status



        }


    }

    #endregion
}
