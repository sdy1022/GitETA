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


public class FormAEntity
{//TID, PartNo, Minor, Description, TMHU_View, From_ECI, To_ECI
    // public int TID { get; set; }
    //designnumber, AItemId, ModuleNumber, key, MAJOR, MINOR, COMMENT
    private int _aitemid;
    public int AItemId
    {
        get
        {
            return _aitemid;
        }
        set
        {
            _aitemid = value;
        }
    }

    private string _designnumber;
    public string DesignNumber
    {
        get
        {
            return _designnumber;
        }
        set
        {
            _designnumber = value;
        }
    }
    // public string PartNo { get; set; }
    // public string Minor { get; set; }
    private string _modulenumber;
    public string ModuleNumber
    {
        get
        {
            return _modulenumber;
        }
        set
        {
            _modulenumber = value;
        }
    }

    // public string Description { get; set; }
    private int _key;
    public int Key
    {
        get
        {
            return _key;
        }
        set
        {
            _key = value;
        }
    }


}

public class FormCEntity
{//TID, PartNo, Minor, Description, TMHU_View, From_ECI, To_ECI
    // public int TID { get; set; }
    //ID, AItemId, TFC, ATT, KEYCODE, ITEMCODE, MODELCODE, ADD_TFC, ADD_INDEX, DEL_TFC, DEL_INDEX, FROM_ECI, TO_ECI, FROM_DATE, TO_DATE
    private int _aitemid;
    public int AItemId
    {
        get
        {
            return _aitemid;
        }
        set
        {
            _aitemid = value;
        }
    }

   

    // public string Description { get; set; }
    private string _keycode;
    public string KEYCODE
    {
        get
        {
            return _keycode;
        }
        set
        {
            _keycode = value;
        }
    }


}


