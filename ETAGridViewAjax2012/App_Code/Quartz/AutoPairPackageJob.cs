using System;
using System.Diagnostics;
using Common.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Quartz;
using System.Net;
using System.IO;


public class AutoPairPackageJob : IJob
{
    #region IJob Members

    public void Execute(JobExecutionContext context)
    {

        //  ILog log = LogManager.GetLogger(typeof(AutoPairPackageJob));

        // log.Info("Task Task executed at " + DateTime.Now.ToString());

        //Logger.Write("Task executed at " + DateTime.Now.ToString());
        CommonDB.AutoPairFormCFromQueue();
        CommonDB.LogMessageToDB("AutoPairFormC Task was executed at " + DateTime.Now.ToString());
      //  Debug.WriteLine("Task executed at " + DateTime.Now.ToString());

        // add logging info to log
        //JobDataMap data = context.MergedJobDataMap;
        //string url = data.GetString("URL") ?? string.Empty;

        //try
        //{

        //    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
        //    myRequest.Method = "GET";
        //    WebResponse myResponse = myRequest.GetResponse();
        //    StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
        //    string result = sr.ReadToEnd();
        //    sr.Close();
        //    myResponse.Close();

        //}
        //catch { }
    }

    #endregion

}
