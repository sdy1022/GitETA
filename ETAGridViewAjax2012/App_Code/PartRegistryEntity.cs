using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;


public class PartRegistsryEntity
{//TID, PartNo, Minor, Description, TMHU_View, From_ECI, To_ECI
    // public int TID { get; set; }

    private int _tid;
    public int TID
    {
        get
        {
            return _tid;
        }
        set
        {
            _tid = value;
        }
    }

    private string _partno;
    public string PartNo
    {
        get
        {
            return _partno;
        }
        set
        {
            _partno = value;
        }
    }
   // public string PartNo { get; set; }
   // public string Minor { get; set; }
    private string _minor;
    public string Minor
    {
        get
        {
            return _minor;
        }
        set
        {
            _minor = value;
        }
    }
    
   // public string Description { get; set; }
    private string _desc;
    public string Description
    {
        get
        {
            return _desc;
        }
        set
        {
            _desc = value;
        }
    }
   // public int TMHU_View { get; set; }
    private int _tmhu;
    public int TMHU_View
    {
        get
        {
            return _tmhu;
        }
        set
        {
            _tmhu = value;
        }
    }
   // public string From_ECI { get; set; }
    private string _fromeci;
    public string From_ECI
    {
        get
        {
            return _fromeci;
        }
        set
        {
            _fromeci = value;
        }
    }
  //  public string To_ECI { get; set; }
    private string _toeci;
    public string To_ECI
    {
        get
        {
            return _toeci;
        }
        set
        {
            _toeci = value;
        }
    }
    private string _material1;
    public string MATERIAL1
    {
        get
        {
            return _material1;
        }
        set
        {
           _material1 = value;
        }
    }

    private string _material2;
    public string MATERIAL2
    {
        get
        {
            return _material2;
        }
        set
        {
            _material2 = value;
        }
    }
    private string _dwg;
    public string DRW
    {
        get
        {
            return _dwg;
        }
        set
        {
            _dwg = value;
        }
    }

    private string _comment;
    public string COMMENT1
    {
        get
        {
            return _comment;
        }
        set
        {
            _comment = value;
        }
    }
    private string _modfrom;
    public string Mod_From
    {
        get
        {
            return _modfrom;
        }
        set
        {
            _modfrom = value;
        }
    }

    private string _fromdate;
    public string FROM_DATE
    {
        get
        {
            return _fromdate;
        }
        set
        {
            _fromdate = value;
        }
    }


    private string _todate;
    public string TO_DATE
    {
        get
        {
            return _todate;
        }
        set
        {
            _todate = value;
        }
    }
}




