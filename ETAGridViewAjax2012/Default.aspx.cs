using System;
using Common.Logging;
using Quartz;
using Quartz.Impl;


public partial class _Default : System.Web.UI.Page
{
    // IScheduler _scheduler = null;
    protected void Page_Load(object sender, EventArgs e)
    {


    }

    protected void Page_Unload(object sender, EventArgs e)
    {

        var ss = 4;

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        string error = null;

        var kk = CommonTool.AutoPairFormCItemByPackage("XXX0", ref error);


        //   Response.Redirect(this.TextBox2.Text.Trim()); 

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        Response.Redirect(this.TextBox1.Text.Trim());

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (this.TextBox3.Text != null)
        {
            //string error = null;

            //var kk = CommonTool.AutoPairFormCItemByPackage(this.TextBox3.Text, ref error);

            CommonDB.AutoPairFormCByPackage(this.TextBox3.Text);

            
         //   JavascriptHelper.Alerts(this, this.TextBox3.Text + " Pairing is Done, please go to formc view page to check");

            Response.Redirect("http://colweb01/etatest/EngineeringTools/FormC/FormCData.aspx?Package=" + this.TextBox3.Text);
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
      
        //ISchedulerFactory sf = new StdSchedulerFactory();
        //IScheduler sched = sf.GetScheduler();

        ////// get a "nice round" time a few seconds in the future...
        //DateTimeOffset startTime = DateBuilder.NextGivenSecondDate(null, 15);

        ////// job1 will only fire once at date/time "ts"
        //IJobDetail job = JobBuilder.Create<SimpleJob>()
        //    .WithIdentity("job1", "group1")
        //    .Build();


        //ISchedulerFactory factory = new StdSchedulerFactory();
        //// get a scheduler 

        //_scheduler = factory.GetScheduler();
        // start the scheduler 
        var _scheduler = GlobalScheduler.GetScheduler();
        if (_scheduler.IsStarted == true)
        {
            _scheduler.ResumeAll();
        }
        else
        {
            _scheduler.Start();
        }




        JobDetail job = new JobDetail("MyJob", typeof(AutoPairPackageJob));


      //  var ss=TriggerUtils.MakeDailyTrigger()
        Trigger trigger = TriggerUtils.MakeMinutelyTrigger(1);
        trigger.StartTimeUtc = TriggerUtils.GetEvenMinuteDate(DateTime.UtcNow);
        trigger.Name = "AutoPairPackageJob1";



        _scheduler.ScheduleJob(job, trigger);

        // _scheduler.Start();


    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        var _scheduler = GlobalScheduler.GetScheduler();

       

        if (_scheduler.IsShutdown==false)
        {
            //_scheduler.PauseJob("MyJob","");
            _scheduler.Shutdown();
        }


        
    }
}
