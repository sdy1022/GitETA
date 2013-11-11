using System;
using System.Collections.Generic;
using System.Data;

public class ETAEntityRepository
{
    public static List<FormAEntity> GetFormAEntityList(string package, int pagesize, int pagenum, ref int totalcount)
    {
        string counts = "";
        string pagecounts = "";
        string sql = "select [AItemId] ,[designnumber] as DesignNumber,[ModuleNumber],[key] as [Key] from [dbo].[FormAItems] where designnumber='" + package + "' order by AItemId";
        //  select * from dbo.Part_Registry where PartNo like '%XXX0%' order by TID
        //"select * from dbo.PartsListItems where DesignNumber like '" + "XXX0" + "%' order by Itemid desc";

        DataTable dt = CommonDB.GetPagingDataTableFromST(sql, pagesize, pagenum, ref counts, ref pagecounts);
        // should call db to get results
        totalcount = Convert.ToInt16(counts);
        List<FormAEntity> list1 = (List<FormAEntity>)CollectionHelper.ConvertTo<FormAEntity>(dt);

        return list1;

    }


    public static List<FormCEntity> GetFormCEntityList(string aitemid, int pagesize, int pagenum, ref int totalcount)
    {
        string counts = "";
        string pagecounts = "";
   //     string sql = "SELECT  [AItemId],[KEYCODE] as KEYCODE FROM [dbo].[03_TSD] Where AItemId=" + aitemid + " order by AItemId";
        string sql = "select [AItemId] ,[KEYCODE] as KEYCODE  FRom [dbo].[03_TSD] where AItemId=" + aitemid + " order by AItemId";
      //  sql = "Select [AItemId],[KEYCODE] as KEYCODE FROM [dbo].[03_TSD] Where AItemId=" + aitemid + " order by AItemId";

  //      string sql = "select [AItemId] ,[designnumber] as [KeyCode] from [dbo].[FormAItems] where AItemId=" + aitemid + " order by AItemId";
        DataTable dt = CommonDB.GetPagingDataTableFromST(sql, pagesize, pagenum, ref counts, ref pagecounts);
        // should call db to get results
        totalcount = Convert.ToInt16(counts);
        List<FormCEntity> list1 = (List<FormCEntity>)CollectionHelper.ConvertTo<FormCEntity>(dt);

        return list1;

    }



}
