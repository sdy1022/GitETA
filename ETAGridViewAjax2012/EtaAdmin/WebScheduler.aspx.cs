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

using Quartz;
using System.Diagnostics;

public partial class WebScheduler : System.Web.UI.Page
{
    public string AutoPairJobStatus
    {
        get
        {
            return (string)ViewState["AutoPairJobStatus"];

        }

        set
        {
            ViewState["AutoPairJobStatus"] = value;
            this.lblapjobstatus.Text = value;

        }

    }
    public string ConfiguratorJobStatus
    {
        get
        {
            return (string)ViewState["ConfiguratorJobStatus"];

        }

        set
        {
            ViewState["ConfiguratorJobStatus"] = value;
            this.lblconjobstatus.Text = value;

        }

    }

    public bool IsAutoPairJobActive
    {
        get
        {
            return (bool)ViewState["IsAutoPairJobActive"];
        }
        set { ViewState["IsAutoPairJobActive"] = value; }


    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
            // show current all jobs status
            GetAllJobStatus();
        }
    }

    private void GetAllJobStatus()
    {
        string jobname = "AutoPairJobQueue";
        GetAPJobStatusByName(jobname);
        jobname = "ConfiguratorQueue";
        GetCFJobStatusByName(jobname);
    }

    private void GetAPJobStatusByName(string jobname)
    {
        var sched = GlobalScheduler.GetScheduler();
        var groups = sched.JobGroupNames;
        for (int i = 0; i < groups.Length; i++)
        {
            string[] names = sched.GetJobNames(groups[i]);
            for (int j = 0; j < names.Length; j++)
            {
                var currentJob = sched.GetJobDetail(names[j], groups[i]);
                //if (sched.GetTriggersOfJob(names[j], groups[i]).Count() > 0)
                //{
                //    // still scheduled.
                //    return true;
                //}
                if (currentJob.Name == jobname)
                {
                    SimpleTrigger kkk = (SimpleTrigger)sched.GetTriggersOfJob(jobname, groups[i])[0];

                    TimeSpan ss = kkk.RepeatInterval;

                    DateTime nextlocaltime = ((DateTime)kkk.GetNextFireTimeUtc()).ToLocalTime();


                    AutoPairJobStatus =
                        string.Format("Job Is Active. Interval Hour Is : {1}. The Next Fire Time Is/Was : {2} ", jobname,
                                      ss.TotalHours, nextlocaltime.ToString());
                    return;
                }
            }
        }

        // at this point . Not found job

        AutoPairJobStatus = string.Format("Job Is Not Active. ", jobname);

    }
    private void GetCFJobStatusByName(string jobname)
    {
        var sched = GlobalScheduler.GetScheduler();
        var groups = sched.JobGroupNames;
        for (int i = 0; i < groups.Length; i++)
        {
            string[] names = sched.GetJobNames(groups[i]);
            for (int j = 0; j < names.Length; j++)
            {
                var currentJob = sched.GetJobDetail(names[j], groups[i]);
                //if (sched.GetTriggersOfJob(names[j], groups[i]).Count() > 0)
                //{
                //    // still scheduled.
                //    return true;
                //}
                if (currentJob.Name == jobname)
                {
                    SimpleTrigger kkk = (SimpleTrigger)sched.GetTriggersOfJob(jobname, groups[i])[0];

                    TimeSpan ss = kkk.RepeatInterval;

                    DateTime nextlocaltime = ((DateTime)kkk.GetNextFireTimeUtc()).ToLocalTime();


                    ConfiguratorJobStatus =
                        string.Format("Job Is Active. Interval Hour Is : {1}. The Next Fire Time Is/Was : {2} ", jobname,
                                      ss.TotalHours, nextlocaltime.ToString());
                    return;
                }
            }
        }

        // at this point . Not found job

        ConfiguratorJobStatus = string.Format("Job Is Not Active. ", jobname);

    }


    protected void btnprocess_Click(object sender, EventArgs e)
    {
        GetAllJobStatus();
    }
    protected void btnap_Click(object sender, EventArgs e)
    {
        //var sched = GlobalScheduler.GetScheduler();
        //var groups = sched.JobGroupNames;
        //for (int i = 0; i < groups.Length; i++)
        //{
        //    string[] names = sched.GetJobNames(groups[i]);
        //    for (int j = 0; j < names.Length; j++)
        //    {
        //        var currentJob = sched.GetJobDetail(names[j], groups[i]);
        //        if (sched.GetTriggersOfJob(names[j], groups[i]).Count() > 0)
        //        {
        //            // still scheduled.
        //        }
        //    }
        //}
        if (IsJobActive("AutoPairJobQueue"))
        {
            // delete current job and 

            DeleteJob("AutoPairJobQueue");

        }
        //Create new Job

        CreateDailyJob("AutoPairJobQueue", typeof(AutoPairPackageJob), Convert.ToInt16(this.DropDownList1.Text) * 60, "AutoPairPackageJobTrigger1");
    }






    public bool CreateDailyJob(string jobname, Type jobtype, int intervalminutes, string triggername)
    {
        bool result = false;
        try
        {
            var _scheduler = GlobalScheduler.GetScheduler();
            if (_scheduler.IsStarted == true)
            {
                _scheduler.ResumeAll();
            }
            else
            {
                _scheduler.Start();
            }

            //  JobDetail job = new JobDetail("AutoPairJobQueue", typeof(AutoPairPackageJob));
            JobDetail job = new JobDetail(jobname, jobtype);


            //  var ss=TriggerUtils.MakeDailyTrigger()
            Trigger trigger = TriggerUtils.MakeMinutelyTrigger(intervalminutes);
            trigger.StartTimeUtc = TriggerUtils.GetEvenMinuteDate(DateTime.UtcNow);
            trigger.Name = triggername;
            _scheduler.ScheduleJob(job, trigger);

            result = true;
        }
        catch (Exception)
        {
            result = false;
            throw;
        }


      //  GetJobStatusByName(jobname);
        GetAllJobStatus();
        return result;

    }
    public bool DeleteJob(string jobname)
    {
        bool result = false;
        var sched = GlobalScheduler.GetScheduler();
        var groups = sched.JobGroupNames;
        for (int i = 0; i < groups.Length; i++)
        {
            string[] names = sched.GetJobNames(groups[i]);
            for (int j = 0; j < names.Length; j++)
            {
                var currentJob = sched.GetJobDetail(names[j], groups[i]);
                //if (sched.GetTriggersOfJob(names[j], groups[i]).Count() > 0)
                //{
                //    // still scheduled.
                //    return true;
                //}
                if (currentJob.Name == jobname)
                {


                    sched.DeleteJob(jobname, groups[i]);

                  // GetJobStatusByName(jobname);
                    GetAllJobStatus();
                    return true;

                }
            }
        }
      //  GetJobStatusByName(jobname);
        GetAllJobStatus();
        return result;

    }
    public bool IsJobActive(string jobname)
    {

        var sched = GlobalScheduler.GetScheduler();
        var groups = sched.JobGroupNames;
        for (int i = 0; i < groups.Length; i++)
        {
            string[] names = sched.GetJobNames(groups[i]);
            for (int j = 0; j < names.Length; j++)
            {
                var currentJob = sched.GetJobDetail(names[j], groups[i]);
                //if (sched.GetTriggersOfJob(names[j], groups[i]).Count() > 0)
                //{
                //    // still scheduled.
                //    return true;
                //}
                if (currentJob.Name == jobname)
                {
                    IsAutoPairJobActive = true;
                    return IsAutoPairJobActive;
                }
            }
        }
        IsAutoPairJobActive = false;
        return IsAutoPairJobActive;

    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        if (IsJobActive("AutoPairJobQueue"))
        {
            // delete current job and 

            DeleteJob("AutoPairJobQueue");

        }
    }
    protected void btncf_Click(object sender, EventArgs e)
    {
        if (IsJobActive("ConfiguratorQueue"))
        {
            // delete current job and 

            DeleteJob("ConfiguratorQueue");

        }
        //Create new Job

        CreateDailyJob("ConfiguratorQueue", typeof(ConfiguratorJob), Convert.ToInt16(this.DropDownList1.Text) * 60, "ConfiguratorQueueJobTrigger1");
    }

    protected void btncfdelete_Click(object sender, EventArgs e)
    {
        if (IsJobActive("ConfiguratorQueue"))
        {
            // delete current job and 

            DeleteJob("ConfiguratorQueue");

        }
    }
}
