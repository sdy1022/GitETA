using System;
using System.Collections.Generic;
using System.Data;

public class PartRegistsryEntityRepository
{
    public static List<PartRegistsryEntity> GetPartRegistsryEntityList(string package, int pagesize, int pagenum, ref int totalcount)
    {
        string counts = "";
        string pagecounts = "";
        string sql = "select * from dbo.Part_Registry where PartNo like '%" + package + "%' order by TID";
        //  select * from dbo.Part_Registry where PartNo like '%XXX0%' order by TID
        //"select * from dbo.PartsListItems where DesignNumber like '" + "XXX0" + "%' order by Itemid desc";

        DataTable dt = CommonDB.GetPagingDataTableFromDB(sql, pagesize, pagenum, ref counts, ref pagecounts);
        // should call db to get results
        totalcount = Convert.ToInt16(counts);
        List<PartRegistsryEntity> list1 = (List<PartRegistsryEntity>)CollectionHelper.ConvertTo<PartRegistsryEntity>(dt);

        return list1;

    }
}
