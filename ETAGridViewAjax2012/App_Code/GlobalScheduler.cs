using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;
using Quartz;
using Quartz.Impl;

/// <summary>
/// Summary description for GlobalScheduler
/// </summary>
public static class GlobalScheduler
{
    private static IScheduler _scheduler = null;

    public static IScheduler GetScheduler()
    {
        if (_scheduler == null || _scheduler.IsShutdown == true)
        {

            ISchedulerFactory factory = new StdSchedulerFactory();
            // get a scheduler 

            _scheduler = factory.GetScheduler();
        }

       
        return _scheduler;
    }

   

}
