using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
public partial class PartnumbersUpdate : System.Web.UI.Page
{
    Dictionary<string, List<string>> GroupsHash = null;
    List<string> initial = null;

    protected Dictionary<string, List<string>> GetDict()
    {
        if (GroupsHash == null)
        {

            GroupsHash = new Dictionary<string, List<string>>();
            MakeIdentifyModelMap();
        }
        return GroupsHash;
    }
       

    protected void Page_Load(object sender, EventArgs e)
    {
        


        if (!this.IsPostBack)
        {
           

            // bindgridview
            this.TextBox1.Text = @"select partnumber, designnumber,dbo.GetModelByPartNo(substring(designnumber,1,4)) as idenmodel,itemid  from  partslistitems where (
dbo.IsMatch(dbo.Removeseparator(partnumber),'^\d{7}$')=1  or    dbo.IsMatch(dbo.Removeseparator(partnumber),'^\d{5}\w{1}\d{2}$')=1 ) order by partnumber";

           
            //61501A-01)  (510)0701	M41X-10
            
            // Get dt like '(510)0101'
            GetDict();
            DataTable dt = CommonDB.temp_9(9).Tables[0];

             string result = GetFinalResult("(110)(01)A-01", "N0TB-3");
// GetFinalResult("(510)(01)03", "MT31-13");
            

            StringBuilder sb = new StringBuilder();

            //string updatesql=string.Format("Update partslistitems set partnumber='{0}{1}' where itemid={2}",result,"(510)0701",3943777);
            //sb.AppendLine(updatesql);

            //int count = 0;
            //foreach (DataRow dr in dt.Rows)
            //{
                
            //    string pno=dr["partnumber"].ToString();
            //    string des=dr["designnumber"].ToString();
            //    string itemid=dr["itemid"].ToString();
            //    string result = GetFinalResult(pno, des);
            //    if (string.IsNullOrEmpty(result))
            //    {
            //        sb.AppendLine(string.Format("{0}:{1}:{2} ", pno, des, itemid));
            //    }
            //    count++;
            //    //string updatesql = string.Format("Update partslistitems set partnumber='{0}{1}' where itemid={2};", result,pno,itemid );
            //   // sb.Append(updatesql);

            //}
            
            //if(GroupsHash.ContainsKey(model))

            //{

                
            // List<string> sdy= GroupsHash[model] as List<string>;

            // bool test=sdy.Contains("G841");


            //}



            //// View all via enumeration
            //foreach (KeyValuePair<string, List<string>> kvp in GroupsHash)
            //{
            //    List<string> list = kvp.Value as List<string>;
            //    if (list == null)
            //        throw new Exception("Something is horribly wrong!");

            //    foreach (string str in list)
            //        Console.WriteLine("Key ({0}) {1}", kvp.Key, str);
            //}


            //// Access an individual item
            //Console.WriteLine("{0}Individual Item: {1} -> {2}{0}", Environment.NewLine, "Test1", GroupsHash["Test1"][1]);



            

        }
    }

    private void MakeIdentifyModelMap()
    {//8FGCU15	G850	M207	S850
        //8FGCU18	G850	M207	S850
        //8FGCSU20	G850	M207	S850

        //8FGCU20	G851	M207	S851
        //8FGCU25	G851	M207	S851
        //8FGCU30	G851	M207	S851
        //8FGCU32	G851	M207	S851
        //8FGU20	G851	M207	S851
        //8FGU25	G851	M207	S851
        //8FGU30	G851	M207	S851
        //8FGU32	G851	M207	S851

        //7FBEU15	G846	M204	S846
        //7FBEU18	G846	M204	S846
        //7FBEU20	G846	M204	S846

        //7FGCU35	G839	M304	S839
        //7FGCU45	G839	M304	S839
        //7FGCU55	G839	M304	S839
        //7FGCU60	G839	M304	S839
        //7FGCU70	G839	M304	S839
        //7FGU35	G839	M304	S839
        //7FGKU40	G839	M304	S839
        //7FGU45	G839	M304	S839
        //7FGAU50	G839	M304	S839
        //7FGU60	G839	M304	S839
        //7FGU70	G839	M304	S839
        //7FGU80	G839	M304	S839

        //7FBCU15	G840	M204	S840
        //7FBCU18	G840	M204	S840
        //7FBCU20	G840	M204	S840
        //7FBCU25	G840	M204	S840
        //7FBCU30	G840	M204	S840
        //7FBCU32	G840	M204	S840
        //30-7FBCU15	G840	M204	S840
        //30-7FBCU18	G840	M204	S840
        //30-7FBCU20	G840	M204	S840
        //30-7FBCU25	G840	M204	S840
        //30-7FBCU30	G840	M204	S840
        //30-7FBCU32	G840	M204	S840


        //7FBCU35	G841	M304	S841
        //7FBCU45	G841	M304	S841
        //7FBCU55	G841	M304	S841
        //30-7FBCU35	G841	M304	S841
        //30-7FBCU45	G841	M304	S841
        //30-7FBCU55	G841	M304	S841


        // add values
        AddValues("8FGCU15", "G850", "M207", "S850");
        AddValues("8FGCU18", "G850", "M207", "S850");
        AddValues("8FGCSU20", "G850", "M207", "S850");

        AddValues("8FGCU20", "G851", "M207", "S851");
        AddValues("8FGCU25", "G851", "M207", "S851");
        AddValues("8FGCU30", "G851", "M207", "S851");
        AddValues("8FGCU32", "G851", "M207", "S851");
        AddValues("8FGU20", "G851", "M207", "S851");
        AddValues("8FGU25", "G851", "M207", "S851");
        AddValues("8FGU30", "G851", "M207", "S851");
        AddValues("8FGU32", "G851", "M207", "S851");


        AddValues("7FBEU15", "G846", "M204", "S846");
        AddValues("7FBEU18", "G846", "M204", "S846");
        AddValues("7FBEU20", "G846", "M204", "S846");


        AddValues("7FGCU35", "G839", "M304", "S839");
        AddValues("7FGCU45", "G839", "M304", "S839");
        AddValues("7FGCU55", "G839", "M304", "S839");
        AddValues("7FGCU60", "G839", "M304", "S839");
        AddValues("7FGCU70", "G839", "M304", "S839");
        AddValues("7FGU35", "G839", "M304", "S839");
        AddValues("7FGKU40", "G839", "M304", "S839");
        AddValues("7FGU45", "G839", "M304", "S839");
        AddValues("7FGAU50", "G839", "M304", "S839");
        AddValues("7FGU60", "G839", "M304", "S839");
        AddValues("7FGU70", "G839", "M304", "S839");
        AddValues("7FGU80", "G839", "M304", "S839");


        AddValues("7FBCU15", "G840", "M204", "S840");
        AddValues("7FBCU18", "G840", "M204", "S840");
        AddValues("7FBCU20", "G840", "M204", "S840");
        AddValues("7FBCU25", "G840", "M204", "S840");
        AddValues("7FBCU30", "G840", "M204", "S840");
        AddValues("7FBCU32", "G840", "M204", "S840");
        AddValues("30-7FBCU15", "G840", "M204", "S840");
        AddValues("30-7FBCU18", "G840", "M204", "S840");
        AddValues("30-7FBCU20", "G840", "M204", "S840");
        AddValues("30-7FBCU25", "G840", "M204", "S840");
        AddValues("30-7FBCU30", "G840", "M204", "S840");
        AddValues("30-7FBCU32", "G840", "M204", "S840");



        AddValues("7FBCU35", "G841", "M304", "S841");
        AddValues("7FBCU45", "G841", "M304", "S841");
        AddValues("7FBCU55", "G841", "M304", "S841");
        AddValues("30-7FBCU35", "G841", "M304", "S841");
        AddValues("30-7FBCU45", "G841", "M304", "S841");
        AddValues("30-7FBCU55", "G841", "M304", "S841");



        initial = null; // Just unhooks the reference to the list<>
    }

    private string GetFinalResult(string partno,string designnumber)
    {
        string result = null;

        string revisedpartno = CommonTool.PartNumberCleanUp(partno);

        DataTable dt = CommonDB.GetModelListByPartno(revisedpartno).Tables[0];
        List<string> modellist = new List<string>();

        if (dt.Rows.Count == 0)
        {
            result = null;
            return result;
            

        }





        // will get this list based on partno

        //SELECT  distinct Model,partno  FROM ETA.dbo.viewStandardParts where partno like '%5100101%'       
        else
        {


            foreach (DataRow dr in dt.Rows)
            {

                modellist.Add(dr["Model"].ToString());

            }

        }

        if (modellist.Count < 2)
        {

            result = modellist[0];
            return result;
        }



        // get the following model based on partno and its deignnumber

        dt = CommonDB.GetIdentityModelByPartnoAndDesignNumber(partno, designnumber).Tables[0];

        if (dt.Rows.Count == 0)
        {
            result = null;
            return result;
        }



        string model = dt.Rows[0]["identifymodel"].ToString();


        if (GroupsHash.ContainsKey(model))
        { 
            List<string> modelmap = GroupsHash[model] as List<string>;

            foreach (string str in modellist)
            {
                if (modelmap.Contains(str))
                {
                    result = str;

                    return result;
                }

            }

        }
        else
        {
            List<string> modelmap=new List<string>() ;
            if (designnumber.Substring(0, 2) == "MC")
            {
                modelmap.Add("G850");
                modelmap.Add("M204");
                modelmap.Add("G850");
            }

            if (designnumber.Substring(0, 2) == "MD")
            {
                modelmap.Add("G839");
                modelmap.Add("M207");
                modelmap.Add("G839");
            }

            if (designnumber.Substring(0, 2) == "ME")
            {
                modelmap.Add("G846");
                modelmap.Add("M204");
                modelmap.Add("G846");
            }


            if (designnumber.Substring(0, 2) == "MT")
            {
                modelmap.Add("G854");

            }

            if (designnumber.Substring(0, 2) == "NM")
            {
              //  modelmap.Add("G854");

            }

            if (designnumber.Substring(0, 2) == "NN")
            {
                //  modelmap.Add("G854");

            }

            if (designnumber.Substring(0, 2) == "NP")
            {
                //  modelmap.Add("G854");

            }

            if (modelmap.Count == 0)
                return null;

            foreach (string str in modellist)
            {
                if (modelmap.Contains(str))
                {
                    result = str;

                    return result;
                }

            }

            result = null;
            
        }

        return result;


      
    }

    private void AddValues(string key,string item1,string item2,string item3)
    {
        initial = new List<string>();

        initial.Add(item1);
        initial.Add(item2);
        initial.Add(item3);

        GroupsHash.Add(key, initial);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        int partnolength = 0;

        if (this.ddllengthcount.SelectedIndex == 0 || this.ddllengthcount.SelectedIndex>15)
        {
            this.GridView1.DataSource = CommonDB.GetPartListINfoByLength(ddllengthcount.SelectedValue, true);
        }
        else
        {

            this.GridView1.DataSource = CommonDB.GetPartListINfoByLength(ddllengthcount.SelectedValue,false);
        }
        this.GridView1.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

       
        
        
        try
        {
            string sql = this.TextBox1.Text.Replace(Environment.NewLine, "");
            this.GridView1.DataSource = CommonDB.temp(sql);
            this.GridView1.DataBind();
        }
        catch
        {
            ;
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string result22 = GetFinalResult("(240)(01)A-01", "NJJ5-9");

        DateTime starttime = DateTime.Now;
       
        GetDict();
        StringBuilder sb = new StringBuilder();
        string sql = this.TextBox1.Text.Replace(Environment.NewLine, "");

        DataTable dt = CommonDB.temp(sql).Tables[0];
      
        //string updatesql=string.Format("Update partslistitems set partnumber='{0}{1}' where itemid={2}",result,"(510)0701",3943777);
        //sb.AppendLine(updatesql);

       


         int count = 0;
         foreach (DataRow dr in dt.Rows)
         {

             string pno = dr["partnumber"].ToString();
             string des = dr["designnumber"].ToString();
             string itemid = dr["itemid"].ToString();

             //string result = count.ToString();
             string result = GetFinalResult(pno, des);
             if (string.IsNullOrEmpty(result))
             {
                // sb.AppendLine(string.Format("{0}:{1}:{2} ", pno, des, itemid));
             }
             else
             {

                 string updatesql = string.Format("Update partslistitems set partnumber='{0}{1}' where itemid={2};{3}", result,pno,itemid,Environment.NewLine );
                 sb.Append(updatesql);
             }
                 count++;
         }


         string pth;

         pth = Server.MapPath(".").ToString();
         pth = pth + "\\" + this.TextBox2.Text + "_query.txt";

         StreamWriter tw = new StreamWriter(pth);
         tw.Write(sb.ToString());

         tw.Close();
         DateTime endtime = DateTime.Now;
         TimeSpan difference = endtime.Subtract(starttime);
         string time = new DateTime(difference.Ticks).ToString("mm:ss");
      //   this.GridView1.DataSource = dt;
       //  this.GridView1.DataBind();
         this.lblpath.Text ="Time:" + time + "\n Path:"+pth;

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //lblfinalresult
            Label lblfinalresult = e.Row.FindControl("lblfinalresult") as Label;
            Label lblidenmodel = e.Row.FindControl("lblidenmodel") as Label;
            Label lblpartno=e.Row.FindControl("lblpartno") as Label ;
            lblfinalresult.Text = GetFinalResult(lblpartno.Text,lblidenmodel.Text);
               

        }


    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
}
