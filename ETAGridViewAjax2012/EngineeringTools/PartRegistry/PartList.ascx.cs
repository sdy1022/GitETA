using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Diagnostics;

public partial class EngineeringTools_PartRegistry_PartList : System.Web.UI.UserControl
{

    #region Prop & Field
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


    public string PARENTPART
    {
        get
        {
            return (string)ViewState["PARENTPART"];
        }
        set
        {
            ViewState["PARENTPART"] = value;
        }
    }

    private DataTable _sourcedt = null;
    public DataTable SourceDT
    {
        get { return _sourcedt; }
        set { _sourcedt = value; }
    }

    #endregion

    #region Form Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {

            BindGridView();


        }



    }

    protected void Gvwpartlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    #endregion

    #region Customized Methods
    /// <summary>
    /// 
    /// </summary>
    public void BindGridView()
    {

        _sourcedt = CommonDB.Get06PartsStructure_05PRList_TSDList(PackageName, PARENTPART);
       // Debug.WriteLine("partlist bindgrid :" + PackageName + ":" + PARENTPART + ":" + _sourcedt.Rows.Count);
        Gvwpartlist.DataSource = _sourcedt;
        Gvwpartlist.DataBind();
    }

    #endregion

}
