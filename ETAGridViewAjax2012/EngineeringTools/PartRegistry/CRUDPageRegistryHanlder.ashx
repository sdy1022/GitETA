<%@ WebHandler Language="C#" Class="CRUDPageRegistryHanlder" %>
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json;
public class CRUDPageRegistryHanlder : IHttpHandler, IRequiresSessionState
{
    #region IHttpHandler Members

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request["mode"] != null)
        {
            string mode = context.Request["mode"];
            switch (mode)
            {
                case "Qry":
                    QueryData(context);
                    break;
                case "Add":
                    InsertData(context);
                    break;
                case "Update":
                    UpdateData(context);
                    break;
                case "Del":
                    DeleteData(context);
                    break;
                case "Authorize":
                    AuthorizeUser(context);
                    break;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool IsReusable
    {
        get { return true; }
    }

    #endregion

    private static void QueryData(HttpContext context)
    {
        // Get total result rows without paging
        int count = 0; //list.Count; //查询总条数
        int pagesize = Convert.ToInt32(context.Request["pageSize"]);
            // == 0 ? 25 : Convert.ToInt32(context.Request["size"]);  //页行数
        int pagenum = Convert.ToInt32(context.Request["pageNumber"]);
            //== 0 ? 1 : Convert.ToInt32(context.Request["page1"]); //当前页
        string package = context.Request["PackageName"];
        context.Response.ContentType = "text/plain";
        List<PartRegistsryEntity> list = PartRegistsryEntityRepository.GetPartRegistsryEntityList(package, pagesize,
                                                                                                  pagenum, ref count);
        // need optimization 
        //StringBuilder builder = new StringBuilder();

        //  var ss = string.Format("{\"total\":{0},\"rows\":[", count);
        // builder.Append();


        //var menu = "{\"total\":" + count + ",\"rows\":[";
        //int a = (pagenum - 1) * pagesize;
        //int b = pagenum * pagesize + 1;

        //for (int i = 0; i < list.Count; i++)
        //{
        //    if (i != list.Count - 1)
        //    {
        //        menu += "{\"TID\":\"" + list[i].TID + "\",\"PartNo\":\"" + list[i].PartNo.ToString() + "\",\"Minor\":\"" +
        //                list[i].Minor.ToString() + "\",\"Description\":\"" + list[i].Description.ToString() +
        //                "\",\"TMHU_View\":\"" + list[i].TMHU_View.ToString() + "\",\"From_ECI\":\"" +
        //                list[i].From_ECI.ToString() + "\",\"To_ECI\":\"" + list[i].To_ECI + "\"},";
        //    }
        //    else
        //    {
        //        menu += "{\"TID\":\"" + list[i].TID + "\",\"PartNo\":\"" + list[i].PartNo.ToString() + "\",\"Minor\":\"" +
        //                list[i].Minor.ToString() + "\",\"Description\":\"" + list[i].Description.ToString() +
        //                "\",\"TMHU_View\":\"" + list[i].TMHU_View.ToString() + "\",\"From_ECI\":\"" +
        //                list[i].From_ECI.ToString() + "\",\"To_ECI\":\"" + list[i].To_ECI + "\"}";
        //    }
        //}
        //menu += "]}";

        var serializer = new JsonSerializer();
        var sb = new StringBuilder();
        var sw = new StringWriter(sb);
        JsonWriter writer = new JsonTextWriter(sw);
        serializer.Serialize(writer, list);
        string json = sb.ToString();
        string finaljson = string.Format("{{\"total\":{0},\"rows\":{1}}}", count, json);
        context.Response.Write(finaljson);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void InsertData(HttpContext context)
    {
        // will implement insertion then

        string result = CommonDB.InsertPrNewEntry(context.Request["PartNo"], context.Request["Minor"],
                                                  context.Request["Description"],
                                                  Convert.ToInt16(context.Request["TMHU_View"]),
                                                  context.Request["From_ECI"],
                                                  context.Request["To_ECI"]);


        context.Response.Write(result);
    }

    /// <summary>
    /// Udpate record
    /// </summary>
    /// <param name="context"></param>
    public void UpdateData(HttpContext context)
    {
        // get tid from table
        //string tid = context.Request["TID"];
        //PartRegistsryEntity entity = new PartRegistsryEntity();
        //entity.PartNo = context.Request["PartNo"];
        //entity.Minor = context.Request["Minor"];
        //entity.Description = context.Request["Description"];
        //entity.TMHU_View = Convert.ToInt16(context.Request["TMHU_View"]);
        //entity.From_ECI = context.Request["From_ECI"];
        //entity.To_ECI = context.Request["To_ECI"];

        string result = CommonDB.UpdatePrNewEntry(Convert.ToInt16(context.Request["TID"]), context.Request["PartNo"],
                                                  context.Request["Minor"], context.Request["Description"],
                                                  Convert.ToInt16(context.Request["TMHU_View"]),
                                                  context.Request["From_ECI"], context.Request["To_ECI"]);
        context.Response.Write(result);
    }

    /// 刪除資料
    /// </summary>
    /// <param name="context"></param>
    public void DeleteData(HttpContext context)
    {
        string ids = context.Request["ID"];
        //result rlt = new result();
        ////rlt = Delete(id);
        //DataContractJsonSerializer json = new DataContractJsonSerializer(rlt.GetType());
        //json.WriteObject(context.Response.OutputStream, rlt);
        string result = CommonDB.DeletePrNewEntry(ids);
        context.Response.Write(result);
    }

    /// <summary>
    /// Authorize current user and return role value
    /// </summary>
    /// <param name="context"></param>
    private static void AuthorizeUser(HttpContext context)
    {
        string uid = context.Request["UID"];

        // Can save current result to session 

        if (context.Session["uid"] == null || context.Session["uid"].ToString() != uid)
        {
            context.Session["uid"] = uid;
            // need to get user role by calling extra function
            context.Session["userrole"] = "admin";
        }

        context.Response.Write(context.Session["userrole"].ToString());
    }
}