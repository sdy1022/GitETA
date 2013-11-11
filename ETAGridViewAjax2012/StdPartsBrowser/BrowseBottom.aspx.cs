using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class StdPartsBrowser_BrowseBottom : System.Web.UI.Page
{

    #region Props
    /// <summary>
    /// Gets or sets the model.
    /// </summary>
    /// <value>The model.</value>
    public string Model
    {
        get
        {
            return (string)ViewState["Model"];
        }
        set
        {
            ViewState["Model"] = value;
        }
    }

    /// <summary>
    /// Gets or sets the assy.
    /// </summary>
    /// <value>The assy.</value>
    public string Assy
    {
        get
        {
            return (string)ViewState["Assy"];
        }
        set
        {
            ViewState["Assy"] = value;
        }
    }

    /// <summary>
    /// Gets or sets the pagecode1.
    /// </summary>
    /// <value>The pagecode1.</value>
    public string Pagecode1
    {
        get
        {
            return (string)ViewState["Pagecode1"];
        }
        set
        {
            ViewState["Pagecode1"] = value;
        }
    }
    /// <summary>
    /// Gets or sets the pagecode2.
    /// </summary>
    /// <value>The pagecode2.</value>
    public string Pagecode2
    {
        get
        {
            return (string)ViewState["Pagecode2"];
        }
        set
        {
            ViewState["Pagecode2"] = value;
        }
    }

    /// <summary>
    /// Gets or sets the pagecode3.
    /// </summary>
    /// <value>The pagecode3.</value>
    public string Pagecode3
    {
        get
        {
            return (string)ViewState["Pagecode3"];
        }
        set
        {
            ViewState["Pagecode3"] = value;
        }
    }

    #endregion

    #region Form Methods
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //Initialize Props
            Model = Request.QueryString["Model"].Trim();
            Assy = Request.QueryString["Assy"].Trim();
            Pagecode1 = Request.QueryString["Pagecode1"].Trim();
            Pagecode2 = Request.QueryString["Pagecode2"].Trim();
            Pagecode3 = Request.QueryString["Pagecode3"].Trim(); 

            BindOuterGridView();

        }


    }

    /// <summary>
    /// Binds the outer grid view.
    /// </summary>
    private void BindOuterGridView()
    {


        DataTable dt = CommonDB.GetStandardPartItemsByvalues(Model, Assy, Pagecode1, Pagecode2, Pagecode3).Tables[0];

       gvouter.DataSource = dt;

       gvouter.DataBind();



    }

    #endregion


    #region Event Methods
    /// <summary>
    /// Handles the RowCreated event of the gvCustomers control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void gvCustomers_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SqlDataSource ctrl = e.Row.FindControl("sqlDsOrders") as SqlDataSource;
            if (ctrl != null && e.Row.DataItem != null)
            {
                ctrl.SelectParameters["CustomerID"].DefaultValue = ((DataRowView)e.Row.DataItem)["CustomerID"].ToString();
            }
        }
    }
    /// <summary>
    /// Handles the RowCreated event of the gvouter control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void gvouter_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string partno=  ((DataRowView)e.Row.DataItem)["PartNo"].ToString();

            int aaa = partno.IndexOf('-');

         Image sdy = e.Row.FindControl("pnlouter").FindControl("imgCollapsible") as Image;
            if (aaa != -1)
            {
               

              
                sdy.Visible = true;
                
                GridView gvinner = e.Row.FindControl("pnlinner").FindControl("gvinner") as GridView;

                gvinner.DataSource  = CommonDB.GetSubStandardPartItemsByvalues(partno).Tables[0];
                gvinner.DataBind();
            }
            else
            {
               
                sdy.Visible = false;
            }

        }


       
    }
        
 
    
    #endregion

}
