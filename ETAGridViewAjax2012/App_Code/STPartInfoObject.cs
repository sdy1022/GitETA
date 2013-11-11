using System;
using System.Collections.Generic;
using System.Web;
using System.Collections.Generic; 

/// <summary>
/// Summary description for STPartInfoObject
/// </summary>
public class STPartInfoObject
{

    private string strCLASS_NAME;
    public string CLASS_NAME
    {
        get
        {
            return strCLASS_NAME;
        }
        set
        {
            strCLASS_NAME = value;
        }
    }
    

    private string strCN_PART_NUMBER;
    public string CN_PART_NUMBER
    {
        get
        {
            return strCN_PART_NUMBER;
        }
        set
        {
            strCN_PART_NUMBER = value;
        }
    }

    private string strTDM_DESCRIPTION;
    public string TDM_DESCRIPTION
    {
        get
        {
            return strTDM_DESCRIPTION;
        }
        set
        {
            strTDM_DESCRIPTION = value;
        }
    }


    private string strREVISION;
    public string REVISION
    {
        get
        {
            return strREVISION;
        }
        set
        {
            strREVISION = value;
        }
    }


    private string strTDM_ID;
    public string TDM_ID
    {
        get
        {
            return strTDM_ID;
        }
        set
        {
            strTDM_ID = value;
        }
    }


    private string strFILE_NAME;
    public string FILE_NAME
    {
        get
        {
            return strFILE_NAME;
        }
        set
        {
            strFILE_NAME = value;
        }
    }
    private DateTime strMODIFICATION_DATE;
    public DateTime MODIFICATION_DATE
    {
        get
        {
            return strMODIFICATION_DATE;
        }
        set
        {
            strMODIFICATION_DATE = value;
        }
    }

        
    public STPartInfoObject()
    {
        //
        // TODO: Add constructor logic here
        //

    }

  
}
