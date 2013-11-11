<%@ Application Language="C#" %>
<%@ Import Namespace="Quartz" %>
<%@ Import Namespace="Quartz.Impl" %>
<%@ Import Namespace="log4net" %>

<script runat="server">

    public static readonly ILog log = CommonTool.log;
        
        //(log4net.ILog)log4net.LogManager.GetLogger("Log");
   
    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

        log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(Server.MapPath("~") + @"\log4net1.xml"));
        
        log.Info("Application start "+DateTime.Now.ToString());
        log.Debug("debug");
        log.Error("error");
       
            

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

        Exception ex = Server.GetLastError();
        // log4net.ILog log = (log4net.ILog)log4net.LogManager.GetLogger("Log");
        log.Error(ex.Message, ex);

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
